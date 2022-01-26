export class User {
    name: string;

    constructor(name: string) {
        this.name = name;
    }

    toJSON() {
        return {
            name: this.name,
        };
    }

    static fromJSON(json: any) {
        return new User(json['name']);
    }

    static createEmpty() {
        return new User('');
    }
};

export default User;
