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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<MyData> myDataList = new List<MyData>
{
    new MyData("Sword", "1", Resources.Load<Sprite>("Icons/bear1")),
    new MyData("Shield", "1",Resources.Load<Sprite>("Icons/bear1")),
    new MyData("Potion", "1",Resources.Load<Sprite>("Icons/bear1"))
};


        rootVisualElement = _UIDocument.rootVisualElement;
        var listView = rootVisualElement.Q<MultiColumnListView>();

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

        //// Name column
        //listView.columns[0].makeCell = () => new Label();
        //listView.columns[0].bindCell = (element, index) =>
        //{
        //    (element as Label).text = myDataList[index].Name;
        //};

        //// Icon column
        //listView.columns[1].makeCell = () => new Image();
        //listView.columns[1].bindCell = (element, index) =>
        //{
        //    (element as Image).image = myDataList[index].Icon.texture;
        //};



    }

    // Update is called once per frame
    void Update()
    {

    }
}
