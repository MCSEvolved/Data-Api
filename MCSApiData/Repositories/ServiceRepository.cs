using System;
using MCSApiInterface.Interfaces;
using MCSApiInterface.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MCSApiData.Repositories
{
    public class ServiceRepository : IServiceRepository
	{
        private readonly IMongoCollection<ServiceModel> collection;
        public ServiceRepository(IMongoDatabase database)
		{
            collection = database.GetCollection<ServiceModel>("services");
        }

        public async Task<bool> DeleteService(string id)
        {
            ServiceModel updatedModel = await collection.FindOneAndDeleteAsync(new BsonDocument("_id", id));
            return updatedModel != null;
        }

        public async Task<List<ServiceModel>> GetAllServices()
        {
            var sort = Builders<ServiceModel>.Sort.Ascending("_id");
            List<ServiceModel> services = await collection
                .Find(new BsonDocument())
                .Sort(sort)
                .ToListAsync();
            return services;
        }

        public async Task<ServiceModel> GetServiceById(string id)
        {
            var sort = Builders<ServiceModel>.Sort.Ascending("_id");
            ServiceModel service = await collection
                .Find(new BsonDocument("_id", id))
                .Sort(sort)
                .FirstOrDefaultAsync();
            return service;
        }

        public async Task<ServiceModel> GetServiceByName(string name)
        {
            var sort = Builders<ServiceModel>.Sort.Ascending("Name");
            ServiceModel service = await collection
                .Find(new BsonDocument("Name", name))
                .Sort(sort)
                .FirstOrDefaultAsync();
            return service;
        }

        public async Task<bool> NewService(ServiceModel service)
        {
            if (await collection.Find(new BsonDocument("_id", service.Id)).FirstOrDefaultAsync() == null)
            {
                await collection.InsertOneAsync(service);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateService(ServiceModel service)
        {
            ServiceModel updatedModel = await collection.FindOneAndReplaceAsync(new BsonDocument("_id", service.Id), service);
            return updatedModel != null;
        }

        public async Task<bool> SaveImage(Stream stream, string serviceId, string imageName, string fileName, string hostUrl)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/mcs-api/images/services/{serviceId}");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            FileInfo fileInfo = new FileInfo(fileName);

            string fileNameWithPath = Path.Combine(path, imageName + fileInfo.Extension);

            string imageUrl = Path.Combine(hostUrl, serviceId, imageName + fileInfo.Extension);

            ServiceModel service = await GetServiceById(serviceId);
            if (service == null) {
                return false;
            }

            service.Images.Add(new ImageModel(imageName, imageUrl));

            bool result = await UpdateService(service);
            if (!result) {
                return false;
            }

            using var fileStream = new FileStream(fileNameWithPath, FileMode.Create);
            await stream.CopyToAsync(fileStream);

            return true;
        }

    }
}

