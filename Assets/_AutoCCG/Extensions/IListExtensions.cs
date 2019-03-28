using System;
using System.Collections.Generic;
using System.Linq;

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    public static void WeightedShuffle<T>(this IList<T> targetList, Func<T, float> weightFunction)
    {
        var weights = new List<float>();
        List<int> invalidIds = new List<int>();

        for (int i = 0; i < targetList.Count; i++)
        {
            float weight = weightFunction(targetList[i]);
            if (weight > float.Epsilon)
            {
                weights.Add(weight);
            } else
            {
                invalidIds.Add(i);
            }
        }

        for (int i = invalidIds.Count - 1; i >= 0; i--)
        {
            var invalidId = invalidIds[i];
            var listObject = targetList[invalidId];
            targetList.RemoveAt(invalidId);
            targetList.Add(listObject);
        }

        for (int i = 0; i < weights.Count; i++)
        {
            var r = WeightedChoice(weights);

            var tmp = targetList[i];
            targetList[i] = targetList[r];
            targetList[r] = tmp;

            weights[r] = weights[i];
            weights[i] = 0;
        }
    }

    static int WeightedChoice(List<float> weights)
    {
        var random = UnityEngine.Random.value * weights.Sum();
        for (int i = 0; i < weights.Count; i++)
        {
            random -= weights[i];
            if (random < 0)
            {
                return i;
            }
        }

        throw new KeyNotFoundException();
    }
}