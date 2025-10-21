import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import type {
  CreateDepositCertificateDTO,
  DepositCertificateDTO,
  DepositCertificateViewDTO,
  UpdateDepositCertificateDTO,
} from "../types";
import { depositCertificate } from "../../../api/depositCertificate.api";
import { toast } from "react-toastify";
import { getErrorMessage } from "../../../utils/errorHandler";

export const useDepositCertificate = () => {
  const queryClient = useQueryClient();
  const getAll = useQuery<DepositCertificateDTO[], AxiosError>({
    queryKey: ["DepositCertificates"],
    queryFn: depositCertificate.getAll,
  });

  const getDetail = useQuery<DepositCertificateViewDTO[], AxiosError>({
    queryKey: ["DepositCertificateDetail"],
    queryFn: depositCertificate.getDetail,
  });

  const getById = (id: number) =>
    useQuery<DepositCertificateDTO, AxiosError>({
      queryKey: ["DepositCertificate", id],
      queryFn: () => depositCertificate.getById(id),
      enabled: !!id,
    });

  const createDepositCertificate = useMutation<void, AxiosError, CreateDepositCertificateDTO>({
    mutationFn: depositCertificate.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["DepositCertificateDetail"] });
      toast.success("Deposit Certificate created successfully");
    },
    onError: (error) => toast.error(getErrorMessage(error)),
  });

  const updateDepositCertificate = useMutation<
    void,
    AxiosError,
    { id: number; data: UpdateDepositCertificateDTO }
  >({
    mutationFn: ({ id, data }) => depositCertificate.update(id, data),
    onSuccess: (_data, variables) => {
      queryClient.invalidateQueries({ queryKey: ["DepositCertificates"] });
      queryClient.invalidateQueries({ queryKey: ["DepositCertificateDetail"] });
      queryClient.invalidateQueries({ queryKey: ["DepositCertificate", variables.id] });
      toast.success("Deposit Certificate updated successfully");
    },
    onError: (error) => toast.error(getErrorMessage(error)),
  });

  const deleteDepositCertificate = useMutation<void, AxiosError, number>({
    mutationFn: depositCertificate.delete,
    onSuccess: (_data, id) => {
      queryClient.invalidateQueries({ queryKey: ["DepositCertificates"] });
      queryClient.invalidateQueries({ queryKey: ["DepositCertificateDetail"] });
      queryClient.invalidateQueries({ queryKey: ["DepositCertificate", id] });

      toast.success("Deposit Certificate deleted successfully");
    },
    onError: (error) => toast.error(getErrorMessage(error)),
  });

  return {
    getAll,
    getDetail,
    getById,
    createDepositCertificate,
    updateDepositCertificate,
    deleteDepositCertificate,
  };
};
