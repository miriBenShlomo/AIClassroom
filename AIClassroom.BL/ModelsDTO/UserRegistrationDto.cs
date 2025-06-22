using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIClassroom.BL.ModelsDTO
{
    // DTO זה משמש רק לקבלת נתונים ליצירת משתמש חדש מה-API
    public class UserRegistrationDto
    {
        public string Name { get; set; } = null!;
        public string?Phone{ get; set; }
    }
}
