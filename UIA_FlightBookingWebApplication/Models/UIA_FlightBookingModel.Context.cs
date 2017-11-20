﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FlightBookingSystemEntities : DbContext
    {
        public FlightBookingSystemEntities()
            : base("name=FlightBookingSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Aircraft> Aircraft { get; set; }
        public virtual DbSet<Airline> Airlines { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Farebase> Farebases { get; set; }
        public virtual DbSet<FarebaseGroup> FarebaseGroups { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<MealGroup> MealGroups { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<SeatGroup> SeatGroups { get; set; }
        public virtual DbSet<SeatLabel> SeatLabels { get; set; }
        public virtual DbSet<SpecialService> SpecialServices { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketCancellationLog> TicketCancellationLogs { get; set; }
        public virtual DbSet<TicketChangesLog> TicketChangesLogs { get; set; }
        public virtual DbSet<Route_Reservation> Route_Reservation { get; set; }
    }
}