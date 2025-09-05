
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Sprite = UnityEngine.Sprite;

public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument _UIDocument;
    VisualElement root;
    VisualElement Main, myPick, AIPick, PopupWin, PopupLose, sample;
    Button btn01, btn02, btn03, btn04, btn05, btn06, btn07, resetBtn, startBtn;
    public Sprite spriteA, spriteB;
    public Sprite spriteC;
    public Sprite spriteD;
    public Sprite spriteE;
    public Sprite spriteF;
    public Sprite spriteG;
    

    List<Button> listButton;

    [SerializeField] UnityEvent startEvent;
    void Start()
    {
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
        listButton.Add(btn01);
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
        myPick.style.backgroundImage = btn.style.backgroundImage;
        Label label = myPick.Q<Label>();
        
        
        label.text = btn.name;

        StartCoroutine(RansdomPickByAI());
    }

    public void ShowSessionResult(Ticket ticket)
    {

        Label label = myPick.Q<Label>();

        Debug.Log("my Pick " + label.text);
        Debug.Log("ticket  " + ticket.Name);


        if (label.text == ticket.Name)
        {
            ShowWinPopup();
        }
        else
        {
            ShowLosePopup();
        }

        Debug.Log("winner name " + ticket.Name);

    }

    IEnumerator RansdomPickByAI()
    {
        int aiPick = Random.Range(0, listButton.Count);

        yield return new WaitForSeconds(3);
        AIPick.style.backgroundImage = listButton[aiPick].style.backgroundImage;
        yield return new WaitForSeconds(1);
        resetBtn.SetEnabled(true);
        startBtn.SetEnabled(true);

    }

    public void ShowWinPopup()
    {
        root.RemoveFromClassList("down");
        PopupLose.visible = false;
        PopupWin.visible = true;
        var label = PopupWin.Q<Label>();
        label.text = "you win";
        PopupWin.RemoveFromClassList("dot");

    }
    public void ShowLosePopup()
    {
        root.RemoveFromClassList("down");


        PopupWin.visible = false;
        PopupLose.visible = true;
        var label = PopupLose.Q<Label>();
        label.text = "you lose";
        PopupLose.RemoveFromClassList("dot");


    }

    public void ShowMainUI()
    {
        root.RemoveFromClassList("down");
    }


    private void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.A))
        {
            PopupLose.visible = false;
            PopupWin.visible = true;
            var label = PopupWin.Q<Label>();
            label.text = "you win";
            PopupWin.RemoveFromClassList("dot");

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PopupWin.visible = false;
            PopupLose.visible = true;
            var label = PopupLose.Q<Label>();
            label.text = "you lose";
            PopupLose.RemoveFromClassList("dot");

        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            PopupLose.AddToClassList("dot");
            PopupWin.AddToClassList("dot");

        }*/
    }

}
