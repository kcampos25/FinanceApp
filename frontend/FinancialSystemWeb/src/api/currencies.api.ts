import { axiosInstance, CURRENCIES_API } from '../constants/api-routes';
import type {
  CreateCurrencyDTO,
  CurrencyDTO,
  UpdateCurrencyDTO,
} from '../features/currencies/types';
import type { ListItemDTO } from '../utils/types';

export const currencyApi = {
  getAll: async (): Promise<CurrencyDTO[]> => {
    const { data } = await axiosInstance.get(CURRENCIES_API.getAll);
    return data;
  },

  getCurrencyOptions: async (): Promise<ListItemDTO[]> => {
    const { data } = await axiosInstance.get(CURRENCIES_API.getLookup);
    return data;
  },

  getById: async (id: number): Promise<CurrencyDTO> => {
    const { data } = await axiosInstance.get(CURRENCIES_API.getById(id));
    return data;
  },

  create: async (currency: CreateCurrencyDTO): Promise<void> => {
    await axiosInstance.post(CURRENCIES_API.create, currency);
  },

  update: async (id: number, currency: UpdateCurrencyDTO): Promise<void> => {
    await axiosInstance.put(CURRENCIES_API.update(id), currency);
  },

  delete: async (id: number): Promise<void> => {
    await axiosInstance.delete(CURRENCIES_API.delete(id));
  },
};
