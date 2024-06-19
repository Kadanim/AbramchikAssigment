using ECS.Systems;
using ECS.Systems.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Mono.UI
{
    public class UIManager : MonoBehaviour
    {
        public Button PickupButton;
        public Button DropButton;
        private PlayerInputSystem _playerInputSystem;

        void Start()
        {
            var ecsStartup = GameObject.FindGameObjectWithTag("EcsStartup");
            if (ecsStartup != null)
            {
                var gameStartup = ecsStartup.GetComponent<GameStartup>();
                if (gameStartup != null)
                {
                    _playerInputSystem = gameStartup.PlayerInputSystem;
                }
            }

            if (_playerInputSystem == null)
            {
                Debug.LogError("PlayerInputSystem is not found or not initialized!");
                return;
            }

            PickupButton.onClick.AddListener(OnPickupButtonClicked);
            DropButton.onClick.AddListener(OnDropButtonClicked);
        }

        public void OnPickupButtonClicked()
        {
            _playerInputSystem.Pickup();
        }

        public void OnDropButtonClicked()
        {
            _playerInputSystem.Drop();
        }
    }
}