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
        private double _circleX;
        private double _circleY;

        public AbsolutePage()
        {
            InitializeComponent();
            _circleX = 0.5; // Initial position (50% of the width)
            _circleY = 0.5; // Initial position (50% of the height)
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnCircleTapped;
            gameCircle.GestureRecognizers.Add(tapGestureRecognizer);
        }
        private void OnCircleTapped(object sender, EventArgs e)
        {
            // Incrementa el puntaje
            score++;
            scoreLabel.Text = $"Puntos: {score}";

            // Verificar si el jugador ha ganado
            if (score >= 20)
            {
                DisplayWinningMessage();
                return; // Detener más movimientos después de ganar
            }

            // Mueve el círculo a una nueva posición aleatoria
            // Randomly move the circle
            _circleX = random.NextDouble();
            _circleY = random.NextDouble();

            // Update the position of the circle
            AbsoluteLayout.SetLayoutBounds(gameCircle, new Rectangle(_circleX, _circleY, 100, 100));
            AbsoluteLayout.SetLayoutFlags(gameCircle, AbsoluteLayoutFlags.PositionProportional);

            // Scroll to the new position
            ScrollToCirclePosition();

        }

        private async void ScrollToCirclePosition()
        {
            // Calculate the scroll position to center the circle
            double scrollX = _circleX * (absoluteLayout.Width - gameCircle.Width);
            double scrollY = _circleY * (absoluteLayout.Height - gameCircle.Height);
            await scrollView.ScrollToAsync(scrollX, scrollY, true); // true for animated scroll
        }

        private void DisplayWinningMessage()
        { // Mostrar mensaje de "Ganado"
            scoreLabel.Text = "¡Ganaste!";
            // Opcional: Hacer que el círculo desaparezca
            gameCircle.IsVisible = false;
        }
    }
}