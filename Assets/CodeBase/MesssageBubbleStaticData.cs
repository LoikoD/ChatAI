using UnityEngine;

namespace CodeBase
{
    [CreateAssetMenu(fileName = "MesssageBubble", menuName = "StaticData/MesssageBubble")]
    public class MesssageBubbleStaticData : ScriptableObject
    {
        public MessageBubbleType Type;
        public GameObject Prefab;
    }
}