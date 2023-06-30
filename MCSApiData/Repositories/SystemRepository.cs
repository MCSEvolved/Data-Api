using System;
using MCSApiInterface.Interfaces;
using MCSApiInterface.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MCSApiData.Repositories
{
    public class SystemRepository : ISystemRepository
	{
        private readonly IMongoCollection<SystemModel> collection;

        public SystemRepository(IMongoDatabase database)
		{
            collection = database.GetCollection<SystemModel>("systems");
		}

        public async Task<bool> DeleteSystem(int id)
        {
            SystemModel updatedModel = await collection.FindOneAndDeleteAsync(new BsonDocument("_id", id));
            return updatedModel != null;
        }

        public async Task<List<SystemModel>> GetAllSystems()
        {
            var sort = Builders<SystemModel>.Sort.Ascending("_id");
            List<SystemModel> systems = await collection
                .Find(new BsonDocument())
                .Sort(sort)
                .ToListAsync();
            return systems;
        }

        public async Task<SystemModel> GetSystemById(int id)
        {
            var sort = Builders<SystemModel>.Sort.Ascending("_id");
            SystemModel system = await collection
                .Find(new BsonDocument("_id", id))
                .Sort(sort)
                .FirstOrDefaultAsync();
            return system;
        }

        public async Task<SystemModel> GetSystemByName(string name)
        {
            var sort = Builders<SystemModel>.Sort.Ascending("_id");
            SystemModel system = await collection
                .Find(new BsonDocument("DisplayName", name))
                .Sort(sort)
                .FirstOrDefaultAsync();
            return system;
        }

        public async Task<bool> NewSystem(SystemModel system)
        {
            if (await collection.Find(new BsonDocument("_id", system.Id)).FirstOrDefaultAsync() == null)
            {
                await collection.InsertOneAsync(system);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateSystem(SystemModel system)
        {
            SystemModel updatedModel = await collection.FindOneAndReplaceAsync(new BsonDocument("_id", system.Id), system);
            return updatedModel != null;
        }
    }
}

