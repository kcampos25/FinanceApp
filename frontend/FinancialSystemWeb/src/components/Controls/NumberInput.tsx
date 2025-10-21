import { FormControl, TextField } from "@mui/material";
import {
  Controller,
  type Control,
  type FieldErrors,
  type FieldValues,
  type Path,
} from "react-hook-form";

interface NumberInputProps<T extends FieldValues> {
  name: Path<T>;
  label?: string;
  control: Control<T>;
  errors: FieldErrors<T>;
}

const sanitizeInput = (value: string): string => {
  let sanitized = "";
  let hasDecimal = false;
  let decimalCount = 0;

  for (const char of value) {
    if (char >= "0" && char <= "9") {
      if (hasDecimal) {
        if (decimalCount < 2) {
          sanitized += char;
          decimalCount++;
        } else {
          break;
        }
      } else {
        sanitized += char;
      }
    } else if (char === "." && !hasDecimal) {
      sanitized += char;
      hasDecimal = true;
    }
  }

  return sanitized;
};

const NumberInput = <T extends FieldValues>({
  name,
  label,
  control,
  errors,
}: NumberInputProps<T>) => {
  return (
    <FormControl fullWidth error={!!errors[name]}>
      <Controller
        name={name}
        control={control}
        render={({ field: { onChange, onBlur, value, ref, ...field } }) => (
          <TextField
            {...field}
            inputRef={ref}
            label={label}
            value={value}
            onChange={(e) => {
              const rawValue = e.target.value;
              const sanitized = sanitizeInput(rawValue);
              onChange(sanitized);
            }}
            onBlur={() => {
              const numberValue = Number(String(value).replace(/,/g, ""));
              if (!isNaN(numberValue)) {
                const formatted = numberValue.toLocaleString("en-US", {
                  minimumFractionDigits: 2,
                  maximumFractionDigits: 2,
                });
                onChange(formatted);
              } else {
                onChange("");
              }
              onBlur();
            }}
            error={!!errors[name]}
            helperText={errors[name]?.message as string}
            fullWidth
          />
        )}
      />
    </FormControl>
  );
};

export default NumberInput;
