using ECS.Systems.ObjectGeneration;
using ECS.Systems.Player;
using ECS.Systems.Stack;
using ECS.Systems.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Mono.Data;
using Mono.UI;
using UnityEngine;

namespace ECS.Systems
{
    public class GameStartup : MonoBehaviour
    {
        public StaticData StaticData;
        private EcsWorld _world;
        private EcsSystems _systems;
        
        public PlayerInputSystem PlayerInputSystem { get; private set; }

        void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            Joystick joystick = FindObjectOfType<Joystick>();
            PlayerInputSystem = new PlayerInputSystem();

            _systems
                .Add(new PlayerInitializationSystem())
                .Add(new UIInitializationSystem())
                .Add(new JoystickInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new StackInteractionSystem())
                .Add(new ObjectGeneratorInitializationSystem())
                .Add(new ObjectGeneratorSystem())
                .Add(new UIUpdateSystem())
                .Add(PlayerInputSystem)
                .Inject(StaticData)
                .Inject(joystick)
                .Init();
        }

        void Update()
        {
            _systems.Run();
        }

        void OnDestroy()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}