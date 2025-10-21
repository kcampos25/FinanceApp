import { useQuery } from "@tanstack/react-query";
import { banksApi } from "../../api/banks.api";
import type { ListItemDTO } from "../types";
import type { AxiosError } from "axios";

export const useBankOptions = () => {
  const getBankOptions = useQuery<ListItemDTO[], AxiosError>({
    queryKey: ["banksOptions"],
    queryFn: banksApi.getBankOptions,
  });

  return { getBankOptions };
};
