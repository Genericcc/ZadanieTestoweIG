using System;
using System.Collections.Generic;

namespace Collections
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private readonly List<T> heap;
        
        public int Count => heap.Count - 1;

        public PriorityQueue(int capacity = 32)
        {
            heap = new List<T>(capacity + 1);
            heap.Add(default);
        }

        public void Enqueue(T node)
        {
            heap.Add(node);

            UpHeap(Count);
        }
        
        public T Dequeue()
        {
            if (Count <= 0) throw new InvalidOperationException();

            var value = heap[1];
            heap[1] = heap[Count];

            heap.RemoveAt(Count);

            DownHeap(1);

            return value;
        }

        public T Peek()
        {
            if (Count <= 0) throw new InvalidOperationException();

            return heap[1];
        }

        public void Clear()
        {
            heap.Clear();
            heap.Add(default);
        }

        private void DownHeap(int i)
        {
            var l = i * 2;
            var r = i * 2 + 1;
            while (l <= Count)
            {
                var min = l;

                if (r <= Count && heap[r].CompareTo(heap[l]) < 0) min = r;

                if (heap[min].CompareTo(heap[i]) >= 0)
                    return;

                Swap(i, min);

                i = min;
                l = i * 2;
                r = i * 2 + 1;
            }
        }

        private void UpHeap(int i)
        {
            var parent = i / 2;

            while (parent > 0 && heap[i].CompareTo(heap[parent]) < 0)
            {
                Swap(i, parent);

                i = parent;
                parent = i / 2;
            }
        }

        private void Swap(int i1, int i2)
        {
            (heap[i1], heap[i2]) = (heap[i2], heap[i1]);
        }

    }
}