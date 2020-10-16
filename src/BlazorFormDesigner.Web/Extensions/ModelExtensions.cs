using BlazorFormDesigner.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorFormDesigner.Web.Extensions
{
    public static class ListExtensions
    {
        public static T GetByQuestion<T>(this IEnumerable<T> list, string id) where T : Answer
        {
            return list.FirstOrDefault(element => element.QuestionId == id);
        }
    }
}
