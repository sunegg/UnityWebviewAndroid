using System;
using Random = UnityEngine.Random;

namespace HAHAHA
{
    public static class ArrayExtension
    {

        public static T GetRandom<T>(this T[] array) => array[Random.Range(0, array.Length)];

        public static void LazyFor<T>(this T[,] matrix, Action<int, int> action)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    action?.Invoke(i, j);
                }
            }
        }
        
    }
}

