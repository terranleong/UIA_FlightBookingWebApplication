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
    
    public partial class SeatLabel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SeatLabel()
        {
            this.Reservations = new HashSet<Reservation>();
        }
    
        public string seatID { get; set; }
        public string sgID { get; set; }
        public int claID { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual SeatGroup SeatGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}