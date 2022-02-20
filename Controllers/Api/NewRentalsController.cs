﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        public NewRentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST /api/newRentals
        [HttpPost]
        [Authorize(Roles = "Owner, User, StoreManager")]
        public IActionResult CreateNewRental(NewRentalDto newRental)
        {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == newRental.CustomerId);

            var books = _context.Books
                .Include(b => b.Genre)
                .Where(b => newRental.BookIds.Contains(b.Id))
                .ToList();

            foreach (var book in books)
            {
                if (book.NumberAvailable == 0)
                    return BadRequest("Book is unavailabe");

                book.NumberAvailable--;
                var rental = new Rental()
                {
                    Customer = customer,
                    Book = book,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }

        private ApplicationDbContext _context;
    }
}
