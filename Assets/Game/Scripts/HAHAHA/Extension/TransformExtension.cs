using UnityEngine;

namespace Fs.Extension
{
    public static class TransformExtension
    {

        public static void DestroyChildrens(this Transform transform, int index = 0)
        {
            int childs = transform.childCount;
            for (int i = childs - 1; i >= index; i--)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }

    }
}