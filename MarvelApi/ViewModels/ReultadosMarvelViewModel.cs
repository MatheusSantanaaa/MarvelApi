namespace MarvelApi.ViewModels
{
    public class ReultadosMarvelViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ListaResultadosMarvelViewModel comics { get; set; }
        public ListaResultadosMarvelViewModel series { get; set; }
        public ListaResultadosMarvelViewModel stories { get; set; }
        public ListaResultadosMarvelViewModel events { get; set; }
    }
}
