import React from 'react';

export type UserState = {
    isLogged: boolean;
    name: string | null;
}

export const emptyUserState: UserState = {
    isLogged: false,
    name: null
}

export type UserStateCtxt = {
    state: UserState;
    setState: Function;
    setIsLogged: (isLogged: boolean) => void;
    setName: (name: string | null) => void;
};

const emptyUserStateCtxt: UserStateCtxt = {
    state: emptyUserState,
    setState: () => {},
    setIsLogged: () => {},
    setName: () => {}
}

export const UserContext = React.createContext<UserStateCtxt>(emptyUserStateCtxt);

export default UserContext;
