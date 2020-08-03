import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { map } from 'rxjs/operators';
import { DailyIncomeDto } from '../_models/dailyIncomeDto';

@Injectable({ providedIn: 'root' })
export class DailyIncomeService {
    constructor(private http: HttpClient) { }

    getDailyIncome(clientId: number){
        return this.http.get<DailyIncomeDto[]>(`${environment.apiUrl}/api/DailyIncome/GetDailyIncome?clientId=${clientId}`)
        .pipe(map(result => {
            return result;
        }));
    }
}