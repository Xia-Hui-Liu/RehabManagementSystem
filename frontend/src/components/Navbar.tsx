import React from "react";
import '../Navbar.css'; 
import { userLogin} from '../api/AuthService'; 

const Navbar: React.FC = () => {
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
                {userLogin.name}
            </div>
        </nav>
    );
}

export default Navbar;
