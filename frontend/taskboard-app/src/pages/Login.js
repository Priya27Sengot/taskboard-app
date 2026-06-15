import { Link, Navigate, useNavigate } from "react-router-dom";
import { login } from "../api/authAPI";
import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext";
export default function Login() {
    const navigate = useNavigate();

    const [usercreds, setUserCreds] = useState({
        email: "",
        password: ""
    })
    const [errors, setErrors] = useState({});
    const { setcurrentUser } = useContext(AuthContext);

    const handleLogin = (e) => {
        e.preventDefault();
        console.log("UserCreds", usercreds);
        let validationErrors = validate();
        if (Object.keys(validationErrors).length > 0) {
            return;
        }
        login(usercreds)
            .then(res => {
                console.log(res.data);
                localStorage.setItem("user", JSON.stringify(res.data.data));
                setcurrentUser(res.data.data);
                setErrors({});
                if (res.data.success) {
                    navigate("/dashboard");
                }
            })
            .catch(err => {
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

    function validate() {
        let err = {};
        if (!usercreds.email.trim()) {
            err.email = "Email is required";
        }
        else if (!/\S+@\S+\.\S+/.test(usercreds.email)) {
            err.email = "Please enter valid email";
        }
        if (!usercreds.password.trim()) {
            err.password = "Password is required";
        }
        setErrors(err);
        return err;
    }


    return (
        <>
            <div className="login-container">

                <form className="login-card" onSubmit={handleLogin}>
                    <h3>Login</h3>
                    <div>
                        {errors.email && <p className="error">{errors.email}</p>}
                        <label htmlFor="username">Username</label>
                        <input id="username" placeholder="UserName" value={usercreds.email} onChange={(e) => setUserCreds({ ...usercreds, email: e.target.value })}></input>
                    </div>
                    <div>
                        {errors.password && <p className="error">{errors.password}</p>}
                        <label htmlFor="password">Password</label>
                        <input id="password" type="password" placeholder="Password" value={usercreds.password} onChange={(e) => setUserCreds({ ...usercreds, password: e.target.value })}></input>
                    </div>
                    <div >
                        <button type="submit">Login</button>
                        <Link className="link" to="/register">Register New</Link><br />
                    </div>

                </form>

            </div>

        </>
    )
}