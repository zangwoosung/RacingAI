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
    VisualElement root;
    MultiColumnListView listView;
    [SerializeField] Sprite sp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            List<MyData> mydataList = new List<MyData>();

            for (int i = 0; i < 5; i++)
            {
               
                    string name = $"Item {i + 1}";
                    string time = System.DateTime.Now.AddMinutes(i * 5).ToString("HH:mm");
                Sprite icon = sp; // Use the assigned sprite

                    MyData myData = new MyData(name, time, icon);
                    mydataList.Add(myData);
                
            }
            ShowRanking(mydataList);
        }
    }

    private void Start()
    {
        root = _UIDocument.rootVisualElement;
        listView = root.Q<MultiColumnListView>();
        var closebtn = root.Q<Button>();

        closebtn.clicked += () =>
        {
            root.visible = false;
            _mainUI.ShowMainUI();
        };

        AgentManager.OnSessionEndWithRankingEvent += ShowRanking;
    }

   

    public void ShowRanking(List<MyData> data)
    {
        List<MyData> myDataList = data;
        root.visible = true;
        Debug.Log("myDataList.count " + myDataList.Count);
        int columnCount = listView.columns.Count;

        if (columnCount > 0)
        {
            listView.columns.Clear();
        }
        float totalWidth = listView.resolvedStyle.width;
        float columnWidth = totalWidth / 3f;
        // Column 1: Title
        listView.columns.Add(new Column
        {
            name = "title",
            title = "Title",
            width = totalWidth * 0.3f,
            makeCell = () =>
            {
                var label = new Label();
                label.style.height = new Length(100, LengthUnit.Percent);
                label.style.display = DisplayStyle.Flex;
                label.style.alignItems = Align.Center;       // Vertical centering
                label.style.justifyContent = Justify.Center; // Optional: horizontal centering
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
            title = "Description",
            width = totalWidth * 0.3f,
            makeCell = () =>
            {
                var label = new Label();
                label.style.height = new Length(100, LengthUnit.Percent);
                label.style.display = DisplayStyle.Flex;
                label.style.alignItems = Align.Center;       // Vertical centering
                label.style.justifyContent = Justify.Center; // Optional: horizontal centering
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
            title = "Icon",
            width = totalWidth * 0.4f,
            
            makeCell = () => new Image { scaleMode = ScaleMode.ScaleToFit },
            bindCell = (element, index) =>
            {
                var img = element as Image;
                img.image = myDataList[index].Icon?.texture;
            }
        });
        listView.Rebuild();
        listView.itemsSource = myDataList;
        listView.fixedItemHeight = 40;
    }    
}
