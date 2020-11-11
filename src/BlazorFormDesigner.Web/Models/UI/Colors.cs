using System.Collections.Generic;

namespace BlazorFormDesigner.Web.Models
{
    public static class Colors
    {
        public static readonly string Primary = "#0071BC";
        public static readonly string Secondary = "#0E308E";
        public static readonly string Error = "red";
        public static readonly string Warning = "#ff9800";
        public static readonly string Success = "green";
        public static readonly string Black = "rgb(0, 0, 0)";
        public static readonly string White = "rgb(255, 255, 255)";
        public static readonly string Grey = "rgba(0, 0, 0, 0.54)";
        public static readonly string Transparent = "transparent";

        public static readonly List<string> Palettes = new List<string> { "#0d47a1", "#00838f", "#00b0ff", "#4caf50", "#ff9800", "#dd2c00", "#c51162", "#4527a0", "#880e4f", "#3e2723" };
    }
}
