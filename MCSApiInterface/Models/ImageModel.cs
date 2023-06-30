using System;
namespace MCSApiInterface.Models
{
	public class ImageModel
	{
        public ImageModel(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; private set; }
        public string Path { get; private set; }

        public bool IsValid() {
            return Name != null && Path != null;
        }
    }
}

