using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Core
{
    public enum Department : int
    {

        Dotnet = 1,
        Php = 2,
        Java = 3,
        Salseforce = 4,
        C = 5,
        Networking = 6
    }

    public enum UserRoles:int
    {
        [Description("Admin")]
        Admin =1,

        [Description("Manager")]
        Manager = 2,

        [Description("Teacher")]
        Faculty = 3,
        [Description("Student")]
        Student = 4,

        [Description("Parent")]
        Parent = 5,


    }
}
