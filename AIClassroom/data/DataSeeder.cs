using AIClassroom.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AIClassroom.Data
{
    /// <summary>
    /// A static class responsible for seeding the database with initial data.
    /// </summary>
    public static class DataSeeder
    {
        /// <summary>
        /// Populates the database with initial categories and sub-categories if the database is empty.
        /// </summary>
        /// <param name="app">The application builder, used to create a service scope for accessing the DbContext.</param>
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AIClassroomDbContext>();
                if (context == null) return;

                // ודא שמסד הנתונים נוצר
                context.Database.EnsureCreated();

                // בדוק אם כבר יש קטגוריות, כדי לא להוסיף אותן שוב ושוב
                if (context.Categories.Any())
                {
                    return; // מסד הנתונים כבר מאוכלס
                }

                // יצירת קטגוריות
                var scienceCategory = new Category { Name = "Science" };
                var programmingCategory = new Category { Name = "Programming" };

                // יצירת תתי-קטגוריות
                var subCategories = new SubCategory[]
                {
                    // תתי-קטגוריות של מדע
                    new SubCategory { Name = "Space", Category = scienceCategory },
                    new SubCategory { Name = "Physics", Category = scienceCategory },
                    new SubCategory { Name = "Chemistry", Category = scienceCategory },
                    
                    // תתי-קטגוריות של תכנות
                    new SubCategory { Name = "C#", Category = programmingCategory },
                    new SubCategory { Name = "React", Category = programmingCategory },
                    new SubCategory { Name = "Python", Category = programmingCategory }
                };

                // הוספת הנתונים למסד הנתונים
                context.SubCategories.AddRange(subCategories);

                // שמירת השינויים
                context.SaveChanges();
            }
        }
    }
}