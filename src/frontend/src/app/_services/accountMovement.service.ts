import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { map } from 'rxjs/operators';
// import { User } from '@app/_models';
import { AccountDto } from '../_models/accountDto';
import { AccountMovementDto } from '../_models/accountMovementDto';

@Injectable({ providedIn: 'root' })
export class AccountMovementService {
    constructor(private http: HttpClient) { }

    getAccountHistory(clientId: number){
        return this.http.get<AccountMovementDto[]>(`${environment.apiUrl}/api/AccountHistory/GetAccountHistory?id=${clientId}`)
        .pipe(map(result => {
            return result;
        }));
    }
}