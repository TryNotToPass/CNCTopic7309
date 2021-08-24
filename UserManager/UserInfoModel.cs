using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager
{
    public class UserInfoModel
    {
        public Guid ID { get; set; }
        public int UserLevel { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
    }
}
