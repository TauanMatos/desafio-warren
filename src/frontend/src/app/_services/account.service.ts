import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { map } from 'rxjs/operators';
// import { User } from '@app/_models';
import { AccountDto } from '../_models/accountDto';
import { AccountOperationDto } from '../_models/accountOperationDto';

@Injectable({ providedIn: 'root' })
export class AccountService {
    constructor(private http: HttpClient) { }

    accountOperation(clientId: number, value: number, operation: string) {
        return this.http.post<AccountOperationDto>(`${environment.apiUrl}/api/Account/${operation}`, { "clientId": clientId, "value": value })
        .pipe(map(result => {
            return result;
        }));
    }

    getAccountData(clientId: number){
        return this.http.get<AccountDto>(`${environment.apiUrl}/api/Account/GetAccountData?clientId=${clientId}`)
        .pipe(map(result => {
            return result;
        }));
    }
}