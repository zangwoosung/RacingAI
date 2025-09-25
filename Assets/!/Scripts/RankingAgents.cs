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
    VisualElement rootVisualElement;

    MultiColumnListView listView;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            List<MyData> testData = new List<MyData>
            {
                new MyData("Agent A", "12:34", null),
                new MyData("Agent B", "23:45", null),
                new MyData("Agent C", "34:56", null),
                new MyData("Agent D", "45:67", null),
                new MyData("Agent E", "56:78", null),
                new MyData("Agent F", "67:89", null),
                new MyData("Agent G", "78:90", null),
                new MyData("Agent H", "89:01", null),
                new MyData("Agent I", "90:12", null),
                new MyData("Agent J", "01:23", null)
            };


            ShowRanking(testData);  
        }   


    }
    private void Start()
    {
        rootVisualElement = _UIDocument.rootVisualElement;
        listView = rootVisualElement.Q<MultiColumnListView>();
    }
    public void ShowRanking(List<MyData> data)
    {
        List<MyData> myDataList = data;

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
