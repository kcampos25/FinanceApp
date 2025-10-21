import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  Chip,
  Divider,
  Pagination,
  Typography,
} from '@mui/material';
import MonetizationOnIcon from '@mui/icons-material/MonetizationOn';
import { useState } from 'react';
import type { DepositCertificateViewDTO } from '../types';
import { useNavigate } from 'react-router-dom';

interface DepositCertificatesCardsProps {
  certificates: DepositCertificateViewDTO[];
  urlEdit: string;
  onDeleteClick: (id: number) => void;
}
// list of deposit certificates
const DepositCertificatesCards: React.FC<DepositCertificatesCardsProps> = ({
  certificates,
  urlEdit,
  onDeleteClick,
}) => {
  const [page, setPage] = useState(1);
  const userLocale = navigator.language || 'en-US';
  const navigate = useNavigate();

  const itemsPerPage = 5;
  const totalPages = Math.ceil(certificates.length / itemsPerPage);

  const handleChange = (_: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };

  const paginatedItems = certificates.slice((page - 1) * itemsPerPage, page * itemsPerPage);

  return (
    <Box p={3}>
      <Box display="flex" flexWrap="wrap" gap={3} justifyContent="flex-start">
        {paginatedItems.map((certificate) => (
          <Card
            key={certificate.certificateId}
            sx={{
              width: 300,
              borderRadius: 3,
              boxShadow: 4,
              transition: '0.3s',
              '&:hover': {
                transform: 'scale(1.02)',
                boxShadow: 8,
              },
              display: 'flex',
              flexDirection: 'column',
              justifyContent: 'space-between',
            }}
          >
            <CardContent>
              <Box display="flex" justifyContent="space-between" alignItems="center">
                <Typography variant="h6" color="primary">
                  {certificate.bank}
                </Typography>
                <Chip
                  label={certificate.isActive ? 'Active' : 'Expired'}
                  color={certificate.isActive ? 'success' : 'default'}
                  size="small"
                />
              </Box>

              <Typography variant="subtitle2" color="text.secondary" mb={2}>
                Currency: {certificate.currency}
              </Typography>

              <Divider sx={{ mb: 2 }} />

              <Box display="flex" alignItems="center" gap={1}>
                <MonetizationOnIcon fontSize="small" />
                <Typography>
                  <strong>Amount:</strong>{' '}
                  {new Intl.NumberFormat(userLocale).format(certificate.amount)}
                </Typography>
              </Box>

              <Typography sx={{ mt: 1 }}>
                <strong>Interest Amount:</strong>{' '}
                {new Intl.NumberFormat(userLocale).format(certificate.interest_amount)}
              </Typography>
              <Typography>
                <strong>Expired Date:</strong>{' '}
                {certificate.expiration_date
                  ? new Date(certificate.expiration_date).toLocaleDateString('en-GB')
                  : 'No date available'}
              </Typography>

              <Typography variant="caption" color="text.secondary">
                ID Certificate: {certificate.certificateId}
              </Typography>
            </CardContent>

            <CardActions sx={{ justifyContent: 'flex-end', px: 2 }}>
              <Button
                size="small"
                variant="outlined"
                onClick={() => navigate(`${urlEdit}${String(certificate.certificateId)}`)}
              >
                Edit
              </Button>
              <Button
                size="small"
                color="error"
                variant="outlined"
                onClick={() => onDeleteClick?.(certificate.certificateId)}
              >
                Delete
              </Button>
            </CardActions>
          </Card>
        ))}
      </Box>

      <Box display="flex" justifyContent="center" mt={4}>
        <Pagination count={totalPages} page={page} onChange={handleChange} color="primary" />
      </Box>
    </Box>
  );
};

export default DepositCertificatesCards;
