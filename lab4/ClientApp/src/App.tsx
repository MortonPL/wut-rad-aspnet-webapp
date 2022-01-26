import { useState, useEffect } from 'react';
import { Routes, Route } from 'react-router-dom';

import UserContext, { UserState, UserStateCtxt, emptyUserState } from './Context';
import FetchWrapper from './FetchWrapper';

import MainLayout from './shared-components/MainLayout';
import NotFoundComponent from './routes/NotFound';
import HomeComponent from './routes/Home';
import UserComponent from './routes/User';
import ActivitiesComponent from './routes/Activities';

function App() {
    const [userState, setUserState] = useState<UserState>(emptyUserState);

    const setIsLogged = (logged: boolean) => setUserState(state => ({...state, isLogged: logged}));
    const setName = (name: string | null) => setUserState(state => ({...state, name: name}))

    const userStateProvider: UserStateCtxt = {
        state: userState,
        setState: setUserState,
        setIsLogged: setIsLogged,
        setName: setName
    };

    useEffect(() => {
        FetchWrapper.getUserLogged().then((json: {name: string}) => {
            if (json['name']) {
                setIsLogged(true);
                setName(json['name']);
            }
        })
    }, []);

    return (
        <UserContext.Provider value={userStateProvider}>
            <Routes>
                <Route path="/" element={<MainLayout />}>
                    <Route path="" element={<ActivitiesComponent />} />
                    <Route path="user" element={<UserComponent />} />
                    <Route path="activities" element={<ActivitiesComponent />} />
                    <Route path="*" element={<NotFoundComponent />} />
                </Route>
            </Routes>
        </UserContext.Provider>
    );
}

export default App;
