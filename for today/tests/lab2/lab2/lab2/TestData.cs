using System;

namespace lab2
{
    class TestData
    {
        public static Random r = new Random();
        public static string RandomUserId { get; set; } = r.Next(1000, 9999).ToString();
        public static string ExistedUserId { get; set; } = "102934";
        public static string Password { get; set; } = "123456";
        public static string WrongPassword { get; set; } = "654321";
        public static string FirstName { get; set; } = "Test";
        public static string LastName { get; set; } = "User";
        public static string Email { get; set; } = "test@gmail.com";
        public static string Phone { get; set; } = "0668756185";
        public static string Address { get; set; } = "Test";
        public static string City { get; set; } = "Dnipro";
        public static string State { get; set; } = "Dnipropetrovska";
        public static string Zip { get; set; } = "4900";
        public static string Country { get; set; } = "Ukraine";
    }
}
