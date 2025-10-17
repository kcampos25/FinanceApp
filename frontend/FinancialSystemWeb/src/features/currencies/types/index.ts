export interface CurrencyDTO {
  currencyId: number;
  description: string;
  createdBy: string;
  createdAt: string;
  updatedBy: string;
  updatedAt: string;
}

export interface CreateCurrencyDTO {
  description: string;
  createdBy: string;
}

export interface UpdateCurrencyDTO {
  description: string;
  updatedBy: string;
}

export interface CurrencyFormValues {
  description: string;
}

export type currencyTableColumn = {
  columnName: string;
  fieldName: string;
  isSortable: boolean;
};
