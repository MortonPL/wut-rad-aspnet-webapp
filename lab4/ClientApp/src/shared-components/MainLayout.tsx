import { FunctionComponent, useContext } from 'react';
import { Link, Outlet } from 'react-router-dom';
import { PersonFill, PencilSquare, ClipboardData } from 'react-bootstrap-icons';

import UserContext from '../Context';

type MainLayoutProps = {};

export const MainLayout: FunctionComponent<MainLayoutProps> = () => {
    const userState = useContext(UserContext);

    let disableable = (userState.state.isLogged ? "": " disabled")

    return (<div>
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark px-3">
        <Link className="navbar-brand mb-2" to="/">NTR Lab 4</Link>
        <div className="collapse navbar-collapse">
            <ul className="navbar-nav">
                <li className="nav-item mr-3"><h5>
                    <Link className="nav-link link-light" to="/user">
                        <PersonFill className="me-2" />
                        {userState.state.isLogged ? <>User ({userState.state.name})</> : <>User</>}
                    </Link>
                </h5></li>
                <li className="nav-item mr-3"><h5>
                    <Link className={"nav-link link-light" + disableable} to="/activities">
                        <PencilSquare className="me-2" />
                        Activities
                    </Link>
                </h5></li>
                <li className="nav-item mr-3"><h5>
                    <Link className={"nav-link link-light" + disableable} to="/projects">
                        <ClipboardData className="me-2" />
                        Projects
                    </Link>
                </h5></li>
            </ul>
        </div>
    </nav>
    <div className="flex-grow-1 d-flex" style={{ minHeight: 0 }}>
        <main className="flex-grow-1">
            <div className='p-5'>
                <Outlet />
            </div>
        </main>
    </div>
    </div>)
}

export default MainLayout;
