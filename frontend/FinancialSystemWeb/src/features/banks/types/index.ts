export interface BankDTO {
  bankId: number;
  description: string;
  createdBy: string;
  createdAt: string;
  updatedBy?: string;
  updatedAt?: string;
}

export interface CreateBankDTO {
  description: string;
  createdBy: string;
}

export interface UpdateBankDTO {
  description: string;
  updatedBy: string;
}

export type BankFormValues = Pick<CreateBankDTO, "description">;
