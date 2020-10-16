using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BlazorFormDesigner.Web.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum QuestionType
    {
        [EnumMember(Value = "Text question")]
        Text,
        [EnumMember(Value = "Numeric question")]
        Numeric,
        [EnumMember(Value = "Single chioce question")]
        SingleChoice,
        [EnumMember(Value = "Multiple chioce question")]
        MultipleChoice
    }
}
