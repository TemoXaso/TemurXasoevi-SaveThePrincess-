using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    
    [Header("Scene Management")]
    public string mainMenuSceneName = "MainMenu";

    [Header("Audio Settings")]
    public AudioClip dialogueSound;
    // ✨ EDITED: Range is now increased to allow volume amplification up to 5
    [SerializeField, Range(0f, 5f)] private float dialogueVolume = 4f;
    public AudioSource backgroundMusic;

    private int index;
    // Prevents the scene change from being triggered multiple times
    private bool isDialogueFinished = false; 

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset state when dialogue begins
            isDialogueFinished = false; 
            index = 0;
            dialoguePanel.SetActive(true);
            dialogueText.text = lines[index];

            if (backgroundMusic != null)
                backgroundMusic.Pause();

            PlayDialogueSound();
        }
    }

    void Update()
    {
        // Only process input if dialogue is active and not yet finished
        if (dialoguePanel.activeSelf && !isDialogueFinished && Input.GetKeyDown(KeyCode.E))
        {
            index++;

            if (index < lines.Length)
            {
                dialogueText.text = lines[index];
                PlayDialogueSound();
            }
            else
            {
                // This is the end of the dialogue
                isDialogueFinished = true; // Mark as finished
                dialoguePanel.SetActive(false);

                if (backgroundMusic != null)
                    backgroundMusic.UnPause();
                
                // Start the process to return to the menu
                StartCoroutine(ReturnToMenuAfterDelay());
            }
        }
    }
    
    // Coroutine to handle the delay
    private System.Collections.IEnumerator ReturnToMenuAfterDelay()
    {
        // Calculate the total wait time
        float soundDuration = (dialogueSound != null) ? dialogueSound.length : 0f;
        float totalDelay = soundDuration + 1f; // Sound length + 1 second

        // Wait for the calculated duration
        yield return new WaitForSeconds(totalDelay);

        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }

    void PlayDialogueSound()
    {
        if (dialogueSound != null)
        {
            AudioSource.PlayClipAtPoint(dialogueSound, transform.position, dialogueVolume);
        }
    }
}