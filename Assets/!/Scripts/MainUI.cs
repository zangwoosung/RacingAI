using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    [SerializeField] UIDocument _document;
    
    VisualElement root;

    Button startBtn, ConfirmBtn, Btn01, Btn02, Btn03;



    private void Start()
    {  
        root = _document.rootVisualElement;

        root.AddToClassList("up");

        startBtn = root.Q<Button>("startBtn");
        startBtn.clicked += OnStartBtnClicked;


    }

    private void OnStartBtnClicked()
    {
        root.RemoveFromClassList("up");

    }
}
