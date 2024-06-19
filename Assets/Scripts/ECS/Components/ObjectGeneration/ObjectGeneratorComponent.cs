using UnityEngine;

namespace ECS.Components.ObjectGeneration
{
    public struct ObjectGeneratorComponent
    {
        public GameObject ObjectPrefab;
        public float GenerationInterval;
        public float NextGenerationTime;
    }
}