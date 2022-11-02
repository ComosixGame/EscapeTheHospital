using UnityEngine;
using TMPro;

public class UpdateDoc : MonoBehaviour
{
    
    private TextMeshProUGUI textPro;
    private GameManager gameManager;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        textPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        gameManager.onUpdateDoc.AddListener(UpdateText);
    }


    private void UpdateText(int docs)
    {
        textPro.text = docs.ToString();
    }

    private void OnDisable() 
    {
        gameManager.onUpdateDoc.AddListener(UpdateText);
    }
}
