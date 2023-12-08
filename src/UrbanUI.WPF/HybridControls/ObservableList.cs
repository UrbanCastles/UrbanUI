using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace UrbanUI.WPF.HybridControls
{
    public class ObservableList<T> : List<T>, IList<T>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public new int Count
        {
            get
            {
                return base.Count;
            }
        }

        public new T this[int index]
        {
            get
            {
                return base[index];
            }

            set
            {
                base[index] = value;
            }
        }

        public new void Add(T item)
        {
            base.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public new void AddRange(IEnumerable<T> items)
        {
            if (items.Count() == 0)
                return;

            base.AddRange(items);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items));
        }

        private void Replace(int index, T newItem)
        {
            var oldItem = this[index];
            this[index] = newItem;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem));
        }

        public void ReplaceRange(int startIndex, IEnumerable<T> newItems)
        {
            var oldItems = new List<T>();
            foreach (var item in newItems)
            {
                oldItems.Add(this[startIndex]);
                this[startIndex] = item;
                startIndex++;
            }
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItems, oldItems.ToArray()));
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public new int IndexOf(T item)
        {
            return base.IndexOf(item);
        }

        public new void RemoveAt(int index)
        {
            var removedItem = base[index];
            base.RemoveAt(index);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem));
        }

        public new bool Remove(T item)
        {
            var removeSuccess = base.Remove(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return removeSuccess;
        }

        public new void Clear()
        {
            base.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public new bool Contains(T item)
        {
            return base.Contains(item);
        }

        public new void CopyTo(T[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return base.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
