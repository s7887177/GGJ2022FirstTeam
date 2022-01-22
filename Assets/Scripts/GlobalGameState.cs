using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feng
{
    [CreateAssetMenu(fileName= "GlobalGameState",menuName = "Custom/GlobalGameState")]
    public class GlobalGameState : ScriptableObject
    {
    public string SceneTransitionString = "";
    }
}
