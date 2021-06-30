using UnityEngine;
using System;

namespace Levels
{

    [CreateAssetMenu(fileName = "关卡", menuName = "自制资源/关卡")]
    [Serializable]
    public class Levels : ScriptableObject
    {

        public delegate void OnLevelChange(int i);
        public OnLevelChange onLevelChange;

        int _currentLevel;
        public int currentLevel
        {
            get { return _currentLevel; }
            set
            {
                onLevelChange?.Invoke(value);
                _currentLevel = value;
            }
        }
        public Level[] scenes;


    }

    [Serializable]
    public class Level
    {
        public string name;
        public String sceneName;
        //通关奖励
        public int score;

    }
}
