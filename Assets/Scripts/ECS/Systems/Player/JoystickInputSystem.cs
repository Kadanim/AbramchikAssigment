using ECS.Components.Inputs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Mono.UI;

namespace ECS.Systems.Player
{
    public class JoystickInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<JoystickInputComponent>> _filter = default;
        private readonly EcsPoolInject<JoystickInputComponent> _inputPool = default;
        private readonly EcsCustomInject<Joystick> _joystick = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var inputComponent = ref _inputPool.Value.Get(entity);
                inputComponent.Direction = _joystick.Value.Direction;
            }
        }
    }
}