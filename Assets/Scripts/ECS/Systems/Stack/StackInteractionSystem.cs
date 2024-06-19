using System.Collections.Generic;
using ECS.Components.Player;
using ECS.Components.Stack;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Mono.Data;
using UnityEngine;

namespace ECS.Systems.Stack
{
    public class StackInteractionSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent, PlayerStackComponent>> _playerFilter = default;
        private readonly EcsFilterInject<Inc<PickupComponent>> _pickupFilter = default;
        private readonly EcsFilterInject<Inc<DropComponent>> _dropFilter = default;
        private readonly EcsFilterInject<Inc<StackComponent>> _stackFilter = default;

        private readonly EcsPoolInject<StackComponent> _stackPool = default;
        private readonly EcsPoolInject<PickupComponent> _pickupPool = default;
        private readonly EcsPoolInject<DropComponent> _dropPool = default;
        private readonly EcsPoolInject<PlayerStackComponent> _playerStackPool = default;
        
        private readonly EcsCustomInject<StaticData> _staticData = default;
        
        public void Init(IEcsSystems systems)
        {
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                if (!_playerStackPool.Value.Has(playerEntity)) continue;
                ref var playerStackComponent = ref _playerStackPool.Value.Get(playerEntity);

                var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

                // Pickup logic
                foreach (var pickupEntity in _pickupFilter.Value)
                {
                    if (!_pickupPool.Value.Has(pickupEntity)) continue;

                    foreach (var stackEntity in _stackFilter.Value)
                    {
                        ref var stackComponent = ref _stackPool.Value.Get(stackEntity);
                        if (stackComponent.Stack == null || stackComponent.Stack.Count == 0) continue;
                        var stackTransform = stackComponent.Stack[0].transform;

                        if (Vector3.Distance(playerTransform.position, stackTransform.position) <= _staticData.Value.PickupRadius)
                        {
                            if (playerStackComponent.Stack == null)
                            {
                                playerStackComponent.Stack = new List<GameObject>();
                            }

                            foreach (var obj in stackComponent.Stack)
                            {
                                if (obj == null) continue;
                                var collider = obj.GetComponent<Collider>();
                                if (collider == null) continue;

                                playerStackComponent = UpdatePickupStackVisual(obj, playerTransform,
                                    playerStackComponent, collider);
                            }

                            stackComponent.Stack.Clear();
                            stackComponent.CurrentHeight = 0;
                        }
                    }

                    systems.GetWorld().DelEntity(pickupEntity);
                }

                // Drop logic
                foreach (var dropEntity in _dropFilter.Value)
                {
                    if (!_dropPool.Value.Has(dropEntity)) continue;

                    if (playerStackComponent.Stack != null && playerStackComponent.Stack.Count > 0)
                    {
                        var dropStackEntity = systems.GetWorld().NewEntity();
                        ref var dropStackComponent = ref _stackPool.Value.Add(dropStackEntity);
                        dropStackComponent.Stack = new List<GameObject>();
                        dropStackComponent.CurrentHeight = 0;

                        foreach (var obj in playerStackComponent.Stack)
                        {
                            if (obj == null) continue;
                            var collider = obj.GetComponent<Collider>();
                            if (collider == null) continue;

                            dropStackComponent =
                                UpdateDroppedStackVisual(obj, playerTransform, dropStackComponent, collider);
                        }

                        playerStackComponent.Stack.Clear();
                        playerStackComponent.CurrentHeight = 0;
                    }

                    systems.GetWorld().DelEntity(dropEntity);
                }
            }
        }

        private PlayerStackComponent UpdatePickupStackVisual(GameObject obj, Transform playerTransform,
            PlayerStackComponent playerStackComponent, Collider collider)
        {
            obj.transform.parent = playerTransform;
            obj.transform.localPosition =
                _staticData.Value.PickupOffset + new Vector3(0, playerStackComponent.CurrentHeight, 0);
            playerStackComponent.CurrentHeight += collider.bounds.size.y;
            playerStackComponent.Stack.Add(obj);
            return playerStackComponent;
        }

        private static StackComponent UpdateDroppedStackVisual(GameObject obj, Transform playerTransform,
            StackComponent dropStackComponent, Collider collider)
        {
            obj.transform.parent = null;
            obj.transform.position = playerTransform.position +
                                     new Vector3(0, dropStackComponent.CurrentHeight, 0);
            dropStackComponent.CurrentHeight += collider.bounds.size.y;
            dropStackComponent.Stack.Add(obj);
            return dropStackComponent;
        }
    }
}