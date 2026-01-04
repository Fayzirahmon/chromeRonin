using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractionType { Pickup, Switch, Talk }
    public InteractionType type;

    [Header("Settings")]
    [Tooltip("If true, triggers immediately on touch (like Coins). If false, requires pressing E (like Chests).")]
    public bool autoInteract = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (autoInteract && other.CompareTag("Player"))
        {
            OnInteract();
        }
    }

    public void OnInteract()
    {
        switch (type)
        {
            case InteractionType.Pickup:
                Debug.Log("Picked up item: " + gameObject.name);
                if (GameManager.Instance != null) 
                {
                    GameManager.Instance.AddCoin(1);
                }
                Destroy(gameObject);
                break;
            case InteractionType.Switch:
                Debug.Log("Switch activated!");
                break;
            case InteractionType.Talk:
                Debug.Log("Hello Traveler!");
                break;
        }
    }
}