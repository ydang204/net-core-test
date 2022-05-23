﻿namespace NetCoreTest.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string PasswordHash { get; set; }

        public string VerifyEmailToken { get; set; }

        public bool IsVerified { get; internal set; }
    }
}