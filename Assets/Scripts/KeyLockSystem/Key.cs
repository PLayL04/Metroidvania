using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public string keyType;
    public Text KeyNameText;

    void OnEnable()
    {
        KeyNameText.text = keyType;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var keychain = other.GetComponent<Keychain>();

        if (keychain != null)
        {
            keychain.GrabbedKey(keyType);
            Destroy(gameObject);
        }
    }
}
