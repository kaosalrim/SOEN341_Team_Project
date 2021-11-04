import { MemberSince } from "./membersince";
import { Question } from "./question";
import { UserVotes } from "./uservotes";

export interface Member {
    username:    string;
    token:       null;
    memberSince: MemberSince;
    lastActive:  Date;
    firstName:   string;
    lastName:    string;
    photoUrl:    string;
    email:       string;
    questionsAnswered: number;
    questionsPosted: number;
    questions:   Question[];
    userVotes:   UserVotes[];
}


