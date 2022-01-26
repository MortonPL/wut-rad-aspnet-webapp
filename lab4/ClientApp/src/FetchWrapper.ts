import User from "./entities/User";
import UserActivity from "./entities/UserActivity";
import { dateToString } from "./Helpers";

export default class FetchWrapper {
    static getUserLogged = () => FetchWrapper._fetch(`user/me/`, 'GET');
    static getUsers = () => FetchWrapper._fetch(`user/all/`, 'GET');
    static getUserMonth = (user: string, month: Date) => FetchWrapper._fetch(`useractivity/one/?user=${user}&month=${dateToString(month)}`, 'GET');
    static getProjects = () => FetchWrapper._fetch(`project/all/`, 'GET');

    static postUserLogin = (user: User) => FetchWrapper._fetch(`user/login?user=${user.name}`, 'POST');
    static postUserLogout = () => FetchWrapper._fetch(`user/logout`, 'POST');

    static patchUA = (ua: UserActivity) => FetchWrapper._fetch(`useractivity/edit/`, 'PATCH', ua);

    static putUserRegister = (user: User) => FetchWrapper._fetch(`user/create?user=${user.name}`, 'PUT');

    static deleteUA = (ua: UserActivity) => FetchWrapper._fetch(`useractivity/delete/`, 'DELETE', ua);


    private static _fetch = (url: string, method: string = 'GET', payload?: {}) => {
        const request: RequestInit = { method: method };

        if (payload !== undefined) {
            request.headers = {'Content-Type': 'application/json'};
            request.body = JSON.stringify(payload);
        }

        return fetch(`api/${url}`, request).then(async response => {
            const contentType = response.headers.get('content-type');
            const body = contentType?.includes('application/json') || contentType?.includes('application/problem+json') ? await response.json() : null;
            if (!response.ok) {
                let badBody = response.statusText;
                if (body && body.errors)
                    badBody = body.errors;
                return Promise.reject(badBody);
            }
            return Promise.resolve(body);
        });
    }
}
