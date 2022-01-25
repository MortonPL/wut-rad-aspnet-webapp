import { FunctionComponent } from 'react';
import { Link, Outlet } from 'react-router-dom';
import { PersonFill } from 'react-bootstrap-icons';

type MainLayoutProps = {};

export const MainLayout: FunctionComponent<MainLayoutProps> = () => {

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
                    <li className="nav-item">
                        <Link className="nav-link link-light" to="/user">
                            <PersonFill className="me-2" />
                            User
                        </Link>
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
