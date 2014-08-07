using System;
using System.Collections.Generic;

namespace ChainReaction.DynamoDbRepository.SampleDynamoDbApplication.Models
{
    public class SampleEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public decimal Price { get; set; }
        public List<string> Tags { get; set; }
    }
}
