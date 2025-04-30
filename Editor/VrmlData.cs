using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "VrmlData", menuName = "VrWeb/VrmlData")]
public class VrmlData : ScriptableObject
{
    public string AssetBundlePath = "";
    public SceneAsset SceneAsset = null;
    public string OutputPath = "";
    public string Creator = "";
    public string LegalNotice = "All Rights Reserved";
    public string Copyright = "Copyright";
    public int BundleVersion = 1;
    public string RootDomain = "http://localhost";
}
