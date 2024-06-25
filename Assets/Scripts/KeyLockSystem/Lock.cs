using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public string keyType;
    public Text KeyNameText;

    Canvas m_Canvas;

    void Start()
    {
        KeyNameText.text = keyType;

        m_Canvas = KeyNameText.GetComponentInParent<Canvas>();
        m_Canvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_Canvas.gameObject.SetActive(true);
        
        var keychain = other.GetComponent<Keychain>();

        if (keychain != null && keychain.HaveKey(keyType))
        {
            keychain.UseKey(keyType);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_Canvas.gameObject.SetActive(false);
    }
} 