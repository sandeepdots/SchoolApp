using SchoolApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SchoolApp.Web.Models
{
    public class SubjectViewModel
    {
        
        public int SubjectId { get; set; }
        public string Subject { get; set; }
        public bool IsActive { get; set; }
     
    }

}



