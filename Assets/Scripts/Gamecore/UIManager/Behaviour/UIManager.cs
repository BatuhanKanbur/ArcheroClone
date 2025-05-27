using Gamecore.UIManager.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Gamecore.UIManager.Behaviour
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private Transform skillContainer;
        [SerializeField] private Text scoreText;
        public Transform SkillContainer => skillContainer;
        public Text ScoreText => scoreText;
    }
}
