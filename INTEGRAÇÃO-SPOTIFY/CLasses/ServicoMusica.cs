using INTEGRAÇÃO_SPOTIFY.Interfaces;
using System;
using System.Threading.Tasks;

public class ServicoMusica
{
    private readonly IDownloader _downloader;

    public ServicoMusica(IDownloader downloader)
    {
        _downloader = downloader;
    }

    public async Task Iniciar()
    {
        var youtubeSearch = new YoutubeSearch(); // Cria instância de YoutubeSearch

        while (true)
        {
            Console.WriteLine("Digite o nome de uma música ou a URL (YouTube ou Spotify), ou 'sair' para fechar o programa:");
            string entrada = Console.ReadLine();

            if (entrada.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (entrada.Contains("spotify.com") || entrada.Contains("youtube.com"))
            {
                await _downloader.BaixarMusicasOuPlaylist(entrada, youtubeSearch);  // Passa youtubeSearch
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
