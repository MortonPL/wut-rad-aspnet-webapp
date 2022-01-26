import { FunctionComponent, useContext, useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Table from 'react-bootstrap/Table';

import UserContext from '../Context';
import FetchWrapper from '../FetchWrapper';
import { gmodifyInArray, setArray, gremoveFromArray } from '../Helpers';
import UserMonth from '../entities/UserMonth';
import UserActivity from '../entities/UserActivity';
import Project from '../entities/Project';

type ActivitiesComponentProps = {};

export const ActivitiesComponent: FunctionComponent<ActivitiesComponentProps> = () => {
    const userState = useContext(UserContext);
    const [ua, setUa] = useState(UserActivity.createEmpty());
    const [umonth, setUmonth] = useState(UserMonth.createEmpty());
    const [projectList, setProjectList] = useState([] as Project[]);
    const [isMonthly, setIsMonthly] = useState(false);
    const [editId, setEditId] = useState(0);

    // EVENT HANDLERS
    const handleEdit = () => {
        if (userState.state.isLogged) {
            FetchWrapper.patchUA(ua).then(() => {
                const newUas = gmodifyInArray(umonth.userActivities, ua, 'pid');
                umonth.modify(setUmonth, 'userActivities', newUas);
            })
        }
    };

    const handleDelete = (ua: UserActivity) => {
        if (userState.state.isLogged) {
            FetchWrapper.deleteUA(ua).then(() => {
                const newUas = gremoveFromArray(umonth.userActivities, ua, 'pid');
                umonth.modify(setUmonth, 'userActivities', newUas);
            })
        }
    };

    // EFFECT HOOKS METHODS
    const getUserMonth = () => {
        if (userState.state.isLogged && userState.state.name) {
            FetchWrapper.getUserMonth(userState.state.name, new Date()).then((json: any) => {
                setUmonth(UserMonth.fromJSON(json));
            });
        }
    };

    const getProjectList = () => {
        if (userState.state.isLogged) {
            FetchWrapper.getProjects().then((jsonArray: []) => {
                if (jsonArray.length > 0) {
                    setArray(setProjectList, jsonArray.map(json => Project.fromJSON(json)));
                }
            });
        }
    };

    const preloadEditedUA = () => {
        if (editId > 0) {
            const i = umonth.userActivities.findIndex((ua: UserActivity) => editId === ua['pid']);
            setUa(UserActivity.fromJSON(umonth.userActivities[i]));
        }
    }

    useEffect(getUserMonth, [userState.state.isLogged, userState.state.name]);
    useEffect(getProjectList, [userState.state.isLogged]);
    useEffect(preloadEditedUA, [editId, umonth]);

    // OTHER FUNCTIONS
    const isMutable = (ua: UserActivity) => {
        return userState.state.isLogged && !umonth.frozen; // && check if project is active
    };

    let aEditable = (ua: UserActivity) => {
        return ua.pid === editId ?
        <>
            <td><input type="text" defaultValue={ua.time} placeholder={ua.time.toString()} onChange={e => ua.modify(setUa, 'time', parseInt(e.target.value))}/></td>
            <td><input type="text" defaultValue={ua.description} placeholder={ua.description} onChange={e => ua.modify(setUa, 'description', e.target.value)}/></td>
        </> : <>
            <td>{ua.time}</td>
            <td>{ua.description}</td>
        </>
    };

    let aEditableButtons = (ua: UserActivity) => {
        return isMutable(ua) ?
        <>
            { ua.pid === editId ?
            <>
                <Button type="submit" variant="success" onClick={handleEdit}>Confirm</Button>
                <Button variant="danger" onClick={() => setEditId(-1)}>Cancel</Button>
            </> : <>
                <Button variant="warning" onClick={() => setEditId(ua.pid)}>Edit</Button>
                <Button variant="danger" onClick={() => handleDelete(ua)}>Delete</Button>
            </>
            }
        </> : <></>
    };

    let findProjectName = (ua: UserActivity) => {
        let i = projectList.findIndex((other: any) => ua.projectId === other.projectId);
        if (i > -1) {
            return projectList[i].name;
        } else {
            return "";
        }
    }

    // JSX ELEMENTS
    let aTable = (<>
                <Table striped bordered hover>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Project ID</th>
                    <th>Project Name</th>
                    <th>Subactivity</th>
                    <th>Time spent</th>
                    <th>Description</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {umonth.userActivities.map((ua_: UserActivity) => <tr key={ua_.pid}>
                    <>
                        <td>{ua_.date}</td>
                        <td>{ua_.projectId}</td>
                        <td>{findProjectName(ua_)}</td>
                        <td>{ua_.subactivityId}</td>
                        {aEditable(ua_)}
                        <td>
                            {aEditableButtons(ua_)}
                        </td>
                    </>
                </tr>)}
            </tbody>
        </Table>
    </>)

    let aViewIfLogged = <>{aTable}</>;

    let aViewIfNotLogged = <>Log in to view your activities!</>;

    let aView = userState.state.isLogged ? aViewIfLogged : aViewIfNotLogged;

    return (<>
        {aView}
    </>);
}

export default ActivitiesComponent;
