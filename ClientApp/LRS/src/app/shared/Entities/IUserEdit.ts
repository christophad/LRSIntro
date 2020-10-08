import { IUserTitle } from './IUserTitle';
import { IUserType } from './IUserType';

export interface IUserEdit {
  id: number;
  name: string;
  surname: string;
  birthDate: Date;
  userType: IUserType;
  userTitle: IUserTitle;
  emailAddress: string;
  isActive: boolean;
}
