import { createContext, useState } from "react";
import { useNavigate } from "react-router-dom";

export const AuthContext = createContext(null);

export function AuthProvider({children})
{
 const navigate=useNavigate();    
    const[currentUser,setcurrentUser]= useState(()=>{
        return JSON.parse(localStorage.getItem("user")) }
    )

    const token =currentUser ? currentUser.token :"";
    const isAuthenticated = currentUser && currentUser.token? true :false;
    
    function logoutUser()
    {
     localStorage.removeItem("user");
     setcurrentUser(null);
     navigate("/login"); 
    }

    return(
        <AuthContext.Provider value={{currentUser, logoutUser,isAuthenticated,token,setcurrentUser}}>
            {children}
        </AuthContext.Provider>
    )
}