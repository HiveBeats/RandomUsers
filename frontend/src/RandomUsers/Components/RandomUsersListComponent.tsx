import React from 'react';
import Spinner from '../../Shared/Components/Spinner/Spinner';
import { IRandomUser } from '../Api/api';


type PropType = {
    items: IRandomUser[]|undefined;
    isLoading: boolean;
}

export function RandomUsersListComponent(props: PropType) {
    
    if (props.isLoading) {
        return (
            <React.Fragment>
                <Spinner/>
            </React.Fragment>
        )
    }
    else {
        return (
            <React.Fragment>
                <table>
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Full Name</th>
                            <th>Location</th>
                            <th>Nationality</th>
                        </tr>
                    </thead>
                    <tbody>
                        {props.items && props.items.map((item: IRandomUser, index: number) => (
                            <tr className="" key={`listItem-${index}`}>
                                <td>
                                    <img src={item.picture} alt={item.fullName}></img>
                                </td>
                                <td>{item.fullName}</td>
                                <td>{item.location}</td>
                                <td>{item.nationality}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>                    
            </React.Fragment>
        )
    }
}