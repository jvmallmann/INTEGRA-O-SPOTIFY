using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTEGRAÇÃO_SPOTIFY.Interfaces
{
    public interface IDownloader
    {
        Task BaixarMusicasOuPlaylist(string url, YoutubeSearch youtubeSearch);
    }
}
