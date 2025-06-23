import { useEffect, useState } from "react";
import { useNavigate, Link } from "react-router-dom"; // ← הוספת Link
import "./DashboardPage.css";

export default function DashboardPage() {
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    } else {
      navigate("/login");
    }
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem("user");
    navigate("/login");
  };

  return (
    <div className="dashboard-container">
      <h2>Welcome, {user?.name}</h2>
      <button onClick={handleLogout}>Logout</button>
      <p><strong>ID:</strong> {user?.id}</p>
      <p><strong>Full Name:</strong> {user?.name}</p>
      <p><strong>Phone:</strong> {user?.phone}</p>

      {/* קישור לדף הפרומפט */}
      <Link to="/prompt">
        <button className="submit-button" style={{ marginTop: "1rem" }}>
          עבור ללמידה לפי קטגוריה
        </button>
      </Link>
    </div>
  );
}
