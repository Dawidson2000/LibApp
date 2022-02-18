using System;
using System.Collections.Generic;
using LibApp.Models;
namespace LibApp.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void DeleteBook(int id);
        void UpdateBook(Book book);
        void Save();
    }
}
