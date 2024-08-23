using System;
using System.Text;
using CRUDmvc.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRUDmvc.DataAccessLayer
{
	public class LoginDL : ILoginDL
    {
        //Whatever stirng resides in appsetings.json is now a property in the current file
        //TODO: I understand that we read data from DataAccessLayer, but why did we write this similar code in Program.cs
        //TODO: Need for -configuration : to read data from App.seeting.json

        private readonly IConfiguration _configuration;
        public MongoClient _mongoClient;

        //Creating three collections
        private readonly IMongoCollection<LoginModel> _mongoCollection1;
        private readonly IQueryable<LoginModel> _queryableCollection;

        public LoginDL(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;
                _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
                var db = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
                Console.WriteLine("Connnection Estabilished Successfully! with LogBase");
                _mongoCollection1 = db.GetCollection<LoginModel>(_configuration["Database:CollectionTwo"]);
                _queryableCollection = _mongoCollection1.AsQueryable();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error encountered: {0}", e.Message);
            }
        }
        public async Task<LogReadRecordResponse> ReadRecord()
        {
            LogReadRecordResponse response = new LogReadRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Read Successfully!";
            var filter = Builders<LoginModel>.Filter.Empty;
            try
            {
                response.data = await _mongoCollection1.Find(filter).ToListAsync();
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
                response.data = null;
            }
            return response;
        }
        public async Task<LogInsertRecordResponse> InsertRecord(LoginModel request)
        {
            LogInsertRecordResponse response = new LogInsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Inserted Successfully!";
            try
            {
                await _mongoCollection1.InsertOneAsync(request);
            }
            catch(Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;

        }
        public async Task<LogInsertRecordResponse> UpdateRecord(LoginModel request)
        {
            LogInsertRecordResponse response = new LogInsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Inserted Successfully!";
            try
            {
                var filter = Builders<LoginModel>.Filter.Eq(student => student.Id, request.Id);
                var count = _mongoCollection1.Find(filter).CountDocuments();
                if (count == 0 || request.Username == "" || request.Password == "")
                {
                    response.IsSuccess = false;
                    response.Message = "Please Enter valid Data!";
                    return response;
                }
                var update = Builders<LoginModel>.Update
                    .Set(student => student.Username, request.Username)
                    .Set(student => student.Password, request.Password)
                    .Set(student => student.Role, request.Role)
                    .Set(student => student.Blocked, request.Blocked);
                _mongoCollection1.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<HasAccess> Validate(LoginModel request)
        {
            HasAccess response = new HasAccess();
            
            try
            {
                var query = from r in _queryableCollection
                            where r.Username == request.Username && r.Password == request.Password && r.Role == request.Role
                            select r;
                
                if (!query.Any())
                {
                    response.IsSuccess = false;
                    response.Message = "Doesnot Have Access";
                }
                else
                {                    
                    response.IsSuccess = true;
                    response.Message = "Has Access";
                    foreach (var r in query)
                    {
                        if (r.Blocked == 1)
                        {
                            response.IsSuccess = false;
                            response.Message = "User is Blocked";
                        }
                    }
                }

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = "Exception : " + e.Message + " | " + e.StackTrace + " | " + e.InnerException;
            }
            return response;
        }
        

    }
}

