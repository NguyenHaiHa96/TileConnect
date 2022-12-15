using System.Collections.Generic;
using UnityEngine;

    public static class MyUtils
    {
        public static List<T> GetRandomItemsFromList<T> (List<T> list, int number)
        {
            if (number >= list.Count)
            {
                return list;
            }
            // this is the list we're going to remove picked items from
            List<T> tmpList = new List<T>(list);
            // this is the list we're going to move items to
            List<T> newList = new List<T>();
 
            // make sure tmpList isn't already empty
            while (newList.Count < number && tmpList.Count > 0)
            {
                int index = Random.Range(0, tmpList.Count);
                newList.Add(tmpList[index]);
                tmpList.RemoveAt(index);
            }
 
            return newList;
        }
    
        public static void Shuffle<T>(this IList<T> ts) {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
}
