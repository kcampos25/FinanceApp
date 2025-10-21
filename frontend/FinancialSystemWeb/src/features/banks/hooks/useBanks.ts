import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { banksApi } from "../../../api/banks.api";
import type { BankDTO, CreateBankDTO, UpdateBankDTO } from "../types";
import { getErrorMessage } from "../../../utils/errorHandler";
import { toast } from "react-toastify";
import type { AxiosError } from "axios";

export const useBanks = () => {
  const queryClient = useQueryClient();

  const getAll = useQuery<BankDTO[], AxiosError>({
    queryKey: ["banks"],
    queryFn: banksApi.getAll,
  });

  const getBankById = (id: number) =>
    useQuery<BankDTO, AxiosError>({
      queryKey: ["bank", id],
      queryFn: () => banksApi.getById(id),
      enabled: !!id,
    });

  const createBank = useMutation<void, AxiosError, CreateBankDTO>({
    mutationFn: banksApi.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["banks"] });
      toast.success("Bank created successfully");
    },
    onError: (error) => toast.error(getErrorMessage(error)),
  });

  const updateBank = useMutation<void, AxiosError, { id: number; data: UpdateBankDTO }>({
    mutationFn: ({ id, data }) => banksApi.update(id, data),
    onSuccess: (_data, variables) => {
      queryClient.invalidateQueries({ queryKey: ["banks"] });
      queryClient.invalidateQueries({ queryKey: ["banksOptions"] });
      queryClient.invalidateQueries({ queryKey: ["bank", variables.id] });
      toast.success("Bank updated successfully");
    },
    onError: (error) => toast.error(getErrorMessage(error)),
  });

  const deleteBank = useMutation<void, AxiosError, number>({
    mutationFn: banksApi.delete,
    onSuccess: (_data, id) => {
      queryClient.invalidateQueries({ queryKey: ["banks"] });
      queryClient.invalidateQueries({ queryKey: ["banksOptions"] });
      queryClient.invalidateQueries({ queryKey: ["bank", id] });
      toast.success("Bank deleted successfully");
    },
    onError: (error) => toast.error(getErrorMessage(error)),
  });

  return { getAll, createBank, updateBank, deleteBank, getBankById };
};
