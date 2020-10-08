export interface IUserAddOrUpdate {
  id?: number;
  name: string;
  surname: string;
  birthDate: Date;
  userType: number;
  userTitle: number;
  emailAddress: string;
  isActive: boolean;
}
