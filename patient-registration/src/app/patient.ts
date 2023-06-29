export interface Patient {
    id?: number;
    name: string;
    surname: string;
    telephone: string;
    email: string;
    birthDate: Date;
    doctorsId?: number[];
}
