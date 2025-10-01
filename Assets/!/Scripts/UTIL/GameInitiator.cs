using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [Header("RacingAI")]
    [SerializeField] GameObject _setting;
    [SerializeField] GameObject _world;
    [SerializeField] GameObject _ui;
    [SerializeField] GameObject _scripts;
    [SerializeField] GameManager _gameManager;  
    [SerializeField] AgentManager _agentManager;  

    [Header("Objects")]



    [SerializeField] private GameObject _Canvas;
    [SerializeField] private Light _Light;
    [SerializeField] private GameObject _HPCharacter;
    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _PBRCharacter;
    [SerializeField] private GameObject _PolyartCharacter;
    [SerializeField] private GameObject _Stage;
    async void Start()
    {
       // await DoSomething();
        BindingObject();
        //_loadingScreen.Show();
        await InitializeObjects();
        await CreateObjects();
        PrepareGame();
        // _loadingScreen.Hide();
        await BeginGame();
    }

    async UniTask BeginGame()
    {
        await Task.Delay(10);

        //_HPCharacter.GetComponent<Animator>().enabled = true;
        //_PBRCharacter.GetComponent<Animator>().enabled = true;
        //_PolyartCharacter.GetComponent<Animator>().enabled = true;

        //_HPCharacter.GetComponent<CircleFollower>().radius = 5;
        //_PBRCharacter.GetComponent<CircleFollower>().radius = 5;
        //_PolyartCharacter.GetComponent<CircleFollower>().radius = 5;

        Debug.Log("BeginGame");    
    }
    private void PrepareGame()
    {
        Debug.Log("PrepareGame");
    }

    async UniTask InitializeObjects() 
    {     
        
        _gameManager.Initialize();
        _ui.GetComponent<MainUI>().Initialize();
        _agentManager.Initialize();
        FindFirstObjectByType<CameraManager>().Initialize();
        
        await Task.Delay(10);
       
    }
    async UniTask CreateObjects()
    {
        Debug.Log("CreateObjects start");
        await Task.Delay(1000);
        Debug.Log("CreateObjects end");
    }


    void BindingObject()
    {
        _setting = Instantiate(_setting);
        _world = Instantiate(_world);
        _ui = Instantiate(_ui);
       _scripts = Instantiate(_scripts);       
       
        _gameManager = _scripts.GetComponentInChildren<GameManager>();       

        _agentManager = UnityEngine.Object.FindFirstObjectByType<AgentManager>();
      
        //_agentManager = _scripts.GetComponentInChildren<AgentManager>();

    }


    

    
}
