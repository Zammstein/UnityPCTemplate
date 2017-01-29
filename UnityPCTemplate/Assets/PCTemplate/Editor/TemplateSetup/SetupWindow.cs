using UnityEditor;
using UnityEngine;

public class TemplateSetupWindow : EditorWindow {
    
    private bool steam;

    private bool networkGroup;
    private bool networkIdentifyingConnection;

    private bool saveLoadSystem;
    private bool sceneFading;
    private bool splashscreen;

    private bool optionscreenGroup;
    private bool optionscreenAudio;
    private bool optionscreenControls;
    private bool optionscreenGame;
    private bool optionscreenGraphics;

    private bool mainMenuscreen;

    [MenuItem("Window/Template Setup")]
    public static void ShowWindow() {
        GetWindow(typeof(TemplateSetupWindow));
    }
   
    static void Init() {
        TemplateSetupWindow window = (TemplateSetupWindow)GetWindow(typeof(TemplateSetupWindow));
        window.Show();
    }

    void OnGUI() {
        GUILayout.Label("Features", EditorStyles.boldLabel);

        steam = EditorGUILayout.Toggle("Steam", steam);

        networkGroup = EditorGUILayout.BeginToggleGroup("Networking", networkGroup);
        networkIdentifyingConnection = EditorGUILayout.Toggle("IdentifyingConnection", networkIdentifyingConnection);
        EditorGUILayout.EndToggleGroup();

        saveLoadSystem = EditorGUILayout.Toggle("Save/load system", saveLoadSystem);
        sceneFading = EditorGUILayout.Toggle("Smooth scene fading", sceneFading);
        splashscreen = EditorGUILayout.Toggle("Splashscreen", splashscreen);

        optionscreenGroup = EditorGUILayout.BeginToggleGroup("Option menu", optionscreenGroup);
        optionscreenAudio = EditorGUILayout.Toggle("Audio", optionscreenAudio);
        optionscreenControls = EditorGUILayout.Toggle("Controls", optionscreenControls);
        optionscreenGame = EditorGUILayout.Toggle("Gameplay", optionscreenGame);
        optionscreenGraphics = EditorGUILayout.Toggle("Graphics", optionscreenGraphics);
        EditorGUILayout.EndToggleGroup();

        mainMenuscreen = EditorGUILayout.Toggle("Main menuscreen", mainMenuscreen);
    }

    void Update() {
        if (!networkGroup)
            networkIdentifyingConnection = false;
        if (!optionscreenGroup) {
            optionscreenAudio = false;
            optionscreenControls = false;
            optionscreenGame = false;
            optionscreenGraphics = false;
        }
    }
}
