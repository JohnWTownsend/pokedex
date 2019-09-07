using Pokedex.ApiCallers;
using Pokedex.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pokedex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitPokemonName_Click(object sender, RoutedEventArgs e)
        {
            var pokeApi = new PokemonApiCaller();
            var imageSourceHelper = new ImageSourceHelper();

            var pokemonJson = pokeApi.GetPokemonJson(pokemonNameTxtBox.Text);
            var bitmap = pokeApi.GetPokemonPic(pokemonJson);
            var bitmapImageSource = imageSourceHelper.BitmapToImageSource(bitmap);

            var pokemonDescription = pokeApi.GetPokemonDescription(pokemonJson);

            pokemonFrontImage.Source = bitmapImageSource;
            pokemonNameLabel.Content = pokemonDescription;
        }

        private void PokemonNameTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                SubmitPokemonName_Click(sender, e);
            }
        }
    }
}
