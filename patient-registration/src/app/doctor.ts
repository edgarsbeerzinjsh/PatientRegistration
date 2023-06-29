export interface Doctor {
    id?: number;
    name: string;
    surname: string;
    telephone: string;
    email: string;
    specialization: string;
    patientsId?: number[];
}
