using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationExtension
{
    public abstract class PathMoveComponent
        : MonoBehaviour
    {
        // 到达某个节点的事件
        public event Action<int> OnArriveNode;

        [SerializeField]
        protected List<Vector2> PathNode = new List<Vector2>();
        [SerializeField]
        protected GameObject BallObject;
        [SerializeField]
        protected float TotalTime;

        protected int NodeCount;
        protected int CurrentIndex;
        protected float NodeTime;

        protected virtual void OnEnable()
        {
            this.CurrentIndex = 0;
            this.NodeCount = this.PathNode.Count;
            this.NodeTime = this.TotalTime / this.NodeCount;
            this.DrawPath();
        }

        protected virtual void Update ()
        {
            if (this.isRunning)
                this.SmoothMove(Time.deltaTime);
        }

        protected abstract void SmoothMove(float dt);

        protected bool isRunning = false;
        public void Run()
        {
            this.isRunning = true;
        }

        private void DrawPath()
        {
#if UNITY_EDITOR
            for (var i = 0; i < this.NodeCount -1;++i)
            {
                var s = this.BallObject.transform.TransformPoint(this.PathNode[i]);
                var e = this.BallObject.transform.TransformPoint(this.PathNode[i + 1]);
                Debug.DrawLine(s, e, Color.green, 10f);
            }
#endif
        }

        protected void FireArriveNode(int i)
        {
            this.OnArriveNode.SafeInvoke(this.CurrentIndex);
        }
    }
}
