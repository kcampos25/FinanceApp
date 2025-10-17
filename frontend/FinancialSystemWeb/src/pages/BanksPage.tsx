import React from 'react';
import { Routes, Route, Navigate, useParams, useNavigate } from 'react-router-dom';
import BankForm from '../features/banks/BankForm';
import BankList from '../features/banks/BankList';
import { Alert, CircularProgress, Box } from '@mui/material';
import { useBanks } from '../features/banks/hooks/useBanks';

const EditBank: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { updateBank, getBankById } = useBanks();
  const numericId = Number(id);

  if (!id || isNaN(numericId)) {
    return (
      <Alert severity="warning" sx={{ mt: 4 }}>
        Invalid ID. Please return to the bank list.
      </Alert>
    );
  }

  const { data: bank, isLoading, isError, error } = getBankById(numericId);

  const handleSubmit = async (data: { description: string }) => {
    await updateBank.mutateAsync({
      id: numericId,
      data: {
        description: data.description,
        updatedBy: 'admin',
      },
    });
    navigate('/banks');
  };

  if (isLoading) {
    return (
      <Box display="flex" justifyContent="center" mt={4}>
        <CircularProgress />
      </Box>
    );
  }

  if (isError || !bank) {
    return (
      <Alert severity="error" sx={{ mt: 4 }}>
        Error loading bank: {error?.message || 'Not found'}
      </Alert>
    );
  }

  return (
    <BankForm
      initialValues={{ description: bank.description }}
      onSubmit={handleSubmit}
      isEditMode
    />
  );
};

const CreateBank: React.FC = () => {
  const navigate = useNavigate();
  const { createBank } = useBanks();

  const handleSubmit = async (data: { description: string }) => {
    await createBank.mutateAsync({
      description: data.description,
      createdBy: 'admin',
    });
    navigate('/banks');
  };

  return <BankForm onSubmit={handleSubmit} />;
};

const BanksPage: React.FC = () => {
  return (
    <Routes>
      <Route index element={<BankList />} />
      <Route path="create" element={<CreateBank />} />
      <Route path="edit/:id" element={<EditBank />} />
      <Route path="*" element={<Navigate to="/banks" replace />} />
    </Routes>
  );
};

export default BanksPage;
