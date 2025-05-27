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
            Debug.Log(score);
            if (ScoreText)
                ScoreText.text = $"Score: {score}";
        }
    }
}
