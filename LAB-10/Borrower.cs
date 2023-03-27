using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_10
{
    public class Borrower
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public List<Book> BorrowedBooks { get; set; }
        public int MaxBooksAllowed { get; set; }
        public double FinePerDay { get; set; }
        public int DueDate { get; set; }

    }

    public interface ILibraryManagementSystem
    {
        void AddBook(Book book);
        void RemoveBook(Book book);
        void EditBook(Book book);
        void AddBorrower(Borrower borrower);
        void RemoveBorrower(Borrower borrower);
        void EditBorrower(Borrower borrower);
        void BorrowBook(Borrower borrower, Book book);
        void ReturnBook(Borrower borrower, Book book);
        List<Book> GetAllBooks();
        List<Borrower> GetAllBorrowers();
    }

    public class LibraryManagementSystem : ILibraryManagementSystem
    {
        private List<Book> books;
        private List<Borrower> borrowers;
        int max_capacity { get; set; }

        public LibraryManagementSystem()
        {
            books = new List<Book>();
            borrowers = new List<Borrower>();
        }

        public void AddBook(Book book)
        {
            
            if (books.Any(b => b.ISBN == book.ISBN))
            {
                throw new Exception("Book already exists.");
            }

            
            if (books.Count == max_capacity)
            {
                throw new Exception("Library is full.");
            }

            books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            if (book.TotalCopies != book.AvailableCopies)
            {
                throw new Exception("Book is borrowed.");
            }

            books.Remove(book);
        }

        public void EditBook(Book book)
        {
            var existingBook = books.FirstOrDefault(b => b.ISBN == book.ISBN);

            if (existingBook == null)
            {
                throw new Exception("Book does not exist.");
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.TotalCopies = book.TotalCopies;
            existingBook.AvailableCopies = book.AvailableCopies;
        }

        public void AddBorrower(Borrower borrower)
        {
            if (borrowers.Any(b => b.ID == borrower.ID))
            {
                throw new Exception("Borrower already exists.");
            }

            borrowers.Add(borrower);
        }

        public void RemoveBorrower(Borrower borrower)
        {
            borrowers.Remove(borrower);
        }

        public void EditBorrower(Borrower borrower)
        {
            
            
        }
        public void BorrowBook(Borrower borrower, Book book)
        {
            
            if (borrower.BorrowedBooks.Count >= borrower.MaxBooksAllowed)
            {
                throw new Exception("Borrower has reached their maximum number of books allowed.");
            }

            
            if (book.AvailableCopies == 0)
            {
                throw new Exception("Book is not available.");
            }
 
            borrower.BorrowedBooks.Add(book);
            book.AvailableCopies--;
        }
        public void ReturnBook(Borrower borrower, Book book)
        {

        }
            public List<Book> GetAllBooks()
        {
            return books;
        }

        public List<Borrower> GetAllBorrowers()
        {
            return borrowers;
        }
    }



}
