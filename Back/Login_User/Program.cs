using Login_User.Data;
using Login_User.Repositorio;
using Login_User.Service;

var builder = WebApplication.CreateBuilder(args);

// --- Razor Pages ---
builder.Services.AddRazorPages();

// --- Injeção de Dependência ---
builder.Services.AddScoped<RepositorioUser>();
builder.Services.AddScoped<UserService>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// --- Sessão (para guardar o utilizador autenticado) ---
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// --- Pipeline HTTP ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// --- Teste de conexão ao arrancar ---
try
{
    using var connection = DbConnectionFactory.GetConnection();
    Console.WriteLine("✅ Conexão bem sucedida!");
    Console.WriteLine($"Estado: {connection.State}");
}
catch (Exception ex)
{
    Console.WriteLine("❌ Erro ao conectar:");
    Console.WriteLine(ex.Message);
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.MapGet("/", () => Results.Redirect("/Auth/Login"));
app.Run();