import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import AdminPage from './pages/AdminPage';
// Import other pages/components for each route
// import AdmitPatientPage from './pages/AdmitPatientPage';
// import StockManagementPage from './pages/StockManagementPage';
import LoginPage from './pages/LoginPage';
// Add imports for other pages

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/admin" element={<AdminPage />} />
        {/* <Route path="/admit-patient" element={<AdmitPatientPage />} />
        <Route path="/stock-management" element={<StockManagementPage />} /> */}
        {/* <Route path="/purchasing" element={<PurchasingPage />} />
        <Route path="/user-management" element={<UserManagementPage />} />
        <Route path="/asset-management" element={<AssetManagementPage />} />
        <Route path="/reports" element={<ReportsPage />} />
        <Route path="/dashboard" element={<DashboardPage />} />
        <Route path="/task-management" element={<TaskManagementPage />} />
        <Route path="/staff-management" element={<StaffManagementPage />} /> */}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  );
};

export default App;
