import { useQuery } from '@tanstack/react-query';
import type { ListItemDTO } from '../types';
import type { AxiosError } from 'axios';
import { currencyApi } from '../../api/currencies.api';

export const useCurrencyOptions = () => {
  const getCurrencyOptions = useQuery<ListItemDTO[], AxiosError>({
    queryKey: ['currencyOptions'],
    queryFn: currencyApi.getCurrencyOptions,
  });

  return { getCurrencyOptions };
};
