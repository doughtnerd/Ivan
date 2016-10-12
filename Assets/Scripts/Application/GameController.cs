using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace Arena
{
    public class GameController : Controller<GameMessage>
    {
        private PlayerData playerData;

        private GameObject player;
        private GameObject playerInstance;

        private String savePath;

        void Awake()
        {
            playerData = new PlayerData();
            savePath = UnityEngine.Application.persistentDataPath + "/savegame.dat";
        }

        public override void OnNotification(GameMessage message, GameObject obj, params object[] opData)
        {
            switch (message)
            {
                case GameMessage.NEW_GAME:
                    HandleNewGame();
                    break;
                case GameMessage.START_GAME:
                    HandleStartGame();
                    break;
                case GameMessage.PAUSE_GAME:
                    HandlePauseGame((bool) opData[0]);
                    break;
            }
        }

        void HandlePauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
            //app.NotifyUI(UIMessage.PAUSE_MENU, gameObject, isPaused);
        }

        void HandleStartGame()
        {

        }

        void HandleNewGame()
        {
            playerData = new PlayerData();
        }

        void LoadLevel(int level)
        {
            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }

        class PlayerData
        {

        }

        [Serializable]
        class SerializablePlayerData
        {

        }
    }
}
