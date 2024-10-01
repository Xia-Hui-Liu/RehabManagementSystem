import React from 'react';
import '../Navbar.css'; 

interface NavbarProps {
  firstname: string | null;
}

const Navbar: React.FC<NavbarProps> = ({ firstname }) => {
  return (
    <nav className="navbar">
      <div className="navbar-left">
        <a href="/" className="rainbowrehab">
          <img src="/rainbow.png" alt="logo" className="logo" />
        </a>
      </div>
      <div className="search-box">
        <input
          type="search"
          name="search-form"
          id="search-form"
          className="search-input"
          placeholder="Search content"
        />
      </div>
      <div className="navbar-right">
        {firstname ? `Welcome, ${firstname}` : 'Welcome, Guest'}
      </div>
    </nav>
  );
};

export default Navbar;
