#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class NewSceneCreator : EditorWindow
{
    protected string m_NewSceneName;
    protected readonly GUIContent m_NameContent = new GUIContent ("New Scene Name");

    [MenuItem("Comosix Kit Tools/Create New Scene", priority = 1)]
    static void Init ()
    {
        NewSceneCreator window = GetWindow<NewSceneCreator>();
        window.Show();
    }
    
    void OnGUI() 
    {
        m_NewSceneName = EditorGUILayout.TextField (m_NameContent, m_NewSceneName);

        GUI.enabled = !string.IsNullOrWhiteSpace(m_NewSceneName);
        if (GUILayout.Button ("Create"))
            CheckAndCreateScene ();
    }

    protected void CheckAndCreateScene ()
    {
        if (EditorApplication.isPlaying)
        {
            Debug.LogWarning ("Cannot create scenes while in play mode.  Exit play mode first.");
            return;
        }

        Scene currentActiveScene = SceneManager.GetActiveScene ();

        CreateScene();
    }

    protected void CreateScene ()
    {
        string[] results = AssetDatabase.FindAssets("_TemplateScene");

        if (results.Length > 0)
        {
            string originalScenePath = AssetDatabase.GUIDToAssetPath(results[0]) +".unity";
            string newScenePath = "Assets/EscapeTheHospital/Scenes/" + m_NewSceneName + ".unity";
            if (!AssetDatabase.CopyAsset(originalScenePath, newScenePath))
            {
                Debug.LogError("Couldn't copy the scene to the new location'");
                return;
            }
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);

            Scene newScene = EditorSceneManager.OpenScene(newScenePath, OpenSceneMode.Single);
            AddSceneToBuildSettings(newScene);
            Close();
        }
    }

    protected void AddSceneToBuildSettings (Scene scene)
    {
        EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;

        EditorBuildSettingsScene[] newBuildScenes = new EditorBuildSettingsScene[buildScenes.Length +1];
        for (int i = 0; i < buildScenes.Length; i++)
        {
            newBuildScenes[i] = buildScenes[i];
        }
        newBuildScenes[buildScenes.Length] = new EditorBuildSettingsScene(scene.path, true);
        EditorBuildSettings.scenes = newBuildScenes;
    }
}
#endif