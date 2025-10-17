export interface ColumnTable<T> {
  columnname: string;
  fieldName: keyof T;
  isSortable: boolean;
}

export type CrudFilter<T> = {
  field: keyof T;
  value: string;
};
