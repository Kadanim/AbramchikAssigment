using ECS.Components.UI;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;

namespace ECS.Systems.UI
{
    public class UIInitializationSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var uiEntity = world.NewEntity();

            var uiPool = world.GetPool<UIComponent>();
            ref var uiComponent = ref uiPool.Add(uiEntity);

            uiComponent.UIContainer = GameObject.FindGameObjectWithTag("StackUI");
            uiComponent.ItemText = uiComponent.UIContainer.GetComponent<TextMeshProUGUI>(); 
        }
    }
}