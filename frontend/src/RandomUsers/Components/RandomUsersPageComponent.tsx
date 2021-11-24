import React, {useState, useEffect} from 'react';
import useLoading from '../../Shared/Hooks/useLoading';
import { IRandomUser, IRandomUserFilter, RandomUserApi } from '../Api/api';
import { RandomUsersFilterComponent } from './RandomUsersFilterComponent';
import { RandomUsersListComponent } from './RandomUsersListComponent';

export function RandomUsersPageComponent() {
    const [items, setItems] = useState<IRandomUser[]>();    
    const [correlation, setCorrelation] = useState<string|undefined>(undefined);
    const [doLoading, isLoading] = useLoading(search);
    const service = new RandomUserApi();

    function search(filter: IRandomUserFilter): Promise<any> {
        return service.getFiltered(filter).then(d => {
            setItems(d.users);
            setCorrelation(d.correlationId);
        })
        .catch(d => console.error('error'));
    }

    function exportToFile(filter: IRandomUserFilter): Promise<any> {
        if (items && correlation) {
            const exportFilter = {
                count: filter.count,
                location:filter.location,
                nationality:filter.nationality,
                correlationId:correlation||''
            };
            return service.exportToFile(exportFilter).then().catch(d => console.error('export error'));
        }
        else return Promise.reject();
    }

    return (
        <div className="">       
            <div className="p-grid">
                <div className="p-col-2">
                    <RandomUsersFilterComponent search={doLoading} exportToFile={exportToFile} items={items}></RandomUsersFilterComponent>
                </div>
                <div className="p-col-10">
                    <RandomUsersListComponent items={items} isLoading={isLoading}></RandomUsersListComponent>
                </div>
            </div>  
        </div>
    )
}