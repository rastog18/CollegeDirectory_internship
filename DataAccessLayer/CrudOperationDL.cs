using System;
using System.Text;
using CRUDmvc.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRUDmvc.DataAccessLayer
{
	public class CrudOperationDL : ICrudOperationDL
    {
        //Whatever stirng resides in appsetings.json is now a property in the current file
        //TODO: I understand that we read data from DataAccessLayer, but why did we write this similar code in Program.cs
        //TODO: Need for -configuration : to read data from App.seeting.json

        private readonly IConfiguration _configuration;
        public MongoClient _mongoClient;

        //Creating three collections
        private readonly IMongoCollection<InsertRecordRequest> _mongoCollection1;
        private readonly IQueryable<InsertRecordRequest> _queryableCollection;

        public CrudOperationDL(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;
                _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
                var db = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
                Console.WriteLine("Connnection Estabilished Successfully!");
                _mongoCollection1 = db.GetCollection<InsertRecordRequest>(_configuration["Database:CollectionOne"]);
                _queryableCollection = _mongoCollection1.AsQueryable();                
            }
            catch(Exception e)
            {
                Console.WriteLine("Error encountered: {0}", e.Message);
            }
        }

        public async Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Inserted Successfully!";
            try
            {
                request.CreatedDate = DateTime.Now.ToString();
                request.UpdatedDate = string.Empty;

                await _mongoCollection1.InsertOneAsync(request);
            }
            catch(Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;

        }
        public async Task<ReadRecordResponse> ReadRecord()
        {
            ReadRecordResponse response = new ReadRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Read Successfully!";
            
            try
            {
                response.data = new List<InsertRecordRequest>();
                var filter = Builders<InsertRecordRequest>.Filter.Empty;              
                response.data  = await _mongoCollection1.Find(filter).ToListAsync();
                if (response.data.Count == 0)
                {
                    response.Message = "There are no documents in the database";
                }
                var query = from r in _queryableCollection
                            where r.educationLevel < 1 || r.educationLevel > 5
                            select new FixMeHas
                            {
                                firstName = r.firstName,
                                lastName = r.lastName,
                                Id = r.Id
                            };
                
                response.fixMe = query.ToList();


            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<ViewMarksResponse> ViewMarks(MarksRequest request)
        {
            ViewMarksResponse response = new ViewMarksResponse();
            response.IsSuccess = false;
            response.Message = "User's Marks not uploaded!";
            try
            {
                var query = from r in _queryableCollection
                            where r.Id == request.Id
                            select new ViewMarksResponse
                            {
                                IsSuccess = true,
                                Message = r.GPA
                            };
                if (query != null)
                {
                    response = query.FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<UpdateRecordResponse> UpdateGPA(UpdateGPARequest request)
        {
            UpdateRecordResponse response = new UpdateRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Updated Successfully!";
            try
            {
                var filter = Builders<InsertRecordRequest>.Filter.Eq(student => student.Id, request.YOURId);
                var count = _mongoCollection1.Find(filter).CountDocuments();
                if (count == 0 || request.firstName == "" || request.GPA == "")
                {
                    response.IsSuccess = false;
                    response.Message = "Please Enter valid Data!";
                    return response;
                }
                var update = Builders<InsertRecordRequest>.Update
                    .Set(student => student.firstName, request.firstName)
                    .Set(student => student.lastName, request.lastName)
                    .Set(student => student.GPA, request.GPA);
                _mongoCollection1.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<UpdateRecordResponse> UpdateRecord(UpdateRecordRequest request)
        {
            UpdateRecordResponse response = new UpdateRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Updated Successfully!";
            try
            {
                var filter = Builders<InsertRecordRequest>.Filter.Eq(student => student.Id, request.YOURId);
                var count =  _mongoCollection1.Find(filter).CountDocuments();
                if (count == 0 || request.firstName == "" || request.lastName == "" || request.major == "")
                {
                    response.IsSuccess = false;
                    response.Message = "Please Enter valid Data!";
                    return response;
                }
                request.UpdatedDate = DateTime.Now.ToString();
                var update = Builders<InsertRecordRequest>.Update
                    .Set(student => student.firstName, request.firstName)
                    .Set(student => student.lastName, request.lastName)
                    .Set(student => student.UpdatedDate, request.UpdatedDate)
                    .Set(student => student.age, request.age)
                    .Set(student => student.educationLevel, request.educationLevel)
                    .Set(student => student.major, request.major);
                _mongoCollection1.UpdateOne(filter, update);

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<ReadJsonResponse> ReadWithJson(Dictionary<string, string> searchPair, ReadRecordJsonRequest request)
        {
            ReadJsonResponse response = new ReadJsonResponse();
            try
            {
                response.IsSuccess = true;
                response.Message = "Data Read Successfully!";
                response.data = new List<InsertRecordRequest>();
                var filterBuilder = Builders<InsertRecordRequest>.Filter;
                var filters = new List<FilterDefinition<InsertRecordRequest>>();
                foreach (var item in searchPair)
                {
                    var filter = filterBuilder.Eq(item.Key, item.Value);
                    if (filter != null)
                    {
                        filters.Add(filter);
                    }

                }
                if (request.age != "-")
                {
                    var filter2 = filterBuilder.Lt("age", request.age);
                    if (request.equality == "<")
                    {
                        filter2 = filterBuilder.Gt("age", request.age);
                    }
                    else if(request.equality == "=")
                    {
                        filter2 = filterBuilder.Eq("age", request.age);
                    }

                    filters.Add(filter2);
                }

                var combinedFilter = filterBuilder.And(filters);
                response.data = await _mongoCollection1.Find(combinedFilter).ToListAsync();

            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "Data Could not be Read!";
            }
           
            return response;
        }
        public async Task<DeleteRecordResponse> DeleteRecord(DeleteRecordRequest request)
        {
            DeleteRecordResponse response = new DeleteRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Deleted Successfully!";

            var filter = Builders<InsertRecordRequest>.Filter.Eq(student => student.Id, request.YOURId);
            var count = _mongoCollection1.Find(filter).CountDocuments();
            if (count == 0)
            {
                response.IsSuccess = false;
                response.Message = "Please Enter a valid Id!";
                return response;
            }
            _mongoCollection1.DeleteOne(filter);
            return response;
        }

    }
}

