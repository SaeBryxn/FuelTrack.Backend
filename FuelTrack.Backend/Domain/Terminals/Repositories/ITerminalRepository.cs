using FuelTrack.Backend.Domain.Terminals.Entities;

namespace FuelTrack.Backend.Domain.Terminals.Repositories;

/// <summary>
/// Contract for accessing and managing fuel terminals.
/// </summary>
public interface ITerminalRepository
{
    Task<List<Terminal>> GetAllAsync();
    Task<Terminal?> GetByNameAsync(string name);
    Task AddAsync(Terminal terminal);
    Task SaveChangesAsync();
}