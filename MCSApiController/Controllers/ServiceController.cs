using System;
using Amazon.Auth.AccessControlPolicy;
using MCSApiInterface.Interfaces;
using MCSApiInterface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCSApiController.Controllers
{
    [ApiController]
    [Route("general/service")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository repo;
        public ServiceController(IServiceRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("get/all")]
        [Authorize(Policy = "IsGuest")]
        public async Task<IActionResult> GetAll()
        {
            List<ServiceModel> services = await repo.GetAllServices();
            return services.Count < 1 ? NotFound("No services found") : Ok(services);
        }

        [HttpGet("get/by-id")]
        [Authorize(Policy = "IsGuest")]
        public async Task<IActionResult> GetById(string id)
        {
            ServiceModel service = await repo.GetServiceById(id);
            return service == null ? NotFound($"Service by ID: {id} not found") : Ok(service);
        }

        [HttpGet("get/by-name")]
        [Authorize(Policy = "IsGuest")]
        public async Task<IActionResult> GetByName(string name)
        {
            ServiceModel service = await repo.GetServiceByName(name);
            return service == null ? NotFound($"Service by name: {name} not found") : Ok(service);
        }

        [HttpPost("new")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> New(ServiceModel service)
        {
            if (!service.IsValid()) {
                return BadRequest("Service model is invalid");
            }

            bool result = await repo.NewService(service);
            return result == false ? BadRequest("Service already exists") : Ok();
        }

        [HttpPost("new/image")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> NewImage([FromForm] IFormFile image, [FromQuery] string serviceId, [FromQuery] string imageName) {
            long size = image.Length;
            if (size <= 0) {
                return BadRequest("No file was uploaded");
            }

            var request = HttpContext.Request;
            string hostUrl = $"{request.Scheme}://{request.Host}/general/images/services";

            bool result = await repo.SaveImage(image.OpenReadStream(), serviceId, imageName, image.FileName, hostUrl);
            return !result ? BadRequest("Service doesn't exist") : Ok();


        }

        [HttpDelete("delete")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> Delete(string id)
        {
            bool result = await repo.DeleteService(id);
            return result == false ? BadRequest("Service doesn't exist") : Ok();
        }

        [HttpPut("update")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> Update(ServiceModel service)
        {
            if (!service.IsValid())
            {
                return BadRequest("Service model is invalid");
            }

            bool result = await repo.UpdateService(service);
            return result == false ? BadRequest("Service doesn't exists") : Ok();
        }
    }
}