import { FunctionComponent, useContext, useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

import UserContext from '../Context';
import FetchWrapper from '../FetchWrapper';
import { addToArray, setArray } from '../Helpers';
import User from '../entities/User';

type UserComponentProps = {};

export const UserComponent: FunctionComponent<UserComponentProps> = () => {
    const userState = useContext(UserContext);
    const [luser, setLuser] = useState(User.createEmpty());
    const [ruser, setRuser] = useState(User.createEmpty());
    const [userList, setUserList] = useState([] as User[]);
    const [error, setError] = useState(false);

    // EVENT HANDLERS
    const handleLogout = (event: any) => {
        event.preventDefault();
        FetchWrapper.postUserLogout().then((json: any) => {
            userState.setIsLogged(false);
            userState.setName(null);
            setLuser(new User(""));
            setRuser(new User(""));
        });
    }
    
    const handleLoginSubmit = (event: any) => {
        event.preventDefault();
        if (!userState.state.isLogged && luser && luser.name !== "") {
            FetchWrapper.postUserLogin(luser).then(() => {
                userState.setIsLogged(true);
                userState.setName(luser.name);
                setError(false);
            });
        }
    }

    const handleRegisterSumbit = (event: any) => {
        event.preventDefault();
        if (!userState.state.isLogged && luser) {
            FetchWrapper.putUserRegister(ruser).then(() => {
                FetchWrapper.postUserLogin(ruser).then(() => {
                    addToArray(setUserList, userList, ruser);
                    userState.setIsLogged(true);
                    userState.setName(ruser.name);
                    setError(false);
                });
            }).catch(() => {
                setError(true);
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
                <Form.Label><b>Select your profile from the list:</b></Form.Label>
                <Form.Select className="w-50" onChange={e => {e.target.value !== "Choose profile" ? setLuser(new User(e.target.value)) : setLuser(new User(""))}}>
                    <option key="">Choose profile</option>
                    {userList.map((user_: User) => 
                    <option key={user_.name} value={user_.name}>{user_.name}</option>
                    )}
                </Form.Select>
            </Form.Group>
            <Button type="submit" variant="primary" disabled={luser.name === ""} className="mb-3">
                Log In
            </Button>
        </Form>
    </>) : (<></>);

    let logoutForm = (<>
        <Button type="submit" variant="danger" className="mb-3" onClick={handleLogout}>
            Logout
        </Button>
    </>);

    let registerForm = (<>
        <Form onSubmit={handleRegisterSumbit}>
            <Form.Group className="mb-3" controlId="formRegisterUsername">
                <Form.Label><b>...or create a new profile:</b></Form.Label>
                <Form.Control type="text" className="w-50" placeholder="Enter name" onChange={e => setRuser(new User(e.target.value))} />
            </Form.Group>
            <Button type="submit" variant="primary" className="mb-3">
                Create
            </Button>
        </Form>
    </>)

    let registerError = error ? (<p style={{color:"red"}}>
        This user already exists!
    </p>) : (<p></p>);

    let loginViewIfLogged = (<>
        You are logged in as: <b>{userState.state.name}</b><br/><br/>
        Log out below:<br/>
        {logoutForm}
    </>)

    let loginViewIfNotLogged = (<>
        {loginForm}
        {registerForm}
        {registerError}
    </>);

    let loginView = userState.state.isLogged ? loginViewIfLogged : loginViewIfNotLogged;

    return (<>
        {loginView}
    </>);
}

export default UserComponent;
