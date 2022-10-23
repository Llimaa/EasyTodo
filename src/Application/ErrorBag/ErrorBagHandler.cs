namespace Application.ErrorBag;

public class ErrorBagHandler : IErrorBagHandler
{
    private readonly Dictionary<string, string> errors;

    public ErrorBagHandler() => errors = new();
    public void HandlerError(Dictionary<string, string> errors)
    {
        foreach(var (code, error) in errors) 
            this.errors.TryAdd(code, error);
    }

    public void HandlerError(string code, string error)
    {
        errors.TryAdd(code, error);
    }

    public bool HasError() => errors.Count > 0;

    public Dictionary<string, string> Raise() => errors;
}
