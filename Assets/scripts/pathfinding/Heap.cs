using UnityEngine;
using System.Collections;

public class Heap <T>
{
    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }
}
