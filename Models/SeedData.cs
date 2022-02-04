using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.MembershipTypes.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }

                context.MembershipTypes.AddRange(
                    new MembershipType
                    {
                        Id = 1,
                        SignUpFee = 0,
                        DurationInMonths = 0,
                        DiscountRate = 0,
                        Name = "New",
                    },
                    new MembershipType
                    {
                        Id = 2,
                        SignUpFee = 30,
                        DurationInMonths = 1,
                        DiscountRate = 10,
                        Name = "Average",
                    },
                    new MembershipType
                    {
                        Id = 3,
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15,
                        Name = "Special",
                    },
                    new MembershipType
                    {
                        Id = 4,
                        SignUpFee = 300,
                        DurationInMonths = 12,
                        DiscountRate = 20,
                        Name = "Premium",
                    });
                context.SaveChanges();

                if (context.Customers.Any())
                {
                    Console.WriteLine("Database already seeded2");
                    return;
                }

                context.Customers.AddRange(
                    new Customer
                    {   
                        Name = "Marek",
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 1,
                        Birthdate = new DateTime(2000, 02, 02)
                    },
                    new Customer
                    {
                        Name = "Dawid",
                        HasNewsletterSubscribed = true,
                        MembershipTypeId = 2,
                        Birthdate = new DateTime(2002, 04, 12)
                    });
                
                context.SaveChanges();
            }
        }
    }
}