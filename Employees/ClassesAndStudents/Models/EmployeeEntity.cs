using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Employees.Models
{
    public class EmployeeEntity
    {
        public EmployeeEntity()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string EmployeeName { get; set; }
        [ForeignKey("JobTitleEntity")]
        public int JobTitleEntityId { get; set; }
        public JobTitleEntity JobTitleEntity { get; set; }
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
    }
    public class JobTitleEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string JobTitle { get; set; }
    }
}