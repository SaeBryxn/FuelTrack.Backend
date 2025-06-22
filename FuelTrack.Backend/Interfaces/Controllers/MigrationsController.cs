using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuelTrack.Backend.Infrastructure.Persistence;

namespace FuelTrack.Backend.Interfaces.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MigrationsController : ControllerBase
    {
        private readonly FuelTrackDbContext _dbContext;

        public MigrationsController(FuelTrackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyMigrations()
        {
            try
            {
                await _dbContext.Database.MigrateAsync();
                return Ok("✅ Migraciones aplicadas correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al aplicar migraciones: {ex.Message}");
            }
        }
    }
}