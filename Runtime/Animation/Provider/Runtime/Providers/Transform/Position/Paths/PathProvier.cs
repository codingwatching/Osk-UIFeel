using UnityEngine;

namespace OSK
{
    [System.Serializable]
    public class PathProvier
    {
        public int index;
        public Vector3 position;
        public Quaternion rotation;

        public PathProvier(int index, Vector3 position, Quaternion rotation)
        {
            this.index = index;
            this.position = position;
            this.rotation = rotation;
        }
    } 
}