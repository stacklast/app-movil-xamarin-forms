using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laboratorio_Bimestre_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AbsolutePage : ContentPage
    {
        private int score = 0;
        private Random random = new Random();
        public AbsolutePage()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnCircleTapped;
            gameCircle.GestureRecognizers.Add(tapGestureRecognizer);
        }
        private async void OnCircleTapped(object sender, EventArgs e)
        {
            // Incrementa el puntaje
            score++;
            scoreLabel.Text = $"Puntos: {score}";

            // Mueve el círculo a una nueva posición aleatoria
            double newX = random.NextDouble();
            double newY = random.NextDouble();

            await gameCircle.TranslateTo(newX * absoluteLayout.Width, newY * absoluteLayout.Height, 500, Easing.CubicInOut);

            // Actualiza la posición del círculo en AbsoluteLayout
            AbsoluteLayout.SetLayoutBounds(gameCircle, new Rectangle(newX, newY, 100, 100));
        }
    }
}