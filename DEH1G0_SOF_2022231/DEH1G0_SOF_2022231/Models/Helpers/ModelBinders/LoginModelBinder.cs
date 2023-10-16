using DEH1G0_SOF_2022231.Models.Auth;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;

/// <summary>
/// Custom model binder for the <see cref="LoginModel"/> class.
/// </summary>
public class LoginModelBinder : IModelBinder
{
    /// <summary>
    /// Binds the data from the HTTP request to the <see cref="LoginModel"/> instance.
    /// </summary>
    /// <param name="bindingContext">The <see cref="ModelBindingContext"/> containing information for model binding.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var request = bindingContext.HttpContext.Request;
        request.EnableBuffering();
        var body = new StreamReader(request.Body).ReadToEndAsync().Result;
        request.Body.Position = 0;

        LoginModel? regModel;

        try
        {
            regModel = JsonConvert.DeserializeObject<LoginModel>(body);
        }
        catch (Exception ex)
        {
            regModel = null;
        }

        bindingContext.Result = ModelBindingResult.Success(regModel);
        return Task.CompletedTask;

    }
}

