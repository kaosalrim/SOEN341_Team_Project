import { Answer } from "./answer";

export interface Question {
    id:              number;
    questionTitle:   string;
    questionDetails: string;
    datePosted:      Date;
    answers:         Answer[];
    username:        string;
    hasBestAnswer:   boolean;
}
