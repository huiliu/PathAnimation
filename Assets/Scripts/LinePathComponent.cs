﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationExtension
{
    public class LinePathComponent
        : PathMoveComponent
    {
        private float accumulateTime = 0;
        protected override void SmoothMove(float dt)
        {
            this.accumulateTime += dt;
            if (this.accumulateTime > this.NodeTime)
            {
                ++this.CurrentIndex;
                this.accumulateTime -= this.NodeTime;
                this.FireArriveNode(this.CurrentIndex);
            }

            if (this.CurrentIndex == this.NodeCount - 1)
            {
                this.isRunning = false;
                return;
            }

            var startPos = this.PathNode[this.CurrentIndex];
            var endPos = this.PathNode[this.CurrentIndex + 1];
            Vector2 currentPos = this.BallObject.transform.localPosition;
            var rate = this.accumulateTime / this.NodeTime;

            this.BallObject.transform.localPosition = Vector2.Lerp(startPos, endPos, rate);
        }
    }
}
