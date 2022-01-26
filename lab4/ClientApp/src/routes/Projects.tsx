import { FunctionComponent, useContext, useState, useEffect } from 'react';
import Table from 'react-bootstrap/Table';

import UserContext from '../Context';
import FetchWrapper from '../FetchWrapper';
import { setArray } from '../Helpers';
import Project from '../entities/Project';
import ApprovedActivityStats from '../entities/ApprovedActivityStats';

type ProjectsComponentProps = {};

export const ProjectsComponent: FunctionComponent<ProjectsComponentProps> = () => {
    const userState = useContext(UserContext);
    const [stats, setStatsList] = useState([] as ApprovedActivityStats[]);
    const [projectList, setProjectList] = useState([] as Project[]);

    // EFFECT HOOKS METHODS
    const getStatsList = () => {
        if (userState.state.isLogged && userState.state.name) {
            FetchWrapper.getUserStats(userState.state.name).then((jsonArray: []) => {
                if (jsonArray.length > 0) {
                    setArray(setStatsList, jsonArray.map(json => ApprovedActivityStats.fromJSON(json)));
                }
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

    useEffect(getStatsList, [userState.state.isLogged, userState.state.name]);
    useEffect(getProjectList, [userState.state.isLogged, userState.state.name]);

    // JSX ELEMENTS

    let total = 0;
    stats.forEach((e: ApprovedActivityStats) => {
        total += e.time;
    });

    let header = (<div className="pb-5">
        <table>
            <thead>
                <tr><th style={{width: "300px"}}></th></tr>
            </thead>
            <tbody>
                <tr>
                    <td><h3>Total time approved:{total}</h3></td>
                </tr>
            </tbody>
        </table>
        
    </div>);

    let findProjectName = (projectId: string) => {
        let i = projectList.findIndex((other: any) => projectId === other.projectId);
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
                    <th>Project ID</th>
                    <th>Project Name</th>
                    <th>Approved time</th>
                </tr>
            </thead>
            <tbody>
                {stats.map((stat: ApprovedActivityStats) => <tr key={stat.projectId}>
                    <>
                        <td><b>{stat.projectId}</b></td>
                        <td>{findProjectName(stat.projectId)}</td>
                        <td>{stat.time}</td>
                    </>
                </tr>)}
            </tbody>
        </Table>
    </>)

    let aViewIfLogged = <>{header} {aTable}</>;

    let aViewIfNotLogged = <>Log in to view your project statistics!</>;

    let aView = userState.state.isLogged ? aViewIfLogged : aViewIfNotLogged;

    return (<>
        {aView}
    </>);
}

export default ProjectsComponent;
