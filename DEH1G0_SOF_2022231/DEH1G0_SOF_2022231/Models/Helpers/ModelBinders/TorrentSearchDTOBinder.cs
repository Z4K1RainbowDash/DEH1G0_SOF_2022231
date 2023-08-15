using DEH1G0_SOF_2022231.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DEH1G0_SOF_2022231.Models.Helpers.ModelBinders
{
    public class TorrentSearchDTOBinder : IModelBinder
    {
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


            TorrentSearchDTO? dto;
            
            try
            {
                dto = JsonConvert.DeserializeObject<TorrentSearchDTO>(body);
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
}
