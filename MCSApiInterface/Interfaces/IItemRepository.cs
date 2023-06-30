using System;
using MCSApiInterface.Models;

namespace MCSApiInterface.Interfaces
{
	public interface IItemRepository
	{
		Task<ItemModel> GetItemByName(string name);
		Task<List<ItemModel>> GetItemsByNames(string[] names);
		Task<List<ItemModel>> GetSmeltableItems();
		Task<bool> NewItem(ItemModel item);
		Task<bool> UpdateItem(ItemModel item);
		Task<bool> DeleteItem(string id);
		Task<bool> SaveImage(Stream stream, string itemName, string fileName, string hostUrl);

    }
}

