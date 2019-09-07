using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.ApiCallers
{
    public class PokemonApiCaller
    {
        private readonly string apiBase = "https://pokeapi.co/api/v2/";

        public JObject GetPokemonJson(string pokemonName)
        {
            var pokeApiUrl = apiBase + "pokemon/" + pokemonName.ToLower();

            var webRequest = WebRequest.Create(pokeApiUrl);

            var responseStream = webRequest.GetResponse().GetResponseStream();

            var json = GetJsonFromResponseStream(responseStream);
            return json;
        }

        public Bitmap GetPokemonPic(JObject pokemonJson)
        {
            var picUrl = pokemonJson["sprites"]["front_default"].ToString();

            var webRequest = WebRequest.Create(picUrl);

            var responseStream = webRequest.GetResponse().GetResponseStream();

            return new Bitmap(responseStream);
        }

        public string GetPokemonDescription(JObject pokemonJson)
        {

            var species = GetPokemonSpecies(pokemonJson);
            var description = species["genera"][2]["genus"].ToString();

            return description;
        }


        private JObject GetJsonFromResponseStream(Stream responseStream)
        {
            var jsonString = "";
            if (responseStream != null)
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    // Return next available character or -1 if there are no characters to be read
                    while (streamReader.Peek() > -1)
                    {
                        jsonString += streamReader.ReadLine();
                    }
                }
            }
            return JObject.Parse(jsonString);
        }

        private JObject GetPokemonSpecies(JObject pokemonJson)
        {
            var formRequestUrl = pokemonJson["species"]["url"].ToString();

            var webRequest = WebRequest.Create(formRequestUrl);

            var responseStream = webRequest.GetResponse().GetResponseStream();

            var json = GetJsonFromResponseStream(responseStream);

            return json;
        }
    }
}
