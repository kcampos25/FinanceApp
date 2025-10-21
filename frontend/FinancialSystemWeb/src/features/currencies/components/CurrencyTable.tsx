import {
  Box,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
  TableSortLabel,
  Tooltip,
  type SortDirection,
} from "@mui/material";
import { useMemo, useState } from "react";
import type { CurrencyDTO, currencyTableColumn } from "../types";
import { Delete, Edit } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

interface CurrencyTableProps {
  currencyColumns: currencyTableColumn[];
  currencyData: CurrencyDTO[];
  urlEdit: string;
  searchText: string;
  onDeleteClick: (item: CurrencyDTO) => void;
}

const CurrencyTable: React.FC<CurrencyTableProps> = ({
  currencyColumns,
  currencyData,
  urlEdit,
  searchText,
  onDeleteClick,
}) => {
  const navigate = useNavigate();

  const [columnOrderBy, setColumnOrderBy] = useState<keyof CurrencyDTO | null>(null);
  const [columnOrder, setColumnOrder] = useState<SortDirection>("asc");
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const handleSortByColumn = (fieldName: keyof CurrencyDTO) => {
    if (columnOrderBy === fieldName) {
      setColumnOrder((prev) => (prev === "asc" ? "desc" : "asc"));
    } else {
      setColumnOrderBy(fieldName);
      setColumnOrder("desc");
    }
  };

  const filteredData = useMemo(() => {
    return currencyData.filter((item) =>
      item.description?.toLowerCase().includes(searchText.toLowerCase()),
    );
  }, [currencyData, searchText]);

  const sortedRows = useMemo(() => {
    if (!columnOrderBy) return filteredData;

    return [...filteredData].sort((a, b) => {
      const aValue = a[columnOrderBy];
      const bValue = b[columnOrderBy];

      if (typeof aValue === "string" && typeof bValue === "string") {
        return columnOrder === "asc" ? aValue.localeCompare(bValue) : bValue.localeCompare(aValue);
      }

      if (typeof aValue === "number" && typeof bValue === "number") {
        return columnOrder === "asc" ? aValue - bValue : bValue - aValue;
      }

      return 0;
    });
  }, [filteredData, columnOrder, columnOrderBy]);

  const handleChangePage = (_: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const paginatedRows = useMemo(() => {
    return sortedRows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage);
  }, [sortedRows, page, rowsPerPage]);

  return (
    <>
      <Table
        sx={{
          border: "1px solid #e0e0e0",
          borderRadius: 1,
        }}
      >
        <TableHead>
          <TableRow sx={{ backgroundColor: "rgba(0, 0, 0, 0.04)" }}>
            {currencyColumns.map((column) =>
              column.isSortable ? (
                <TableCell
                  key={column.fieldName}
                  sortDirection={columnOrderBy === column.fieldName ? columnOrder : false}
                >
                  <TableSortLabel
                    active={columnOrderBy === column.fieldName}
                    direction={
                      columnOrderBy === column.fieldName
                        ? (columnOrder as "asc" | "desc")
                        : undefined
                    }
                    onClick={() => handleSortByColumn(column.fieldName as keyof CurrencyDTO)}
                  >
                    {column.columnName}
                  </TableSortLabel>
                </TableCell>
              ) : (
                <TableCell key={column.fieldName}>{column.columnName}</TableCell>
              ),
            )}
            <TableCell align="right">Actions</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {paginatedRows.map((row, index) => (
            <TableRow
              key={row.currencyId}
              hover
              sx={{
                backgroundColor: index % 2 === 0 ? "#fafafa" : "white",
              }}
            >
              <TableCell align="left">{row.currencyId}</TableCell>
              <TableCell align="left">{row.description}</TableCell>
              <TableCell align="right">
                <Box display="flex" justifyContent="flex-end" gap={1}>
                  <Tooltip title="Edit">
                    <IconButton
                      color="primary"
                      onClick={() => navigate(`${urlEdit}${String(row.currencyId)}`)}
                    >
                      <Edit />
                    </IconButton>
                  </Tooltip>
                  <Tooltip title="Delete">
                    <IconButton color="error" onClick={() => onDeleteClick?.(row)}>
                      <Delete />
                    </IconButton>
                  </Tooltip>
                </Box>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <TablePagination
        component="div"
        count={sortedRows.length}
        page={page}
        onPageChange={handleChangePage}
        rowsPerPage={rowsPerPage}
        onRowsPerPageChange={handleChangeRowsPerPage}
        rowsPerPageOptions={[5, 10, 25]}
        showFirstButton
        showLastButton
        labelDisplayedRows={({ from, to, count }) => `${from}-${to} of ${count}`}
        labelRowsPerPage="Rows per page:"
      />
    </>
  );
};

export default CurrencyTable;
