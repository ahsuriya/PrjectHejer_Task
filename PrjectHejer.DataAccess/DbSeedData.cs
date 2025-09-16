using Microsoft.EntityFrameworkCore;
using PrjectHejer.DataAccess.Models;

namespace PrjectHejer.DataAccess
{
    public static class DbSeedData
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate(); // Apply any pending migrations

            // --- Seed EntityTypes ---
            if (!context.EntityTypes.Any())
            {
                context.EntityTypes.AddRange(
                    new EntityType
                    {
                        EntityTypeId = Guid.Parse("7e5a1a10-3c7a-4b2c-8a7f-1c6d9b3a0f55"),
                        Name = "Customer"
                    },
                    new EntityType
                    {
                        EntityTypeId = Guid.Parse("c23f43de-8b77-42db-a9a5-4f7a2d6f1c11"),
                        Name = "Lead"
                    }
                );
                context.SaveChanges();
            }

            // --- Seed Customers ---
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer
                    {
                        CustomerId = Guid.Parse("3f12d4b2-76ab-46f3-b5a4-8e2c9b1d0e12"),
                        Name = "John Doe",
                        Email = "john@example.com",
                        Phone = "1234567890",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Customer
                    {
                        CustomerId = Guid.Parse("bbf2a8b1-2d1c-4e99-909a-0f3d3a94f8c7"),
                        Name = "Alice Smith",
                        Email = "alice@example.com",
                        Phone = "9876543210",
                        CreatedAt = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }

            // --- Seed Leads ---
            if (!context.Leads.Any())
            {
                context.Leads.AddRange(
                    new Lead
                    {
                        LeadId = Guid.Parse("54d3a8b0-5a29-41d7-9f92-1c73a98b42f4"),
                        LeadName = "Bob Potential",
                        Email = "bob@leads.com",
                        Phone = "111222333",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Lead
                    {
                        LeadId = Guid.Parse("9c81f52a-2b47-4c5a-872e-4f51c2a934b7"),
                        LeadName = "Carol Prospect",
                        Email = "carol@leads.com",
                        Phone = "444555666",
                        CreatedAt = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
