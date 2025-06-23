import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { registerUser } from "../services/apiService";

export default function RegisterPage() {
  const [name, setName] = useState("");
  const [phone, setPhone] = useState("");
  const [error, setError] = useState(null);

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);
    try {
      const userData = { name, phone };
      const registeredUser = await registerUser(userData);

      // שומר את המשתמש בלוקאל סטורג'
      localStorage.setItem("user", JSON.stringify(registeredUser));

      // מנווט לפי השם
      if (registeredUser.name.toLowerCase() === "admin") {
        navigate("/admin");
      } else {
        navigate("/dashboard");
      }
    } catch (err) {
      setError(err.message || "Registration failed.");
    }
  };

  return (
    <div className="register-page">
      <h2>הרשמה</h2>
      <form onSubmit={handleSubmit}>
        <label>
          שם:
          <input
            type="text"
            required
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </label>

        <label>
          טלפון:
          <input
            type="tel"
            required
            value={phone}
            onChange={(e) => setPhone(e.target.value)}
          />
        </label>

        {error && <p style={{ color: "red" }}>{error}</p>}

        <button type="submit">הרשם</button>
      </form>

      <p>
        כבר רשום? <Link to="/login">התחבר</Link>
      </p>
    </div>
  );
}
