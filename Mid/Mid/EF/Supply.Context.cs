﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mid.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SupplyEntities1 : DbContext
    {
        public SupplyEntities1()
            : base("name=SupplyEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<FoodItem> FoodItems { get; set; }
        public virtual DbSet<FoodTracke> FoodTrackes { get; set; }
        public virtual DbSet<NGO> NGOs { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
    }
}
