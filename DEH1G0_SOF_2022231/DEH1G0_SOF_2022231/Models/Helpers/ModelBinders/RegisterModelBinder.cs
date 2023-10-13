using DEH1G0_SOF_2022231.Models.Auth;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;

/// <summary>
/// Custom model binder for the <see cref="RegisterModel"/> class.
/// </summary>
public class RegisterModelBinder : IModelBinder
{

    /// <summary>
    /// Binds the data from the HTTP request to the <see cref="RegisterModel"/> instance.
    /// </summary>
    /// <param name="bindingContext">The <see cref="ModelBindingContext"/> containing information for model binding.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the binding context is null.</exception>
    public  Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }
        var request = bindingContext.HttpContext.Request;
        request.EnableBuffering();
        var body = new StreamReader(request.Body).ReadToEndAsync().Result;
        request.Body.Position = 0;

        RegisterModel? regModel;

        try
        {
            regModel = JsonConvert.DeserializeObject<RegisterModel>(body);
        }
        catch (Exception ex)
        {
            regModel = null;
        }

        bindingContext.Result = ModelBindingResult.Success(regModel);
        return Task.CompletedTask;
    }
}

