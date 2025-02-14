using UnityEngine;

namespace CodeBase.API
{
    [System.Serializable]
    public class AIErrorResponse
    {
        [SerializeField] private string error;

        public string Error => error;
    }
}