import { axiosInstance, DEPOSIT_CERTIFICATES_API } from '../constants/api-routes';
import type {
  CreateDepositCertificateDTO,
  DepositCertificateDTO,
  DepositCertificateViewDTO,
  UpdateDepositCertificateDTO,
} from '../features/depositCertificate/types';

export const depositCertificate = {
  getAll: async (): Promise<DepositCertificateDTO[]> => {
    const { data } = await axiosInstance.get(DEPOSIT_CERTIFICATES_API.getAll);
    return data;
  },

  getDetail: async (): Promise<DepositCertificateViewDTO[]> => {
    const { data } = await axiosInstance.get(DEPOSIT_CERTIFICATES_API.getDetails);
    return data;
  },

  getById: async (id: number): Promise<DepositCertificateDTO> => {
    const { data } = await axiosInstance.get(DEPOSIT_CERTIFICATES_API.getById(id));
    return data;
  },

  create: async (data: CreateDepositCertificateDTO): Promise<void> => {
    await axiosInstance.post(DEPOSIT_CERTIFICATES_API.create, data);
  },

  update: async (id: number, data: UpdateDepositCertificateDTO): Promise<void> => {
    await axiosInstance.put(DEPOSIT_CERTIFICATES_API.update(id), data);
  },

  delete: async (id: number): Promise<void> => {
    await axiosInstance.delete(DEPOSIT_CERTIFICATES_API.delete(id));
  },
};
