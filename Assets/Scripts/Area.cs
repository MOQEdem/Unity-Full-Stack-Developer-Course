using System;

public class Area
{
    public int Count { get; private set; }

    public int MaxCount { get; private set; }

    public Area(int maxCount)
    {
        if (maxCount <= 0)
        {
            throw new ArgumentException();
        }

        MaxCount = maxCount;
    }

    public Area(int maxCount, int count)
    {
        if (maxCount <= 0 || count < 0)
        {
            throw new ArgumentException();
        }

        Count = count;
        MaxCount = maxCount;
    }

    public void AddResource(int count, out int change)
    {
        if (Count + count > MaxCount)
        {
            change = Count + count - MaxCount;
            Count = MaxCount;
        }
        else
        {
            change = 0;
            Count += count;
        }
    }

    public bool CanAddResources(int count)
    {
        return Count + count <= MaxCount;
    }

    public bool CanAddResources()
    {
        return Count <= MaxCount;
    }

    public bool CanRemoveResources(int count)
    {
        return Count >= count;
    }

    public int RemoveResources(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException();
        }

        int removeResources = Math.Min(Count, count);
        Count -= removeResources;
        return removeResources;
    }
}