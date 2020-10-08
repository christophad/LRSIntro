// TODO why have IUserEdit as well? use this only since you can have the values on dropdowns
export interface IUserAddOrUpdate {
  id?: number;
  name: string;
  surname: string;
  birthDate: Date;
  userTypeId: number;
  userTitleId: number;
  emailAddress: string;
  isActive: boolean;
}
