import { AxiosError } from 'axios';

export const today = new Date().toISOString().substring(0, 10); // "YYYY-MM-DD"

export const isAxiosError = (error: unknown): error is AxiosError => {
  return (
    typeof error === 'object' &&
    error !== null &&
    'isAxiosError' in error &&
    (error as AxiosError).isAxiosError === true
  );
};

export const getErrorMessage = (error: unknown): string => {
  if (isAxiosError(error)) {
    const data = error.response?.data;

    if (data && typeof data === 'object') {
      //  FluentValidation
      if ('errors' in data && typeof data.errors === 'object' && data.errors !== null) {
        const validationErrors = data.errors as Record<string, string>;
        return Object.values(validationErrors).join('\n');
      }

      if ('message' in data && typeof data.message === 'string') {
        return data.message;
      }

      if ('error' in data && typeof data.error === 'string') {
        return data.error;
      }
    }

    return error.message || 'Unexpected API error';
  }

  if (typeof error === 'object' && error !== null) {
    if ('message' in error && typeof error.message === 'string') {
      return error.message;
    }
  }

  if (typeof error === 'string') {
    return error;
  }

  return 'An unknown error occurred';
};
