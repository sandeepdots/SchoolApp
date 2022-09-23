using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApp.Web.Model
{
    public class DepartmentViewModel
    {

        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "DepartmentName is required.")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "DepartmentDescription is required.")]
        public string DepartmentDescription { get; set; }
        public System.DateTime UpdateOn { get; set; }
        [Required(ErrorMessage = "IsActive is required.")]
        public bool IsActive { get; set; }
        


    }

    }
