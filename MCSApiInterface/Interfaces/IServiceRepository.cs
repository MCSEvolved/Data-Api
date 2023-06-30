using System;
using MCSApiInterface.Models;

namespace MCSApiInterface.Interfaces
{
	public interface IServiceRepository
	{
		Task<List<ServiceModel>> GetAllServices();
		Task<ServiceModel> GetServiceById(string id);
		Task<ServiceModel> GetServiceByName(string name);
		Task<bool> NewService(ServiceModel service);
		Task<bool> UpdateService(ServiceModel service);
		Task<bool> DeleteService(string id);
		Task<bool> SaveImage(Stream stream, string serviceId, string imageName, string fileName, string hostUrl);

    }
}

