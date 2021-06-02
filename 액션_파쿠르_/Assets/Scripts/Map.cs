using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] patternPrefab;

    [SerializeField]
    private float createInterval = 160.0f;
    private bool isCreated = false;

    void Start()
    {
        Debug.Log(Player.P_instance.transform.position.z);
    }

    void Update()
    {
        StartCoroutine("CreateMap");
    }

    IEnumerator CreateMap()
    {
        if ( (int)Player.P_instance.transform.position.z % createInterval == 0 && isCreated == false)
        {
            int maxPattern = 3;
            if (MyGameManager.P_instance.stage == 1)
                maxPattern = 3;
            else if(MyGameManager.P_instance.stage == 2)
                maxPattern = 5;
            else if (MyGameManager.P_instance.stage == 3)
                maxPattern = 7;

            int patternNumber = Random.Range(0, maxPattern);

            Debug.Log(patternNumber);

            Vector3 mapPosition = new Vector3(0, 0, Player.P_instance.transform.position.z + createInterval);
            GameObject PatternInstance = Instantiate(patternPrefab[patternNumber], mapPosition, Quaternion.identity);
            PatternInstance.transform.SetParent(this.transform);
            isCreated = true;

            yield return new WaitForSeconds(1.0f);
            isCreated = false;
        }
    }
}
