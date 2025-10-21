import type { DepositCertificateFormValues } from "./types";
import * as yup from "yup";

// Validation with Yup
export const validationSchema: yup.ObjectSchema<DepositCertificateFormValues> = yup.object({
  bankId: yup.number().moreThan(0, "Bank is required").required(),
  currencyId: yup.number().moreThan(0, "Currency is required").required(),
  owner_name: yup.string().nullable().optional().max(20, "Max 20 characters"),
  description: yup.string().nullable().optional().max(20, "Max 20 characters"),
  comment: yup.string().nullable().optional().max(2000, "Max 2000 characters"),
  amount: yup
    .string()
    .transform((value) => value.replace(/,/g, ""))
    .test("is-number", "Amount must be a valid number", (value) => !isNaN(Number(value)))
    .test("greater-than-zero", "Amount must be greater than zero", (value) => Number(value) > 0)
    .required("Amount is required"),

  start_date: yup
    .string()
    .transform((value, originalValue) => (originalValue === "" ? null : value))
    .required("Start Date is required"),
  expiration_date: yup
    .string()
    .required("Expiration Date is required")
    .test(
      "is-after-or-equal-start-date",
      "Expiration Date must be after or equal to Start Date",
      function (value) {
        const { start_date } = this.parent;
        if (!value || !start_date) return true;
        const start = new Date(start_date);
        const end = new Date(value);
        return end >= start;
      },
    ),
  interest_amount: yup
    .string()
    .transform((value) => value.replace(/,/g, "")) // quitar comas
    .test("is-number", "Interest Amount must be a valid number", (value) => !isNaN(Number(value)))
    .test(
      "greater-than-zero",
      "Interest Amount must be greater than zero",
      (value) => Number(value) > 0,
    )
    .test(
      "less-than-amount",
      "Interest Amount cannot be greater than Total Amount",
      function (value) {
        const { amount } = this.parent;
        const interest = Number(value?.replace(/,/g, "") ?? "");
        const total = Number((amount || "").replace(/,/g, ""));
        return interest <= total;
      },
    )
    .required("Interest Amount is required"),
  isActive: yup.boolean().required(),
});
