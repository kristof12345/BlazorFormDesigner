namespace BlazorFormDesigner.Web.Models
{
    public class ErrorResponse
    {
        public string Content { get; set; }

        public ErrorResponse() { }

        public ErrorResponse(string content)
        {
            Content = content;
        }
    }
}
