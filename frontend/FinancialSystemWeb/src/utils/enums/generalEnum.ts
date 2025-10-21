export const CrudMode = {
  List: "List",
  Create: "Create",
  Edit: "Edit",
} as const;

export type CrudMode = (typeof CrudMode)[keyof typeof CrudMode];
