import * as moment from 'moment';

export class AuthResponse {
    authenticated: boolean;
    accessToken: string;
    expiration: moment.Moment;
    created:  moment.Moment;
    clientId: number;
    message: string;
}