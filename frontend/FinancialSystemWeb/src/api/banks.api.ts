import { BANKS_API, axiosInstance } from "../constants/api-routes";
import type { BankDTO, CreateBankDTO, UpdateBankDTO } from "../features/banks/types";
import type { ListItemDTO } from "../utils/types";

export const banksApi = {
  getAll: async (): Promise<BankDTO[]> => {
    const { data } = await axiosInstance.get(BANKS_API.getAll);
    return data;
  },

  getById: async (id: number): Promise<BankDTO> => {
    const { data } = await axiosInstance.get(BANKS_API.getById(id));
    return data;
  },

  getBankOptions: async (): Promise<ListItemDTO[]> => {
    const { data } = await axiosInstance.get(BANKS_API.getLookup);
    return data;
  },

  create: async (bank: CreateBankDTO): Promise<void> => {
    await axiosInstance.post(BANKS_API.create, bank);
  },

  update: async (id: number, bank: UpdateBankDTO): Promise<void> => {
    await axiosInstance.put(BANKS_API.update(id), bank);
  },

  delete: async (id: number): Promise<void> => {
    await axiosInstance.delete(BANKS_API.delete(id));
  },
};
