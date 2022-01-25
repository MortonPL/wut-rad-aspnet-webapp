
export default class FetchWrapper {
    static getUserLogged = () => FetchWrapper._fetch('user/me/', 'GET');

    static postUserLogin = (user: string) => FetchWrapper._fetch(`user/login?user=${user}`, 'POST');


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