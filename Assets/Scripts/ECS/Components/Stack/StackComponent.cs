using System.Collections.Generic;
using UnityEngine;

namespace ECS.Components.Stack
{
    public struct StackComponent
    {
        public List<GameObject> Stack;
        public float CurrentHeight;
    }
}