using System.Net.Http.Headers;
using System.Text;

namespace Application.Common.ThirdParty;

public class Fetch
{
    //public static async Task<T> GetAsync<T>(string url, string authen = null) where T : class, new()
    //{
    //    try
    //    {
    //        using (HttpClient ObjHttpClient = new HttpClient())
    //        {
    //            ObjHttpClient.DefaultRequestHeaders.Accept.Clear();
    //            if (!string.IsNullOrEmpty(authen))
    //                ObjHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(authen)));
    //            ObjHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //            HttpResponseMessage ObjHttpResponseMessage = new HttpResponseMessage();
    //            ObjHttpResponseMessage = await ObjHttpClient.GetAsync(url);

    //            string response = await ObjHttpResponseMessage.Content.ReadAsStringAsync();

    //            if (!string.IsNullOrEmpty(response))
    //                //return JsonConvert.DeserializeObject<T>(response);

    //            return null;
    //        }
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}
    //public static async Task<T> PostAsync<T>(string url, object data, string authen = null) where T : class, new()
    //{
    //    try
    //    {
    //        using (HttpClient ObjHttpClient = new HttpClient())
    //        {
    //            ObjHttpClient.DefaultRequestHeaders.Accept.Clear();
    //            if (!string.IsNullOrEmpty(authen))
    //                ObjHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(authen)));
    //            ObjHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //            StringContent ObjContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

    //            HttpResponseMessage ObjHttpResponseMessage = new HttpResponseMessage();
    //            ObjHttpResponseMessage = await ObjHttpClient.PostAsync(url, ObjContent);

    //            string response = await ObjHttpResponseMessage.Content.ReadAsStringAsync();

    //            if (!string.IsNullOrEmpty(response))
    //                return JsonConvert.DeserializeObject<T>(response);

    //            return null;
    //        }
    //    }
    //    catch (Exception exc)
    //    {
    //        throw new FieldAccessException(exc.Message);
    //    }
    //}
}
