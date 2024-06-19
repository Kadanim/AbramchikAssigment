using ECS.Components.ObjectGeneration;
using ECS.Components.Stack;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems.ObjectGeneration
{
    public class ObjectGeneratorSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<ObjectGeneratorComponent>> _generatorFilter = default;
        private readonly EcsFilterInject<Inc<StackComponent>> _stackFilter = default;

        private readonly EcsPoolInject<ObjectGeneratorComponent> _generatorPool = default;
        private readonly EcsPoolInject<StackComponent> _stackPool = default;

        public void Init(IEcsSystems systems)
        {
        }

        public void Run(IEcsSystems systems)
        {
            float currentTime = Time.time;

            foreach (var generatorEntity in _generatorFilter.Value)
            {
                ref var generatorComponent = ref _generatorPool.Value.Get(generatorEntity);

                if (currentTime >= generatorComponent.NextGenerationTime)
                {
                    GameObject newObject = GameObject.Instantiate(generatorComponent.ObjectPrefab);
                    newObject.name = generatorComponent.ObjectPrefab.name;

                    foreach (var stackEntity in _stackFilter.Value)
                    {
                        ref var stackComponent = ref _stackPool.Value.Get(stackEntity);
                        stackComponent.Stack.Add(newObject);
                        UpdateStackVisual(stackComponent);
                        break;
                    }

                    generatorComponent.NextGenerationTime = currentTime + generatorComponent.GenerationInterval;
                }
            }
        }

        private void UpdateStackVisual(StackComponent stackComponent)
        {
            var stackHeight = 0f;
            foreach (var obj in stackComponent.Stack)
            {
                var collider = obj.GetComponent<Collider>();
                var height = collider != null ? collider.bounds.size.y : 1f;
                obj.transform.localPosition = new Vector3(0, stackHeight, 0);
                stackHeight += height;
            }
        }
    }
}