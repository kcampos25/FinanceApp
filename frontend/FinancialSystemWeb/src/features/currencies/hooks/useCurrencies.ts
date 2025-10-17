import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import type { CreateCurrencyDTO, CurrencyDTO, UpdateCurrencyDTO } from '../types';
import type { AxiosError } from 'axios';
import { currencyApi } from '../../../api/currencies.api';
import { toast } from 'react-toastify';
import { getErrorMessage } from '../../../utils/errorHandler';

export const useCurrencies = () => {
  const queryClient = useQueryClient();

  const getAll = useQuery<CurrencyDTO[], AxiosError>({
    queryKey: ['currencies'],
    queryFn: currencyApi.getAll,
  });

  const getById = (id: number) =>
    useQuery<CurrencyDTO, AxiosError>({
      queryKey: ['currency', id],
      queryFn: () => currencyApi.getById(id),
      enabled: !!id,
    });

  const createCurrency = useMutation<void, AxiosError, CreateCurrencyDTO>({
    mutationFn: currencyApi.create,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['currencies'] });
      toast.success('Currency created successfully');
    },

    onError: (error) => toast.error(getErrorMessage(error)),
  });

  const updateCurrency = useMutation<void, AxiosError, { id: number; data: UpdateCurrencyDTO }>({
    mutationFn: ({ id, data }) => currencyApi.update(id, data),

    onSuccess: (_data, variables) => {
      queryClient.invalidateQueries({ queryKey: ['currencies'] });
      queryClient.invalidateQueries({ queryKey: ['currencyOptions'] });
      queryClient.invalidateQueries({ queryKey: ['currency', variables.id] });
      toast.success('Currency updated successfully');
    },
    onError: (error) => toast.error(getErrorMessage(error)),
  });

  const deleteCurrency = useMutation<void, AxiosError, number>({
    mutationFn: currencyApi.delete,
    onSuccess: (_data, id) => {
      queryClient.invalidateQueries({ queryKey: ['currencies'] });
      queryClient.invalidateQueries({ queryKey: ['currencyOptions'] });
      queryClient.invalidateQueries({ queryKey: ['currency', id] });
      toast.success('Currency deleted successfully');
    },
    onError: (error) => toast.error(getErrorMessage(error)),
  });

  return { getAll, getById, createCurrency, updateCurrency, deleteCurrency };
};
