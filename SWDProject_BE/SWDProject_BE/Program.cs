using Microsoft.OpenApi.Models;
using SWDProject_BE.AppStarts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Install AutoMapper
builder.Services.ConfigureAutoMapper();
// Install DI and dbcontext
builder.Services.InstallService(builder.Configuration);
// Swagger config
//builder.Services.ConfigureSwaggerServices("SWDProject");
builder.Services.ConfigureAuthService(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen(c =>
//{
//	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//	c.IncludeXmlComments(xmlPath);
//});

builder.Services.AddSwaggerGen(c =>
{
	//c.OperationFilter<SnakecasingParameOperationFilter>();
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Tour Booking API",
		Version = "v1"
	});
	c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

	var securitySchema = new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		Reference = new OpenApiReference
		{
			Type = ReferenceType.SecurityScheme,
			Id = "Bearer"
		}
	};
	c.AddSecurityDefinition("Bearer", securitySchema);
	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
						securitySchema,
					new string[] { "Bearer" }
		}
				});
});
// Add CORS
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
