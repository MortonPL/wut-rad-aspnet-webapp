export class ApprovedActivityStats {
    projectId: string;
    time: number;

    constructor(projectId: string, time: number) {
        this.projectId = projectId;
        this.time = time;
    }

    toJSON() {
        return {
            projectId: this.projectId,
            time: this.time
        };
    }

    static fromJSON(json: any) {
        return new ApprovedActivityStats(json['projectId'], json['time']);
    }

    static createEmpty() {
        return new ApprovedActivityStats('', 0);
    }
}

export default ApprovedActivityStats;
