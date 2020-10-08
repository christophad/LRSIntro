import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IAlert } from '../shared/Entities/IAlert';

@Injectable()
export class AlertService {
  private alert = new BehaviorSubject<IAlert>({ status: false });

  alertStatus = this.alert.asObservable();

  constructor() {}

  public showAlert(message: string): void {
    this.alert.next({ status: true, message: message });
  }

  public hideAlert(): void {
    this.alert.next({ status: false });
  }
}
