//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UIA_FlightBookingWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Route_Reservation
    {
        public int recordID { get; set; }
        public string resID { get; set; }
        public string flightID { get; set; }
        public string type { get; set; }
        public int customer { get; set; }
    
        public virtual Customer Customer1 { get; set; }
        public virtual Flight Flight { get; set; }
    }
}