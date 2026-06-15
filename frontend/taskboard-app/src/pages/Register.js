import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { register } from "../api/authAPI";

export default function Register() {
    const navigate = useNavigate();
    const [userRegistartion, setUserRegistration] = useState({
        username: "",
        email: "",
        password: ""
    });
    const [confirmPassword,setConfirmPassword]=useState("");
    const [errors, setErrors] = useState({});

    function validate() {
        let err = {};
        if (!userRegistartion.username.trim()) {
            err.username = "Username is required"
        }
        if (!userRegistartion.email.trim()) {
            err.email = "Email is required";
        }
        else if (!/\S+@\S+\.\S+/.test(userRegistartion.email)) {
            err.email = "Please enter valid email";
        }
        if (!userRegistartion.password.trim()) {
            err.password = "Password is required";
        }
        if(userRegistartion.password.trim() !== confirmPassword.trim())
        {
            err.confirmPassword="Password and Confirm password do not match";
        }
        setErrors(err);
        return err;
    }
    const handleRegister = (e) => {
        e.preventDefault();
        console.log("Register", userRegistartion);
        let validationErrors = validate();
        if (Object.keys(validationErrors).length > 0) {
            return;
        }
        register(userRegistartion)
            .then((res) => {
                console.log(res.data)
                if (res.data.success) {
                    window.alert(res.data.description);
                    navigate("/login");
                }
                else {
                    window.alert(res.data.description);
                }
            })
            .catch((err) => {
                console.log(err)
               if (err.response && err.response.data.description) {
                    window.alert(err.response.data.description)
                }
                else {
                    window.alert("An error occured.Please try again after sometime");
                }
            }
            );
    }
    return (
        <>
            <div className="login-container">

                <div className="login-card">
                    <h3>Register User</h3>

                    <form onSubmit={handleRegister}>
                        <div>
                             {errors.username && <p className="error">{errors.username}</p>}
                            <label htmlFor="Username">Username</label>
                            <input id="Username" placeholder="Username" value={userRegistartion.username} onChange={(e) => {
                                 //validate();
                                 setUserRegistration({ ...userRegistartion, username: e.target.value })
                            }
                           }></input>
                        </div>
                        <div>
                             {errors.email && <p className="error">{errors.email}</p>}
                            <label htmlFor="Email">Email</label>
                            <input id="Email" placeholder="Email" value={userRegistartion.email} onChange={(e) => {
                                //validate();
                                setUserRegistration({ ...userRegistartion, email: e.target.value })
                            }}></input>
                        </div>
                        <div>
                             {errors.password && <p className="error">{errors.password}</p>}
                            <label htmlFor="Password">Password</label>
                            <input id="Password" type="password" placeholder="Password" value={userRegistartion.password} onChange={(e) => {
                                //validate();
                                 setUserRegistration({ ...userRegistartion, password: e.target.value })
                            }
                           }></input>
                        </div>
                        <div>
                             {errors.confirmPassword && <p className="error">{errors.confirmPassword}</p>}
                            <label htmlFor="confirmPassword">Confirm Password</label>
                            <input id="confirmPassword" type="password" placeholder="Confirm Password" value={confirmPassword} onChange={(e) =>{
                                //validate();
                                 setConfirmPassword(e.target.value )
                            }
                            }></input>
                        </div>
                        <button type="submit">Register</button>
                    </form>
                    <p>Already have an account ?
                        <Link className="link" to="/login">Login</Link>
                    </p>

                </div>
            </div>

        </>
    )
}