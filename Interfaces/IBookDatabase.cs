using System;
using System.Collections.Generic;
using LibApp.Models;
namespace LibApp.Interfaces
{
    public interface IBookDatabase
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void DeleteBook(int id);
        void UpdateBook(Book book);
        void Save();
    }
}
