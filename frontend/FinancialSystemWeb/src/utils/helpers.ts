export const formatNumberWithCommas = (
  value: number | string | null | undefined,
  options?: {
    minDecimals?: number;
    maxDecimals?: number;
  },
): string => {
  if (value === null || value === undefined || value === '') return '';

  const number = typeof value === 'string' ? Number(value.replace(/,/g, '')) : value;

  if (isNaN(number)) return '';

  const { minDecimals = 2, maxDecimals = 2 } = options || {};

  return number.toLocaleString('en-US', {
    minimumFractionDigits: minDecimals,
    maximumFractionDigits: maxDecimals,
  });
};

export const formatDate = (date: string | Date | null | undefined): string => {
  if (!date) return '';

  const isoString = typeof date === 'string' ? date : date.toISOString();

  const [yyyy, mm, dd] = isoString.split('T')[0].split('-');

  if (!yyyy || !mm || !dd) return '';

  return `${yyyy}-${mm}-${dd}`;
};
