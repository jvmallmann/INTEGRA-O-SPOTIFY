class Program
{
    static async Task Main(string[] args)
    {
        var youtubeSearch = new YoutubeSearch();
        var spotifyDownloader = new SpotifyDownloader();

        while (true)
        {
            Console.WriteLine(" Nome de uma música ou a URL, ou 'sair' para fechar:");
            string entrada = Console.ReadLine();

            if (entrada.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (entrada.Contains("spotify.com"))
            {
                await spotifyDownloader.BaixarMusicasOuPlaylist(entrada, youtubeSearch);
            }
            else if (entrada.Contains("youtube.com") || entrada.Contains("youtu.be"))
            {
                await youtubeSearch.BaixarMusicaYoutube(entrada);
            }
            else
            {
                await youtubeSearch.ListarMusicasYoutube(entrada);

                Console.Write("Digite o número da música que deseja baixar: ");
                if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= 10)
                {
                    await youtubeSearch.BaixarMusicaYoutube(entrada);  
                }
                else
                {
                    Console.WriteLine("Escolha inválida.");
                }
            }
        }
    }
}

