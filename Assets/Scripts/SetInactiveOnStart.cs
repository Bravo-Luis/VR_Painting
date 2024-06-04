using UnityEngine;

public class SetInactiveOnStart : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gameObject in gameObjects) gameObject.SetActive(false);
        Destroy(this);
    }
}
