using DEH1G0_SOF_2022231.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;

/// <summary>
/// Custom model binder for the <see cref="TorrentSearchDto"/> class.
/// </summary>
public class TorrentSearchDtoBinder : IModelBinder
{
    /// <summary>
    /// Binds the data from the HTTP request to the <see cref="TorrentSearchDto"/> instance.
    /// </summary>
    /// <param name="bindingContext">The <see cref="ModelBindingContext"/> containing information for model binding.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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


        TorrentSearchDto? dto;
        
        try
        {
            dto = JsonConvert.DeserializeObject<TorrentSearchDto>(body);
        }
        catch (Exception ex)
        {
            dto = null;
        }

        bindingContext.Result = ModelBindingResult.Success(dto);
        return Task.CompletedTask;
    }

    private void CheckSubcategory(bool categoryIsSelected, bool subCategory, ModelBindingContext bindingContext, string categoryName)
    {
        if (categoryIsSelected && !subCategory)
        {
            bindingContext.ModelState.TryAddModelError(
            bindingContext.ModelName, $"You must select a {categoryName} subcategory");
        }
    }
}

