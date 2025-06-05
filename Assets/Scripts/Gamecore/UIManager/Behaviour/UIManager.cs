using Gamecore.UIManager.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gamecore.UIManager.Behaviour
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private Transform skillContainer;
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private Text endGameText;
        public Transform SkillContainer => skillContainer;
        public Text ScoreText => scoreText;
        public void ShowEndGamePanel(float score)
        {
            endGamePanel.SetActive(true);
            endGameText.text = $"YOU DESTROYED <color=red>{score}</color> ENEMIES!";
        }
        public void OnRestartButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
