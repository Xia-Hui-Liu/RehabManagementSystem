import React, {useEffect, useState} from 'react';
import '../AdminPage.css'; 
import Navbar from '../components/Navbar';
import Sidebar from '../components/Sidebar';

const AdminPage: React.FC = () => {
  const [firstName, setFirstName] = useState<string| null>(null);

  useEffect (() => {
    const userData = localStorage.getItem("user");
    console.log(userData);
    if (userData){
      const parsedUser = JSON.parse(userData);
      setFirstName(parsedUser.firstName);
    }
  }, []);

  return (
    <div className="admin-layout">
      <Navbar firstname={firstName} /> {/* Add username/firstName if available */}
      <div className="admin-body">
        <Sidebar />
        <main className="admin-content">
          <h2>Admin Dashboard</h2>
          <p>Welcome to the admin dashboard! Here you can manage patients, stock, tasks, and more.</p>
          {/* Other admin content goes here */}
        </main>
      </div>
    </div>
  );
};

export default AdminPage;
