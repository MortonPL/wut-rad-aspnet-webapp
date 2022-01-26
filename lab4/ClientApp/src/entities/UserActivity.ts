export class UserActivity {
    month: Date;
    userName: string;
    pid: number;
    projectId: string;
    date: Date;
    subactivityId: string;
    time: number;
    description: string;

    constructor(month: Date, userName: string, pid: number, projectId: string, date: Date, subactivityId: string, time: number, description: string) {
        this.month = month;
        this.userName = userName;
        this.pid = pid;
        this.projectId = projectId;
        this.date = date;
        this.subactivityId = subactivityId;
        this.time = time;
        this.description = description;
    }

    toJSON() {
        return {
            month: this.month,
            userName: this.userName,
            pid: this.pid,
            projectId: this.projectId,
            date: this.date,
            subactivityId: this.subactivityId,
            time: this.time,
            description: this.description
        };
    }

    static fromJSON(json: any) {
        return new UserActivity(json['month'], json['userName'], json['pid'], json['projectId'], json['date'], json['subactivityId'], json['time'], json['description']);
    }

    static createEmpty() {
        return new UserActivity(new Date(), '', 0, '', new Date(), '', 0, '');
    }

    modify(setter: any, key: string, value: any) {
        setter(UserActivity.fromJSON({...this.toJSON(), [key]:value}));
    }
};

export default UserActivity;
