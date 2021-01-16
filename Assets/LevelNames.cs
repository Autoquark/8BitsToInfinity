using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    static class LevelNames
    {
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
