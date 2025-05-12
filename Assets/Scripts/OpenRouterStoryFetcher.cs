using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class OpenRouterStoryFetcher : MonoBehaviour
{
    [Header("OpenRouter Settings")]
    public string apiKey = "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"; 
    public string model = "nousresearch/deephermes-3-mistral-24b-preview:free";

    [TextArea]
    public string generatedStory;

    [Header("TTS")]
    public ElevenlabsAPI elevenLabsTTS;

    private void Start()
    {
        StartCoroutine(FetchStoryFromOpenRouter());
    }

    IEnumerator FetchStoryFromOpenRouter()
    {
        string prompt = "Generate a short, immersive backstory (2–4 sentences) about why the player is trapped in an escape room. Make it creative, mysterious, and replayable.";

        string jsonData = "{\"model\": \"" + model + "\", \"messages\": [{\"role\": \"user\", \"content\": \"" + prompt + "\"}]}";

        UnityWebRequest request = new UnityWebRequest("https://openrouter.ai/api/v1/chat/completions", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        request.SetRequestHeader("HTTP-Referer", "https://yourgameurl-or-unityproject.com"); // required by OpenRouter
        request.SetRequestHeader("OpenRouter-Model", model); // optional but helps direct the request

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("OpenRouter API request failed: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
        else
        {
            string result = request.downloadHandler.text;
            var response = JsonUtility.FromJson<ChatResponseWrapper>(FixJson(result));
            generatedStory = response.choices[0].message.content.Trim();
            Debug.Log("AI-Generated Escape Story:\n" + generatedStory);

            if (elevenLabsTTS != null)
            {
                elevenLabsTTS.GetAudio(generatedStory);
            }
        }
    }

    // Fix JSON for Unity parsing
    private string FixJson(string json)
    {
        return json.Replace("\"object\":\"chat.completion\",", "");
    }

    [System.Serializable]
    public class ChatResponseWrapper
    {
        public Choice[] choices;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
    }

    [System.Serializable]
    public class Message
    {
        public string role;
        public string content;
    }
}
