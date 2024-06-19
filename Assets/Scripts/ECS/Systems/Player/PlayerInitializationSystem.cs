using System.Collections.Generic;
using ECS.Components.Inputs;
using ECS.Components.Movement;
using ECS.Components.Physics;
using ECS.Components.Player;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems.Player
{
    public class PlayerInitializationSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var playerEntity = world.NewEntity();

            var inputPool = world.GetPool<JoystickInputComponent>();
            var movementPool = world.GetPool<MovementComponent>();
            var playerPool = world.GetPool<PlayerComponent>();
            var rigidbodyPool = world.GetPool<RigidbodyComponent>();
            var playerStackPool = world.GetPool<PlayerStackComponent>();

            inputPool.Add(playerEntity);
            movementPool.Add(playerEntity);
            playerPool.Add(playerEntity);
            rigidbodyPool.Add(playerEntity);
            playerStackPool.Add(playerEntity);

            ref var movementComponent = ref movementPool.Get(playerEntity);
            movementComponent.Speed = 5f;

            ref var rigidbodyComponent = ref rigidbodyPool.Get(playerEntity);
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            rigidbodyComponent.Rigidbody = playerObject.GetComponent<Rigidbody>();

            ref var playerStackComponent = ref playerStackPool.Get(playerEntity);
            playerStackComponent.StackEntity = -1;
            playerStackComponent.Stack = new List<GameObject>();
        }
    }
}