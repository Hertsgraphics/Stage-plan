//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stage_plan.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class MailingList
    {
        public int Id { get; set; }
        public bool IsOptin { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string ConfirmToken { get; set; }
        public bool IsConfirmed { get; set; }
        public System.DateTime DateOptInRequest { get; set; }
        public System.DateTime DateOptInConfirm { get; set; }
    }
}
