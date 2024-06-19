using ECS.Components.Inputs;
using ECS.Components.Movement;
using ECS.Components.Physics;
using ECS.Components.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems.Player
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent, MovementComponent, RigidbodyComponent>> _filter = default;
        private readonly EcsPoolInject<MovementComponent> _movementPool = default;
        private readonly EcsPoolInject<JoystickInputComponent> _inputPool = default;
        private readonly EcsPoolInject<RigidbodyComponent> _rigidbodyPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var movement = ref _movementPool.Value.Get(entity);
                ref var input = ref _inputPool.Value.Get(entity);
                ref var rigidbodyComponent = ref _rigidbodyPool.Value.Get(entity);

                movement.Direction = input.Direction;

                Vector3 velocity = new Vector3(movement.Direction.x, 0, movement.Direction.y) * movement.Speed;
                rigidbodyComponent.Rigidbody.velocity = velocity;

                if (velocity != Vector3.zero)
                {
                    rigidbodyComponent.Rigidbody.transform.forward = velocity;
                }
            }
        }
    }
}