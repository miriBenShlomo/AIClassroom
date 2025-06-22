using AIClassroom.BL.API;
using AIClassroom.BL.Mapping;
using AIClassroom.BL.Services;
using AIClassroom.DAL.Interfaces;
using AIClassroom.DAL.Models;
using AIClassroom.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// 1. ����� ������� �������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. ����� ������ �������
builder.Services.AddAutoMapper(typeof(MappingProfile));
// �� ������ ����� ��� AIClassroomDbContext (�� AlClassroomDBContext)
builder.Services.AddDbContext<AIClassroomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. ����� Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IPromptRepository, PromptRepository>();

// 4. ����� Services (�� ����� ������� �� �������)
builder.Services.AddScoped<IUserService, UserService>();
// �� ������ ����� ��� CategoryService (�� CategoriesService)
builder.Services.AddScoped<ICategoryService, CategoryService>();
// �� ������ ����� ��� SubCategoryService (�� SubCategoriesService)
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
// �� ������ ����� ��� PromptServiceBL (�� PromptService)
builder.Services.AddScoped<IPromptService, PromptServiceBL>();
// �� ������ ����� ��� AIServiceBL (�� AIService)
builder.Services.AddScoped<IAIServiceBL, AIServiceBL>();

// 5. ����� OpenAI
// �� ������ ����� ��� AIServiceBL (�� AIService)
builder.Services.AddHttpClient<IAIServiceBL, AIServiceBL>(client =>
{
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    if (!string.IsNullOrEmpty(apiKey))
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }
});

// 6. ����� CORS
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