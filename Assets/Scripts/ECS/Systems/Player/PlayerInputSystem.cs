using ECS.Components.Player;
using ECS.Components.Stack;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems.Player
{
    public class PlayerInputSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent, PlayerStackComponent>> _playerFilter = default;
        private readonly EcsPoolInject<PickupComponent> _pickupPool = default;
        private readonly EcsPoolInject<DropComponent> _dropPool = default;

        private int _playerEntity;

        public void Init(IEcsSystems systems)
        {
            _playerEntity = -1;
            foreach (var entity in _playerFilter.Value)
            {
                _playerEntity = entity;
                break;
            }
        }

        public void Pickup()
        {
            var world = _pickupPool.Value.GetWorld();
            var pickupEntity = world.NewEntity();
            _pickupPool.Value.Add(pickupEntity);
        }

        public void Drop()
        {
            var world = _dropPool.Value.GetWorld();
            var dropEntity = world.NewEntity();
            _dropPool.Value.Add(dropEntity);
        }
    }
}