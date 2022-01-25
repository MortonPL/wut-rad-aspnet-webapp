import Subactivity from "./Subactivity";

export class Project {
    projectId: string;
    name: string;
    budget: number;
    managerName: string;
    active: boolean;
    subactivities: Subactivity[];

    constructor(projectId: string, name: string, budget: number, managerName: string, active: boolean, subactivities: Subactivity[]) {
        this.projectId = projectId;
        this.name = name;
        this.budget = budget;
        this.managerName = managerName;
        this.active = active;
        this.subactivities = subactivities;
    }

    toJSON() {
        return {
            projectId: this.projectId,
            name: this.name,
            budget: this.budget,
            managerName: this.managerName,
            active: this.active,
            subactivities: this.subactivities,
        };
    }

    static fromJSON(json: any) {
        return new Project(json['projectId'], json['name'], json['budget'], json['managerName'], json['active'], json['subactivities']);
    }

    static createEmpty() {
        return new Project('', '', 0, '', true, []);
    }
};

export default Project;
