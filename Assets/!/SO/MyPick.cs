using UnityEngine;

[CreateAssetMenu(fileName = "MyPick", menuName = "Scriptable Objects/MyPick")]
public class MyPick : ScriptableObject
{
    public int rank = 0;    
    public string pick;
    public Texture2D pickTexture;

}
