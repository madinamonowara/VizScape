using UnityEngine;
using Convai.Scripts.Runtime.Core;

/// <summary>
/// Disables the NPC's collider for 40 seconds at the start of the scene,
/// disables collider whenever the NPC is talking,
/// and safely re-enables it when the talk button is pressed (or after speech ends).
/// All collider access is null-checked.
/// </summary>
public class SafeNPCInteraction_AutoDisableWithStartup : MonoBehaviour
{
    public ConvaiNPC convaiNPC;
    public Collider npcCollider;

    private bool wasTalkingLastFrame = false;
    private bool startupDisable = true;
    private float startupTimer = 40f;

    void Reset()
    {
        convaiNPC = GetComponent<ConvaiNPC>();
        npcCollider = GetComponent<Collider>();
    }

    void OnEnable()
    {
        if (ConvaiInputManager.Instance != null)
            ConvaiInputManager.Instance.talkKeyInteract += OnTalkKeyInteract;
    }

    void OnDisable()
    {
        if (ConvaiInputManager.Instance != null)
            ConvaiInputManager.Instance.talkKeyInteract -= OnTalkKeyInteract;
    }

    void Start()
    {
        // Disable collider at the beginning of the scene, if possible
        if (npcCollider != null)
            npcCollider.enabled = false;

        startupDisable = true;
        startupTimer = 40f;
    }

    void Update()
    {
        if (convaiNPC == null || npcCollider == null)
            return;

        // Handle initial 40s disable at scene start
        if (startupDisable)
        {
            startupTimer -= Time.deltaTime;
            if (startupTimer <= 0f)
            {
                npcCollider.enabled = true; // Enable after 40 seconds if not talking
                startupDisable = false;
            }
            return; // Don't process talking logic during startup disable period
        }

        // Disable collider as soon as NPC starts talking
        if (convaiNPC.IsCharacterTalking)
        {
            npcCollider.enabled = false;
            wasTalkingLastFrame = true;
        }
        // Optionally, automatically re-enable collider after talking ends
        else if (wasTalkingLastFrame && !convaiNPC.IsCharacterTalking)
        {
            npcCollider.enabled = true;
            wasTalkingLastFrame = false;
        }
    }

    /// <summary>
    /// Safe button handler: re-enables collider if missing or disabled.
    /// </summary>
    void OnTalkKeyInteract(bool pressed)
    {
        if (pressed && npcCollider != null && !npcCollider.enabled)
        {
            npcCollider.enabled = true;
            Debug.Log("Collider re-enabled by talk button press.");
        }
    }
}