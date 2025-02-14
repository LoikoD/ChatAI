using UnityEngine;

namespace CodeBase.API
{
    [System.Serializable]
    public class AIRequest
    {
        [SerializeField] private string inputs;

        public string Inputs => inputs;

        public AIRequest(string message)
        {
            inputs = message;
        }
    }
}