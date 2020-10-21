using BlazorFormDesigner.BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorFormDesigner.BusinessLogic.Extensions
{
    public static class ListExtensions
    {
        public static T GetById<T>(this IEnumerable<T> list, string id) where T : Form
        {
            return list.FirstOrDefault(element => element.Id == id);
        }

        public static T GetByQuestion<T>(this IEnumerable<T> list, string id) where T : Answer
        {
            return list.FirstOrDefault(element => element.QuestionId == id);
        }

        public static bool ContainsAll<T>(this IEnumerable<T> list, IEnumerable<T> subset)
        {
            return !subset.Except(list).Any();
        }

        public static bool Same<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            return list1.Except(list2).Count() == 0 && list2.Except(list1).Count() == 0;
        }
    }
}
