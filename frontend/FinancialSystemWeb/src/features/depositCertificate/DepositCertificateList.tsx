import { useNavigate } from 'react-router-dom';
import CrudPage from '../../layouts/CrudPage';
import { CrudMode } from '../../utils/enums/generalEnum';
import DepositCertificateCard from './components/DepositCertificateCard';
import { useDepositCertificate } from './hooks/useDepositCertificate';
import ConfirmDialog from '../../components/ConfirmDialog';
import { useState } from 'react';
import { Alert, Box, CircularProgress } from '@mui/material';

const DepositCertificateList: React.FC = () => {
  const [depositCertificateToDelete, setDepositCertificateToDelete] = useState<number | null>(null);

  const navigate = useNavigate();
  const { getDetail, deleteDepositCertificate } = useDepositCertificate();
  const { data: DepositCertificates = [], isLoading, isError, error } = getDetail;

  const handleGoToCreate = () => {
    navigate('/depositCertificates/create');
  };

  const confirmDelete = async () => {
    if (depositCertificateToDelete) {
      try {
        await deleteDepositCertificate.mutateAsync(depositCertificateToDelete);
      } catch {
        // Error already handled by the onError hook (toast)
      } finally {
        setDepositCertificateToDelete(null);
      }
    }
  };

  return (
    <CrudPage
      mode={CrudMode.List}
      formTitle="Deposit Certificates"
      onAddClick={handleGoToCreate}
      addTittle="Add Deposit Certificate"
      isSubmitting={false}
      onCancelClick={() => ({})}
    >
      {isLoading && (
        <Box display="flex" justifyContent="center" my={4}>
          <CircularProgress />
        </Box>
      )}

      {isError && (
        <Alert severity="error" sx={{ mb: 2 }}>
          Error loading Deposit Certificates: {(error as Error).message}
        </Alert>
      )}

      {!isLoading && !isError && DepositCertificates.length === 0 && (
        <Alert severity="info" sx={{ mb: 2 }}>
          No Deposit Certificates found. Try adding a new Deposit Certificate.
        </Alert>
      )}

      <DepositCertificateCard
        certificates={DepositCertificates}
        urlEdit="/depositCertificates/edit/"
        onDeleteClick={(id) => setDepositCertificateToDelete(id)}
      />

      <ConfirmDialog
        open={depositCertificateToDelete !== null}
        message="Are you sure you want to delete this Deposit Certificate?"
        onCancel={() => setDepositCertificateToDelete(null)}
        onConfirm={confirmDelete}
      />
    </CrudPage>
  );
};

export default DepositCertificateList;
