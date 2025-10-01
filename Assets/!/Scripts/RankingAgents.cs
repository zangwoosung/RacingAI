using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class MyData
{
    public string Name { get; set; }
    public string Time { get; set; }
    public Sprite Icon { get; set; }

    public MyData(string name, string time, Sprite icon)
    {
        Name = name;
        Time = time;
        Icon = icon;
    }
}


public class RankingAgents : MonoBehaviour
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] MainUI  _mainUI;
    VisualElement rootVisualElement;
    MultiColumnListView listView;

  
    private void Start()
    {
        rootVisualElement = _UIDocument.rootVisualElement;
        listView = rootVisualElement.Q<MultiColumnListView>();
        var closebtn = rootVisualElement.Q<Button>("CloseBtn"); 

        closebtn.clicked += () =>
        {
            rootVisualElement.visible = false;
            _mainUI.ShowMainUI();
        };

        AgentManager.OnSessionEndWithRankingEvent += ShowRanking;
    }

   

    public void ShowRanking(List<MyData> data)
    {
        List<MyData> myDataList = data;
        rootVisualElement.visible = true;
        Debug.Log("myDataList.count " + myDataList.Count);
        int columnCount = listView.columns.Count;

        if (columnCount > 0)
        {
            listView.columns.Clear();
        }

        // Column 1: Title
        listView.columns.Add(new Column
        {
            name = "title",
            title = "Title",
            width = 150,
            makeCell = () => new Label(),
            bindCell = (element, index) =>
            {
                (element as Label).text = myDataList[index].Name;
            }
        });

        // Column 2: Description
        listView.columns.Add(new Column
        {
            name = "description",
            title = "Description",
            width = 250,
            makeCell = () => new Label(),
            bindCell = (element, index) =>
            {
                (element as Label).text = myDataList[index].Time;
            }
        });

        // Column 3: Icon (Sprite)
        listView.columns.Add(new Column
        {
            name = "icon",
            title = "Icon",
            width = 60,
            makeCell = () => new Image { scaleMode = ScaleMode.ScaleToFit },
            bindCell = (element, index) =>
            {
                var img = element as Image;
                img.image = myDataList[index].Icon?.texture;
            }
        });

        listView.itemsSource = myDataList;      

    }    
}
