using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Costa.Models.Validation
{
    public static class ValidationRegister
    {
        public static IServiceCollection AddValidation(this IServiceCollection services) 
        {
            var assemblyWithValidators = Assembly.GetExecutingAssembly();

            return services.AddValidatorsFromAssembly(assemblyWithValidators);
        }
    }
}
