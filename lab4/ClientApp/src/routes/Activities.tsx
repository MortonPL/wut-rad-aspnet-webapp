import { FunctionComponent, useContext, useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

import UserContext from '../Context';
import FetchWrapper from '../FetchWrapper';
import { addToArray, setArray } from '../Helpers';
import UserMonth from '../entities/UserMonth';

type ActivitiesComponentProps = {};

export const ActivitiesComponent: FunctionComponent<ActivitiesComponentProps> = () => {
    const userState = useContext(UserContext);
    const [umonth, setUmonth] = useState(UserMonth.createEmpty());

    let aViewIfLogged = <>Nice!</>;

    let aViewIfNotLogged = <>Log in to view your activities!</>;

    let aView = userState.state.isLogged ? aViewIfLogged : aViewIfNotLogged;

    return (<>
        {aView}
    </>);
}

export default ActivitiesComponent;
