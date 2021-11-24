import React, {useState, useEffect} from 'react';
import useLoading from '../../Shared/Hooks/useLoading';
import { IRandomUser, IRandomUserFilter, RandomUserApi } from '../Api/api';
import { RandomUsersFilterComponent } from './RandomUsersFilterComponent';
import { RandomUsersListComponent } from './RandomUsersListComponent';
import { CSVLink } from 'react-csv';
export function RandomUsersPageComponent() {
    const [items, setItems] = useState<IRandomUser[]>();
    const [doLoading, isLoading] = useLoading(search);
    const service = new RandomUserApi();

    function search(filter: IRandomUserFilter): Promise<any> {
        return service.getFiltered(filter).then(d => {
            setItems(d.users);
        })
        .catch(d => console.error('error'));
    }

    return (
        <div className="">
            <div className="" style={{textAlign:"left"}}>
                {items && <CSVLink data={items} filename={"export.csv"} className="btn btn-success" style={{textAlign:"left"}} target="_blank">Download CSV</CSVLink>}
            </div>
            
            <div className="p-grid">
                <div className="p-col-2">
                    <RandomUsersFilterComponent search={search}></RandomUsersFilterComponent>
                </div>
                <div className="p-col-10">
                    <RandomUsersListComponent items={items} isLoading={isLoading}></RandomUsersListComponent>
                </div>
            </div>  
        </div>
    )
}