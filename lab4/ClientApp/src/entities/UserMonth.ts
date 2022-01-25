import ApprovedActivity from "./ApprovedActivity";
import UserActivity from "./UserActivity";

export class UserMonth {
    month: Date
    userName: string;
    frozen: boolean;
    userActivities: UserActivity[];
    approvedActivities: ApprovedActivity[];

    constructor(month: Date, userName: string, frozen: boolean, userActivities: UserActivity[], approvedActivities: ApprovedActivity[]) {
        this.month = month;
        this.userName = userName;
        this.frozen = frozen;
        this.userActivities = userActivities;
        this.approvedActivities = approvedActivities;
    }

    toJSON() {
        return {
            month: this.month,
            userName: this.userName,
            frozen: this.frozen,
            userActivities: this.userActivities,
            approvedActivities: this.approvedActivities
        };
    }

    static fromJSON(json: any) {
        return new UserMonth(json['month'], json['userName'], json['frozen'], json['userActivities'], json['approvedActivities']);
    }

    static createEmpty() {
        return new UserMonth(new Date(), '', false, [], []);
    }
};

export default UserMonth;
