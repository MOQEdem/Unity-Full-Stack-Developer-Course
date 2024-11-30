using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
public class Converter
{
    private Area _inResources;
    private Area _outRecources;
    private float _currentConvertingTime;
    private bool _isConverting;
    private int _resourcesSupplied;
    private int _resourcesDemanded;
    private float _timeConvertResources;

    public bool IsActive { get; private set; }
    public int ConversionResourcesCount => _inResources.Count;
    public int ConvertedResourcesCount => _outRecources.Count;

    public Converter(int suppliedCount, int demandedCount, int resourcesSupplied, int resourcesDemanded,
        float timeConvertResources)
    {
        _timeConvertResources = timeConvertResources;
        _resourcesDemanded = resourcesDemanded;
        _resourcesSupplied = resourcesSupplied;
        _inResources = new(suppliedCount);
        _outRecources = new(demandedCount);
    }

    public void PutConversionResources(int count, out int overflow)
    {
        if(count <= 0)
            throw new ArgumentException("count <= 0");

        _inResources.AddResource(count, out overflow);
    }

    public bool HasFreeSpace()
    {
        return _inResources.Count < _inResources.MaxCount;
    }

    public void SetActive(bool state)
    {
        if (state == false)
            TurnOff();

        IsActive = state;
    }

    private bool CanOutputResources()
    {
        return _outRecources.CanAddResources();
    }

    private bool CanStartConverting()
    {
        return CanTakeResources() && CanOutputResources();
    }

    private void TurnOff()
    {
        if (_isConverting)
        {
            _currentConvertingTime = 0;
            _isConverting = false;
            _inResources.AddResource(_resourcesDemanded, out int change);
        }
    }

    public void Update(float deltaTime)
    {
        if (deltaTime < 0)
            throw new ArgumentException("deltaTime < 0");

        if (!IsActive)
            return;

        if (!_isConverting)
            BeginCycle();

        if (_isConverting)
            UpdateCycle(deltaTime);
    }

    private void BeginCycle()
    {
        if (CanStartConverting())
        {
            _isConverting = true;
            TakeResourcesToConvert();
        }
        else
        {
            IsActive = false;
        }
    }

    private void UpdateCycle(float deltaTime)
    {
        _currentConvertingTime += deltaTime;

        if (_currentConvertingTime >= _timeConvertResources)
        {
            _isConverting = false;
            _currentConvertingTime = 0;

            _outRecources.AddResource(_resourcesSupplied, out int change);

            if (!_outRecources.CanAddResources(_resourcesSupplied))
                IsActive = false;
        }
    }

    internal bool CanTakeResources()
    {
        return _inResources.CanRemoveResources(_resourcesDemanded);
    }

    internal void TakeResourcesToConvert()
    {
        _inResources.RemoveResources(_resourcesDemanded);
    }
}