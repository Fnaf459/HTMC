using System;
using System.Collections.Generic;

class HashTable
{
    private class HashNode
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public HashNode Next { get; set; }

        public HashNode(int key, int value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }

    private List<HashNode>[] buckets;
    private int capacity;
    private int size;

    public HashTable(int capacity)
    {
        this.capacity = capacity;
        buckets = new List<HashNode>[capacity];
        for (int i = 0; i < capacity; i++)
        {
            buckets[i] = new List<HashNode>();
        }
    }

    private int GetBucketIndex(int key)
    {
        return key % capacity;
    }

    private void Rehash()
    {
        int newCapacity = capacity * 2;
        List<HashNode>[] newBuckets = new List<HashNode>[newCapacity];
        for (int i = 0; i < newCapacity; i++)
        {
            newBuckets[i] = new List<HashNode>();
        }

        for (int i = 0; i < capacity; i++)
        {
            foreach (var node in buckets[i])
            {
                int newIndex = node.Key % newCapacity;
                newBuckets[newIndex].Add(node);
            }
        }

        buckets = newBuckets;
        capacity = newCapacity;
    }

    public void Insert(int key, int value)
    {
        if (size == capacity)
        {
            Rehash();
        }

        int bucketIndex = GetBucketIndex(key);
        foreach (var node in buckets[bucketIndex])
        {
            if (node.Key == key)
            {
                node.Value = value;
                return;
            }
        }

        buckets[bucketIndex].Add(new HashNode(key, value));
        size++;
    }

    public int Search(int key)
    {
        int bucketIndex = GetBucketIndex(key);
        foreach (var node in buckets[bucketIndex])
        {
            if (node.Key == key)
            {
                return node.Value;
            }
        }
        return -1;
    }

    public void Display()
    {
        for (int i = 0; i < capacity; i++)
        {
            Console.Write($"Bucket {i}: ");
            foreach (var node in buckets[i])
            {
                Console.Write($"(Key: {node.Key}, Value: {node.Value}) ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        HashTable hashTable = new HashTable(5);

        hashTable.Insert(10, 100);
        hashTable.Insert(22, 220);
        hashTable.Insert(31, 310);

        int value = hashTable.Search(22);
        Console.WriteLine("Значение по ключу 22: " + value);

        Console.WriteLine("Хэш-таблица:");
        hashTable.Display();
    }
}
