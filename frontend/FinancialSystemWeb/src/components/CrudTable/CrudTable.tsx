import {
  Box,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TableSortLabel,
  Tooltip,
  TablePagination,
  type SortDirection,
} from '@mui/material';
import { useMemo, useState } from 'react';
import type { ColumnTable, CrudFilter } from './types';
import { Delete, Edit } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';

type CrudTableProps<T> = {
  columns: ColumnTable<T>[];
  rows: T[];
  columnKeyname: keyof T;
  urlEdit: string;
  onDeleteClick?: (item: T) => void;
  filters?: CrudFilter<T>[];
  filterFunction?: (row: T, filters: CrudFilter<T>[]) => boolean;
};

const CrudTable = <T,>({
  columns,
  rows,
  columnKeyname,
  urlEdit,
  onDeleteClick,
  filters = [],
  filterFunction,
}: CrudTableProps<T>) => {
  const [columnOrderBy, setColumnOrderBy] = useState<keyof T | null>(null);
  const [columnOrder, setColumnOrder] = useState<SortDirection>('asc');
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const navigate = useNavigate();

  const handleSortByColumn = (fieldName: keyof T) => {
    if (columnOrderBy === fieldName) {
      setColumnOrder((prev) => (prev === 'asc' ? 'desc' : 'asc'));
    } else {
      setColumnOrder('asc');
      setColumnOrderBy(fieldName);
    }
  };

  const handleChangePage = (_: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const filteredRows = useMemo(() => {
    if (!filterFunction || filters.length === 0) return rows;
    return rows.filter((row) => filterFunction(row, filters));
  }, [rows, filters, filterFunction]);

  const sortedRows = useMemo(() => {
    if (!columnOrderBy) return filteredRows;
    return [...filteredRows].sort((a, b) => {
      const aValue = a[columnOrderBy];
      const bValue = b[columnOrderBy];

      if (typeof aValue === 'string' && typeof bValue === 'string') {
        return columnOrder === 'asc' ? aValue.localeCompare(bValue) : bValue.localeCompare(aValue);
      }

      if (typeof aValue === 'number' && typeof bValue === 'number') {
        return columnOrder === 'asc' ? aValue - bValue : bValue - aValue;
      }

      return 0;
    });
  }, [filteredRows, columnOrder, columnOrderBy]);

  const paginatedRows = useMemo(() => {
    return sortedRows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage);
  }, [sortedRows, page, rowsPerPage]);

  return (
    <>
      <Table
        sx={{
          border: '1px solid #e0e0e0',
          borderRadius: 1,
        }}
      >
        <TableHead>
          <TableRow sx={{ backgroundColor: 'rgba(0, 0, 0, 0.04)' }}>
            {columns.map((column) =>
              column.isSortable ? (
                <TableCell
                  key={String(column.fieldName)}
                  sortDirection={columnOrderBy === column.fieldName ? columnOrder : false}
                >
                  <TableSortLabel
                    active={columnOrderBy === column.fieldName}
                    direction={
                      (columnOrderBy === column.fieldName ? columnOrder : undefined) as
                        | 'asc'
                        | 'desc'
                        | undefined
                    }
                    onClick={() => handleSortByColumn(column.fieldName)}
                  >
                    {column.columnname}
                  </TableSortLabel>
                </TableCell>
              ) : (
                <TableCell key={String(column.fieldName)}>{column.columnname}</TableCell>
              ),
            )}
            <TableCell align="right">Actions</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {paginatedRows.map((row, index) => (
            <TableRow
              key={String(row[columnKeyname])}
              hover
              sx={{
                backgroundColor: index % 2 === 0 ? '#fafafa' : 'white',
              }}
            >
              {columns.map((column) => (
                <TableCell
                  key={String(column.fieldName)}
                  align={
                    column.fieldName === columnKeyname
                      ? 'left'
                      : typeof row[column.fieldName] === 'number'
                        ? 'right'
                        : 'left'
                  }
                >
                  {String(row[column.fieldName])}
                </TableCell>
              ))}

              <TableCell align="right">
                <Box display="flex" justifyContent="flex-end" gap={1}>
                  <Tooltip title="Edit">
                    <IconButton
                      color="primary"
                      onClick={() => navigate(`${urlEdit}${String(row[columnKeyname])}`)}
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
      />
    </>
  );
};

export default CrudTable;
