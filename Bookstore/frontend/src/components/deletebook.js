import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { toast, ToastContainer } from 'react-toastify'; // Import toast and ToastContainer from react-toastify
import 'react-toastify/dist/ReactToastify.css'; // Import the CSS for toastify
import bookImage from './images/deleteimage.jpg';

const DeleteBook = () => {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    fetchBooks();
  }, []);

  const fetchBooks = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/books');
      setBooks(response.data);
    } catch (error) {
      console.error('Error fetching books:', error);
      toast.error('Failed to fetch books.'); // Show error message
    }
  };

  const handleDeleteBook = async (bookId) => {
    if (window.confirm('Are you sure you want to delete this book?')) {
      try {
        await axios.delete(`http://localhost:5000/api/books/${bookId}`);
        // Update the books state after deletion
        setBooks(books.filter(book => book.book_id !== bookId));
        toast.success('Book deleted successfully!'); // Show success message
      } catch (error) {
        console.error('Error deleting book:', error);
        toast.error('Failed to delete book.'); // Show error message
      }
    }
  };

  return (
    <div
      className="delete-book-container"
      style={{
        backgroundImage: `url(${process.env.PUBLIC_URL}/images/background.jpg)`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        minHeight: '100vh',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        padding: '20px',
      }}
    >
      <ToastContainer position="top-right" autoClose={5000} hideProgressBar={false} newestOnTop={false} closeOnClick rtl={false} pauseOnFocusLoss draggable pauseOnHover />
      
      <div className="card shadow delete-book-card" style={{ maxWidth: '800px', width: '100%' }}>
        <div className="card-body">
          <div className="text-center mb-4">
            <h3 className="card-title text-center mb-4">Delete Book</h3>
            <img src={bookImage} alt="Book" className="delete-book-image" style={{ maxWidth: '300px', height: 'auto' }} />
          </div>

          <ul className="list-group">
            {books.map(book => (
              <li key={book.book_id} className="list-group-item d-flex justify-content-between align-items-center">
                <div className="d-flex align-items-center">
                  <img src={`http://localhost:5000/${book.image}`} alt="Book Cover" className="book-image me-3" style={{ maxWidth: '100px', height: 'auto' }} />
                  <div>
                    <h6 className="mb-0">{book.title}</h6>
                  </div>
                </div>
                <div>
                  <button onClick={() => handleDeleteBook(book.book_id)} className="btn btn-sm btn-outline-danger">
                    Delete
                  </button>
                </div>
              </li>
            ))}
          </ul>
          <div className="mt-3 text-center">
            <Link to="/dashboard" className="btn btn-outline-primary">Back to Dashboard</Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DeleteBook;
