using System;
namespace MCSApiInterface.Models
{
	public class Service
	{
        public Service(string id, string name, string displayName, List<Image> images)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
            Images = images;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public List<Image> Images { get; private set; } = new List<Image>();
    }
}

