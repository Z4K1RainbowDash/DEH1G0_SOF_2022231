using DEH1G0_SOF_2022231.Models.Auth;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DEH1G0_SOF_2022231.Models.Helpers.ModelBinders
{
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
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            var firstName = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
            var lastName = bindingContext.ValueProvider.GetValue("LastName").FirstValue;
            var email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
            var username = bindingContext.ValueProvider.GetValue("Username").FirstValue;
            var password = bindingContext.ValueProvider.GetValue("Password").FirstValue;
            var image = bindingContext.ActionContext.HttpContext.Request.Form.Files["Image"];


            var model = new RegisterModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Username = username,
                Password = password,
                Image = image
            };


            if (image != null && !image.ContentType.StartsWith("image"))
            {
                bindingContext.ModelState.TryAddModelError(
                 bindingContext.ModelName, "You must select an IMAGE!");
            }


            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;

        }

    }
}
