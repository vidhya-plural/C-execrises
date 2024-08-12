import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import authEditImg from './images/authoredit.avif'; // Import authoredit.avif image

const EditAuthorForm = () => {
  const { author_id } = useParams(); // Fetch author_id from URL parameters
  const [authors, setAuthors] = useState([]);
  const [selectedAuthorId, setSelectedAuthorId] = useState(author_id || '');
  const [authorDetails, setAuthorDetails] = useState(null);
  const [name, setName] = useState('');
  const [biography, setBiography] = useState('');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchAuthors();
  }, []);

  useEffect(() => {
    if (selectedAuthorId) {
      fetchAuthorDetails(selectedAuthorId);
    }
  }, [selectedAuthorId]);

  const fetchAuthors = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/authors');
      setAuthors(response.data);
    } catch (error) {
      console.error('Error fetching authors:', error);
    }
  };

  const fetchAuthorDetails = async (id) => {
    setLoading(true);
    try {
      const response = await axios.get(`http://localhost:5000/api/authors/${id}`);
      setAuthorDetails(response.data);
      setName(response.data.name);
      setBiography(response.data.biography);
    } catch (error) {
      console.error('Error fetching author details:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleAuthorChange = (event) => {
    const selectedId = event.target.value;
    setSelectedAuthorId(selectedId);
    if (selectedId) {
      fetchAuthorDetails(selectedId);
    } else {
      setAuthorDetails(null);
      setName('');
      setBiography('');
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      const response = await axios.put(
        `http://localhost:5000/api/authors/${selectedAuthorId}`,
        {
          author_id: selectedAuthorId,
          name,
          biography
        },
        {
          headers: {
            'Content-Type': 'application/json'
          }
        }
      );

      if (response.status === 200) {
        toast.success('Author updated successfully');
        setAuthorDetails(response.data);
      } else {
        toast.error('Failed to update author');
      }
    } catch (error) {
      console.error('Error updating author:', error.response || error.message);
      toast.error(`Error updating author: ${error.response?.data || error.message}`);
    }
  };

  return (
    <div
      className="edit-author-container"
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
      <div
        className="card shadow"
        style={{
          maxWidth: '600px',
          margin: 'auto',
          padding: '20px',
          backgroundColor: '#f8f9fa',
          boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
        }}
      >
        <img
          src={authEditImg}
          alt="Author Edit"
          className="card-img-top mb-4"
          style={{ maxWidth: '50%', height: 'auto', margin: '0 auto' }}
        />
        <h2 className="text-center mb-4">Edit Author</h2>

        <div className="mb-3">
          <label htmlFor="authorSelect" className="form-label">Select Author:</label>
          <select
            id="authorSelect"
            className="form-select"
            value={selectedAuthorId}
            onChange={handleAuthorChange}
          >
            <option value="">Select an author</option>
            {authors.map(author => (
              <option key={author.author_id} value={author.author_id}>
                {author.name}
              </option>
            ))}
          </select>
        </div>

        {loading ? (
          <p>Loading author details...</p>
        ) : authorDetails ? (
          <div>
            <form onSubmit={handleSubmit}>
              <div className="mb-3">
                <label htmlFor="name" className="form-label">Name:</label>
                <input
                  type="text"
                  id="name"
                  className="form-control"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  required
                />
              </div>

              <div className="mb-3">
                <label htmlFor="biography" className="form-label">Biography:</label>
                <textarea
                  id="biography"
                  className="form-control"
                  value={biography}
                  onChange={(e) => setBiography(e.target.value)}
                  required
                />
              </div>

              <div className="mb-3 d-flex justify-content-between">
                <Link to="/dashboard" className="btn btn-secondary">Back to Dashboard</Link>
                <button type="submit" className="btn btn-primary">Update Author</button>
              </div>
            </form>

            <ToastContainer /> {/* Include ToastContainer here */}
          </div>
        ) : (
          <p>No author selected</p>
        )}
      </div>
    </div>
  );
};

export default EditAuthorForm;
