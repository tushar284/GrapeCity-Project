
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace ImageGalleryUsingDynamicControl
{
    class DataFetcher
    {
        async Task<string> GetDatafromService(string search)
        {
            string read = null;
            try
            {
                var azure = @"https://imagefetcher20200529182038.azurewebsites.net";
                string url = azure + @"/api/fetch_images?query=" +search + "&max_count=10";
                using (HttpClient c = new HttpClient())
                {
                    read = await c.GetStringAsync(url);
                }
            }
            catch
            {
                read = File.ReadAllText(@"Data/sampleData.json");
            }
            return read;

        }
        public async Task<List<ImageItem>> GetImageData(string search)
        {
            string data = await GetDatafromService(search);
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        }

    }
}
