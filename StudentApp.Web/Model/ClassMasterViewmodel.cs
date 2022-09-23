using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Web.Model
{
    public class ClassMasterViewmodel
    {

        public int ClassId { get; set; }
        [Required(ErrorMessage = "Class Name is required.")]
        public string Class { get; set; }
       
        [Required(ErrorMessage = "IsActive is required.")]
        public bool IsActive { get; set; }

        //public Nullable<bool> IsActive { get; set; }

    }
}