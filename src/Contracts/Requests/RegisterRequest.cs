﻿namespace BabySounds.Contracts.Requests;

public sealed record RegisterRequest
{
    public required string FirstName { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
