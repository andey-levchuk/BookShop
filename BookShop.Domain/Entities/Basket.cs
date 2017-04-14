using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain.Entities;

namespace BookShop.Domain.Entities
{
    public class Basket
    {
        private List<ChosenBook> booksCollection = new List<ChosenBook>();

        public void AddBookType(int quantity, Book book)
        {
            ChosenBook type = booksCollection
                .Where(x => x.Book.BookID == book.BookID)
                .FirstOrDefault();

            if (type == null)
            {
                booksCollection.Add(new ChosenBook
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
            {
                type.Quantity += quantity;
            }
        }

        public void RemoveBookType(Book book)
        {
            booksCollection.RemoveAll(x => x.Book.BookID == book.BookID);
        }

        public void Clear()
        {
            booksCollection.Clear();
        }

        public decimal ComputeOrderValue()
        {
            return booksCollection.Sum(x => x.Book.Price * x.Quantity);
        }

        public IEnumerable<ChosenBook> Types
        {
            get { return booksCollection; }
        }
    }

    public class ChosenBook
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
