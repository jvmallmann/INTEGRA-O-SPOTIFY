using INTEGRAÇÃO_SPOTIFY.Interfaces;
using YoutubeExplode;

public class YoutubeDownloader : IDownloader
{
    private readonly YoutubeClient _youtubeClient;

    public YoutubeDownloader()
    {
        _youtubeClient = new YoutubeClient();
    }

    public async Task BaixarMusicasOuPlaylist(string url, YoutubeSearch youtubeSearch)
    {
        if (url.Contains("playlist"))
        {
            await BaixarPlaylistYoutube(url);
        }
        else
        {
            await youtubeSearch.BaixarMusicaYoutube(url);  // Baixa uma única música
        }
    }

    private async Task BaixarPlaylistYoutube(string url)
    {
        var videos = _youtubeClient.Playlists.GetVideosAsync(url);

        Console.WriteLine($"Baixando playlist: {url}");

        await foreach (var video in videos)
        {
            var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
            var audioStream = streamManifest.GetAudioOnlyStreams()
                .OrderByDescending(s => s.Bitrate)  // Ordena os streams por bitrate
                .FirstOrDefault();  // Seleciona o stream com a maior taxa de bits

            if (audioStream != null)
            {
                var titulo = LimparTitulo(video.Title);
                var caminho = Path.Combine(@"C:\Users\joaov\Desktop\MUSICAS - API", $"{titulo}.mp3");
                Directory.CreateDirectory(Path.GetDirectoryName(caminho));

                await _youtubeClient.Videos.Streams.DownloadAsync(audioStream, caminho);
                Console.WriteLine($"Baixado: {video.Title}");
            }
            else
            {
                Console.WriteLine($"Erro ao obter stream de áudio para: {video.Title}");
            }
        }
    }


    private string LimparTitulo(string titulo)
    {
        var invalidos = Path.GetInvalidFileNameChars();
        return string.Join("_", titulo.Split(invalidos, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
    }
}
