//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stage_Plan.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountVenue
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string VenueUrl { get; set; }
        public string VenueName { get; set; }
        public int AccountId { get; set; }
    
        public virtual Account Account { get; set; }
    }
}