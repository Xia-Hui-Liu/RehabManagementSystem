import React, {useEffect, useState} from 'react';
import '../AdminPage.css'; 
import Navbar from '../components/Navbar';
import Sidebar from '../components/Sidebar';

const AdminPage: React.FC = () => {
  const [firstName, setFirstName] = useState<string| null>(null);
  const [isSidebarOpen, setSidebarOpen] = useState(false);

  useEffect (() => {
    const userData = localStorage.getItem("user");
    console.log(userData);
    if (userData){
      const parsedUser = JSON.parse(userData);
      setFirstName(parsedUser.firstname);
    }
  }, []);

  const toggleSidebar = () => {
    setSidebarOpen(!isSidebarOpen);
  }

  return (
    <div className="admin-layout">
      <Navbar firstname={firstName} toggleSidebar={toggleSidebar} /> 
      <Sidebar  className={isSidebarOpen ? 'open' : 'collapsed'}  />
      <main className={`admin-content ${isSidebarOpen ? 'open' : 'collapsed'}`}>
        <h2>Admin Dashboard</h2>
        <p>Welcome to the admin dashboard! Here you can manage patients, stock, tasks, and more.</p>
        {/* Other admin content goes here */}
      </main>
      <footer className="admin-footer">
        &copy; 2024 Rainbow Rehab Center. All rights reserved.
      </footer>
    </div>

  );
};

export default AdminPage;
