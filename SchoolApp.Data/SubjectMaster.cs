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
    
    public partial class SubjectMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubjectMaster()
        {
            this.FacultyClassAllocations = new HashSet<FacultyClassAllocation>();
        }
    
        public int SubjectId { get; set; }
        public string Subject { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacultyClassAllocation> FacultyClassAllocations { get; set; }
    }
}
