﻿namespace ODataBookStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? Username { get; set; }
        public byte[]? Password { get; set; }
    }
}
