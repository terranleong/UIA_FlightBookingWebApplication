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
    
    public partial class Passenger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Passenger()
        {
            this.Tickets = new HashSet<Ticket>();
            this.TicketCancellationLogs = new HashSet<TicketCancellationLog>();
            this.TicketChangesLogs = new HashSet<TicketChangesLog>();
        }
    
        public string passport { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string ageCategory { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketCancellationLog> TicketCancellationLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketChangesLog> TicketChangesLogs { get; set; }
    }
}