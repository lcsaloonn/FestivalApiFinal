using System;

namespace Application.UserCase.Hoodie
{
    public class HoodieReadDto
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
    }
}