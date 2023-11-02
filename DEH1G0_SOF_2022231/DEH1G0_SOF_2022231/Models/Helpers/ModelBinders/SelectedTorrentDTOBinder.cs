using DEH1G0_SOF_2022231.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;

/// <summary>
/// Custom model binder for the <see cref="SelectedTorrentDto"/> class.
/// </summary>
public class SelectedTorrentDtoBinder : IModelBinder
{
    /// <summary>
    /// Binds the data from the HTTP request to the <see cref="SelectedTorrentDto"/> instance.
    /// </summary>
    /// <param name="bindingContext">The <see cref="ModelBindingContext"/> containing information for model binding.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation</returns>
    /// <exception cref="ArgumentNullException">Thrown when the binding context is null.</exception>
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

        SelectedTorrentDto? dto;

        try
        {
            dto = JsonConvert.DeserializeObject<SelectedTorrentDto>(body);
        }
        catch (Exception ex)
        {
            dto = null;
        }

        bindingContext.Result = ModelBindingResult.Success(dto);
        return Task.CompletedTask;
    }
}