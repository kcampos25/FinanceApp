import { Navigate, Route, Routes, useNavigate, useParams } from "react-router-dom";
import CurrencyList from "../features/currencies/CurrencyList";
import CurrencyForm from "../features/currencies/CurrencyForm";
import { useCurrencies } from "../features/currencies/hooks/useCurrencies";
import { Alert, Box, CircularProgress } from "@mui/material";

const EditCurrency: React.FC = () => {
  const { id } = useParams<{ id: string }>();

  const navigate = useNavigate();
  const { getById, updateCurrency } = useCurrencies();

  const { data: currency, isLoading, isError, error } = getById(Number(id));

  if (isLoading) {
    return (
      <Box display="flex" justifyContent="center" mt={4}>
        <CircularProgress />
      </Box>
    );
  }

  if (isError || !currency) {
    return (
      <Alert severity="error" sx={{ mt: 4 }}>
        Error loading currency: {error?.message || "Not found"}
      </Alert>
    );
  }

  const handleSubmit = async (data: { description: string }) => {
    await updateCurrency.mutateAsync({
      id: Number(id),
      data: {
        description: data.description,
        updatedBy: "admin",
      },
    });
    navigate("/currencies");
  };

  return <CurrencyForm initialValues={currency} onSubmit={handleSubmit} isEditMode />;
};

const CreateCurrency: React.FC = () => {
  const navigate = useNavigate();
  const { createCurrency } = useCurrencies();

  const handleSubmit = async (data: { description: string }) => {
    await createCurrency.mutateAsync({
      description: data.description,
      createdBy: "admin",
    });
    navigate("/currencies");
  };

  return <CurrencyForm onSubmit={handleSubmit} />;
};

const CurrencyPage: React.FC = () => {
  return (
    <Routes>
      <Route index element={<CurrencyList />} />
      <Route path="create" element={<CreateCurrency />} />
      <Route path="edit/:id" element={<EditCurrency />} />
      <Route path="*" element={<Navigate to="/currencies" replace />} />
    </Routes>
  );
};

export default CurrencyPage;
