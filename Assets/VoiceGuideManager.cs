using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.IO;

public class VoiceGuide : MonoBehaviour
{
    [SerializeField] private string elevenLabsApiKey = "YOUR_API_KEY"; // Replace with your actual API key
    [SerializeField] private string voiceId = "JBFqnCBsd6RMkjVDRZzb"; // Example voice ID
    [SerializeField] private string modelId = "eleven_multilingual_v2"; // Example model ID
    [SerializeField] private AudioSource audioSource;

    private const string apiUrl = "https://api.elevenlabs.io/v1/text-to-speech/";

    public void Speak(string textToSpeak)
    {
        StartCoroutine(GenerateAndPlayAudio(textToSpeak));
    }

    private IEnumerator GenerateAndPlayAudio(string text)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned to VoiceGuide!");
            yield break;
        }

        string url = apiUrl + voiceId;
        string jsonPayload = $"{{\"text\": \"{text}\", \"model_id\": \"{modelId}\"}}";
        byte[] postData = Encoding.UTF8.GetBytes(jsonPayload);

        using (UnityWebRequest www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(postData);
            www.downloadHandler = new DownloadHandlerAudioClip(url, AudioType.MPEG); // Default, might need adjustment
            www.SetRequestHeader("xi-api-key", elevenLabsApiKey);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play();
                Debug.Log("Voice Guide: " + text);
            }
            else
            {
                Debug.LogError($"Eleven Labs API Error: {www.error} - Status Code: {www.responseCode}");
                Debug.LogError($"Response Body: {www.downloadHandler.text}");
            }
        }
    }

    public bool IsSpeaking()
    {
        return audioSource != null && audioSource.isPlaying;
    }
}