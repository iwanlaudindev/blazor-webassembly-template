namespace Water.Management.Application;

public class ApplicationSettings
{
    public ApplicationSettings()
    {
        BaseAddress = "http://localhost:5000/";
        UIFramework = "WebAssembly";
    }
    public string BaseAddress { get; set; }
    public string UIFramework { get; set; }
}
