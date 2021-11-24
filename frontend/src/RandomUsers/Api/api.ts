import apiBase from '../../apiBase';

export interface IRandomUserFilter {
    count?: number;
    location?: string;
    nationality?: string;
}

export interface IRandomUser {
    fullName: string;
    location: string;
    nationality: string;
    picture: string;
}

export interface IRandomUserResponse {
    correlationId: string;
    users: IRandomUser[];
}

export class RandomUserApi {
    getFiltered(filter: IRandomUserFilter): Promise<IRandomUserResponse> {
        return apiBase.get<IRandomUserResponse>('RandomUser', { params: filter }).then(d => d.data);
    }
}