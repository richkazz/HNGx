using HNGx.Abstractions;

namespace HNGx.Extensions
{
    public static class HNGxExtension
    {
        /// <summary>
        /// Registers the endpoint definitions in the web application.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void RegisterEndpointDefinitions(this WebApplication app)
        {
            var endpointDefinitionTypes = typeof(Program).Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface);

            foreach (var endpointDefinitionType in endpointDefinitionTypes)
            {
                if (Activator.CreateInstance(endpointDefinitionType) is IEndpointDefinition endpointDefinition)
                {
                    endpointDefinition.RegisterEndpoints(app);
                }
            }
        }
    }
}
