export interface UserForRegister {
    email: string;
    password: string;
}

export interface UserForLogin {
    email: string;
    password: string;
    token:string;
}

export interface User{
    id: number;
    email: string;
    password: string;
    token:string;
}

