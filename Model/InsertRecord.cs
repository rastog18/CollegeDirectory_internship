using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUDmvc.Model
{
	public class InsertRecordRequest
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string? Id { get; set; }
		public string CreatedDate { get; set; }
		public string UpdatedDate { get; set; }
		[Required]
		public string firstName { get; set; }        
		public string lastName { get; set; }
        [Required]
        public string age { get; set; }
        [Required]
		public string major{ get; set; }
        [Required]
        public int educationLevel { get; set; }
        public string GPA { get; set; }
	}
    public class ReadRecordJsonRequest
    {
        public string firstName { get; set; }
        public string age { get; set; }
        public string equality { get; set; }
        
    }
    public class MarksRequest
    {
        [Required]
        public string? Id { get; set; }
    }
    public class UpdateRecordRequest
	{
		[Required]
        public string? YOURId { get; set; }
        public string UpdatedDate { get; set; }
        [Required]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string age { get; set; }
        public string major { get; set; }
        public int educationLevel { get; set; }
    }
    public class UpdateGPARequest
    {
        [Required]
        public string? YOURId { get; set; }
        [Required]
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Required]
        public string GPA { get; set; }
    }
    public class DeleteRecordRequest
    {
        [Required]
        public string? YOURId { get; set; }
    }
    public class ViewMarksResponse
    {
        public bool IsSuccess { get; set; }
		public string Message { get; set; }
    }
    public class InsertRecordResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
    public class UpdateRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class DeleteRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class FixMeHas
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? Id { get; set; }
    }
    public class ReadRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<InsertRecordRequest> data { get; set; }
        public List<FixMeHas> fixMe { get; set; }
    }
    public class ReadJsonResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<InsertRecordRequest> data { get; set; }   
    }

}

