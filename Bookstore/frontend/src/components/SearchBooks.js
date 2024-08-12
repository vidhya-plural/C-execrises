import React, { useState, useEffect } from 'react';
import axios from 'axios';

const SearchBooks = () => {
  const [query, setQuery] = useState('');
  const [books, setBooks] = useState([]);
  const [error, setError] = useState('');

  // Function to fetch all books when component mounts
  const fetchBooks = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/books');
      setBooks(response.data);
      setError('');
    } catch (err) {
      setError('Error fetching data');
      console.error(err);
    }
  };

  // useEffect hook to fetch books when component mounts
  useEffect(() => {
    fetchBooks();
  }, []); // Empty dependency array ensures this effect runs only once on mount

  const handleSearch = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/books/search', {
        params: { query }
      });
      setBooks(response.data);
      setError('');
    } catch (err) {
      setError('Error fetching data');
      console.error(err);
    }
  };

  const handleChange = (e) => {
    setQuery(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    handleSearch();
  };
<h2>books search before</h2>
  return (
    <div>
      <h2>books search before1</h2>
      <h2>Search Books</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="searchInput" className="form-label">Search by Title:</label>
          <input
            type="text"
            className="form-control"
            id="searchInput"
            value={query}
            onChange={handleChange}
          />
        </div>
        <button type="submit" className="btn btn-primary">Search</button>
      </form>

      {error && <div className="alert alert-danger mt-3">{error}</div>}

      <h2>books search after</h2>
      <ul className="list-group">
            {currentBooks.map(book => (
              <li key={book.book_id} className="list-group-item d-flex justify-content-between align-items-center">
                <div className="d-flex align-items-center">
                  {/* Use the correct path based on where images are stored */}
                  <img 
                      src={`http://localhost:5000/${book.image}`} 
                      alt="Book Cover" 
                      className="book-image me-3" 
                      style={{ maxWidth: '150px', height: 'auto' }} 
                    />

                  <div>
                    <h6 className="mb-0">{book.title}</h6>
                  </div>
                </div>
                <div>
                  <Link to={`/books/${book.book_id}/edit`} className="btn btn-sm btn-outline-primary me-2">
                    Edit
                  </Link>
                  <button onClick={() => handleDeleteBook(book.book_id)} className="btn btn-sm btn-outline-danger">
                    Delete
                  </button>
                </div>
              </li>
            ))}
          </ul>
<h2>books</h2>
      <div className="mt-4">
        {books.length > 0 ? (
          <div>
            <h3>Search Results:</h3>
            <ul className="list-group">
              {books.map((book) => (
                <li key={book.book_id} className="list-group-item d-flex align-items-center">
                  <img 
  src={`http://localhost:5000/${book.image}`} 
  alt="Book Cover" 
  className="book-image me-3" 
  style={{ maxWidth: '150px', height: 'auto' }} 
/>
<h2>books search inside</h2>
                  <div>
                    <strong>Title:</strong> {book.title}<br />
                    <strong>Author:</strong> {book.author_name}<br />
                    <strong>Genre:</strong> {book.genre_name}<br />
                    <strong>Price:</strong> ${book.price}<br />
                    <strong>Publication Date:</strong> {new Date(book.publication_date).toLocaleDateString()}
                  </div>
                </li>
              ))}
            </ul>
          </div>
        ) : (
          <p>No books found</p>
        )}
      </div>
    </div>
  );
};

export default SearchBooks;
