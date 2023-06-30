using MCSApiInterface.Models;

namespace MCSApiInterface.Interfaces
{
	public interface ISystemRepository
	{
		Task<List<SystemModel>> GetAllSystems();
		Task<SystemModel> GetSystemById(int id);
		Task<SystemModel> GetSystemByName(string name);
		Task<bool> NewSystem(SystemModel system);
		Task<bool> UpdateSystem(SystemModel system);
		Task<bool> DeleteSystem(int id);
	}
}

