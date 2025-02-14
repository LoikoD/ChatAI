using UnityEngine.Networking;

namespace CodeBase.API
{
    public class ResultMessage
    {
        private readonly UnityWebRequest.Result _result;
        private readonly string _message;

        public UnityWebRequest.Result Result => _result;
        public string Message => _message;

        public ResultMessage(UnityWebRequest.Result result, string message)
        {
            _result = result;
            _message = message;
        }
    }
}