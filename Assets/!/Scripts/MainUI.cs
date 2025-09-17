
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Sprite = UnityEngine.Sprite;

public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] UnityEvent startEvent;
    [SerializeField] AgentManager agentManager;
    public MyPick myPickSO;
    public static event Action<string> SessionClearEvent;
    public Sprite spriteA, spriteB, spriteC, spriteD, spriteE, spriteF, spriteG;

    VisualElement root, Main, myPick, AIPick, PopupWin, PopupLose, sample;
    Button btn01, btn02, btn03, btn04, btn05, btn06, btn07, resetBtn, startBtn;
    List<Button> listButton;
    Sprite sprite;
    Texture2D texture;


    Dictionary<string, Sprite> SpriteDict = new Dictionary<string, Sprite>();

    void Start()
    {
        sprite = spriteA;
        root = _UIDocument.rootVisualElement;

        root.AddToClassList("default");
        btn01 = root.Q<Button>("btn01");
        btn02 = root.Q<Button>("btn02");
        btn03 = root.Q<Button>("btn03");
        btn04 = root.Q<Button>("btn04");
        btn05 = root.Q<Button>("btn05");
        btn06 = root.Q<Button>("btn06");
        btn07 = root.Q<Button>("btn07");
        startBtn = root.Q<Button>("startBtn");
        resetBtn = root.Q<Button>("resetBtn");

        PopupWin = root.Q<VisualElement>("PopupWin");

        var CloseWin = PopupWin.Q<Button>();
        CloseWin.clicked += () =>
        {
            PopupWin.AddToClassList("dot");
        };

        PopupLose = root.Q<VisualElement>("PopupLose");
        var closeLose = PopupLose.Q<Button>();
        closeLose.clicked += () =>
        {
            PopupLose.AddToClassList("dot");
        };

        sample = root.Q<VisualElement>("sample");


        PopupWin.visible = false;
        PopupLose.visible = false;
        sample.visible = false;

        myPick = root.Q<VisualElement>("myPick");
        AIPick = root.Q<VisualElement>("AIPick");

        myPick.style.backgroundImage = btn01.style.backgroundImage;
        btn01.style.backgroundImage = new StyleBackground(spriteA.texture);
        btn02.style.backgroundImage = new StyleBackground(spriteB.texture);
        btn03.style.backgroundImage = new StyleBackground(spriteC.texture);
        btn04.style.backgroundImage = new StyleBackground(spriteD.texture);
        btn05.style.backgroundImage = new StyleBackground(spriteE.texture);
        btn06.style.backgroundImage = new StyleBackground(spriteF.texture);
        btn07.style.backgroundImage = new StyleBackground(spriteG.texture);
        
        SpriteDict.Add("bear", spriteA);
        SpriteDict.Add("cat",  spriteB);
        SpriteDict.Add("dog",  spriteC);
        SpriteDict.Add("eagle", spriteD);
        SpriteDict.Add("puma", spriteE);
        SpriteDict.Add("shark", spriteF);
        SpriteDict.Add("tiger", spriteG);


        btn01.clicked += () => SwapBackgroundswith(btn01);
        btn02.clicked += () => SwapBackgroundswith(btn02);
        btn03.clicked += () => SwapBackgroundswith(btn03);
        btn04.clicked += () => SwapBackgroundswith(btn04);
        btn05.clicked += () => SwapBackgroundswith(btn05);
        btn06.clicked += () => SwapBackgroundswith(btn06);
        btn07.clicked += () => SwapBackgroundswith(btn07);

        startBtn.clicked += () =>
        {

            startEvent.Invoke();

            root.AddToClassList("down");

        };
        resetBtn.clicked += () => ResetPicks();
        listButton = new List<Button>();
        listButton.Add(btn01);  // label, sprite
        listButton.Add(btn02);
        listButton.Add(btn03);
        listButton.Add(btn04);
        listButton.Add(btn05);
        listButton.Add(btn06);
        listButton.Add(btn07);

        startBtn.SetEnabled(false);
        resetBtn.SetEnabled(false);
    }
    public void ResultPopup(Action action, string message = "who win?")
    {
        var closePopupBtn = PopupWin.Q<Button>("closeBtn");
        var body = PopupWin.Q<VisualElement>("body");
        body.Remove(closePopupBtn);
        Label label = PopupWin.Q<Label>();
        label.text = message;

        Button button = new Button();
        PopupWin.Add(button);
        button.clicked += () => { action(); };

    }


    private void ResetPicks()
    {
        myPick.style.backgroundImage = null;
        AIPick.style.backgroundImage = null;
    }

    private void SwapBackgroundswith(Button btn)
    {
        StopAllCoroutines();
        AIPick.style.backgroundImage = null; 

        myPick.style.backgroundImage = btn.style.backgroundImage;
        
        Label label = myPick.Q<Label>();
        
        SpriteRenderer spriteRenderer = new SpriteRenderer();
       
        var obj = btn.style.backgroundImage.value.texture;

        label.text = obj.name;
        
        //TODO.... 스크립터블 오브젝트에 담기.... 스크립터블오브젝 코드를 잘 이해하면 코드가 쉬워짐....  이게 없던 시절에는 코드가 10줄정도 더
        myPickSO.pick = btn.style.backgroundImage.value.texture.name;
        myPickSO.pickTexture = obj;


        StartCoroutine(RansdomPickByAI());
    }       
      
   
    public void TicketAndSprite(Ticket ticket, SpriteRenderer sr)
    {      

        root.RemoveFromClassList("down");

        PopupLose.visible = false;
        PopupWin.visible = true;

        this.texture = SpriteDict[ticket.Name].texture;
        
        Label pick = myPick.Q<Label>();
        var photo = PopupWin.Q<VisualElement>("photo");
        photo.style.backgroundImage = new StyleBackground(texture);

        Debug.Log($"{pick.text}  ***  ticket name {ticket.Name}  sprite name  {this.texture.name}");

        var label = PopupWin.Q<Label>();

        int index = 0;




        if (pick.text == ticket.Name) //ticket.Name)
        {
            label.text = "You won!";
            //SessionClearEvent?.Invoke(index.ToString());
           

        }
        else
        {
            label.text = "You lost!";
        }

        SessionClearEvent?.Invoke(index.ToString());

        PopupWin.RemoveFromClassList("dot");

    }
  

    IEnumerator RansdomPickByAI()
    {
        int aiPick = Random.Range(0, listButton.Count);

        yield return new WaitForSeconds(2);

        AIPick.style.backgroundImage = listButton[aiPick].style.backgroundImage;
        
        yield return new WaitForSeconds(1);
        resetBtn.SetEnabled(true);
        startBtn.SetEnabled(true);

    }   

    public void ShowMainUI()
    {
        root.RemoveFromClassList("down");
    }

}
