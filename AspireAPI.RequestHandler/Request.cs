using Aspire.Security;
using Aspire.Security.Secrets;
using System.Net.Http.Headers;

namespace AspireAPI.RequestHandler
{
  public class Request
  {

    HttpClient client = new HttpClient();
    public Request(APIInfo apiinfo)
    {
      
      client.BaseAddress = new Uri(apiinfo.Url);
      if (!string.IsNullOrEmpty(apiinfo.Headers.Name)) client.DefaultRequestHeaders.Add(apiinfo.Headers.Name, apiinfo.Headers.Value);

    }

    public string GET(string path)
    {
      var responseTask = client.GetAsync(path);
      responseTask.Wait();

      try
      {
        HttpResponseMessage result = responseTask.Result;

        Task<string> readTask = result.Content.ReadAsStringAsync();
        readTask.Wait();

        return readTask.Result;

      }
      catch (HttpRequestException? ex)
      {
        throw ex.GetBaseException();

      }

    }

    public HttpResponseMessage POST(string Path, HttpContent Content)
    {
      try
      {
        Task<HttpResponseMessage> responseTask  = client.PostAsync(Path, Content);
        return responseTask.Result;
      }
      catch (HttpRequestException? ex)
      {
        throw ex.GetBaseException();
      }
    }

    public HttpResponseMessage Delete(string path)
    {
      try 
      { 
        Task<HttpResponseMessage> responseTask = client.DeleteAsync(path);
        return responseTask.Result;
      }

      catch (HttpRequestException? ex)
      {
        throw ex.GetBaseException();
      }
}



  }

}