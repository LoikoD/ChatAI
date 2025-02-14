using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.API
{
    public class APIHandler
    {

        public readonly string _apiUrl;
        public readonly string _apiToken;

        public APIHandler(string apiUrl, string apiToken)
        {
            _apiUrl = apiUrl;
            _apiToken = apiToken;
        }

        public async Task<string> GetAIResponse(string message)
        {
            AIRequest aiRequest = new(message);
            using UnityWebRequest request = UnityWebRequest.Post(_apiUrl, JsonUtility.ToJson(aiRequest), "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _apiToken);

            await request.SendWebRequest();

            string response = request.downloadHandler.text;
            if (request.result == UnityWebRequest.Result.Success)
            {
                AIResponse aiResponse = JsonUtility.FromJson<AIResponse>("{\"aiMessages\":" + response + "}");

                if (aiResponse.AIMessages.Length > 0)
                {
                    return aiResponse.AIMessages[0].GeneratedText;
                }
                else
                {
                    throw new APIException("Ошибка обработки ответа!");
                }
            }
            else
            {
                AIErrorResponse aiResponse = JsonUtility.FromJson<AIErrorResponse>(response);
                string errorMessage = string.IsNullOrEmpty(aiResponse.Error) ? "Ошибка соединения!" : aiResponse.Error;
                throw new APIException(errorMessage);
            }
        }
    }
}