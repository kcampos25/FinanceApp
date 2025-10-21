import { FormControl, FormHelperText, InputLabel, MenuItem, Select } from "@mui/material";
import {
  Controller,
  type Control,
  type FieldErrors,
  type FieldValues,
  type Path,
} from "react-hook-form";
import type { ListItemDTO } from "../../utils/types";

interface SelectInputProps<T extends FieldValues> {
  name: Path<T>;
  label?: string;
  control: Control<T>;
  errors: FieldErrors<T>;
  isLoading?: boolean;
  data: ListItemDTO[];
}

const SelectInput = <T extends FieldValues>({
  name,
  label,
  control,
  errors,
  isLoading,
  data,
}: SelectInputProps<T>) => {
  return (
    <FormControl fullWidth error={!!errors[name]}>
      <InputLabel id={`${label}-label`}></InputLabel>
      <Controller
        name={name}
        control={control}
        render={({ field }) => (
          <Select labelId={`${label}-label-select`} {...field} disabled={isLoading} label={label}>
            <MenuItem value={0}>
              <em>Select a {label}</em>
            </MenuItem>
            {data.map((item) => (
              <MenuItem key={item.code} value={item.code}>
                {item.description}
              </MenuItem>
            ))}
          </Select>
        )}
      />
      <FormHelperText>{errors[name]?.message as string}</FormHelperText>
    </FormControl>
  );
};

export default SelectInput;
