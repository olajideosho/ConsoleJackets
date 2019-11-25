using System;
namespace ConsoleJackets.Models
{
    public class Jacket
    {
        public Jacket()
        {
        }

        public int Id { get; set; }
        public string JacketOwner { get; set; }
        public string JacketID { get; set; }
        public string Location { get; set; }
    }
}
