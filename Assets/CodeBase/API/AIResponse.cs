using UnityEngine;

namespace CodeBase.API
{
    [System.Serializable]
    public class AIResponse
    {
        [SerializeField] private AIMessage[] aiMessages;

        public AIMessage[] AIMessages => aiMessages;
    }
}