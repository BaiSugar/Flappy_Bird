using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreat : MonoBehaviour
{
    public BgMovent bgMovent;

    public List<GameObject> pipeList;
    private float cameraPos;
    private void Start()
    {
        cameraPos = Screen.width*1f / Screen.height * Camera.main.orthographicSize;
        pipeList = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            var prefabs = Resources.Load<GameObject>("Prefabs/PipeObj");
            var go = GameObject.Instantiate(prefabs);
            go.transform.position = new Vector3(i*5+5, 0, 0);
            go.transform.SetParent(this.transform,true);

            pipeList.Add(go);
        }
    }

    private void Update()
    {
        if (GameStrateManager.GetState() != State.Playin && GameStrateManager.GetState() != State.None) return;
        foreach (var item in pipeList)
        {
            item.transform.position += new Vector3(-bgMovent.speed * Time.deltaTime,0,0);

            if (item.transform.position.x < -cameraPos - 1)
            {
                item.transform.position =  new Vector3(pipeList[pipeList.Count - 1].transform.position.x + 5,Random.Range(-1,2f),0);
                pipeList.Remove(item);
                pipeList.Add(item);
                return;
            }
        }
    }

    public void ReStart()
    {
        for (int i = 0; i < 3; i++)
        {
            pipeList[i].transform.position = new Vector3(i * 5 + 5, Random.Range(-1, 2f), 0);
        }
    }
}
