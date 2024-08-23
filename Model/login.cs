using System;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CRUDmvc.Model
{
    public class LoginModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Blocked { get; set; }
    }

    public class ResponseLogin
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
    public class LogReadRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<LoginModel> data { get; set; }
    }
    public class ValidateTokenModel
    {
        public string Token { get; set; }
    }

    public class HasAccess
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class LogInsertRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class LogUpdateRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

