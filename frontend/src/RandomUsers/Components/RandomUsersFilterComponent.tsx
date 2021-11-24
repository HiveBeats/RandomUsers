import { InputText } from 'primereact/inputtext';
import React, {useState} from 'react';
import { IRandomUserFilter } from '../Api/api';

type PropType = {
    search:(filter: IRandomUserFilter) => void
}

export function RandomUsersFilterComponent(props: PropType) {
    const [number, setCount] = useState<number|undefined>(undefined);
    const [location, setLocation] = useState<string|undefined>(undefined);
    const [nationality, setNationality] = useState<string|undefined>(undefined);
    

    const onSubmit = (e: any) => {
        e.preventDefault();
        const filter = {
            count:number,
            location:location,
            nationality:nationality
        };
        props.search(filter);
    }

    return (
        <form onSubmit={onSubmit}>
            <div className="form-group">
                <label htmlFor="locationId" className="">Location</label>
                <InputText id="locationId" value={location} onChange={(e:any) => setLocation(e.target.value)} placeholder="Provide Country Name" 
                            style={{width:'100%'}}/>
            </div>
            <div className="form-group">
                <label htmlFor="nationalityId" className="">Nationality</label>
                <InputText id="nationalityId" value={nationality} onChange={(e:any) => setNationality(e.target.value)} placeholder="Provide Nationality" 
                            style={{width:'100%'}}/>
            </div>
            
            <div className="text-right">
                <button type="submit" className="btn btn-primary">Search</button>
            </div>
        </form>
    )
}