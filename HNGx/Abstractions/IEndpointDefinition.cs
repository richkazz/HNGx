namespace HNGx.Abstractions
{
    // Represents an interface for defining endpoint registrations
    public interface IEndpointDefinition
    {
        // Registers the endpoints in the given WebApplication
        void RegisterEndpoints(WebApplication app);
    }
}
