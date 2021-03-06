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
    
    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            this.Reservations = new HashSet<Reservation>();
            this.Route_Reservation = new HashSet<Route_Reservation>();
        }
    
        public string flightID { get; set; }
        public string aircID { get; set; }
        public string mgID { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public decimal fareInfant { get; set; }
        public decimal fareChild { get; set; }
        public decimal fareYouth { get; set; }
        public decimal fareAdult { get; set; }
        public decimal fareSenior { get; set; }
        public System.DateTime dates { get; set; }
        public System.TimeSpan times { get; set; }
        public int durationHour { get; set; }
        public int durationMin { get; set; }
        public Nullable<int> timeDelayMin { get; set; }
        public string mealFreq { get; set; }
    
        public virtual Aircraft Aircraft { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Route_Reservation> Route_Reservation { get; set; }
    }
}
