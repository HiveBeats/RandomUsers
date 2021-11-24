import apiBase from '../../apiBase';

export interface IRandomUserFilter {
    count?: number;
    location?: string;
    nationality?: string;
}

export interface IExportRandomUsersFilter extends IRandomUserFilter {
    correlationId: string;
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

    exportToFile(filter: IExportRandomUsersFilter): Promise<any> {
        return apiBase.get<any>('RandomUser/file', { params: filter, responseType: 'blob' }).then(res => {
            let fileName = res.headers['content-disposition']
                            .split(';')
                            .find((n: string) => n.includes('filename='))
                            .replace('filename=', '')
                            .trim();
            let url = window.URL.createObjectURL(new Blob([res.data]));

            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', fileName);
            document.body.appendChild(link);
            link.click();
        });
    }
}