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

        public static bool Same<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            return list1.Except(list2).Count() == 0 && list2.Except(list1).Count() == 0;
        }
    }
}
