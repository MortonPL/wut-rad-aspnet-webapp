export class Subactivity {
    subactivityId: string;

    constructor(subactivityId: string) {
        this.subactivityId = subactivityId;
    }

    toJSON() {
        return {
            subactivityId: this.subactivityId,
        };
    }

    static fromJSON(json: any) {
        return new Subactivity(json['subactivityId']);
    }

    static createEmpty() {
        return new Subactivity('');
    }
};

export default Subactivity;
