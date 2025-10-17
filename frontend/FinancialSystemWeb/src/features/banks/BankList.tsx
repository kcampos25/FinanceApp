import React, { useMemo, useState } from 'react';
import { Box, CircularProgress, Alert, TextField } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useBanks } from './hooks/useBanks';
import type { ColumnTable, CrudFilter } from '../../components/CrudTable/types';
import CrudTable from '../../components/CrudTable/CrudTable';
import ConfirmDialog from '../../components/ConfirmDialog';
import CrudPage from '../../layouts/CrudPage';
import { CrudMode } from '../../utils/enums/generalEnum';

const BankList: React.FC = () => {
  const navigate = useNavigate();
  const { getAll, deleteBank } = useBanks();
  const { data: banks = [], isLoading, isError, error } = getAll;

  const [filterText, setFilterText] = useState('');
  const [bankToDelete, setBankToDelete] = useState<(typeof banks)[0] | null>(null);

  const columns: ColumnTable<(typeof banks)[0]>[] = [
    { fieldName: 'bankId', columnname: 'ID', isSortable: true },
    { fieldName: 'description', columnname: 'Description', isSortable: true },
  ];

  const filters: CrudFilter<(typeof banks)[0]>[] = useMemo(() => {
    return filterText ? [{ field: 'description', value: filterText }] : [];
  }, [filterText]);

  const filterFunction = (bank: (typeof banks)[0], filters: CrudFilter<(typeof banks)[0]>[]) => {
    return filters.every((f) =>
      String(bank[f.field]).toLowerCase().includes(f.value.toLowerCase()),
    );
  };

  const confirmDelete = async () => {
    if (bankToDelete) {
      try {
        await deleteBank.mutateAsync(bankToDelete.bankId);
      } catch {
        // Error already handled by onError of the hook (toast)
      } finally {
        setBankToDelete(null);
      }
    }
  };
  const handleGoToCreate = () => {
    navigate('/banks/create');
  };

  return (
    <CrudPage
      mode={CrudMode.List}
      formTitle="Banks"
      onAddClick={handleGoToCreate}
      addTittle="ADD BANK"
      isSubmitting={false}
      onCancelClick={() => {}}
    >
      <TextField
        label="Search Banks"
        variant="outlined"
        size="small"
        value={filterText}
        onChange={(e) => setFilterText(e.target.value)}
        sx={{ mb: 2, width: '300px' }}
      />

      {isLoading && (
        <Box display="flex" justifyContent="center" my={4}>
          <CircularProgress />
        </Box>
      )}

      {isError && (
        <Alert severity="error" sx={{ mb: 2 }}>
          Error loading banks: {(error as Error).message}
        </Alert>
      )}

      {!isLoading && !isError && banks.length === 0 && (
        <Alert severity="info" sx={{ mb: 2 }}>
          No banks found. Try adding a new bank.
        </Alert>
      )}

      {!isLoading && !isError && banks.length > 0 && (
        <CrudTable
          columns={columns}
          rows={banks}
          columnKeyname="bankId"
          urlEdit="/banks/edit/"
          onDeleteClick={(bank) => setBankToDelete(bank)}
          filters={filters}
          filterFunction={filterFunction}
        />
      )}

      <ConfirmDialog
        open={bankToDelete !== null}
        message="Are you sure you want to delete this bank?"
        onCancel={() => setBankToDelete(null)}
        onConfirm={confirmDelete}
      />
    </CrudPage>
  );
};

export default BankList;
