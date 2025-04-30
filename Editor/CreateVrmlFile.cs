using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditorInternal;
using UnityEngine.SceneManagement;

public class CreateVrmlFile : EditorWindow
{
    private const string ROOT_DOMAIN = "%ROOT_DOMAIN%";
    private const string CREATOR = "%CREATOR%";
    private const string LEGAL_NOTICE = "%LEGAL_NOTICE%";
    private const string COPYRIGHT = "%COPYRIGHT%";
    private const string BUNDLE_VERSION = "%BUNDLE_VERSION%";
    private const string ASSET_BUNDLE_NAME = "%ASSET_BUNDLE_NAME%";
    private const string SCENE_PATH = "%SCENE_PATH%";

   
    private static VrmlData m_Data = null;
    private static SerializedObject m_SerializedObject;

    [MenuItem( "VRWeb/Create VRML File" )]
    static void BuildVRMLFile()
    {
        Scene scene = SceneManager.GetActiveScene();

        VrmlData[] vrmlList = Resources.FindObjectsOfTypeAll<VrmlData>();
        if (vrmlList.Length == 0)
            return;

        m_Data = vrmlList[0];

        if (scene.IsValid())
            m_Data.SceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);

        var window = EditorWindow.GetWindow( typeof(CreateVrmlFile), false, "VRML File Creator" );
        window.minSize = new Vector2( 700, 500 );
        window.maxSize = new Vector2(700, 500 );

    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Creator Info", EditorStyles.boldLabel);

        m_Data.Creator = EditorGUILayout.TextField( "Creator", m_Data.Creator );
        m_Data.LegalNotice = EditorGUILayout.TextField( "Legal Notice", m_Data.LegalNotice, GUILayout.Height( 50 ) );
        m_Data.Copyright = EditorGUILayout.TextField("Copyright", m_Data.Copyright );

        EditorGUILayout.Space( 10 );

        EditorGUILayout.LabelField( "Web", EditorStyles.boldLabel );
        m_Data.RootDomain = EditorGUILayout.TextField( "Root Domain", m_Data.RootDomain );

        EditorGUILayout.Space( 10 );

        EditorGUILayout.LabelField( "Asset", EditorStyles.boldLabel );
        
        
        EditorGUILayout.BeginHorizontal();

        m_Data.AssetBundlePath = EditorGUILayout.TextField("Asset Bundle", m_Data.AssetBundlePath);
        if ( GUILayout.Button( "...", GUILayout.Width( 40 ) ) )
            m_Data.AssetBundlePath = EditorUtility.OpenFilePanel("Asset Bundle", m_Data.AssetBundlePath, "" );

        EditorGUILayout.EndHorizontal();

        m_Data.BundleVersion = EditorGUILayout.IntField( "Bundle Version", m_Data.BundleVersion );
        
        EditorGUILayout.Space( 10 );

        m_Data.SceneAsset = EditorGUILayout.ObjectField( "Scene", m_Data.SceneAsset, typeof( SceneAsset ) )
            as SceneAsset;

        EditorGUILayout.Space( 10 );

        EditorGUILayout.LabelField( "VRML Creation", EditorStyles.boldLabel );

        EditorGUILayout.BeginHorizontal();
        m_Data.OutputPath = EditorGUILayout.TextField("Output Path", m_Data.OutputPath);

        if ( GUILayout.Button( "...", GUILayout.Width(40) ) )
            m_Data.OutputPath = EditorUtility.OpenFolderPanel("Output Path", m_Data.OutputPath, "");

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(50);

        GUI.enabled = IsValid();

        if ( GUILayout.Button( "Copy VRML and bundle File", GUILayout.Height( 50 ) ) )
            SaveVrmlFile( false );

        if ( GUILayout.Button("Run Hopper with VRML and Bundle File", GUILayout.Height( 50 ) ) )
            SaveVrmlFile( true );

        GUI.enabled = true;
    }

    private void SaveVrmlFile( bool runHopper)
    {
        string vrmlTemplate = Resources.Load<TextAsset>("VRML v2").text;
        string assetBundleName = Path.GetFileName(m_Data.AssetBundlePath);

        vrmlTemplate = vrmlTemplate.Replace(ROOT_DOMAIN, m_Data.RootDomain);
        vrmlTemplate = vrmlTemplate.Replace(CREATOR, m_Data.Creator);
        vrmlTemplate = vrmlTemplate.Replace(LEGAL_NOTICE, m_Data.LegalNotice);
        vrmlTemplate = vrmlTemplate.Replace(COPYRIGHT, m_Data.Copyright);
        vrmlTemplate = vrmlTemplate.Replace(BUNDLE_VERSION, m_Data.BundleVersion.ToString());
        vrmlTemplate = vrmlTemplate.Replace(ASSET_BUNDLE_NAME, assetBundleName);
        vrmlTemplate = vrmlTemplate.Replace(SCENE_PATH, AssetDatabase.GetAssetPath(m_Data.SceneAsset));

        string outputPath = Path.Combine(m_Data.OutputPath, $"{m_Data.SceneAsset.name}.vrml");
        File.WriteAllText(outputPath, vrmlTemplate);

        string sourcePath = m_Data.AssetBundlePath;
        string destPath = Path.Combine( Directory.GetParent( outputPath ).FullName, Path.GetFileName(m_Data.AssetBundlePath));

        File.Copy(sourcePath, destPath, true);
        m_Data.BundleVersion++;

        if (runHopper)
        {
            string url = m_Data.RootDomain + "/" + m_Data.SceneAsset.name + ".vrml";
            string callableUrl = "Hopper:" + Uri.EscapeDataString( url );
            Application.OpenURL( callableUrl );
        }
    }

    bool IsValid()
    {
        return m_Data.SceneAsset != null &&
               !string.IsNullOrEmpty( m_Data.AssetBundlePath ) &&
               File.Exists( m_Data.AssetBundlePath ) &&
               !string.IsNullOrEmpty( m_Data.OutputPath ) &&
               Directory.Exists( m_Data.OutputPath ) &&
               !string.IsNullOrEmpty( m_Data.RootDomain ) &&
               !string.IsNullOrEmpty( m_Data.Creator ) &&
               !string.IsNullOrEmpty( m_Data.LegalNotice ) &&
               !string.IsNullOrEmpty( m_Data.Copyright );

    }
}
