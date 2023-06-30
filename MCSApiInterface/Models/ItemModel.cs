using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MCSApiInterface.Models
{
	public class ItemModel
	{
        public ItemModel(string name, string displayName, int stackSize, bool? smeltable, ImageModel? image)
        {
            Name = name;
            DisplayName = displayName;
            StackSize = stackSize;
            Smeltable = smeltable ?? false;
            Image = image;
        }

        
   
        [BsonId]
        public string Name { get; private set; }
		public string DisplayName { get; private set; }
        public int StackSize { get; private set; } = -1;
        public bool? Smeltable { get; private set; } = false;
        public ImageModel? Image { get; private set; }

        public bool IsValid() {
            if (Image == null) {
                return Name != null && DisplayName != null && StackSize > -1;
            }
            else {
                return Name != null && DisplayName != null && StackSize > -1 && Image.IsValid();
            }
        }

        public void OverrideImage(ImageModel image) {
            Image = image;
        }

    }
}

