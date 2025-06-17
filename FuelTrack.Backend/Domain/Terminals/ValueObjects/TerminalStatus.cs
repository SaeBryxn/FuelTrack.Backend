namespace FuelTrack.Backend.Domain.Terminals.ValueObjects;

/// <summary>
/// Defines the operational status of a fuel terminal.
/// </summary>
public enum TerminalStatus
{
    Idle = 0,
    Active = 1,
    Blocked = 2
}