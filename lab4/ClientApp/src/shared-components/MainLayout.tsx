import { FunctionComponent, useContext } from 'react';
import { Link, Outlet } from 'react-router-dom';
import { PersonFill } from 'react-bootstrap-icons';

import UserContext from '../Context';

type MainLayoutProps = {};

export const MainLayout: FunctionComponent<MainLayoutProps> = () => {
    const userState = useContext(UserContext);

    return (
    <div id="container" className="container-fluid vh-100 p-0 d-flex flex-column">
        <header className="navbar navbar-expand navbar-dark bg-dark px-3">
            <Link className="navbar-brand mb-2" to="/">NTR Lab 4</Link>
            <div className="d-flex flex-row-reverse w-100 mb-2">
            </div>
        </header>
        <div className="flex-grow-1 d-flex" style={{ minHeight: 0 }}>
            <nav className="bg-secondary" style={{ minWidth: '240px', maxWidth: '240px' }}>
                <ul className="nav nav-pills flex-column mb-auto">
                    <li className="nav-item mt-3">
                        <Link className="nav-link link-light" to="/user"><h5>
                            <PersonFill className="me-2" />
                            {userState.state.isLogged ? <>User ({userState.state.name})</> : <>User</>}
                        </h5></Link>
                    </li>
                </ul>
            </nav>
            <main className="flex-grow-1">
                <div className='p-5'>
                    <Outlet />
                </div>
            </main>
        </div>
    </div>
    )
}

export default MainLayout;
