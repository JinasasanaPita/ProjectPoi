using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.heapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].heapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public bool Contains(T item)
    {
        return Equals(items[item.heapIndex], item);
    }

    public int Count { get { return currentItemCount; } }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.heapIndex * 2 + 1;
            int childIndexRight = item.heapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount)
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                        swapIndex = childIndexRight;

                if (item.CompareTo(items[swapIndex]) < 0)
                    Swap(item, items[swapIndex]);
                else return;
            }
            else return;
        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.heapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else break;

            parentIndex = (item.heapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.heapIndex] = itemB;
        items[itemB.heapIndex] = itemA;
        int itemAIndex = itemA.heapIndex;
        itemA.heapIndex = itemB.heapIndex;
        itemB.heapIndex = itemAIndex;
    }
}

/* 
 * We would like each item in the heap to keep track of its own idex and make comparisons amongst itself.
 * However, since we are working with a generic object T, we cannot be sure if the item allows itself to
 * store its own index and make comparisons amongst itself. To overcome this, we use an interface which every
 * generic item of type T will have
 */
public interface IHeapItem<T> : IComparable<T>
{
    int heapIndex { get; set; }
}