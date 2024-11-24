using System;

public class Converter
{
    private Area _inResources;
    private Area _outRecources;
    private int _resourcesSupplied = 3;
    private int _resourcesDemanded = 5;
    private float _timeConvertResources = 3f;
    private float _currentConvertingTime;
    private bool _isConverting;

    public bool IsActive { get; private set; }
    public int ConversionResourcesCount => _inResources.Count;
    public int ConvertedResourcesCount => _outRecources.Count;

    public Converter(int suppliedCount, int demandedCount)
    {
        _inResources = new(suppliedCount);
        _outRecources = new(demandedCount);
    }

    public bool CanTakeResources()
    {
        return _inResources.CanRemoveResources(_resourcesDemanded);
    }

    public void PutConversionResources(int count)
    {
        _inResources.AddResource(count);
    }

    public bool HasFreeSpace()
    {
        return _inResources.Count < _inResources.MaxCount;
    }

    public void TakeResourcesToConvert()
    {
        _inResources.RemoveResources(_resourcesDemanded);
    }

    public void SetActive(bool state)
    {
        if (state == false)
            TurnOff();

        IsActive = state;
    }

    private void TurnOff()
    {
        if (_isConverting)
        {
            _currentConvertingTime = 0;
            _isConverting = false;
            _inResources.AddResource(_resourcesDemanded);
        }
    }

    public void Update(float deltaTime)
    {
        if (!IsActive)
            return;

        if (_isConverting)
        {
            _currentConvertingTime += deltaTime;
        }
        else
        {
            if (CanTakeResources())
            {
                _isConverting = true;
                TakeResourcesToConvert();
            }
            else
            {
                IsActive = false;
            }
        }

        if (_currentConvertingTime >= _timeConvertResources)
        {
            _isConverting = false;
            _currentConvertingTime = 0;

            _outRecources.AddResource(_resourcesSupplied);

            if (!_outRecources.CanAddResources(_resourcesSupplied))
                IsActive = false;
        }
    }
}

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

    public void AddResource(int count)
    {
        Count = Math.Min(Count += count, MaxCount);
    }

    public bool CanAddResources(int count)
    {
        return Count + count <= MaxCount;
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