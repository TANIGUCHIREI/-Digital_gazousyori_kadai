using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class capturetest : MonoBehaviour
{
    public GameObject one_yen;
    public GameObject five_yen;
    public GameObject ten_yen;
    public GameObject fiftiy_yen;
    public GameObject one_hundred_yen;
    public GameObject five_hundred_yen;
    public GameObject board;
    public GameObject Light;
    List<GameObject> coins = new List<GameObject>();
    List<GameObject> Prefab_coins = new List<GameObject>();
    GameObject camera;
    UnityEngine.Perception.GroundTruth.PerceptionCamera scripts;
    Vector3 rotate_arg;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        scripts = camera.GetComponent<UnityEngine.Perception.GroundTruth.PerceptionCamera>();

        coins.Add(one_yen);
        coins.Add(five_yen);
        coins.Add(ten_yen);
        coins.Add(fiftiy_yen);
        coins.Add(one_hundred_yen);
        coins.Add(five_hundred_yen);

        rotate_arg = new Vector3(Random.Range(0,35),0, Random.Range(0, 35));
        board.GetComponent<Transform>().Rotate(rotate_arg);

        for (int i = 0; i < Random.Range(10, 30); i++){
            int coin_type = Random.Range(0, 6);
            float x = Random.Range(-.5f, .5f);
            float y = Random.Range(0, 2.0f);
            float z = Random.Range(-.5f, .5f);
            Vector3 pos = new Vector3(x, y, z);
            GameObject Prefab = Instantiate(coins[coin_type],pos, Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f,180f), Random.Range(0f, 180f)));
            Prefab.GetComponent<Renderer>().material.SetFloat("_Metalic", Random.Range(0f, .8f));
            Prefab.GetComponent<Renderer>().material.SetFloat("_NormalScale", Random.Range(0.2f, 1f));
            Prefab_coins.Add(Prefab);
        }

    }
    // Update is called once per frame

    float time = 0;
    bool capture = true;

    int N = 1000;
    int count = 0;
    void Update()
    {
        //camera.GetComponent<Camera>().fieldOfView = Random.Range(10, 50);

        //scripts.RequestCapture();
        //Debug.Log("Done");
        //Input.GetKeyDown(KeyCode.Space

        if(count < N)
        {
            if (time > 2.5f && capture == true)
            {
                scripts.RequestCapture();
                capture = false;
            }
            if (time > 3.0f)
            {

                ResetEnviron();
                Debug.Log(count);
                time = 0;
                count += 1;
                capture = true;

            }


            time += Time.deltaTime;
        }

        
    }

    void ResetEnviron()
    {
        for(int i = 0; i < Prefab_coins.Count; i++)
        {
            Destroy(Prefab_coins[i]);
        }

        Prefab_coins.Clear();




        board.transform.rotation =  Quaternion.Euler(0f, 0f, 0f); //‚±‚ê‚Å‰ñ“]‚ðƒŠƒZƒbƒg
        rotate_arg = new Vector3(Random.Range(-25, 25), 0, Random.Range(-25, 25));
        board.GetComponent<Transform>().Rotate(rotate_arg);
        float r = Random.Range(0f, 1.0f);
        float g = Random.Range(0f, 1.0f);
        float b = Random.Range(0f, 1.0f);
        float alpha = Random.Range(0f, 1.0f);
        board.GetComponent<Renderer>().material.color = new Color(r,g,b,alpha);
        Light.GetComponent<Light>().intensity = Random.Range(100000,300000);
        Light.transform.rotation = Quaternion.Euler(Random.Range(45f,135f), Random.Range(-45f,45f), 0f);

        camera.GetComponent<Transform>().position = new Vector3(0, Random.Range(1.0f,2.0f), 0);
        //Light.GetComponent<Light>().color = new Color(g, r, b, 1);
        for (int i = 0; i < Random.Range(10, 30); i++)
        {
            int coin_type = Random.Range(0, 6);
            float x = Random.Range(-.5f, .5f);
            float y = Random.Range(0, 2.0f);
            float z = Random.Range(-.5f, .5f);
            Vector3 pos = new Vector3(x, y, z);
            GameObject Prefab = Instantiate(coins[coin_type], pos, Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f, 180f), Random.Range(0f, 180f)));
            Prefab.GetComponent<Renderer>().material.SetFloat("_Metalic", Random.Range(0f, .8f));
            Prefab.GetComponent<Renderer>().material.SetFloat("_NormalScale", Random.Range(0.2f, 1f));
            Prefab_coins.Add(Prefab);
        }

        
    }
}
