import { useState, useEffect } from 'react';
import { Routes, Route } from 'react-router-dom';

import UserContext, { UserState, UserStateCtxt, emptyUserState } from './Context';
import FetchWrapper from './FetchWrapper';

import MainLayout from './shared-components/MainLayout';
import NotFound from './routes/NotFound';
import Home from './routes/home/Home';
import User from './routes/user/User';

function App() {
    const [userState, setUserState] = useState<UserState>(emptyUserState);

    const userStateProvider: UserStateCtxt = {
        state: userState,
        setState: setUserState,
        setIsLogged: (logged: boolean) => setUserState(state => ({...state, isLogged: logged})),
        setName: (name: string | null) => setUserState(state => ({...state, name: name}))
    };

    useEffect(() => {
        FetchWrapper.getUserLogged().then((json: {name: string}) => {
            if (json['name']) {
                userStateProvider.setIsLogged(true);
                userStateProvider.setName(json['name']);
            }
        })
    }, []);

    return (
        <UserContext.Provider value={userStateProvider}>
            <Routes>
                <Route path="/" element={<MainLayout />}>
                    <Route path="" element={<Home />} />
                    <Route path="user" element={<User />} />
                    <Route path="*" element={<NotFound />} />
                </Route>
            </Routes>
        </UserContext.Provider>
    );
}

export default App;
