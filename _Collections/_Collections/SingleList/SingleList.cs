using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Collections.SingleList
{
    class SingleList<T> : IEnumerable where T : IComparable<T>
    {
        private T[] _item = new T[0];
        private int _index = 0;
        private int _count;

        public int Count
        {
            get
            {
                _count = _item.Length;
                return _count;
            }
        }

        public SingleList(int capacity)
        {
            this._item = new T[capacity];
        }

        public SingleList() { }

        public T this[int index]
        {
            get { return this._item[index]; }
            set
            {
                if (index > Count)
                    _item = new T[Count + 1];


                _item[index] = value;
                _index = index == _index ? _index + 1 : _index;
            }
        }

        private void Resize()
        {
            T[] oldItem = _item;
            _item = new T[++_count];
            for (int i = 0; i < oldItem.Length; i++)
            {
                _item[i] = oldItem[i];
            }
        }

        /// <summary>
        /// Adds an object to the end of the <see cref="SingleList{T}" />.
        /// </summary>
        /// <param name="value">The object to be added to the end of the <see cref="SingleList{T}" /></param>
        public void Add(T value)
        {
            if (_index >= Count)
            {
                Resize();
            }
            _item[_index++] = value;
        }

        /// <summary>
        /// Determines whether an element is in the <see cref="SingleList{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="SingleList{T}" />.</param>
        /// <param name="count">count of items containing in <see cref="SingleList{T}" />.</param>
        /// <returns>true if item is found in the <see cref="SingleList{T}" /> otherwise false.</returns>
        public bool Contains(T item, out int count)
        {
            bool isContain = false;
            count = 0;
            for (int i = 0; i < _item.Length; i++)
            {
                if (item.Equals(_item[i]))
                {
                    count++;
                    isContain = true;
                }
            }

            return isContain;
        }

        /// <summary>
        /// Determines whether an element is in the <see cref="SingleList{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="SingleList{T}" />.</param>
        /// <returns>true if item is found in the <see cref="SingleList{T}" /> otherwise false.</returns>
        public bool Contains(T item)
        {
            bool isContain = false;
            for (int i = 0; i < _item.Length; i++)
            {
                if (item.Equals(_item[i]))
                {
                    isContain = true;
                }
            }

            return isContain;
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="SingleList{T}" />.
        /// </summary>
        public void Sort()
        {
            for (int i = 0; i < _item.Length; i++)
            {
                for (int j = i + 1; j < _item.Length; j++)
                {
                    if (_item[i].CompareTo(_item[j]) > 0)
                    {
                        T temp = _item[i];
                        _item[i] = _item[j];
                        _item[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Reverses the order of the elements in the entire <see cref="SingleList{T}" />.
        /// </summary>
        public void Reverse()
        {
            for (int i = 0; i < _item.Length / 2; i++)
            {
                T temp = _item[i];
                _item[i] = _item[_item.Length - i - 1];
                _item[_item.Length - i - 1] = temp;
            }
        }

        /// <summary>
        /// Reverses the order of the elements in the specified range.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to reverse.</param>
        /// <param name="count">The number of elements in the range to reverse.</param>
        public void Reverse(int index, int count)
        {
            int j = count;
            for (int i = index; i <= count / 2; i++)
            {
                T temp = _item[i];
                _item[i] = _item[j];
                _item[j--] = temp;
            }
        }

        /// <summary>
        /// Removes all elements from the <see cref="SingleList{T}" />.
        /// </summary>
        public void Clear()
        {
            _item = new T[0];
            _index = 0;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="SingleList{T}" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="SingleList{T}" />.</param>
        /// <returns>true if item is successfully removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            int removedItemIndex = 0;
            bool flag = false;
            for (int i = 0; i < _item.Length; i++)
            {
                if (item.Equals(_item[i]))
                {
                    removedItemIndex = i;
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                RemoveAt(removedItemIndex);
            }

            return flag;
        }

        /// <summary>
        /// Removes the element at the specified index of the <see cref="SingleList{T}" />.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            T[] oldItem = _item;
            _item = new T[oldItem.Length - 1];
            _index = 0;
            for (int i = 0; i < oldItem.Length; i++)
            {
                if (index != i)
                {
                    _item[_index++] = oldItem[i];
                }
            }
        }

        /// <summary>
        /// Removes a range of elements from the <see cref="SingleList{T}" />.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        public void RemoveRange(int index, int count)
        {
            T[] oldItem = _item;
            _item = new T[oldItem.Length - count];
            _index = 0;
            for (int i = 0; i < oldItem.Length; i++)
            {
                if (index != i)
                {
                    _item[_index++] = oldItem[i];
                }
                else if (index < count)
                {
                    index++;
                }
            }
        }

        /// <summary>
        /// Inserts an element into the <see cref="SingleList{T}" />.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        public void Insert(int index, T item)
        {
            T[] oldItem = _item;
            _item = new T[oldItem.Length + 1];
            _index = 0;
            for (int i = 0; i < oldItem.Length; i++)
            {
                Condition:
                if (index != i)
                {
                    _item[_index++] = oldItem[i];
                }
                else
                {
                    _item[_index++] = item;
                    index = -1;
                    goto Condition;
                }
            }
        }

        /// <summary>
        /// Inserts an elements into the <see cref="SingleList{T}" />.
        /// </summary>
        /// <param name="index">he zero-based index at which item should be inserted.</param>
        /// <param name="items">The objects to insert.</param>
        public void Insert(int index, params T[] items)
        {
            T[] oldItem = _item;
            int count = items.Length;
            _item = new T[items.Length + oldItem.Length];
            _index = 0;
            for (int i = 0; i < oldItem.Length; i++)
            {
                if (index != i)
                {
                    _item[_index++] = oldItem[i];
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        _item[_index++] = items[j];
                    }
                    _item[_index++] = oldItem[i];
                }
            }
        }

        /// <summary>
        /// Copies the elements of the <see cref="SingleList{T}" /> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="SingleList{T}" /></returns>
        public T[] ToArray()
        {
            T[] array = new T[_count];
            for (int i = 0; i < _item.Length; i++)
            {
                array[i] = _item[i];
            }
            return array;
        }

        /// <summary>
        /// Copies the elements of the <see cref="List{T}" /> 
        /// </summary>
        /// <returns>Returns <see cref="List{T}"/></returns>
        public List<T> ToList()
        {
            return _item.ToList();
        }

        /// <summary>
        /// Copies the elements of the <see cref="Stack{T}" /> 
        /// </summary>
        /// <returns>Returns <see cref="Stack{T}"/></returns>
        public Stack<T> ToStack()
        {
            Stack<T> stack = new Stack<T>();
            for (int i = _item.Length - 1; i >= 0; i--)
            {
                stack.Push(_item[i]);
            }

            return stack;
        }

        /// <summary>
        /// Copies the elements of the <see cref="Queue{T}" />
        /// </summary>
        /// <returns>Returns <see cref="Queue{T}"/></returns>
        public Queue<T> ToQueue()
        {
            Queue<T> queue = new Queue<T>();
            for (int i = 0; i < _item.Length; i++)
            {
                queue.Enqueue(_item[i]);
            }

            return queue;
        }

        /// <summary>
        /// Copies a range of elements from the <see cref="SingleList{T}" /> to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="index">The zero-based index in the source <see cref="SingleList{T}"/> at which copying begins.</param>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="SingleList{T}"/>.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <param name="count">The number of elements to copy.</param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            for (int i = 0; i < _item.Length; i++)
            {
                if (i == index && index < count)
                {
                    array[arrayIndex++] = _item[i];
                    index++;
                }
            }
        }

        /// <summary>
        /// Copies a range of elements from the <see cref="SingleList{T}"/> to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="SingleList{T}"/>.</param>
        /// <param name="arrayIndex">The zero-based index in the source <see cref="SingleList{T}"/> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(0, array, arrayIndex, _count);
        }

        /// <summary>
        ///  Copies the entire <see cref="SingleList{T}" /> to a compatible one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="List{T}"/>.</param>
        public void CopyTo(T[] array)
        {
            CopyTo(0, array, 0, _count);
        }

        /// <summary>
        ///  Creates a shallow copy of a range of elements in the source <see cref="SingleList{T}"/>
        /// </summary>
        /// <param name="index">The zero-based <see cref="SingleList{T}"/> index at which the range starts.</param>
        /// <param name="count">The number of elements in the range.</param>
        /// <returns>A shallow copy of a range of elements in the source <see cref="SingleList{T}"/></returns>
        public SingleList<T> GetRange(int index, int count)
        {
            SingleList<T> singleList = new SingleList<T>();
            singleList._index = 0;
            singleList._item = new T[count];
            for (int i = 0; i < _item.Length; i++)
            {
                if (index == i && index <= count)
                {
                    singleList[singleList._index] = _item[i];
                    singleList._index++;
                    index++;
                }
            }

            return singleList;
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="SingleList{T}"/>
        /// </summary>
        /// <param name="item"> The object to locate in the <see cref="SingleList{T}"/></param>
        /// <returns>The zero-based index of the first occurrence of item within the entire <see cref="SingleList{T}"/>, if found; otherwise, –1.</returns>
        public int IndexOf(T item)
        {
            return IndexOf(item, 0, _count - 1);
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="SingleList{T}"/> that extends from the specified index to the last element.
        /// </summary>
        /// <param name="item">The object to locate in <see cref="SingleList{T}"/></param>
        /// <param name="index">The zero-based starting index of the search.</param>
        /// <returns>The zero-based index of the first occurrence of item within the range of elementsin the <see cref="SingleList{T}"/> that extends from index to the lastelement, if found; otherwise, –1.</returns>
        public int IndexOf(T item, int index)
        {
            return IndexOf(item, index, _count - 1);
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="SingleList{T}"/> that starts at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="item">The object to locate in <see cref="SingleList{T}"/></param>
        /// <param name="index">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="SingleList{T}"/> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
        public int IndexOf(T item, int index, int count)
        {
            int indexOf = -1;
            for (int i = index; i <= count; i++)
            {
                if (_item[i].Equals(item))
                {
                    indexOf = i;
                    break;
                }
            }

            return indexOf;
        }

        /// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the entire <see cref="SingleList{T}" />.</summary>
        /// <returns>The zero-based index of the last occurrence of <paramref name="item" /> within the entire the <see cref="SingleList{T}" />, if found; otherwise, –1.</returns>
        /// <param name="item">The object to locate in the <see cref="SingleList{T}" />. The value can be null for reference types.</param>
        public int LastIndexOf(T item)
        {
            return LastIndexOf(item, 0, _count);
        }

        /// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="SingleList{T}" /> that extends from the first element to the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of <paramref name="item" /> within the range of elements in the <see cref="SingleList{T}" /> that extends from the first element to <paramref name="index" />, if found; otherwise, –1.</returns>
        /// <param name="item">The object to locate in the <see cref="SingleList{T}" />. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the backward search.</param>
        public int LastIndexOf(T item, int index)
        {
            return LastIndexOf(item, index, _count);
        }

        /// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="SingleList{T}" /> that contains the specified number of elements and ends at the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of <paramref name="item" /> within the range of elements in the <see cref="SingleList{T}" /> that contains <paramref name="count" /> number of elements and ends at <paramref name="index" />, if found; otherwise, –1.</returns>
        /// <param name="item">The object to locate in the <see cref="SingleList{T}" />. The value can be null for reference types.</param>
        /// <param name="index">The zero-based starting index of the backward search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        public int LastIndexOf(T item, int index, int count)
        {
            int lastIndexOf = -1;
            for (int i = count - 1; i >= index; i--)
            {
                if (item.Equals(_item[i]))
                {
                    lastIndexOf = i;
                    break;
                }
            }

            return lastIndexOf;
        }

        /// <summary>
        /// Orders <see cref="SingleList{T}" /> randomly
        /// </summary>
        public void Randomize()
        {
            Random rnd = new Random();
            _item = _item.OrderBy<T, int>((item) => rnd.Next()).ToArray();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _item.GetEnumerator();
        }  
    }
}
