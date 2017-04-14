using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain.Abstract;
using BookShop.Domain.Entities;

namespace BookShop.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Book> Books
        {
            get { return context.Books; }
        }

        public void SaveBook(Book book)
        {
            if(book.BookID == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbChange = context.Books.Find(book.BookID);
                if(dbChange != null)
                {
                    dbChange.BookName = book.BookName;
                    dbChange.Author = book.Author;
                    dbChange.Description = book.Description;
                    dbChange.Category = book.Category;
                    dbChange.Price = book.Price;
                    dbChange.ImageData = book.ImageData;
                    dbChange.ImageType = book.ImageType;
                }
            }
            context.SaveChanges();
        }

        public Book DeleteBook(int bookId)
        {
            Book dbDelete = context.Books.Find(bookId);
            if(dbDelete != null)
            {
                context.Books.Remove(dbDelete);
                context.SaveChanges();
            }
            return dbDelete;
        }
    }
}
