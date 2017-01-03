using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_plan.Ui.Models.Contact
{
    public interface IContact
    {
        string Name { get; }
        string Email { get; }
        string Message { get; }
        string Verification { get; }
        bool DidSend { get; }
        bool Send(string subject);
    }
}
