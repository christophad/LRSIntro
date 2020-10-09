export interface IUserAddOrUpdate {
  id?: number;
  name: string;
  surname: string;
  birthDate?: Date | null;
  userTypeId: number;
  userTitleId: number;
  emailAddress: string;
  isActive: boolean;
}
