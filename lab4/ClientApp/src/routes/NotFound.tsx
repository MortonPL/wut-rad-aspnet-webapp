import { FunctionComponent } from 'react';

type NotFoundComponentProps = {};

export const NotFoundComponent: FunctionComponent<NotFoundComponentProps> = () => {
    return (
        <p>
            Page not found!
        </p>
    );
}

export default NotFoundComponent;
