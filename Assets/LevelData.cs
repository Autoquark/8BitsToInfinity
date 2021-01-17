using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets
{
    static class LevelData
    {
        static LevelData()
        {
            if (_lastLevelIndex == -1)
            {
                _lastLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
            }
        }

        public static int _firstLevelIndex = 2;
        public static int _lastLevelIndex = -1;
        public static int _levelCount => _lastLevelIndex - _firstLevelIndex + 1;

        public static IReadOnlyDictionary<string, string> LevelNamesBySceneName = new Dictionary<string, string>
        {
            { "Level_Catapult", "Catapult" },
            { "Level_Gauntlet", "Gauntlet" },
            { "Level_Pachinko", "Pachinko" },
            { "Level_Seesaw", "Seesaw" },
            { "Level_Spinner1", "Spin to Win" },
            { "Level_Spinner2", "Spinstack" },
            { "Level_ThreeWay", "2 out of 3 Ain't Enough" },
            { "Level_Turnstile", "Turnstile" },
            { "Level_TurnTheRightWay", "Turn the Right Way" },
            { "Level_TurnTurnTurn", "Turn Turn Turn!" },
            { "Level_Tutorial", "Tutorial" },
        };
    }
}
