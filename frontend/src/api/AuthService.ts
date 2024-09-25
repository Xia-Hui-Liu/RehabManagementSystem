import axios from "axios";

const API_URL = 'http://localhost:5007/api';

export const userLogin = async(email: string, password: string): Promise<any> => {
    try {
        const response = await axios.post(`${API_URL}/auth/login`, {
            email,
            password
        });
        console.log(response.data);
        return response.data;
    } catch (error : any) {
        if (axios.isAxiosError(error) && error.response)
        {
            console.error('Server Error:', error.response.data);
            throw new Error(error.response.data.message || 'An error occurred');
        } else {
            console.error('Client Error:', error.message);
            throw new Error('An error occurred while sending the request');
        }
        
    }
}

export const userRegister = async (email: string, password: string, role: string): Promise<any> => {
    try {
        const response = await axios.post(`${API_URL}/auth/register`, {
            email,
            password,
            role
        }, {
            // Include credentials (cookies) in the request
            withCredentials: true
        });
        return response.data;
    } catch (error: any) {
        if (axios.isAxiosError(error) && error.response) {
            console.error('Server Error:', error.response.data);
            throw new Error(error.response.data.message || 'An error occurred');
        } else {
            // Handle client-side or network errors
            console.error('Client Error:', error.message);
            throw new Error('An error occurred while sending the request');
        }
    }
}

