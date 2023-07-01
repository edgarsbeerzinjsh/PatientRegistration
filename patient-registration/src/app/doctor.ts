export interface Doctor {
    id?: number;
    name: string;
    surname: string;
    telephone: string;
    eMail: string;
    specialization: string;
    patientsId?: number[];
}
