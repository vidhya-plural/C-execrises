import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import DatePicker from 'react-datepicker';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import 'react-datepicker/dist/react-datepicker.css';
import editBookImage from './images/editbook.jpg';

const EditBookForm = () => {
  const { book_id } = useParams();
  
  const [selectedBook, setSelectedBook] = useState(null);
  const [title, setTitle] = useState('');
  const [authorId, setAuthorId] = useState('');
  const [genreId, setGenreId] = useState('');
  const [price, setPrice] = useState('');
  const [publicationDate, setPublicationDate] = useState(null);
  const [authors, setAuthors] = useState([]);
  const [genres, setGenres] = useState([]);

  useEffect(() => {
    fetchAuthors();
    fetchGenres();
    if (book_id) {
      fetchBook(book_id);
    }
  }, [book_id]);

  const fetchAuthors = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/authors');
      setAuthors(response.data);
    } catch (error) {
      console.error('Error fetching authors:', error);
    }
  };

  const fetchGenres = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/genres');
      setGenres(response.data);
    } catch (error) {
      console.error('Error fetching genres:', error);
    }
  };

  const fetchBook = async (id) => {
    try {
      const response = await axios.get(`http://localhost:5000/api/books/${id}`);
      const book = response.data;
      setSelectedBook(book);
      setTitle(book.title);
      setAuthorId(book.author_id); // Use author_id directly
      setGenreId(book.genre_id); // Use genre_id directly
      setPrice(book.price);
      setPublicationDate(new Date(book.publication_date)); // Convert string to Date
    } catch (error) {
      console.error('Error fetching book:', error);
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const bookData = {
        book_id: selectedBook.book_id, // Use the selected book's ID
        title,
        author_id: parseInt(authorId, 10), // Convert to integer
        genre_id: parseInt(genreId, 10), // Convert to integer
        price: parseFloat(price),
        publication_date: publicationDate ? publicationDate.toISOString().split('T')[0] : '', // Format date
      };

      const response = await axios.put(`http://localhost:5000/api/books/${selectedBook.book_id}`, bookData);

      if (response.status === 200) {
        toast.success('Book updated successfully!');
      } else {
        toast.error('Failed to update book.');
      }
    } catch (error) {
      console.error('Error updating book:', error);
      toast.error('Failed to update book.');
    }
  };

  const handleBack = () => {
    window.history.back();
  };

  return (
    <div
      className="container"
      style={{
        position: 'relative',
        minHeight: '100vh',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        padding: '20px',
        background: `url(${editBookImage}) center/cover no-repeat`,
      }}
    >
      <ToastContainer />
      <div
        className="card shadow"
        style={{
          backgroundColor: 'rgba(255, 255, 255, 0.9)',
          backdropFilter: 'blur(5px)',
          borderRadius: '10px',
          padding: '20px',
        }}
      >
        <div className="card-body">
          <h5 className="card-title text-center mb-4">Edit Book</h5>
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="title" className="form-label">Title:</label>
              <input
                type="text"
                id="title"
                className="form-control"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
              />
            </div>

            <div className="mb-3">
              <label htmlFor="authorId" className="form-label">Author:</label>
              <select
                id="authorId"
                className="form-select"
                value={authorId}
                onChange={(e) => setAuthorId(e.target.value)}
                required
              >
                <option value="">Select Author</option>
                {authors.map((author) => (
                  <option key={author.author_id} value={author.author_id}>
                    {author.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="mb-3">
              <label htmlFor="genreId" className="form-label">Genre:</label>
              <select
                id="genreId"
                className="form-select"
                value={genreId}
                onChange={(e) => setGenreId(e.target.value)}
                required
              >
                <option value="">Select Genre</option>
                {genres.map((genre) => (
                  <option key={genre.genre_id} value={genre.genre_id}>
                    {genre.genre_name}
                  </option>
                ))}
              </select>
            </div>

            <div className="mb-3">
              <label htmlFor="price" className="form-label">Price:</label>
              <input
                type="number"
                step="0.01"
                id="price"
                className="form-control"
                value={price}
                onChange={(e) => setPrice(e.target.value)}
                required
              />
            </div>

            <div className="mb-3">
              <label htmlFor="publicationDate" className="form-label">Publication Date:</label>
              <DatePicker
                id="publicationDate"
                selected={publicationDate}
                onChange={(date) => setPublicationDate(date)}
                className="form-control"
                dateFormat="yyyy-MM-dd"
                required
              />
            </div>

            <div className="text-center">
              <button type="submit" className="btn btn-primary me-2">Update Book</button>
              <button type="button" className="btn btn-outline-secondary" onClick={handleBack}>Back</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default EditBookForm;
