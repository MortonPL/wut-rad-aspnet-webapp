import { FunctionComponent } from 'react';
import FetchWrapper from '../../FetchWrapper';


type UserProps = {};

export const User: FunctionComponent<UserProps> = () => {

    FetchWrapper.postUserLogin("balbinka").then();

    return (
        <p>
            Balbinka
        </p>
    );
}

export default User;
