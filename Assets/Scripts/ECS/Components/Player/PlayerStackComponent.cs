using System.Collections.Generic;
using UnityEngine;

namespace ECS.Components.Player
{
    public struct PlayerStackComponent
    {
        public List<GameObject> Stack;
        public float CurrentHeight;
        public int StackEntity;
    }
}