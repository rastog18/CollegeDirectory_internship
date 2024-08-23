using System;
using CRUDmvc.Model;
using Microsoft.AspNetCore.Mvc;

namespace CRUDmvc.DataAccessLayer
{
	public interface ICrudOperationDL
	{
        public Task<ViewMarksResponse> ViewMarks(MarksRequest request);
        public Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request);
        public Task<ReadRecordResponse> ReadRecord();
        public Task<UpdateRecordResponse> UpdateRecord(UpdateRecordRequest request);
        public Task<DeleteRecordResponse> DeleteRecord(DeleteRecordRequest request);
        public Task<ReadJsonResponse> ReadWithJson(Dictionary<string, string> searchPair, ReadRecordJsonRequest request);
        public  Task<UpdateRecordResponse> UpdateGPA(UpdateGPARequest request);

    }
}

