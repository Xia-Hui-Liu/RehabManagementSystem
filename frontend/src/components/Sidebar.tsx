import React from 'react';
import { Link } from "react-router-dom";

const Sidebar: React.FC = () => {
    return(
        <>
            <aside className="admin-sidebar">
                <nav className="admin-nav">
                    <Link to="/admit-patient" className="admin-nav-link">Admit Patient</Link>
                    <Link to="/stock-management" className="admin-nav-link">Stock Management</Link>
                    <Link to="/purchasing" className="admin-nav-link">Purchasing</Link>
                    <Link to="/user-management" className="admin-nav-link">User Management</Link>
                    <Link to="/asset-management" className="admin-nav-link">Asset Management</Link>
                    <Link to="/reports" className="admin-nav-link">Reports</Link>
                    <Link to="/dashboard" className="admin-nav-link">Dashboard</Link>
                    <Link to="/task-management" className="admin-nav-link">Task Management</Link>
                    <Link to="/staff-management" className="admin-nav-link">Staff Management</Link>
                </nav>
            </aside>
        </>
    )
}

export default Sidebar;