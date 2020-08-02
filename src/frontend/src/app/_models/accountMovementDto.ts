import * as moment from 'moment';

export class AccountMovementDto {
    id: number;
    accountOperation: string;
    operationDate: string;
    amount: string;
    accountId: number;
}