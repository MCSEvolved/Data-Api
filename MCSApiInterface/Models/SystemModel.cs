using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MCSApiInterface.Models
{
	public class SystemModel
	{
        public SystemModel(int id, string displayName, string? description, List<string>? produces)
        {
            Id = id;
            DisplayName = displayName;
            Description = description;
            Produces = produces ?? new List<string>();
        }

        [BsonId]
        public int Id { get; private set; } = -1;
		public string DisplayName { get; private set; }
		public string? Description { get; private set; }
        public List<string> Produces { get; private set; }

        public bool IsValid() {
            return Id > -1 && DisplayName != null;
        }
	}
}

