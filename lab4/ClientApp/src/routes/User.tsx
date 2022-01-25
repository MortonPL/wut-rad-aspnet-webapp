import { FunctionComponent, useContext, useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

import UserContext from '../Context';
import FetchWrapper from '../FetchWrapper';
import { setArray } from '../Helpers';
import User from '../entities/User';

type UserComponentProps = {};

export const UserComponent: FunctionComponent<UserComponentProps> = () => {
    const userState = useContext(UserContext);
    const [user, setUser] = useState(User.createEmpty());
    const [userList, setUserList] = useState([] as User[]);

    // EVENT HANDLERS
    const handleLogout = () => {
        FetchWrapper.postUserLogout().then((json: any) => {
            userState.setIsLogged(false);
            userState.setName(null);
        });
    }
    
    const handleLoginSubmit = (event: any) => {
        event.preventDefault();
        if (!userState.state.isLogged && user) {
            FetchWrapper.postUserLogin(user).then(() => {
                userState.setIsLogged(true);
                userState.setName(user.name);
            });
        }
    }

    // EFFECT HOOK METHODS
    const getUsers = () => {
        if (!userState.state.isLogged) {
            FetchWrapper.getUsers().then((jsonArray: []) => {
                if (jsonArray.length > 0) {
                    setArray(setUserList, jsonArray.map(json => User.fromJSON(json)));
                }
            });
        }
    };

    useEffect(getUsers, [userState.state.isLogged]);

    // JSX ELEMENTS
    let loginForm = !userState.state.isLogged ? (<>
        <Form onSubmit={handleLoginSubmit}>
            <Form.Group className="mb-3" controlId="formLoginUsername">
                <Form.Label>Select your profile from the list:</Form.Label>
                <Form.Select className="w-50" onChange={e => setUser(new User(e.target.value))}>
                    {userList.map((user_: User) => 
                    <option key={user_.name} value={user_.name}>{user_.name}</option>
                    )}
                </Form.Select>
            </Form.Group>
            <Button type="submit" variant="primary" className="mb-3">
                Log In
            </Button>
        </Form>
    </>) : (<></>);

    let logoutForm = (<>
        <Button type="submit" variant="danger" className="mb-3" onClick={handleLogout}>
            Logout
        </Button>
    </>);

    let loginViewIfLogged = (<>
        You are logged in as: <b>{userState.state.name}</b><br/><br/>
        Log out below:<br/>
        {logoutForm}
    </>)

    let loginViewIfNotLogged = loginForm;

    let loginView = userState.state.isLogged ? loginViewIfLogged : loginViewIfNotLogged;

    return (<>
        {loginView}
    </>);
}

export default UserComponent;
