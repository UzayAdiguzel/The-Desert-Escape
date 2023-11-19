using System;
using System.Collections.Generic;

namespace Helpers
{
    public class ObjectListTracker<T>
    {
        private readonly List<T> _currentList = new List<T>();
        private readonly List<T> _shouldAddList = new List<T>();
        private readonly List<T> _shouldRemoveList = new List<T>();

        public List<T> CurrentList
        {
            get
            {
                SyncList();
                return _currentList;
            }
        }

        public List<T> PureCurrentList => _currentList;

        public ObjectListTracker()
        {
            var checkList = ObjectHelper.TryToGetObjList(out List<T> currentFissileList);
            if (checkList)
            {
                foreach (var t in currentFissileList)
                    _currentList.Add(t);
            }
            
            
            ObjectHelper.OnAddObject += MarkerObjectHelperOnOnAddObject;
            ObjectHelper.OnRemoveObject += MarkerObjectHelperOnOnRemoveObject;
        }

        public void Dispose()
        {
            ObjectHelper.OnAddObject -= MarkerObjectHelperOnOnAddObject;
            ObjectHelper.OnRemoveObject -= MarkerObjectHelperOnOnRemoveObject;
        }
        
        private void MarkerObjectHelperOnOnAddObject(Type type, int id)
        {
            if (type != typeof(T))
                return;

            if (!ObjectHelper.TryToGetObj(id, out T target))
                return;

            _shouldAddList.Add(target);
        }

        private void MarkerObjectHelperOnOnRemoveObject(Type type, int id)
        {
            if (type != typeof(T))
                return;
            
            if (!ObjectHelper.TryToGetObj(id, out T target))
                return;

            _shouldRemoveList.Add(target);
        }
        
        public void SyncList()
        {
            foreach (var fissileEntity in _shouldAddList)
                _currentList.Add(fissileEntity);
            _shouldAddList.Clear();
            
            foreach (var fissileEntity in _shouldRemoveList)
                _currentList.Remove(fissileEntity);
            _shouldRemoveList.Clear();
        }
        
        public T GetRandomItem()
        {
            SyncList();
            if (_currentList.Count <= 0)
                return default;

            T randomItem = _currentList.RandomItem();
            return randomItem;
        }

    }
}