using System;
using System.Net.Http;

namespace BlazorFormDesigner.Web.Services
{
    public static class AppService
    {
        public static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") };
    }
}
