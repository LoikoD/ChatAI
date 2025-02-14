using UnityEngine;

namespace CodeBase.API
{
    [System.Serializable]
    public class AIMessage
    {
        [SerializeField] private string generated_text;

        public string GeneratedText => generated_text;
    }
}