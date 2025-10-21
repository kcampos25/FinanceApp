import { useNavigate } from "react-router-dom";
import CrudPage from "../../layouts/CrudPage";
import { CrudMode } from "../../utils/enums/generalEnum";
import { useCurrencies } from "./hooks/useCurrencies";
import { Alert, Box, CircularProgress, TextField } from "@mui/material";
import type { currencyTableColumn } from "./types";
import { useState } from "react";
import ConfirmDialog from "../../components/ConfirmDialog";
import CurrencyTable from "./components/CurrencyTable";

const currencyList: React.FC = () => {
  const navigate = useNavigate();
  const [filterText, setFilterText] = useState("");
  const { getAll, deleteCurrency } = useCurrencies();

  const { data: currencies = [], isLoading, isError, error } = getAll;

  const [currencyToDelete, setcurrencyToDelete] = useState<(typeof currencies)[0] | null>(null);

  const currencyColumns: currencyTableColumn[] = [
    { columnName: "ID", fieldName: "currencyId", isSortable: true },
    { columnName: "Description", fieldName: "description", isSortable: true },
  ];

  const handleGoToCreate = () => {
    navigate("/currencies/create");
  };

  const confirmDelete = async () => {
    if (currencyToDelete) {
      try {
        await deleteCurrency.mutateAsync(currencyToDelete.currencyId);
      } catch {
        // Error already handled by onError of the hook (toast)
      } finally {
        setcurrencyToDelete(null);
      }
    }
  };

  return (
    <CrudPage
      mode={CrudMode.List}
      formTitle="Currencies"
      onAddClick={handleGoToCreate}
      addTittle="ADD CURRENCY"
      isSubmitting={false}
      onCancelClick={() => {}}
    >
      {isLoading && (
        <Box display="flex" justifyContent="center" my={4}>
          <CircularProgress />
        </Box>
      )}

      {isError && (
        <Alert severity="error" sx={{ mb: 2 }}>
          Error loading currencies: {(error as Error).message}
        </Alert>
      )}

      {!isLoading && !isError && currencies.length === 0 && (
        <Alert severity="info" sx={{ mb: 2 }}>
          No currencies found. Try adding a new currency.
        </Alert>
      )}

      <TextField
        label="Search Currency"
        variant="outlined"
        size="small"
        value={filterText}
        onChange={(e) => setFilterText(e.target.value)}
        sx={{ mb: 2, width: "300px" }}
      ></TextField>

      {!isLoading && !isError && currencies.length > 0 && (
        <CurrencyTable
          currencyColumns={currencyColumns}
          currencyData={currencies}
          urlEdit="/currencies/edit/"
          searchText={filterText}
          onDeleteClick={(currency) => setcurrencyToDelete(currency)}
        ></CurrencyTable>
      )}

      <ConfirmDialog
        open={currencyToDelete !== null}
        message="Are you sure you want to delete this currency?"
        onCancel={() => setcurrencyToDelete(null)}
        onConfirm={confirmDelete}
      />
    </CrudPage>
  );
};

export default currencyList;
