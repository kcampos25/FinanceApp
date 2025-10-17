import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import HomePage from '../pages/HomePage';
import BanksPage from '../pages/BanksPage';
import MainLayout from '../layouts/MainLayout';
import CurrencyPage from '../pages/CurrencyPage';
import DepositCertificatePage from '../pages/DepositCertificatePage';

const AppRoutes: React.FC = () => (
  <MainLayout>
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/banks/*" element={<BanksPage />} />
      <Route path="/currencies/*" element={<CurrencyPage />} />
      <Route path="/depositCertificates/*" element={<DepositCertificatePage />} />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  </MainLayout>
);

export default AppRoutes;
