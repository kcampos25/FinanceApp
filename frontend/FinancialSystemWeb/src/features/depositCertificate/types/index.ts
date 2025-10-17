export interface DepositCertificateDTO {
  certificateId: number;
  bankId: number;
  currencyId: number;
  owner_name?: string | null;
  description?: string | null;
  comment?: string | null;
  amount: number;
  start_date: string;
  expiration_date: string;
  interest_amount: number;
  isActive: boolean;
  createdBy: string;
  createdAt: string;
  updatedBy: string;
  updatedAt: string;
}

export interface DepositCertificateViewDTO {
  certificateId: number;
  bank: string;
  currency: string;
  description: string;
  amount: number;
  isActive: boolean;
  interest_amount: number;
  expiration_date: string;
}

export interface CreateDepositCertificateDTO {
  bankId: number;
  currencyId: number;
  owner_name?: string | null;
  description?: string | null;
  comment?: string | null;
  amount: number;
  start_date: string;
  expiration_date: string;
  interest_amount: number;
  isActive: boolean;
  createdBy: string;
}

export interface UpdateDepositCertificateDTO {
  bankId: number;
  currencyId: number;
  owner_name?: string | null;
  description?: string | null;
  comment?: string | null;
  amount: number;
  start_date: string;
  expiration_date: string;
  interest_amount: number;
  isActive: boolean;
  updatedBy: string;
}

export interface DepositCertificateFormValues {
  bankId: number;
  currencyId: number;
  owner_name?: string | null;
  description?: string | null;
  comment?: string | null;
  amount: string;
  start_date: string;
  expiration_date: string;
  interest_amount: string;
  isActive: boolean;
}
