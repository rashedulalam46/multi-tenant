using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant.Models
{
    public class EmailSettings
    {
        public string ResMail { get; set; }
        public string DispMail { get; set; }
        public string ActMail { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverTitle { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpPortNumber { get; set; }
        public string Domain { get; set; }
        public string DomainIDM { get; set; }
        public string FileServer { get; set; }
        public string NoreplyEmail { get; set; }
        public string Password { get; set; }

    }

    public class EmailModel
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
    }
}
