import axios from "axios";

export const API_BASE_URL = "https://localhost:44364/api";

export const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: { "Content-Type": "application/json" },
});

// Banks APIS
export const BANKS_API = {
  getAll: `${API_BASE_URL}/banks`,
  getLookup: `${API_BASE_URL}/banks/options`,
  getById: (id: number) => `${API_BASE_URL}/banks/${id}`,
  create: `${API_BASE_URL}/banks`,
  update: (id: number) => `${API_BASE_URL}/banks/${id}`,
  delete: (id: number) => `${API_BASE_URL}/banks/${id}`,
};

// Currencies APIS
export const CURRENCIES_API = {
  getAll: `${API_BASE_URL}/currency`,
  getLookup: `${API_BASE_URL}/currency/options`,
  getById: (id: number) => `${API_BASE_URL}/currency/${id}`,
  create: `${API_BASE_URL}/currency`,
  update: (id: number) => `${API_BASE_URL}/currency/${id}`,
  delete: (id: number) => `${API_BASE_URL}/currency/${id}`,
};

// deposit certificate APIS
export const DEPOSIT_CERTIFICATES_API = {
  getAll: `${API_BASE_URL}/depositCertificate`,
  getById: (id: number) => `${API_BASE_URL}/depositCertificate/${id}`,
  getDetails: `${API_BASE_URL}/depositCertificate/details`,
  create: `${API_BASE_URL}/depositCertificate`,
  update: (id: number) => `${API_BASE_URL}/depositCertificate/${id}`,
  delete: (id: number) => `${API_BASE_URL}/depositCertificate/${id}`,
};
