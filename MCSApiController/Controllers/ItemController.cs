using System;
using Amazon.Auth.AccessControlPolicy;
using MCSApiInterface.Interfaces;
using MCSApiInterface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCSApiController.Controllers
{
    [ApiController]
    [Route("mcs-api/item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository repo;
        public ItemController(IItemRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("get/smeltable")]
        [Authorize(Policy = "IsGuest")]
        public async Task<IActionResult> GetAll()
        {
            List<ItemModel> items = await repo.GetSmeltableItems();
            return items.Count < 1 ? NotFound("No smeltable items found") : Ok(items);
        }

        [HttpGet("get/by-names")]
        [Authorize(Policy = "IsGuest")]
        public async Task<IActionResult> GetById(string[] names)
        {
            List<ItemModel> items = await repo.GetItemsByNames(names);
            return items.Count < 1 ? NotFound("No items found") : Ok(items);
        }

        [HttpGet("get/by-name")]
        [Authorize(Policy = "IsGuest")]
        public async Task<IActionResult> GetByName(string name)
        {
            ItemModel item = await repo.GetItemByName(name);
            return item == null ? NotFound($"Item by name: {name} not found") : Ok(item);
        }

        [HttpGet("is-smeltable")]
        [Authorize(Policy = "IsGuest")]
        public async Task<IActionResult> IsSmeltable(string name) {
            ItemModel item = await repo.GetItemByName(name);
            return item == null ? NotFound($"Item by name: {name} not found") : Ok(item.Smeltable);
        }

        [HttpPost("new")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> New(ItemModel item) {
            if (!item.IsValid()) {
                return BadRequest("Item model is not valid");
            }

            bool result = await repo.NewItem(item);

            return result == false ? BadRequest("Item already exists") : Ok();
        }

        [HttpPost("new/image")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> NewImage([FromForm] IFormFile image, [FromQuery] string itemName)
        {
            long size = image.Length;
            if (size <= 0)
            {
                return BadRequest("No file was uploaded");
            }

            var request = HttpContext.Request;
            string hostUrl = $"{request.Scheme}://{request.Host}/general/images/items";

            bool result = await repo.SaveImage(image.OpenReadStream(), itemName, image.FileName, hostUrl);
            return !result ? BadRequest("Item doesn't exist") : Ok();


        }

        [HttpDelete("delete")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> Delete(string name) {
            bool result = await repo.DeleteItem(name);

            return result == false ? BadRequest("Item doesn't exist") : Ok();
        }

        [HttpPut("update")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> Update(ItemModel item) {
            if (!item.IsValid())
            {
                return BadRequest("Item model is not valid");
            }

            bool result = await repo.UpdateItem(item);

            return result == false ? BadRequest("Item doesn't exists") : Ok();
        }
    }
}

