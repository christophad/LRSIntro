export interface IUser {
  id: number;
  name: string;
  surname: string;
  birthDate: Date;
  userType: string;
  userTitle: string;
  userTypeId: number;
  userTitleId: number;
  emailAddress: string;
  isActive: boolean;
}
