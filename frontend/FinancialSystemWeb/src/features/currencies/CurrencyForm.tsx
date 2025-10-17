import { useNavigate } from 'react-router-dom';
import CrudPage from '../../layouts/CrudPage';
import { CrudMode } from '../../utils/enums/generalEnum';
import type { CurrencyFormValues } from './types';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { TextField } from '@mui/material';

interface CurrencyFormProps {
  initialValues?: CurrencyFormValues;
  onSubmit: (data: CurrencyFormValues) => void;
  isEditMode?: boolean;
}

const schema = yup.object({
  description: yup.string().required('Description is required').max(100),
});

const CurrencyForm: React.FC<CurrencyFormProps> = ({
  initialValues,
  onSubmit,
  isEditMode = false,
}) => {
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<CurrencyFormValues>({
    resolver: yupResolver(schema),
    defaultValues: initialValues || { description: '' },
  });
  const handleGoToList = () => {
    navigate('/currencies');
  };
  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <CrudPage
        mode={isEditMode ? CrudMode.Edit : CrudMode.Create}
        formTitle={isEditMode ? 'Edit Currency' : 'Add Currency'}
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
        ></TextField>
      </CrudPage>
    </form>
  );
};

export default CurrencyForm;
