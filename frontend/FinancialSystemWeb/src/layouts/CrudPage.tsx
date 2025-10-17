import { Box, Button, Typography } from '@mui/material';
import type { CrudMode as CrudModeType } from '../utils/enums/generalEnum';
import { CrudMode } from '../utils/enums/generalEnum';

type CrudPageProps = {
  mode: CrudModeType;
  formTitle: string;
  onAddClick: () => void;
  addTittle: string;
  isSubmitting: boolean;
  onCancelClick: () => void;
  children: React.ReactNode;
};

const CrudPage: React.FC<CrudPageProps> = ({
  mode,
  formTitle,
  onAddClick,
  addTittle,
  isSubmitting,
  onCancelClick,
  children,
}) => {
  const isEditOrCreate = mode === CrudMode.Edit || mode === CrudMode.Create;

  return (
    <Box
      sx={{
        width: '100%',
        maxWidth: {
          xs: '100%', // Small screens: use full width
          sm: 600, // Tablets
          md: 900, // Normal desktops
          lg: 1400, // Large desktops
        },
        bgcolor: 'background.paper',
        borderRadius: 2,
        boxShadow: 3,
        overflowX: 'auto', // Prevents horizontal overflows
        overflowY: 'auto',
        p: { xs: 2, sm: 3, md: 4 }, // Adaptive padding
        mx: 'auto', // Horizontal centering
        maxHeight: '90vh', // Taller on large screens
      }}
    >
      {mode === CrudMode.List && (
        <Box
          display="flex"
          flexDirection={{ xs: 'column', sm: 'row' }}
          justifyContent="space-between"
          alignItems={{ xs: 'stretch', sm: 'center' }}
          mb={3}
          gap={2}
        >
          <Typography variant="h5">{formTitle}</Typography>
          <Button variant="contained" onClick={onAddClick}>
            {addTittle}
          </Button>
        </Box>
      )}

      {isEditOrCreate && (
        <Typography variant="h5" mb={2}>
          {formTitle}
        </Typography>
      )}

      {children}

      {isEditOrCreate && (
        <Box mt={3} display="flex" flexWrap="wrap" gap={2}>
          <Button variant="contained" color="primary" type="submit" disabled={isSubmitting}>
            {isSubmitting
              ? mode === CrudMode.Edit
                ? 'Updating...'
                : 'Creating...'
              : mode === CrudMode.Edit
                ? 'Update'
                : 'Create'}
          </Button>

          <Button variant="outlined" onClick={onCancelClick}>
            Cancel
          </Button>
        </Box>
      )}
    </Box>
  );
};

export default CrudPage;
