using System;
using UnityEngine;

namespace Game.Scripts.Runtime.Input
{
    public class InputReader : ScriptableObject
    {
        public event Action OnClick;
    }
}