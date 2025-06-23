const API_BASE_URL = "/api";

// עוזר לעיבוד תגובות מהשרת
async function handleResponse(response) {
  if (!response.ok) {
    const errorText = await response.text();
    let errorMessage;

    try {
      const errorJson = JSON.parse(errorText);
      errorMessage = errorJson.message || errorJson.title || "Request failed";
    } catch {
      errorMessage = errorText || `HTTP ${response.status}: ${response.statusText}`;
    }

    throw new Error(errorMessage);
  }

  return await response.json();
}

// הרשמת משתמש
export async function registerUser(userData) {
  try {
    const response = await fetch(`${API_BASE_URL}/Users/register`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(userData),
    });
    return await handleResponse(response);
  } catch (error) {
    if (error.name === 'TypeError' && error.message.includes('fetch')) {
      throw new Error("Unable to connect to server. Please check if the server is running.");
    }
    throw error;
  }
}

// התחברות
export async function loginUser(userData) {
  try {
    const response = await fetch(`${API_BASE_URL}/Users/login`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(userData),
    });
    return await handleResponse(response);
  } catch (error) {
    if (error.name === 'TypeError' && error.message.includes('fetch')) {
      throw new Error("Unable to connect to server. Please check if the server is running.");
    }
    throw error;
  }
}

// הבאת כל המשתמשים
export async function getAllUsers() {
  try {
    const response = await fetch(`${API_BASE_URL}/Users/all`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    return await handleResponse(response);
  } catch (error) {
    if (error.name === 'TypeError' && error.message.includes('fetch')) {
      throw new Error("Unable to connect to server. Please check if the server is running.");
    }
    throw error;
  }
}

// כל הקטגוריות
export async function getAllCategories() {
  const response = await fetch(`${API_BASE_URL}/Categories`);
  return await handleResponse(response);
}

// תתי קטגוריות לפי קטגוריה
export async function getSubcategoriesByCategory(categoryId) {
  const response = await fetch(`${API_BASE_URL}/Categories/${categoryId}/subcategories`);
  return await handleResponse(response);
}

// שליחת פרומפט וקבלת שיעור
export async function sendPrompt(promptData) {
 
  const response = await fetch(`${API_BASE_URL}/Prompt/generate-lesson`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(promptData),
  });

  return await handleResponse(response);
}
