using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private ApplicationDbContext _context;
        private IMapper _mapper;
        
        public BooksController(IBookRepository bookRepository, IGenreRepository genreRepository, IMapper mapper, ApplicationDbContext context)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _context = context;
            _mapper = mapper;
        }

        //GET /api/books
        [HttpGet]
        [Authorize(Roles = "Owner, User, StoreManager")]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetBooks()
                                .ToList()
                                .Select(_mapper.Map<Book, BookDto>);
  
            return Ok(books);
        }

        // GET /api/books/{id}
        [HttpGet("{id}", Name = "GetBook")]
        [Authorize(Roles = "Owner, User, StoreManager")]
        public async Task<IActionResult> GetBook(int id)
        {
            Console.WriteLine("START");
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == id);
            await Task.Delay(2000);

            if (book == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Console.WriteLine("END");

            return Ok(_mapper.Map<BookDto>(book));
        }

        // POST /api/books
        [HttpPost]
        [Authorize(Roles = "Owner, StoreManager")]
        public IActionResult CreateBook(BookDto bookrDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(bookrDto);
            _bookRepository.AddBook(book);
            _bookRepository.Save();
            bookrDto.Id = book.Id;

            return CreatedAtRoute(nameof(GetBook), new { id = bookrDto.Id }, bookrDto);
        }

        // PUT /api/books/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner, StoreManager")]
        public void UpdateBook(int id, BookDto bookDto)
        {
            var bookInDb = _bookRepository.GetBooks().SingleOrDefault(b => b.Id == id);
            if (bookInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _mapper.Map(bookDto, bookInDb);
            _bookRepository.Save();
        }

        // DELETE /api/books/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner, StoreManager")]
        public void DeleteBook(int id)
        {
            var bookInDb = _bookRepository.GetBooks().SingleOrDefault(b => b.Id == id);
            if (bookInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _bookRepository.DeleteBook(bookInDb.Id);
            _bookRepository.Save();
        }
    }
}
