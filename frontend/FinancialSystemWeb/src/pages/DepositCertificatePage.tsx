import { Navigate, Route, Routes, useNavigate, useParams } from "react-router-dom";
import DepositCertificateList from "../features/depositCertificate/DepositCertificateList";
import { useDepositCertificate } from "../features/depositCertificate/hooks/useDepositCertificate";
import { Alert, Box, CircularProgress } from "@mui/material";
import type { DepositCertificateFormValues } from "../features/depositCertificate/types";
import DepositCertificateForm from "../features/depositCertificate/DepositCertificateForm";
import { formatDate, formatNumberWithCommas } from "../utils/helpers";

const EditDepositCertificate: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { getById, updateDepositCertificate } = useDepositCertificate();
  const { data: depositCertificate, isLoading, isError, error } = getById(Number(id));

  if (isLoading) {
    return (
      <Box display="flex" justifyContent="center" mt={4}>
        <CircularProgress />
      </Box>
    );
  }

  if (isError || !depositCertificate) {
    return (
      <Alert severity="error" sx={{ mt: 4 }}>
        Error loading deposit certificate: {error?.message || "Not found"}
      </Alert>
    );
  }

  const handleSubmit = async (data: DepositCertificateFormValues) => {
    await updateDepositCertificate.mutateAsync({
      id: Number(id),
      data: {
        bankId: data.bankId,
        currencyId: data.currencyId,
        owner_name: data.owner_name,
        description: data.description,
        comment: data.comment,
        amount: Number((data.amount ?? "").replace(/,/g, "")),
        interest_amount: Number((data.interest_amount ?? "").replace(/,/g, "")),
        start_date: data.start_date,
        expiration_date: data.expiration_date,
        isActive: data.isActive,
        updatedBy: "admin",
      },
    });
    navigate("/DepositCertificates");
  };

  const initialFormValues: DepositCertificateFormValues = {
    ...depositCertificate,
    start_date: formatDate(depositCertificate.start_date),
    expiration_date: formatDate(depositCertificate.expiration_date),
    amount: formatNumberWithCommas(depositCertificate.amount),
    interest_amount: formatNumberWithCommas(depositCertificate.interest_amount),
  };

  return (
    <DepositCertificateForm initialValues={initialFormValues} onSubmit={handleSubmit} isEditMode />
  );
};

const CreateDepositCertificate: React.FC = () => {
  const navigate = useNavigate();
  const { createDepositCertificate } = useDepositCertificate();

  const handleSubmit = async (data: DepositCertificateFormValues) => {
    await createDepositCertificate.mutateAsync({
      bankId: data.bankId,
      currencyId: data.currencyId,
      owner_name: data.owner_name,
      description: data.description,
      comment: data.comment,
      amount: Number((data.amount ?? "").replace(/,/g, "")),
      interest_amount: Number((data.interest_amount ?? "").replace(/,/g, "")),
      start_date: data.start_date,
      expiration_date: data.expiration_date,
      isActive: data.isActive,
      createdBy: "admin",
    });
    navigate("/DepositCertificates");
  };

  return <DepositCertificateForm onSubmit={handleSubmit} />;
};

const DepositCertificatePage: React.FC = () => {
  return (
    <Routes>
      <Route index element={<DepositCertificateList />} />
      <Route path="create" element={<CreateDepositCertificate />} />
      <Route path="edit/:id" element={<EditDepositCertificate />} />
      <Route path="*" element={<Navigate to="/DepositCertificates" />} />
    </Routes>
  );
};

export default DepositCertificatePage;
