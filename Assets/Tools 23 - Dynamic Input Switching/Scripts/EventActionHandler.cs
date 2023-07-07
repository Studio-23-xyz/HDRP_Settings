using UnityEngine;

namespace Assets.Tools_23_Dynamic_Input_Switching.Scripts
{
    public abstract class EventActionHandler : MonoBehaviour
    {
        public abstract void HandleInputAction(string contextName);
    }
}
