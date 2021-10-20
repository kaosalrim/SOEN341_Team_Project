export interface Answer {
    id:            number;
    rank:          number;
    isBestAnswer:  boolean;
    answerDetails: string;
    datePosted:    Date;
    questionId:    number;
    username:      string;
}
