using System;
namespace CRUDmvc.Model
{
    public class Record
    {
        // Example properties
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ReadRecordViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<Record> Records { get; set; }

    }

}

