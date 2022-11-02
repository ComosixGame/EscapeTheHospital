using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class LoadScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public bool loadOnAwake;
    [HideInInspector] public int nextLevel;
    private AsyncOperation operation;
    private int levelIndex;
    private GameManager gameManager;
    

    private void Awake() 
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        gameManager = GameManager.Instance;
    }

    private void OnEnable() 
    {
        gameManager.onEndGame.AddListener(OnEndGame);
    }

    // Start is called before the first frame update
    private void Start() {
        if(loadOnAwake) {
            int LatestLevel = PlayerData.LoadData().LatestLevel;
            if(LatestLevel == 0) {
                gameManager.UnlockNewLevel(nextLevel);
                LoadNewScene(nextLevel);
            } else {
                LoadNewScene(LatestLevel);
            }
        }
    }

    public void ResetLevel(bool resumeGame) {
        if(resumeGame) {
            gameManager.ResumeGame();
        }
        LoadNewScene(levelIndex);
    }

      public void LoadNextLevel() {
        LoadNewScene(nextLevel);
    }

    public void LoadNewScene(int index) {
        StartCoroutine(LoadAsync(index));
        operation.completed += InitGame;
    }

    IEnumerator LoadAsync(int index) {
        operation = SceneManager.LoadSceneAsync(index);
        loadingScreen.SetActive(true);
        while(!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress/ 0.9f);
            loadingBar.value = progress;
            yield return null;
        }
    }

    private void InitGame(AsyncOperation asyncOperation) {
        gameManager.InitGame();
    }

    private void OnEndGame(bool isWin) {
        if(isWin) {
            gameManager.UnlockNewLevel(nextLevel);
        }
    }

    private void OnDisable() {
        gameManager.onEndGame.RemoveListener(OnEndGame);
    }
}


#if UNITY_EDITOR
    [CustomEditor(typeof(LoadScene))]
    public class EditorLoadScene : Editor {
        string[] scenes;
        private void OnEnable() {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            scenes = new string[sceneCount];

            for( int i = 0; i < sceneCount; i++ )
            {
                scenes[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
        }
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            LoadScene loadScene = target as LoadScene;
 
            EditorGUI.BeginChangeCheck();
            int indexScene = EditorGUILayout.Popup("Next Level" ,loadScene.nextLevel, scenes);
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(loadScene, "Update Next Level");
                loadScene.nextLevel = indexScene;
            }
        }
    }
#endif


