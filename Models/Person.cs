using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodefirst.Models
{
    public class Person
    {
        public int Id { get; set; }
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
        public string? Title { get; set; }
        public int Age { get; set; } = 0;
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<Email> EmailAddresses { get; set; } = new List<Email>();
    }
}
