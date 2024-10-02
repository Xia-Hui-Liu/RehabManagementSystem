import React from 'react';
import { Link } from "react-router-dom";
// import '../Sidebar.css'; 

interface SidebarProps {
    className?: string;
}
const Sidebar: React.FC<SidebarProps> = ({ className }) => {
    return(
        <>
            <aside className={`admin-sidebar ${className || ''}`}>
                <nav className="admin-nav">
                    <Link to="/dashboard" className="admin-nav-link">Dashboard</Link>
                    <Link to="/admit-patient" className="admin-nav-link">Admit Patient</Link>
                    <Link to="/stock-management" className="admin-nav-link">Stock Management</Link>
                    <Link to="/purchasing" className="admin-nav-link">Purchasing</Link>
                    <Link to="/user-management" className="admin-nav-link">User Management</Link>
                    <Link to="/asset-management" className="admin-nav-link">Asset Management</Link>
                    <Link to="/reports" className="admin-nav-link">Reports</Link>
                    <Link to="/task-management" className="admin-nav-link">Task Management</Link>
                    <Link to="/staff-management" className="admin-nav-link">Staff Management</Link>
                </nav>
            </aside>
        </>
    )
}

export default Sidebar;