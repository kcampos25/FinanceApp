import { Checkbox, FormControl, FormControlLabel, FormHelperText } from '@mui/material';
import {
  Controller,
  type Control,
  type FieldErrors,
  type FieldValues,
  type Path,
} from 'react-hook-form';

interface CheckBoxInputProps<T extends FieldValues> {
  name: Path<T>;
  label?: string;
  control: Control<T>;
  errors: FieldErrors<T>;
}

const CheckBoxInput = <T extends FieldValues>({
  name,
  label,
  control,
  errors,
}: CheckBoxInputProps<T>) => {
  return (
    <FormControl error={!!errors[name]}>
      <FormControlLabel
        control={
          <Controller
            name={name}
            control={control}
            render={({ field }) => (
              <Checkbox
                {...field}
                checked={field.value ?? false}
                onChange={(e) => field.onChange(e.target.checked)}
              />
            )}
          />
        }
        label={label}
      />
      {errors[name] && <FormHelperText>{errors[name]?.message as string}</FormHelperText>}
    </FormControl>
  );
};

export default CheckBoxInput;
