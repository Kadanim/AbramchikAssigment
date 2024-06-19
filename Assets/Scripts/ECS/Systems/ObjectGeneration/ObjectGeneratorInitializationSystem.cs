using System.Collections.Generic;
using ECS.Components.ObjectGeneration;
using ECS.Components.Stack;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Mono.Data;
using UnityEngine;

namespace ECS.Systems.ObjectGeneration
{
    public class ObjectGeneratorInitializationSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var generatorEntity = world.NewEntity();
            var generatorPool = world.GetPool<ObjectGeneratorComponent>();

            ref var generatorComponent = ref generatorPool.Add(generatorEntity);
            generatorComponent.ObjectPrefab = _staticData.Value.ObjectPrefab;
            generatorComponent.GenerationInterval = _staticData.Value.GenerationInterval;
            generatorComponent.NextGenerationTime = Time.time + generatorComponent.GenerationInterval;

            var stackEntity = world.NewEntity();
            var stackPool = world.GetPool<StackComponent>();

            ref var stackComponent = ref stackPool.Add(stackEntity);
            stackComponent.Stack = new List<GameObject>();
            stackComponent.CurrentHeight = 0f;
        }
    }
}