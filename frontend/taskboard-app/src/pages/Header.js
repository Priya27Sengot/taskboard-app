import react from 'react';
import { useEffect,useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
export default function Header()
{      
    const {currentUser,isAuthenticated,logoutUser} = useContext(AuthContext);
    
    return (
        <>
        <header className='App-header'>
        <h3>TaskBoard</h3>
       {isAuthenticated && <div className='App-user'>
        <h5>Welcome , {currentUser.username}</h5>
        <button onClick={logoutUser}>Logout</button>        
       </div>}
        </header>
      
        </>
    )
}