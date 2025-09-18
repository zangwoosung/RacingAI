using System.Collections.Generic;
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
    VisualElement root;

    MultiColumnListView listView;
    private void Start()
    {
        // hook  and DataBinding.....MyData  이것을 이해하면 "데이터 바인딩" 끝. 
        root = _UIDocument.rootVisualElement;
        listView = root.Q<MultiColumnListView>();


        //TEST Code 
        List<MyData> data = new List<MyData>();

        for (int i = 0; i < 5; i++)
        {
            Sprite mysprite = Resources.Load<Sprite>("cons/tiger");
                             //***  리소스 폴더에 있는 것을 로드함. 폴더 구조 확인필요.
                             //해당 폴더에 tiger르는 스프라이이 있어야 함. 
            MyData temp = new MyData("1", "2", mysprite);
            data.Add(temp);
        }


        ShowRanking(data);

    }
    



    public void ShowRanking(List<MyData> data)
    {
        List<MyData> myDataList = data;

        Debug.Log("myDataList.count " + myDataList.Count);

        // Column 1: Title
        listView.columns.Add(new Column
        {
            name = "title",
            title = "이름",
            width = 150,
            makeCell = () =>
            {
                var label = new Label();
                label.style.flexGrow = 1;
                label.style.whiteSpace = WhiteSpace.Normal; // allows wrapping
                label.style.height = 200;
                return label;
            },

            bindCell = (element, index) =>
            {
                (element as Label).text = myDataList[index].Name;
            }
        });

        // Column 2: Description
        listView.columns.Add(new Column
        {
            name = "description",
            title = "걸린시간",
            width = 250,
            makeCell = () =>
            {
                var label = new Label();
                label.style.flexGrow = 1;
                label.style.whiteSpace = WhiteSpace.Normal; 
                label.style.height = 100;
                label.style.fontSize = 200;
                return label;
            },
            bindCell = (element, index) =>
            {
                (element as Label).text = myDataList[index].Time;
            }
        });

        // Column 3: Icon (Sprite)
        listView.columns.Add(new Column
        {
            name = "icon",
            title = "사진",
            width = 60,
            makeCell = () => new Image { scaleMode = ScaleMode.ScaleToFit },
            bindCell = (element, index) =>
            {
                var img = element as Image;
                img.image = myDataList[index].Icon?.texture;
            }
        });

        listView.fixedItemHeight = 100; // height in pixels
        listView.virtualizationMethod = CollectionVirtualizationMethod.FixedHeight;

        listView.itemsSource = myDataList;      

    }    
}
