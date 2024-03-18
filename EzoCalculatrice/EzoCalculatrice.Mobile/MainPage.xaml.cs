namespace EzoCalculatrice.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnDigitClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            DisplayEntry.Text += button.Text;
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            DisplayEntry.Text += $" {button.Text} ";
        }

        private void OnParenthesisClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            DisplayEntry.Text += button.Text;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            DisplayEntry.Text = string.Empty;
        }

        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var encodedExpression = Uri.EscapeDataString(DisplayEntry.Text);
                    var url = $"https://localhost:7011/api/Calculator/evaluate?expression={encodedExpression}";

                    var response = await client.GetStringAsync(url); // Realiza la llamad

                    if (double.TryParse(response, out double result))
                    {
                        DisplayEntry.Text = result.ToString(); // Muestra el resultado en el display
                    }
                    else
                    {
                        DisplayEntry.Text = "Error";
                    }
                }
            }
            catch(Exception ex) 
            {
                DisplayEntry.Text = "Error";
            }
        }
    }

}
