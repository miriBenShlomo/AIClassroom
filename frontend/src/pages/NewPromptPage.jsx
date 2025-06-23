// src/pages/NewPromptPage.jsx
import React, { useEffect, useState } from "react";
import {
  MenuItem,
  Select,
  TextField,
  Button,
  Alert,
} from "@mui/material";
import "../pages/DashboardPage.css";
import * as apiService from "../services/apiService";

export default function NewPromptPage() {
  const [user, setUser] = useState(null);
  const [categories, setCategories] = useState([]);
  const [subcategories, setSubcategories] = useState([]);
  const [categoryId, setCategoryId] = useState("");
  const [subcategoryId, setSubcategoryId] = useState("");
  const [promptText, setPromptText] = useState("");
  const [response, setResponse] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  useEffect(() => {
    apiService.getAllCategories().then(setCategories).catch(console.error);
  }, []);

  useEffect(() => {
    if (categoryId) {
      apiService
        .getSubcategoriesByCategory(categoryId)
        .then(setSubcategories)
        .catch(console.error);
    }
  }, [categoryId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!promptText || !subcategoryId || !categoryId || !user?.id) {
      setError("יש למלא את כל השדות");
      return;
    }

    setError(null);
    setLoading(true);

    try {
      const result = await apiService.sendPrompt({
        UserId: user.id,
        CategoryId: categoryId,
        SubCategoryId: subcategoryId,
        PromptText: promptText,
      });
      setResponse(result.response || "אין תגובה מה-AI.");
    } catch (err) {
      setError("שגיאה בשליחת הפרומפט: " + err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="dashboard-container">
      <h1 className="dashboard-title">למידה לפי קטגוריה</h1>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>קטגוריה</label>
          <Select
            value={categoryId}
            onChange={(e) => setCategoryId(e.target.value)}
            className="form-input"
            fullWidth
          >
            {categories.map((cat) => (
              <MenuItem key={cat.id} value={cat.id}>
                {cat.name}
              </MenuItem>
            ))}
          </Select>
        </div>

        <div className="form-group">
          <label>תת־קטגוריה</label>
          <Select
            value={subcategoryId}
            onChange={(e) => setSubcategoryId(e.target.value)}
            className="form-input"
            fullWidth
            disabled={!categoryId}
          >
            {subcategories.map((sub) => (
              <MenuItem key={sub.id} value={sub.id}>
                {sub.name}
              </MenuItem>
            ))}
          </Select>
        </div>

        <div className="form-group">
          <label>Prompt</label>
          <TextField
            multiline
            minRows={3}
            value={promptText}
            onChange={(e) => setPromptText(e.target.value)}
            className="form-textarea"
            fullWidth
          />
        </div>

        <Button
          type="submit"
          disabled={loading}
          className="submit-button"
          variant="contained"
        >
          {loading ? "שולח..." : "שלח לפרומפט"}
        </Button>
      </form>

      {error && <Alert severity="error" sx={{ mt: 2 }}>{error}</Alert>}

      {response && (
        <div className="response-container">
          <h2>תשובת ה־AI</h2>
          <p>{response}</p>
        </div>
      )}
    </div>
  );
}
