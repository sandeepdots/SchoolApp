//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolApp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class FacultyMaster
    {
        public int FacultyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public System.DateTime DOJ { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<int> DepartmentId { get; set; }
    
        public virtual DepartmentMaster DepartmentMaster { get; set; }
    }
}
