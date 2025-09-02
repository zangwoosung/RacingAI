
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;
using Sprite = UnityEngine.Sprite;

public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument _UIDocument;
    VisualElement root;
    VisualElement myPick;
    Button btn01;
    Button btn02;
    public Sprite spriteA;
    public Sprite spriteB;


    void Start()
    {
        root = _UIDocument.rootVisualElement;

        btn01 = root.Q<Button>("btn01");
        btn02 = root.Q<Button>("btn02");


        myPick = root.Q<VisualElement>("myPick");
       
        btn01.style.backgroundImage = new StyleBackground(spriteA.texture);
        btn02.style.backgroundImage = new StyleBackground(spriteB.texture);
        myPick.style.backgroundImage = new StyleBackground(spriteB.texture);
        
        btn01.clicked += () => SwapBackgrounds();
        btn02.clicked += () => SwapBackgrounds();


       


    }
   
    void SwapBackgrounds()
    {
        var temp = btn01.style.backgroundImage;
        btn01.style.backgroundImage = btn02.style.backgroundImage;
        btn02.style.backgroundImage = temp;

        myPick.style.backgroundImage = btn01.style.backgroundImage; 
    }
    private void Btn01_onClick()
    {
        // Get the current sprites from each button
        Sprite spriteA = btn01.style.backgroundImage.value.sprite;
        Sprite spriteB = btn02.style.backgroundImage.value.sprite;

        // Swap them
        btn01.style.backgroundImage = new StyleBackground(spriteB);
        btn02.style.backgroundImage = new StyleBackground(spriteA);


        Debug.Log("AAAAAaa");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
