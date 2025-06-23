import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getAllUsers } from "../services/apiService";

export default function AdminDashboardPage() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (!storedUser) {
      navigate("/login");
      return;
    }

    async function loadUsers() {
      try {
        setLoading(true);
        const all = await getAllUsers();
        setUsers(all);
      } catch (err) {
        setError("Error loading users: " + err.message);
      } finally {
        setLoading(false);
      }
    }

    loadUsers();
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem("user");
    navigate("/login");
  };

  if (loading) {
    return (
      <div className="page-container">
        <p>Loading users...</p>
      </div>
    );
  }

  return (
    <div className="page-container">
      <div className="dashboard-header">
        <h2>Admin Dashboard - All Users</h2>
        <button onClick={handleLogout} className="logout-btn">
          Logout
        </button>
      </div>

      {error && <p className="error">{error}</p>}

      {users.length === 0 ? (
        <p>No users found.</p>
      ) : (
        <div className="users-list">
          <ul>
            {users.map((u) => (
              <li key={u.id || u.name} className="user-item">
                <strong>{u.name}</strong> - {u.phone}
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
