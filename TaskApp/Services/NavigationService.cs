using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaskApp.Services
{
    public static class NavigationService
    {
        public static async Task NavigateWithAnimation(Page currentPage, Page newPage)
        {
            var currentPageAnimation = new Animation
            {
                { 0, 0.5, new Animation(v => currentPage.TranslationX = v, 0, -currentPage.Width) },
                { 0, 0.5, new Animation(v => currentPage.Scale = v, 1, 0.8) }
            };

            var newPageAnimation = new Animation
            {
                { 0.5, 1, new Animation(v => newPage.TranslationX = v, newPage.Width, 0) },
                { 0.5, 1, new Animation(v => newPage.Scale = v, 0.8, 1) }
            };

            newPage.TranslationX = newPage.Width;
            newPage.Scale = 0.8;

            await Application.Current.MainPage.Navigation.PushAsync(newPage, false);

            var parentAnimation = new Animation
            {
                { 0, 1, currentPageAnimation },
                { 0, 1, newPageAnimation }
            };

            parentAnimation.Commit(Application.Current.MainPage, "PageTransition", 16, 500, Easing.CubicInOut, (v, c) =>
            {
                currentPage.TranslationX = 0;
                currentPage.Scale = 1;
                newPage.TranslationX = 0;
                newPage.Scale = 1;
            });
        }

    }
}
