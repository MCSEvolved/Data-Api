using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MCSApiInterface.Models
{
	public class ServiceModel
	{
        public ServiceModel(string id, string name, string displayName, List<ImageModel>? images)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
            Images = images ?? new List<ImageModel>();
        }

        [BsonId]
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public List<ImageModel> Images { get; private set; }

        public bool IsValid() {
            foreach (ImageModel image in Images)
            {
                if (!image.IsValid()) {
                    return false;
                }
            }

            return Id != null && Name != null && DisplayName != null;
        }
    }
}

