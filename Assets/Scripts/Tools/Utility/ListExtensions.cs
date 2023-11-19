using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class ListExtensions
{
    public static T RandomItem<T>(this List<T> list)
    {
        var randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }

    public static T RandomItemRemove<T>(this List<T> list)
    {
        var item = list.RandomItem();
        list.Remove(item);
        return item;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        var n = list.Count;
        for (var i = 0; i <= n - 2; i++)
        {
            //random index
            var rdn = Random.Range(0, n - i);

            //swap positions
            (list[i], list[i + rdn]) = (list[i + rdn], list[i]);
        }
    }
    
    public static void AddToFront<T>(this List<T> list, T item) => list.Insert(0, item);
    
    public static void AddBeforeOf<T>(this List<T> list, T item, T newItem)
    {
        var targetPosition = list.IndexOf(item);
        list.Insert(targetPosition, newItem);
    }
    
    public static void AddAfterOf<T>(this List<T> list, T item, T newItem)
    {
        var targetPosition = list.IndexOf(item) + 1;
        list.Insert(targetPosition, newItem);
    }

    public static void PrintList<T>(this List<T> list, string log = "")
    {
        log += "[";
        for (var i = 0; i < list.Count; i++)
        {
            log += list[i].ToString();
            log += i != list.Count - 1 ? ", " : "]";
        }
    }
}