using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();
    public List<Transform> currentPlatforms = new List<Transform>();

    public int offset;

    private Transform player;
    private Transform currentPlatformPoint;
    private int platformIndex;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for ( int i = 0 ; i < platforms.Count ; i++ ){
            Transform p = Instantiate(platforms[i], new Vector3(0, 0, 86 * i), transform.rotation).transform;
            currentPlatforms.Add(p);
            offset += 86;
        }

        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
    }

    public GameObject platform;

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKey(KeyCode.A)){
            Recycle(platform);
        }

        float distance = player.position.z - currentPlatformPoint.position.z;

        if ( distance >= 5 ){ // já saiu da plataforma
            Recycle(currentPlatforms[platformIndex].gameObject);
            platformIndex++;

            if ( platformIndex > currentPlatforms.Count - 1 ){
                platformIndex = 0;
            }

            currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
        }
    }

    public void Recycle(GameObject platform) {
        platform.transform.position = new Vector3(0, 0, offset);
        offset += 86;
    }
}
