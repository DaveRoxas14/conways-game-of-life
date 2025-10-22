using UnityEngine;

namespace Game.Scripts.Runtime.Rules
{
    [CreateAssetMenu(menuName = StrattonConstants.MENU_NAME.RULE_MENU)]
    public class RuleSO : ScriptableObject, IRule
    {
        public bool ComputeNext(ICell cell, ICell[] neighbors)
        {
            var alive = 0;
            
            foreach (var neighbor in neighbors)
            {
                if (neighbor.IsAlive)
                    alive++;
            }

            if (cell.IsAlive)
            {
                return alive is 2 or 3;
            }
            else
            {
                return alive is 3;
            }
        }
    }
}