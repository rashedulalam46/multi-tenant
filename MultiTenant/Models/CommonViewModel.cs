using System;
using System.Collections.Generic;

namespace MultiTenant.Models
{
    public class CommonViewModel    {
              
        public string AccountCode { get; set; }
        public string UserID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string Success { get; set; }
        public string Msg { get; set; }
        public int ResId { get; set; }
        public string FileName { get; set; }
        public string ReturnValue { get; set; }
        public string ReturnText { get; set; }
        public string Sec { get; set; }
        public string Track { get; set; }
        public string ActionMode { get; set; }

    }


}
