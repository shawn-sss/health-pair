import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  sharedIns: string;

  sharedSpec: string;

  constructor() {}
}
