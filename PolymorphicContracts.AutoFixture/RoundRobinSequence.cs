using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace PolymorphicContracts.AutoFixture;

internal class RoundRobinSequence : IEnumerable<int>
{
    private readonly int _min;
    private readonly int _max;
    private int _current;

    internal RoundRobinSequence(int min, int max)
    {
        _min = min;
        _max = max;
        _current = min;
    }

    [SuppressMessage("ReSharper", "IteratorNeverReturns")]
    public IEnumerator<int> GetEnumerator()
    {
        while (true)
        {
            _current++;
            if (_current > _max)
            {
                _current = _current % 2 == 0 ? _min + 1 : _min;
            }
            yield return _current;
            _current++;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}