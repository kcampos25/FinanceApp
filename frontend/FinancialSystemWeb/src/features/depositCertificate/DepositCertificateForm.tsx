import { Box, TextField } from "@mui/material";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import type { DepositCertificateFormValues } from "./types";
import CrudPage from "../../layouts/CrudPage";
import { CrudMode } from "../../utils/enums/generalEnum";
import { useNavigate } from "react-router-dom";
import { useBankOptions } from "../../utils/hooks/useBankOptions";
import { useCurrencyOptions } from "../../utils/hooks/useCurrencyOptions";
import NumberInput from "../../components/Controls/NumberInput";
import SelectInput from "../../components/Controls/SelectInput";
import { today } from "../../utils/errorHandler";
import { validationSchema } from "./validationSchema";
import CheckBoxInput from "../../components/Controls/CheckBoxInput";

interface DepositCertificateFormProps {
  initialValues?: DepositCertificateFormValues;
  onSubmit: (data: DepositCertificateFormValues) => void;
  isEditMode?: boolean;
}

//Default values for creation
const defaultFormValues: DepositCertificateFormValues = {
  bankId: 0,
  currencyId: 0,
  owner_name: "",
  description: "",
  comment: "",
  amount: "0,00",
  start_date: today,
  expiration_date: today,
  interest_amount: "0,00",
  isActive: false,
};
//edit form deposit certifcate
const DepositCertificateForm: React.FC<DepositCertificateFormProps> = ({
  initialValues,
  onSubmit,
  isEditMode,
}) => {
  const navigate = useNavigate();

  const { getBankOptions } = useBankOptions();
  const { data: bankOptions = [], isLoading: isBankLoading } = getBankOptions;

  const { getCurrencyOptions } = useCurrencyOptions();
  const { data: currencyOptions = [], isLoading: isCurrencyLoading } = getCurrencyOptions;

  const {
    register,
    control,
    handleSubmit,
    formState: { isSubmitting, errors },
  } = useForm<DepositCertificateFormValues>({
    resolver: yupResolver(validationSchema),
    defaultValues: initialValues ?? defaultFormValues,
    mode: "onChange",
  });

  const handleGoToList = () => {
    navigate("/depositCertificates");
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <CrudPage
        mode={isEditMode ? CrudMode.Edit : CrudMode.Create}
        formTitle={isEditMode ? "Edit Deposit Certificate" : "Add Deposit Certificate"}
        onAddClick={() => ({})}
        addTittle=""
        isSubmitting={isSubmitting}
        onCancelClick={handleGoToList}
      >
        <Box
          sx={{
            display: "grid",
            gap: 2,
            gridTemplateColumns: {
              xs: "1fr",
              sm: "repeat(2, 1fr)",
            },
          }}
        >
          {/* Bank */}
          <SelectInput
            name="bankId"
            label="Bank"
            control={control}
            errors={errors}
            isLoading={isBankLoading}
            data={bankOptions}
          />

          {/* Currency */}
          <SelectInput
            name="currencyId"
            label="Currency"
            control={control}
            errors={errors}
            isLoading={isCurrencyLoading}
            data={currencyOptions}
          />

          {/* Owner Name */}
          <TextField
            fullWidth
            label="Owner Name"
            {...register("owner_name")}
            error={!!errors.owner_name}
            helperText={errors.owner_name?.message}
          />

          {/* Description */}
          <TextField
            fullWidth
            label="Description"
            {...register("description")}
            error={!!errors.description}
            helperText={errors.description?.message}
          />

          {/* Amount */}
          <NumberInput name="amount" label="Amount" control={control} errors={errors} />

          {/* interest_amount */}
          <NumberInput
            name="interest_amount"
            label="Interest Amount"
            control={control}
            errors={errors}
          />

          {/* Start Date */}
          <TextField
            fullWidth
            label="Start Date"
            type="date"
            slotProps={{
              inputLabel: {
                shrink: true,
              },
            }}
            {...register("start_date")}
            error={!!errors.start_date}
            helperText={errors.start_date?.message}
          />

          {/* Expiration Date */}
          <TextField
            fullWidth
            label="Expiration Date"
            type="date"
            slotProps={{
              inputLabel: {
                shrink: true,
              },
            }}
            {...register("expiration_date")}
            error={!!errors.expiration_date}
            helperText={errors.expiration_date?.message}
          />

          {/* Comment */}
          <Box sx={{ gridColumn: "span 2" }}>
            <TextField
              fullWidth
              label="Comment"
              multiline
              rows={4}
              {...register("comment")}
              error={!!errors.comment}
              helperText={errors.comment?.message}
            />
          </Box>

          {/* Is Active */}
          <Box sx={{ gridColumn: "span 2" }}>
            <CheckBoxInput name="isActive" label="Is Active" control={control} errors={errors} />
          </Box>
        </Box>
      </CrudPage>
    </form>
  );
};

export default DepositCertificateForm;
