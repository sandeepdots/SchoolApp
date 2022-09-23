using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Core
{
    class SubjectsEnum
    {
  
        public enum SubjectsType
        {
            [Description("Math")]
            Math = 1,

            [Description("Science")]
            Science = 2,

            [Description("Arts")]
            Arts = 3,
            [Description("Other")]
            Other = 4,

        }
    }


    
}
