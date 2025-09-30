using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{

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
        await Task.Delay(1000);

        _HPCharacter.GetComponent<Animator>().enabled = true;
        _PBRCharacter.GetComponent<Animator>().enabled = true;
        _PolyartCharacter.GetComponent<Animator>().enabled = true;

        _HPCharacter.GetComponent<CircleFollower>().radius = 5;
        _PBRCharacter.GetComponent<CircleFollower>().radius = 5;
        _PolyartCharacter.GetComponent<CircleFollower>().radius = 5;

        Debug.Log("BeginGame");    
    }
    private void PrepareGame()
    {
        Debug.Log("PrepareGame");
    }

    async UniTask InitializeObjects() 
    {
        Debug.Log("InitializeObjects start");
         

        _HPCharacter.GetComponent<Animator>().enabled = false;
        _PBRCharacter.GetComponent<Animator>().enabled = false;
        _PolyartCharacter.GetComponent<Animator>().enabled = false;

        _HPCharacter.GetComponent<CircleFollower>().radius = 1;
        _PBRCharacter.GetComponent<CircleFollower>().radius = 1;
        _PolyartCharacter.GetComponent<CircleFollower>().radius = 1;
        await Task.Delay(1000);

        Debug.Log("InitializeObjects end");
      //  await UniTask.Delay(10);
    }
    async UniTask CreateObjects()
    {
        Debug.Log("CreateObjects start");
        await Task.Delay(1000);
        Debug.Log("CreateObjects end");
    }


    void BindingObject()
    {
        _Canvas = Instantiate(_Canvas);
        _Light = Instantiate(_Light);
        _HPCharacter = Instantiate(_HPCharacter);
        _Camera = Instantiate(_Camera);
        _PBRCharacter = Instantiate(_PBRCharacter);
        _PolyartCharacter = Instantiate(_PolyartCharacter);
        _Stage = Instantiate(_Stage);

    }


    

    
}
