using UnityEngine;

namespace Game.Scripts.Runtime.Rules
{
    [CreateAssetMenu(menuName = StrattonConstants.MENU_NAME.RULE_MENU)]
    public class RuleSO : ScriptableObject, IRule
    {
        public bool ComputeNext(ICell cell, ICell[] neighbors)
        {
            return false;
        }
    }
}