using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceApp.Entities
{
    class Admin :User
    {
        public Admin(int userId, string name, string email, string password) : base(userId, name, email, password)
        {
        }
    }
}
