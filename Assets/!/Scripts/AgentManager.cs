
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public Agent[] agents;


    public void StartToRun(Vector3 pos)
    {
        foreach (var agent in agents)
        {
            float speed = Random.Range(13f, 50f);
            Vector3 des = pos;// new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
            agent.Setup(speed, des);
            agent.CallBackAction(MyAction);
            agent.CallBackFunc(MyFunc);

        }
    }
    void MyAction(string name)
    {
        Debug.Log("what is the name?" + name);
    }
    string  MyFunc(int num)
    {
        Debug.Log("My Func " + num);
        return "100";
    }
}   
