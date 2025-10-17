import React from 'react';
import { useForm } from 'react-hook-form';
import { TextField } from '@mui/material';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import type { BankFormValues } from './types';
import CrudPage from '../../layouts/CrudPage';
import { CrudMode } from '../../utils/enums/generalEnum';
import { useNavigate } from 'react-router-dom';

interface BankFormProps {
  initialValues?: BankFormValues;
  onSubmit: (data: BankFormValues) => void;
  isEditMode?: boolean;
}
const schema = yup.object({
  description: yup.string().required('Description is required').max(100),
});

const BankForm: React.FC<BankFormProps> = ({ initialValues, onSubmit, isEditMode = false }) => {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<BankFormValues>({
    resolver: yupResolver(schema),
    defaultValues: initialValues || { description: '' },
  });

  const handleGoToList = () => {
    navigate('/banks');
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <CrudPage
        mode={isEditMode ? CrudMode.Edit : CrudMode.Create}
        formTitle={isEditMode ? 'Edit Bank' : 'Add Bank'}
        onAddClick={() => {}}
        addTittle=""
        isSubmitting={isSubmitting}
        onCancelClick={handleGoToList}
      >
        <TextField
          label="Description"
          fullWidth
          margin="normal"
          {...register('description')}
          error={!!errors.description}
          helperText={errors.description?.message}
        />
      </CrudPage>
    </form>
  );
};

export default BankForm;
