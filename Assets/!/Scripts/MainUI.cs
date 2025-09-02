
using System;
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
    Button btn03;
    Button btn04;
    Button btn05;
    Button btn06;
    Button btn07;

    public Sprite spriteA;
    public Sprite spriteB;
    public Sprite spriteC;
    public Sprite spriteD;
    public Sprite spriteE;
    public Sprite spriteF;
    public Sprite spriteG;


    void Start()
    {
        root = _UIDocument.rootVisualElement;

        btn01 = root.Q<Button>("btn01");
        btn02 = root.Q<Button>("btn02");
        btn03 = root.Q<Button>("btn03");
        btn04 = root.Q<Button>("btn04");
        btn05 = root.Q<Button>("btn05");
        btn06 = root.Q<Button>("btn06");
        btn07 = root.Q<Button>("btn07");


        myPick = root.Q<VisualElement>("myPick");
        myPick.style.backgroundImage = btn01.style.backgroundImage;
        btn01.style.backgroundImage = new StyleBackground(spriteA.texture);
        btn02.style.backgroundImage = new StyleBackground(spriteB.texture);
        btn03.style.backgroundImage = new StyleBackground(spriteC.texture);
        btn04.style.backgroundImage = new StyleBackground(spriteD.texture);
        btn05.style.backgroundImage = new StyleBackground(spriteE.texture);
        btn06.style.backgroundImage = new StyleBackground(spriteF.texture);
        btn07.style.backgroundImage = new StyleBackground(spriteG.texture);
      
        btn01.clicked += () => SwapBackgroundswith(btn01);
        btn02.clicked += () => SwapBackgroundswith(btn02);
        btn03.clicked += () => SwapBackgroundswith(btn03);
        btn04.clicked += () => SwapBackgroundswith(btn04);
        btn05.clicked += () => SwapBackgroundswith(btn05);
        btn06.clicked += () => SwapBackgroundswith(btn06);
        btn07.clicked += () => SwapBackgroundswith(btn07);


       


    }

    private void SwapBackgroundswith(Button btn)
    {
        myPick.style.backgroundImage = btn.style.backgroundImage;
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
