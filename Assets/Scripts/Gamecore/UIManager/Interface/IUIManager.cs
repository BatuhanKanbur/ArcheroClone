using UnityEngine;
using UnityEngine.UI;

namespace Gamecore.UIManager.Interface
{
    public interface IUIManager
    {
        public Transform SkillContainer { get; }
        public Text ScoreText { get; }
        public void UpdateScore(int score)
        {
            if (ScoreText)
                ScoreText.text = $"SCORE : {score}";
        }
        public void ShowEndGamePanel(float score);
    }
}
