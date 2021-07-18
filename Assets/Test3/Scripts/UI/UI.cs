using UnityEngine;
using UnityEngine.UI;

namespace Test3
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _gameHud;
        [SerializeField] private GameObject _completedLevel;
        [SerializeField] private GameObject _gameOver;

        [Header("Game HUD")]
        [SerializeField] private Slider _enemiesCounterSlider;
        [SerializeField] private Text _currentLevelText;
        [SerializeField] private Text _nextLevelText;


        private void Start()
        {
            Game.ChangeGameStateEvent += OnChangedGameState;
            Game.ChangeEnemiesCountEvent += OnChangedEnemiesCount;
        }

        private void OnDestroy()
        {
            Game.ChangeGameStateEvent -= OnChangedGameState;
            Game.ChangeEnemiesCountEvent -= OnChangedEnemiesCount;
        }

        private void OnChangedGameState(GAME_STATE gameState)
        {
            _mainMenu.SetActive(gameState == GAME_STATE.MAIN_MENU);
            _gameHud.SetActive(gameState == GAME_STATE.GAME || gameState == GAME_STATE.WAITING);
            _completedLevel.SetActive(gameState == GAME_STATE.COMPLETED);
            _gameOver.SetActive(gameState == GAME_STATE.GAME_OVER);
        }

        private void OnChangedEnemiesCount(int levelIndex, int enemiesCount, int aliveEnemiesCount)
        {
            _enemiesCounterSlider.maxValue = enemiesCount;
            _enemiesCounterSlider.value = enemiesCount - aliveEnemiesCount;


            _currentLevelText.text = levelIndex.ToString();
            _nextLevelText.text = (levelIndex + 1).ToString();
        }

    }
}
