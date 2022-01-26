import { FunctionComponent, useContext, useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Table from 'react-bootstrap/Table';

import UserContext from '../Context';
import FetchWrapper from '../FetchWrapper';
import { gmodifyInArray, setArray, gremoveFromArray, dateToString } from '../Helpers';
import UserMonth from '../entities/UserMonth';
import UserActivity from '../entities/UserActivity';
import Project from '../entities/Project';
import Subactivity from '../entities/Subactivity';

type ActivitiesComponentProps = {};

export const ActivitiesComponent: FunctionComponent<ActivitiesComponentProps> = () => {
    const userState = useContext(UserContext);
    const [ua, setUa] = useState(UserActivity.createEmpty());
    const [umonth, setUmonth] = useState(UserMonth.createEmpty());
    const [projectList, setProjectList] = useState([] as Project[]);
    const [project, setProject] = useState(Project.createEmpty());
    const [date, setDate] = useState(dateToString(new Date()));
    const [editId, setEditId] = useState(0);

    // EVENT HANDLERS
    const handleCreate = (event: any) => {
        event.preventDefault();
        if (userState.state.isLogged && userState.state.name) {
            ua.month = umonth.month;
            ua.userName = userState.state.name;
            ua.date = new Date();
            FetchWrapper.putUA(ua).then(() => {
                getUserMonth();
            })
        }
    };

    const handleEdit = (event: any) => {
        event.preventDefault();
        if (userState.state.isLogged) {
            FetchWrapper.patchUA(ua).then(() => {
                const newUas = gmodifyInArray(umonth.userActivities, ua, 'pid');
                umonth.modify(setUmonth, 'userActivities', newUas);
                setEditId(-1);
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
            modifyWrap(setUa, umonth.userActivities[i], 'projectId', umonth.userActivities[i].projectId);
        }
    }

    useEffect(getUserMonth, [userState.state.isLogged, userState.state.name]);
    useEffect(getProjectList, [userState.state.isLogged]);
    useEffect(preloadEditedUA, [editId, umonth]);

    // OTHER FUNCTIONS
    const isMutable = (ua: UserActivity) => {
        return userState.state.isLogged && !umonth.frozen; // && check if project is active
    };

    const modifyWrap = (setter: Function, ua: UserActivity, key: string, value: any) => {
        if (value !== "")
        {
            let i = projectList.findIndex((other: any) => value === other.projectId);
            ua.modify(setter, key, value);
            setProject(projectList[i]);
        }
        else
        {
            setProject(Project.createEmpty());
        }
    }

    // JSX ELEMENTS

    let total = 0;
    umonth.userActivities.filter((ua_: UserActivity) => date === dateToString(new Date(ua_.date.toString()))).forEach((e, i, a) => {
        total += e.time;
    });

    let header = (<div className="pb-5">
        <div className="pb-2">
            <h3>
            <thead>
                <tr><th style={{width: "300px"}}></th></tr>
            </thead>
            <tbody>
                <tr>
                    <td>Current date:</td><td>{date}</td>
                </tr>
                <tr>
                    <td>Total time spent:</td><td>{total}</td>
                </tr>
                <tr>
                    <td>Status:</td><td>{umonth.frozen ? <span style={{color:"red"}}>Frozen</span> : <span style={{color:"green"}}>active</span>}</td>
                </tr>
            </tbody>
            </h3>
        </div>
        Select date: <input type="date" onChange={e => setDate(e.target.value)} />
    </div>);

    let aEditable = (ua: UserActivity) => {
        return ua.pid === editId ?
        <>
            <td>
                <Form.Select className="w-100" onChange={e => {e.target.value !== "Choose subactivity" ? ua.modify(setUa, 'subactivityId', e.target.value) : ua.modify(setUa, 'subactivityId', "")}}>
                    <option key="">Choose subactivity</option>
                    {project.subactivities.map((sub_: Subactivity) => 
                    <option key={sub_.subactivityId} value={sub_.subactivityId}>{sub_.subactivityId}</option>
                    )}
                </Form.Select>
            </td>
            <td><input type="text" defaultValue={ua.time} placeholder={ua.time.toString()} onChange={e => ua.modify(setUa, 'time', parseInt(e.target.value))}/></td>
            <td><input type="text" defaultValue={ua.description} placeholder={ua.description} onChange={e => ua.modify(setUa, 'description', e.target.value)}/></td>
        </> : <>
            <td>{ua.subactivityId}</td>
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
                {umonth.userActivities.filter((ua_: UserActivity) => date === dateToString(new Date(ua_.date.toString()))).map((ua_: UserActivity) => <tr key={ua_.pid}>
                    <>
                        <td>{ua_.date}</td>
                        <td>{ua_.projectId}</td>
                        <td>{findProjectName(ua_)}</td>
                        {aEditable(ua_)}
                        <td>
                            {aEditableButtons(ua_)}
                        </td>
                    </>
                </tr>)}
            </tbody>
        </Table>
    </>)

    let aForm = (<>
        <Form onSubmit={handleCreate}>
            <Form.Group className="mb-3" controlId="formProjectSelect">
                <Form.Label><b>Choose the project:</b></Form.Label>
                <Form.Select className="w-50" onChange={e => {e.target.value !== "Choose project" ? modifyWrap(setUa, ua, 'projectId', e.target.value) : modifyWrap(setUa, ua, 'projectId', "")}}>
                    <option key="">Choose project</option>
                    {projectList.filter((project_: Project) => project_.active).map((project_: Project) => 
                    <option key={project_.projectId} value={project_.projectId}>{project_.name}</option>
                    )}
                </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formSubactivitySelect">
                <Form.Label><b>Choose the subactivity:</b></Form.Label>
                <Form.Select className="w-50" onChange={e => {e.target.value !== "Choose subactivity" ? ua.modify(setUa, 'subactivityId', e.target.value) : ua.modify(setUa, 'subactivityId', "")}}>
                    <option key="">Choose subactivity</option>
                    {project.subactivities.map((sub_: Subactivity) => 
                    <option key={sub_.subactivityId} value={sub_.subactivityId}>{sub_.subactivityId}</option>
                    )}
                </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formTime">
                <Form.Label><b>Enter time spent:</b></Form.Label>
                <Form.Control type="number" className="w-50" onChange={e => ua.modify(setUa, 'time', parseInt(e.target.value))} />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formTime">
                <Form.Label><b>Enter description</b></Form.Label>
                <Form.Control type="text" className="w-50" placeholder="Description goes here..." onChange={e => ua.modify(setUa, 'description', e.target.value)} />
            </Form.Group>
            <Button type="submit" variant="primary" disabled={ua.projectId === "" || ua.subactivityId === ""} className="mb-3">
                Create
            </Button>
        </Form>
    </>)

    let aViewIfLogged = <>{header} {aTable} {aForm}</>;

    let aViewIfNotLogged = <>Log in to view your activities!</>;

    let aView = userState.state.isLogged ? aViewIfLogged : aViewIfNotLogged;

    return (<>
        {aView}
    </>);
}

export default ActivitiesComponent;
