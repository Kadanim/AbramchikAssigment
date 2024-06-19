using ECS.Components.Player;
using ECS.Components.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems.UI
{
    public class UIUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent, PlayerStackComponent>> _playerFilter = default;
        private readonly EcsFilterInject<Inc<UIComponent>> _uiFilter = default;

        private readonly EcsPoolInject<PlayerStackComponent> _playerStackPool = default;
        private readonly EcsPoolInject<UIComponent> _uiPool = default;

        public void Init(IEcsSystems systems)
        {
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                ref var playerStackComponent = ref _playerStackPool.Value.Get(playerEntity);

                foreach (var uiEntity in _uiFilter.Value)
                {
                    ref var uiComponent = ref _uiPool.Value.Get(uiEntity);

                    UpdateStackUI(uiComponent, playerStackComponent);
                }
            }
        }

        private void UpdateStackUI(UIComponent uiComponent, PlayerStackComponent playerStackComponent)
        {
            var itemCount = playerStackComponent.Stack.Count;
            var itemNames = string.Join(", ", playerStackComponent.Stack.ConvertAll(obj => obj.name));

            uiComponent.ItemText.text = $"Items: {itemNames}\nCount: {itemCount}";
        }
    }
}