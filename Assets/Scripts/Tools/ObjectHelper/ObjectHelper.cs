using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class ObjectHelper
    {
        private static readonly Dictionary<Type, Dictionary<int, IObject>> ObjTypeIdDict 
            = new Dictionary<Type, Dictionary<int, IObject>>();

        public static event Action<Type,int> OnAddObject;
        public static event Action<Type,int> OnRemoveObject;

        public static void AddObj<T>(this IObject targetObject)
        {
            var t = typeof(T);
            if (!ObjTypeIdDict.ContainsKey(t))
                ObjTypeIdDict.Add(t, new Dictionary<int, IObject>());

            if (ObjTypeIdDict[t].ContainsKey(targetObject.InstanceID))
                return;
           
            ObjTypeIdDict[t].Add(targetObject.InstanceID, targetObject);
            
            OnAddObject?.Invoke(t, targetObject.InstanceID);
        }

        public static void RemoveObj<T>(this IObject targetObject)
        {
            var t = typeof(T);
            if (!ObjTypeIdDict.ContainsKey(t))
                return;
            
            if (!ObjTypeIdDict[t].ContainsKey(targetObject.InstanceID))
                return;

            OnRemoveObject?.Invoke(t, targetObject.InstanceID);
            ObjTypeIdDict[t].Remove(targetObject.InstanceID);
        }

        public static bool TryToGetObj<T>(int targetID, out T target)
        {
            var t = typeof(T);
            target = default;
            if (!ObjTypeIdDict.ContainsKey(t))
                return false;

            if (!ObjTypeIdDict[t].ContainsKey(targetID))
                return false;

            target = (T) ObjTypeIdDict[t][targetID];
            
            return true;
        }

        public static bool TryToGetObjCount<T>(out int targetCount)
        {
            var t = typeof(T);
            targetCount = 0;
            if (!ObjTypeIdDict.ContainsKey(t))
                return false;

            targetCount = ObjTypeIdDict[t].Count;
            return true;
        }

        public static bool TryToGetObjList<T>(out List<T> targetList)
        {
            var t = typeof(T);
            targetList = null;
            if (!ObjTypeIdDict.ContainsKey(t))
                return false;

            var enumerable = ObjTypeIdDict[t].Values.Cast<T>();
            targetList = enumerable.ToList();
            
            return true;
        }

        public static void ClearAll()
        {
            ObjTypeIdDict.Clear();
        }
    }
}