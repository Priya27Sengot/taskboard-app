import axios from 'axios';
import {baseAPI} from './axiosClient';

export const login=(loginuser)=>{
    try{
   return axios.post(baseAPI+"/Auth/Login",loginuser);
    }
    catch(error)
    {
window.alert(error);
    }

}

export const register=(user)=>{
    return axios.post(baseAPI+"/Auth/Register",user);
}