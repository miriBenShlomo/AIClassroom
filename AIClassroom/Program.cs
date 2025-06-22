using AIClassroom.BL.API;
using AIClassroom.BL.Mapping;
using AIClassroom.BL.Services;
using AIClassroom.DAL.Interfaces;
using AIClassroom.DAL.Models;
using AIClassroom.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// 1. רישום שירותים בסיסיים
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. רישום תלויות הפרויקט
builder.Services.AddAutoMapper(typeof(MappingProfile));
// שם המחלקה הנכון הוא AIClassroomDbContext (לא AlClassroomDBContext)
builder.Services.AddDbContext<AIClassroomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. רישום Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IPromptRepository, PromptRepository>();

// 4. רישום Services (עם השמות הנכונים של המחלקות)
builder.Services.AddScoped<IUserService, UserService>();
// שם המחלקה הנכון הוא CategoryService (לא CategoriesService)
builder.Services.AddScoped<ICategoryService, CategoryService>();
// שם המחלקה הנכון הוא SubCategoryService (לא SubCategoriesService)
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
// שם המחלקה הנכון הוא PromptServiceBL (לא PromptService)
builder.Services.AddScoped<IPromptService, PromptServiceBL>();
// שם המחלקה הנכון הוא AIServiceBL (לא AIService)
builder.Services.AddScoped<IAIServiceBL, AIServiceBL>();

// 5. רישום OpenAI
// שם המחלקה הנכון הוא AIServiceBL (לא AIService)
builder.Services.AddHttpClient<IAIServiceBL, AIServiceBL>(client =>
{
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    if (!string.IsNullOrEmpty(apiKey))
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }
});

// 6. הגדרת CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();