using DEH1G0_SOF_2022231.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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



            TorrentSearchDTO vm = new TorrentSearchDTO();

            Movies movies = new Movies();
            movies.IsSelected = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.IsSelected").FirstValue);
            movies.SdHu = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.SdHu").FirstValue);
            movies.SdEn = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.SdEn").FirstValue);
            movies.DvdrHu = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.DvdrHu").FirstValue);
            movies.DvdrEn = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.DvdrEn").FirstValue);
            movies.Dvd9Hu = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.Dvd9Hu").FirstValue);
            movies.Dvd9En = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.Dvd9En").FirstValue);
            movies.HdHu = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.HdHu").FirstValue);
            movies.HdEn = bool.Parse(bindingContext.ValueProvider.GetValue("Movies.HdEn").FirstValue);

            Series series = new Series();
            series.IsSelected = bool.Parse(bindingContext.ValueProvider.GetValue("Series.IsSelected").FirstValue);
            series.SdHu = bool.Parse(bindingContext.ValueProvider.GetValue("Series.SdHu").FirstValue);
            series.SdEn = bool.Parse(bindingContext.ValueProvider.GetValue("Series.SdEn").FirstValue);
            series.DvdrHu = bool.Parse(bindingContext.ValueProvider.GetValue("Series.DvdrHu").FirstValue);
            series.DvdrEn = bool.Parse(bindingContext.ValueProvider.GetValue("Series.DvdrEn").FirstValue);
            series.HdHu = bool.Parse(bindingContext.ValueProvider.GetValue("Series.HdHu").FirstValue);
            series.HdEn = bool.Parse(bindingContext.ValueProvider.GetValue("Series.HdEn").FirstValue);

            Musics musics = new Musics();
            musics.IsSelected = bool.Parse(bindingContext.ValueProvider.GetValue("Musics.IsSelected").FirstValue);
            musics.Mp3Hu = bool.Parse(bindingContext.ValueProvider.GetValue("Musics.Mp3Hu").FirstValue);
            musics.Mp3En = bool.Parse(bindingContext.ValueProvider.GetValue("Musics.Mp3En").FirstValue);
            musics.LosslessHu = bool.Parse(bindingContext.ValueProvider.GetValue("Musics.LosslessHu").FirstValue);
            musics.LosslessEn = bool.Parse(bindingContext.ValueProvider.GetValue("Musics.LosslessEn").FirstValue);
            musics.Clip = bool.Parse(bindingContext.ValueProvider.GetValue("Musics.Clip").FirstValue);

            Games games = new Games();
            games.IsSelected = bool.Parse(bindingContext.ValueProvider.GetValue("Games.IsSelected").FirstValue);
            games.Iso = bool.Parse(bindingContext.ValueProvider.GetValue("Games.Iso").FirstValue);
            games.Rip = bool.Parse(bindingContext.ValueProvider.GetValue("Games.Rip").FirstValue);
            games.Console = bool.Parse(bindingContext.ValueProvider.GetValue("Games.Console").FirstValue);

            Programs programs = new Programs();
            programs.IsSelected = bool.Parse(bindingContext.ValueProvider.GetValue("Programs.IsSelected").FirstValue);
            programs.Iso = bool.Parse(bindingContext.ValueProvider.GetValue("Programs.Iso").FirstValue);
            programs.Rip = bool.Parse(bindingContext.ValueProvider.GetValue("Programs.Rip").FirstValue);
            programs.Mobile = bool.Parse(bindingContext.ValueProvider.GetValue("Programs.Mobile").FirstValue);

            Books books = new Books();
            books.IsSelected = bool.Parse(bindingContext.ValueProvider.GetValue("Books.IsSelected").FirstValue);
            books.EBookHu = bool.Parse(bindingContext.ValueProvider.GetValue("Books.EBookHu").FirstValue);
            books.EBookEn = bool.Parse(bindingContext.ValueProvider.GetValue("Books.EBookEn").FirstValue);


            vm.Movies = movies;
            vm.Series = series;
            vm.Musics = musics;
            vm.Games = games;
            vm.Programs = programs;
            vm.Books = books;


            vm.SearchText = bindingContext.ValueProvider.GetValue("SearchText").FirstValue;

            bool categorySelected = movies.IsSelected || series.IsSelected || musics.IsSelected || games.IsSelected || programs.IsSelected || books.IsSelected;

            if (categorySelected)
            {
                bool movieSub = movies.SdHu || movies.SdEn || movies.DvdrHu || movies.DvdrEn || movies.Dvd9Hu || movies.Dvd9En || movies.HdHu || movies.HdEn;
                bool seriesSub =  series.SdHu || series.SdEn || series.DvdrHu || series.DvdrEn || series.HdHu || series.HdEn;
                bool musicSub = musics.Mp3Hu || musics.Mp3En || musics.LosslessHu || musics.LosslessEn || musics.Clip;
                bool gamesSub = games.Iso || games.Rip || games.Console;
                bool programSub = programs.Iso || programs.Rip || programs.Mobile;
                bool bookSub = books.EBookHu || books.EBookEn;

                this.CheckSubcategory(movies.IsSelected, movieSub, bindingContext, "Movie");
                
                this.CheckSubcategory(series.IsSelected, seriesSub, bindingContext, "Series");
                
                this.CheckSubcategory(musics.IsSelected, musicSub, bindingContext, "Music");
                
                this.CheckSubcategory(games.IsSelected, gamesSub, bindingContext, "Game");
                
                this.CheckSubcategory(programs.IsSelected, programSub, bindingContext, "Program");
                
                this.CheckSubcategory(books.IsSelected, bookSub, bindingContext, "Book");
                

            }
            else
            {
                bindingContext.ModelState.TryAddModelError(
                bindingContext.ModelName, "You must select a category");
                
            }
            

            bindingContext.Result = ModelBindingResult.Success(vm);
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
