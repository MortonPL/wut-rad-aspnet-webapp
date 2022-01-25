export class ApprovedActivity {
    month: Date;
    userName: string;
    projectId: string;
    time: number;

    constructor(month: Date, userName: string, projectId: string, time: number) {
        this.month = month;
        this.userName = userName;
        this.projectId = projectId;
        this.time = time;
    }

    toJSON() {
        return {
            month: this.month,
            userName: this.userName,
            projectId: this.projectId,
            time: this.time
        };
    }

    static fromJSON(json: any) {
        return new ApprovedActivity(json['month'], json['userName'], json['projectId'], json['time']);
    }

    static createEmpty() {
        return new ApprovedActivity(new Date(), '', '', 0);
    }
};

export default ApprovedActivity;
