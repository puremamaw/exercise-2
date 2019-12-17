using System;

class Account
{
    public Guid AccountId { get; set; } = Guid.NewGuid();

    public string Username { get; set; }

    public string Password { get; set; }

    public float Balance { get; set; } = 0.0f;

}