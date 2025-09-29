using UnityEngine;

[CreateAssetMenu(fileName = "MyPick", menuName = "Scriptable Objects/MyPick")]
public class MyPick : ScriptableObject
{
    public int rank = 0;    
    public string pick;  // 스프라이트 이름 
    public Texture2D pickTexture; // 이미지 

}
