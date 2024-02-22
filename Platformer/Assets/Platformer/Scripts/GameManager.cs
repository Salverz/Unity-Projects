using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public Camera mainCamera;
    public GameObject questionBlock;
    public Material questionMaterial;
    private int coins = 0;
    private float lastUpdateTime = 0;

    // Update is called once per frame
    void Update()
    {
        // Timer Text
        int intTime = 400 - (int)Time.realtimeSinceStartup;
        string timeStr = $"TIME\n{intTime}";
        timerText.text = timeStr;

        // Clicking on screen
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1000);

            if (hit.collider != null) {
                if (hit.collider.name == "Brick(Clone)") 
                {
                    Debug.Log(hit.collider.name);
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.name == "Question(Clone)")
                {
                    Debug.Log(hit.collider.name);
                    coins++;
                    string coinStr = $"COIN: x{coins}";
                    coinText.text = coinStr;
                }
            }
        }

        // Camera scrolling
        if (Input.GetKey(KeyCode.RightArrow)) {
            Transform cameraTransform = mainCamera.GetComponent<Transform>();
            Vector3 currentCameraPos = cameraTransform.position;
            Vector3 newCameraPos = new Vector3(currentCameraPos.x + .1f, currentCameraPos.y, currentCameraPos.z);
            cameraTransform.position = newCameraPos;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            Transform cameraTransform = mainCamera.GetComponent<Transform>();
            Vector3 currentCameraPos = cameraTransform.position;
            Vector3 newCameraPos = new Vector3(currentCameraPos.x - .1f, currentCameraPos.y, currentCameraPos.z);
            cameraTransform.position = newCameraPos;
        }

        // Animate Question Block
        float time = Time.realtimeSinceStartup;
        if (lastUpdateTime + .2f < time) {
            int textureId = Shader.PropertyToID("_MainTex");
            Vector2 newTextureOffset = new Vector2(0, Mathf.Round((questionMaterial.GetTextureOffset(textureId).y + .2f) * 10) / 10);
            questionMaterial.SetTextureOffset(textureId, newTextureOffset);
            lastUpdateTime = time;
        }
    }
}
