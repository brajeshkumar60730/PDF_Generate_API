using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkSP_Demo.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public DateTime? RecordCreatedOn {  get; set; }
    }
}
