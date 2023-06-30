using System;
using System.Xml.Linq;
using MCSApiInterface.Interfaces;
using MCSApiInterface.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MCSApiData.Repositories
{
    public class ItemRepository : IItemRepository
	{
        private readonly IMongoCollection<ItemModel> collection;
        public ItemRepository(IMongoDatabase database)
		{
            collection = database.GetCollection<ItemModel>("items");
        }

        public async Task<bool> DeleteItem(string id)
        {
            ItemModel updatedModel = await collection.FindOneAndDeleteAsync(new BsonDocument("_id", id));
            return updatedModel != null;
        }

        public async Task<ItemModel> GetItemByName(string name)
        {
            var sort = Builders<ItemModel>.Sort.Ascending("_id");
            ItemModel item = await collection
                .Find(new BsonDocument("_id", name))
                .Sort(sort)
                .FirstOrDefaultAsync();
            return item;
        }

        public async Task<List<ItemModel>> GetItemsByNames(string[] names)
        {
            var sort = Builders<ItemModel>.Sort.Ascending("_id");
            List<ItemModel> items = await collection
                .Find(model => names.Contains(model.Name))
                .Sort(sort)
                .ToListAsync();
            return items;
        }

        public async Task<List<ItemModel>> GetSmeltableItems()
        {
            var sort = Builders<ItemModel>.Sort.Ascending("Name");
            List<ItemModel> items = await collection
                .Find(new BsonDocument("Smeltable", true))
                .Sort(sort)
                .ToListAsync();
            return items;
        }

        public async Task<bool> NewItem(ItemModel item)
        {
            if (await collection.Find(new BsonDocument("_id", item.Name)).FirstOrDefaultAsync() == null) {
                await collection.InsertOneAsync(item);
                return true;
            } else {
                return false;
            }
        }

        public async Task<bool> UpdateItem(ItemModel item)
        {

            ItemModel updatedModel = await collection.FindOneAndReplaceAsync(new BsonDocument("_id", item.Name), item);
            return updatedModel != null;
        }



        public async Task<bool> SaveImage(Stream stream, string itemName, string fileName, string hostUrl)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/mcs-api/images/items");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            FileInfo fileInfo = new FileInfo(fileName);

            string fileNameWithPath = Path.Combine(path, itemName + fileInfo.Extension);

            string imageUrl = Path.Combine(hostUrl, itemName + fileInfo.Extension);

            ItemModel item = await GetItemByName(itemName);
            if (item == null)
            {
                return false;
            }

            item.OverrideImage(new ImageModel(itemName, imageUrl));

            bool result = await UpdateItem(item);
            if (!result)
            {
                return false;
            }

            using var fileStream = new FileStream(fileNameWithPath, FileMode.Create);
            await stream.CopyToAsync(fileStream);

            return true;
        }
    }
}

