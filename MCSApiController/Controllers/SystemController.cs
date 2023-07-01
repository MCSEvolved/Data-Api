using System;
using MCSApiInterface.Interfaces;
using MCSApiInterface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCSApiController.Controllers
{
	[ApiController]
	[Route("mcs-api/system")]
	public class SystemController : ControllerBase
	{
		private readonly ISystemRepository repo;
		public SystemController(ISystemRepository repo)
		{
			this.repo = repo;
		}

		[HttpGet("get/all")]
		[Authorize(Policy = "IsGuest")]
		public async Task<IActionResult> GetAll() {
			List <SystemModel> systems = await repo.GetAllSystems();
			return systems.Count < 1 ? NotFound("No systems found") : Ok(systems);
		}

		[HttpGet("get/by-id")]
		[Authorize(Policy = "IsGuest")]
		public async Task<IActionResult> GetById(int id) {
            SystemModel system = await repo.GetSystemById(id);
            return system == null ? NotFound($"System by ID: {id} not found") : Ok(system);
        }

        [HttpGet("get/by-name")]
        [Authorize(Policy = "IsGuest")]
        public async Task<IActionResult> GetByName(string name)
        {
            SystemModel system = await repo.GetSystemByName(name);
            return system == null ? NotFound($"System by name: {name} not found") : Ok(system);
        }

        [HttpPost("new")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> New(SystemModel system)
        {
            if (!system.IsValid()) {
                return BadRequest("System model is invalid");
            }

            bool result = await repo.NewSystem(system);
            return result == false ? BadRequest("System already exists") : Ok();
        }

        [HttpDelete("delete")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await repo.DeleteSystem(id);
            return result == false ? BadRequest("System doesn't exist") : Ok();
        }

        [HttpPut("update")]
        [Authorize(Policy = "IsService")]
        public async Task<IActionResult> Update(SystemModel system)
        {
            if (!system.IsValid())
            {
                return BadRequest("System model is invalid");
            }

            bool result = await repo.UpdateSystem(system);
            return result == false ? BadRequest("System doesn't exists") : Ok();
        }
    }
}