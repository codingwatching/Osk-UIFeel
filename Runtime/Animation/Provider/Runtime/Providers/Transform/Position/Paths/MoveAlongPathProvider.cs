using System;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace OSK
{
    [DisallowMultipleComponent]
    public class MoveAlongPathProvider : DoTweenBaseProvider
    {
        public List<PathProvier> paths = new List<PathProvier>();
        public PathType typePath = PathType.CatmullRom;
        public PathMode pathMode = PathMode.Full3D;
 
        public bool isClosedPath = false;
        public bool isLocal = true;
        public bool isStartFirstPoint = true;
        public bool isLookAt = false;
        
        public override object GetStartValue() => paths.FirstOrDefault()?.position ?? Vector3.zero;
        public override object GetEndValue() =>  paths.LastOrDefault()?.position ?? Vector3.zero;

        public override void ProgressTween(bool isPlayBackwards)
        {
            if (paths == null || paths.Count == 0)
            {
                Debug.LogWarning("Path points are not defined!");
                return;
            }

            if (isStartFirstPoint)
            {
                transform.position = paths[0].position;
                transform.eulerAngles = paths[0].rotation.eulerAngles;
            }

            Vector3[] pathPositions;
            if (isClosedPath)
            {
                pathPositions = paths.Select(p => p.position).ToArray().Concat(new Vector3[] { paths[0].position })
                    .ToArray();
            }
            else
            {
                pathPositions = paths.Select(p => p.position).ToArray();
            }

            PathType pathType = typePath == PathType.Linear ? PathType.Linear : PathType.CatmullRom;
            bool applyLookAt = isLookAt || !isLocal;

            if (isLocal)
            {
                tweener = (applyLookAt)
                    ? transform.DOLocalPath(pathPositions, settings.duration, pathType).SetLookAt(0.01f)
                    : transform.DOLocalPath(pathPositions, settings.duration, pathType);
            }
            else
            {
                tweener = (applyLookAt)
                    ? transform.DOPath(pathPositions, settings.duration, pathType).SetLookAt(0.01f)
                    : transform.DOPath(pathPositions, settings.duration, pathType);
            }
            
            base.ProgressTween(isPlayBackwards);
        }

  
        public override void PlayOnEnable()
        {
            base.PlayOnEnable();
        }

        #if UNITY_EDITOR
        [Button]
        public void OpenWindownEditPath()
        {
            //UnityEditor.EditorWindow.GetWindow<MoveAlongPathEditorWindow>();
        }
        #endif

        public void AddPathPoint()
        {
            var idx = paths.Count;
            PathProvier newPathProvier = new PathProvier(idx, transform.position, transform.rotation);
            paths.Add(newPathProvier);
        }

        public void AddPathPoint(Vector3 position, Quaternion rotation)
        {
            PathProvier newPathProvier = new PathProvier(paths.Count, position, rotation);
            paths.Add(newPathProvier);
        }

        public void RemovePathPoint(PathProvier pathProvier)
        {
            if (paths.Contains(pathProvier))
            {
                paths.Remove(pathProvier);
            }
        }
    }
}