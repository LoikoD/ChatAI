using CodeBase.API;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeBase
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private RectTransform _chatContent;
        [SerializeField] private GameObject _loadMessage;

        [SerializeField] private List<MesssageBubbleStaticData> _messageBubblesData;
        [SerializeField] private APIStaticData _apiConfig;

        private const float Msg_Max_Width_Percentage = 0.66f;

        private readonly List<GameObject> _messageBubbles = new();

        private int _activeRequests = 0;
        private APIHandler _apiHandler;

        private void Awake()
        {
            _apiHandler = new(_apiConfig.Url, _apiConfig.Token);
        }

        public async void SendChatMessage()
        {
            string text = _inputField.text;

            if (string.IsNullOrEmpty(text))
                return;

            AddMessage(text, MessageBubbleType.User);

            _inputField.text = "";
            _inputField.ActivateInputField();

            try
            {
                _activeRequests++;
                ShowLoadMessage();
                string responseMessage = await _apiHandler.GetAIResponse(text);
                AddMessage(responseMessage, MessageBubbleType.Bot);
            }
            catch (APIException ex)
            {
                AddMessage(ex.Message, MessageBubbleType.Error);
            }

            _activeRequests--;
            if (_activeRequests == 0)
            {
                HideLoadMessage();
            }
            else
            {
                _loadMessage.transform.SetAsLastSibling();
            }
        }

        private void AddMessage(string text, MessageBubbleType bubbleType)
        {
            GameObject msgBubble = Instantiate(_messageBubblesData.Find(x => x.Type == bubbleType).Prefab, _chatContent);

            TMP_Text tmpText = msgBubble.GetComponentInChildren<TMP_Text>();

            SetText(text, tmpText);

            _messageBubbles.Add(msgBubble);
        }

        private void SetText(string text, TMP_Text tmpTextUi)
        {
            string currentText = "";
            string[] words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(currentText))
                {
                    string prevText = currentText;
                    string testText = currentText + words[i] + " ";

                    if (ExceedsLineWidth(testText, tmpTextUi))
                    {
                        currentText = prevText.TrimEnd() + "\n";
                    }
                }
                currentText += words[i] + " ";
            }

            tmpTextUi.text = currentText;
        }

        private bool ExceedsLineWidth(string testText, TMP_Text tmpTextUi)
        {
            tmpTextUi.text = testText;
            tmpTextUi.ForceMeshUpdate();

            TMP_TextInfo textInfo = tmpTextUi.textInfo;
            float currentLineWidth = textInfo.lineCount > 0
                ? textInfo.lineInfo[textInfo.lineCount - 1].length
                : 0;

            return currentLineWidth > _chatContent.rect.width * Msg_Max_Width_Percentage;
        }

        private void ShowLoadMessage()
        {
            _loadMessage.transform.SetAsLastSibling();
            _loadMessage.SetActive(true);
        }

        private void HideLoadMessage()
        {
            _loadMessage.SetActive(false);
        }

    }
}