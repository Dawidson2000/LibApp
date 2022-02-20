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
                if (!context.MembershipTypes.Any())
                {
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
                    Console.WriteLine("MembershipTypes already seeded");
                }
         
                if (!context.Customers.Any())
                {
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
                    Console.WriteLine("Customers already seeded");
                }

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                    new Book
                    {
                        Name = "Book One",
                        AuthorName = "Author One",
                        GenreId = 1,
                        DateAdded = new DateTime(2022, 01, 10),
                        ReleaseDate = new DateTime(2020, 01, 10),
                        NumberInStock = 5,
                        NumberAvailable = 5,
                    },
                    new Book
                    {
                        Name = "Book Two",
                        AuthorName = "Author Two",
                        GenreId = 2,
                        DateAdded = new DateTime(2020, 01, 10),
                        ReleaseDate = new DateTime(2018, 01, 10),
                        NumberInStock = 10,
                        NumberAvailable = 3,
                    });
                    Console.WriteLine("Books already seeded");
                }
                context.SaveChanges();
                return;
            }
        }
    }
}