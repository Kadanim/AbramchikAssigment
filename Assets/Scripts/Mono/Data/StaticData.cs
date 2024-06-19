using UnityEngine;

namespace Mono.Data
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Generation Settings")] 
        public GameObject ObjectPrefab;
        public float GenerationInterval;

        [Header("Pickup Settings")] 
        public float PickupRadius = 4f;
        public Vector3 PickupOffset = new Vector3(0, 0.8f, 0.4f);
    }
}