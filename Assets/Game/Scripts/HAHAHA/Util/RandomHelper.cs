using UnityEngine;

public static class RandomHelper
{
    public static bool GetRandomBool() => Random.Range(0, 2) ==0;
}
