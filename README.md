# AIClassroom - AI Learning Platform 🎓🤖

מערכת מיני-פלטפורמה ללמידה מונחית בינה מלאכותית — פיתוח Full Stack עם React ו־.NET Core, כולל חיבור ל־OpenAI, ניהול משתמשים, לוגיקה עסקית, בסיס נתונים, ודאשבורד נוח.

---

## 📌 תיאור המערכת

המערכת מאפשרת למשתמשים:
- להירשם ולהתחבר
- לבחור תחום לימוד ותת־תחום
- לשלוח פרומפט ולקבל שיעור שנוצר ע״י בינה מלאכותית (GPT)
- לשמור היסטוריית למידה
- לצפות בפרומפטים קודמים (עבור המשתמש או אדמין)

---

## 🛠 טכנולוגיות בשימוש

### 🔙 Backend (C# ASP.NET Core)
- ASP.NET Core Web API
- AutoMapper
- Entity Framework Core
- PostgreSQL
- שכבות DAL, BL, ו־Controllers
- OpenAI API Integration
- Dependency Injection
- RESTful API
- הפרדה למודולים: Users, Categories, SubCategories, Prompts

### 🌐 Frontend (React)
- React (Vite)
- React Router
- MUI + CSS מודרני
- ניהול State מקומי
- מסכים: הרשמה, התחברות, דאשבורד, היסטוריה, אדמין

---

## 🧱 מבנה הקוד

```plaintext
📁 backend/
 ├── AIClassroom/              ← Web API project
 │    ├── Controllers/         ← User, Prompt, Category
 │    ├── Program.cs, appsettings.json
 ├── AIClassroom.BL/          ← Business Logic
 │    ├── API/                ← Interfaces
 │    ├── Services/           ← Logic
 │    ├── ModelsDTO/          ← DTOs
 │    └── Mapping/            ← AutoMapper profile
 ├── AIClassroom.DAL/         ← Data Access Layer
 │    ├── Models/             ← Entity Models
 │    ├── Interfaces/         ← Repositories Interfaces
 │    ├── Repositories/       ← Repositories
 │    └── AIClassroomDbContext.cs

📁 frontend/
 ├── src/
 │   ├── pages/               ← Dashboard, Auth, Admin
 │   ├── components/          ← טפסים, טבלאות
 │   └── App.jsx
 └── package.json, index.html
