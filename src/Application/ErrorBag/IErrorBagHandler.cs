namespace Application.ErrorBag;

public interface IErrorBagHandler 
{
    void HandlerError(Dictionary<string, string> errors);

    void HandlerError(string code, string error);

    bool HasError();

    Dictionary<string, string> Raise();
}