import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ILoader } from '../shared/Entities/ILoader';

@Injectable()
export class LoaderService {
  private loader = new BehaviorSubject<ILoader>({ status: false });

  loaderStatus = this.loader.asObservable();

  constructor() {}

  public showLoader(): void {
    this.loader.next({ status: true });
  }

  public hideLoader(): void {
    this.loader.next({ status: false });
  }
}
