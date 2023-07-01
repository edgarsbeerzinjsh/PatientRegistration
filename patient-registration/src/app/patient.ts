export interface Patient {
    id?: number;
    name: string;
    surname: string;
    telephone: string;
    eMail: string;
    birthDate: Date;
    doctorsId?: number[];
}
