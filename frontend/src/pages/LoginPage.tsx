import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { userLogin} from '../api/AuthService'; 

const LoginPage: React.FC = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate(); 

  const handleLogin = async (e:React.FormEvent) => {
    e.preventDefault();
    setError(null);
    try {
      const result = await userLogin(username, password);
      if (result.role.includes('Admin')) { 
        navigate('/admin');
      } else {
        navigate('/user');
      }
    } catch (err: any) {
      setError(err.message); 
      console.error('Login failed:', err); // Optionally log error for debugging
    }
  };

  return (
    <div className="login-container">
      <h2>Rainbow Rehab Management System</h2>
      <form onSubmit={handleLogin} className="login-form">
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          className="login-input"
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="login-input"
        />
        <button type="submit" className="login-button">Login</button>
      </form>
      {error && <p className="error-message">{error}</p>}
    </div>
  )
}

export default LoginPage;
