using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using PFM.Domain.Entities;
using PFM.Persistence.Repositories;
using PFM.Persistence.Repositories.Bills;
using PFM.Persistence.Repositories.BillTypes;
using PFM.Persistence.Repositories.Budgets;
using PFM.Persistence.Repositories.Categories;
using PFM.Persistence.Repositories.Debts;
using PFM.Persistence.Repositories.Goals;
using PFM.Persistence.Repositories.Transactions;
using PFM.Persistence.Repositories.TransactionTypes;

namespace PFM.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

       services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
       services.AddScoped<IBillRepository, BillRepository>();
       services.AddScoped<IBillTypeRepository, BillTypeRepository>();
       services.AddScoped<IBudgetRepository, BudgetRepository>();
       services.AddScoped<ICategoryRepository, CategoryRepository>();
       services.AddScoped<IDebtRepository, DebtRepository>();
       services.AddScoped<IGoalRepository, GoalRepository>();
       services.AddScoped<ITransactionRepository, TransactionRepository>();
       services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
       services.AddScoped<UserManager<AppUser>>();
        
        return services;
    }
}