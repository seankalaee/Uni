using System;

public class GenList<T>
{
    private T[] data;
    private int size;
    private int capacity;

    public int Count => size;  // Property to get current size

    public GenList()
    {
        capacity = 8;  // Initial capacity
        data = new T[capacity];
        size = 0;
    }

    public void Add(T item)
    {
        if (size == capacity)
        {
            capacity *= 2;  // Double capacity
            T[] newData = new T[capacity];
            Array.Copy(data, newData, size);
            data = newData;
        }
        data[size] = item;
        size++;
    }

    public void Remove(int index)
    {
        if (index < 0 || index >= size) throw new IndexOutOfRangeException("Invalid index.");

        for (int i = index; i < size - 1; i++)
        {
            data[i] = data[i + 1];
        }
        size--;
    }

    public T this[int i] => data[i];  // Indexer for access
}
