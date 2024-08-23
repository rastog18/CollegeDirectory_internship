using System;
using CRUDmvc.Model;
using Microsoft.AspNetCore.Mvc;

namespace CRUDmvc.DataAccessLayer
{
	public interface ILoginDL
	{
        public Task<LogInsertRecordResponse> InsertRecord(LoginModel request);
        public Task<LogInsertRecordResponse> UpdateRecord(LoginModel request);
        public Task<LogReadRecordResponse> ReadRecord();
        public Task<HasAccess> Validate(LoginModel login);
    }
}

