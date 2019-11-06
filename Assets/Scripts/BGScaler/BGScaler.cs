using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour
{

    // Update is called once per frame
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        float height = sr.bounds.size.y; // lấy biên y
        float width = sr.bounds.size.x; // lấy biên x


        float worldHeight = Camera.main.orthographicSize * 2f; // value = 10
        float worldWidth = worldHeight * Screen.width/Screen.height; // 10 * 480/800
        //transform.localScale = new Vector3(worldWidth, worldHeight, 0); // value of Background scale 

        tempScale.y = worldHeight / height; //gán ngược lại giá trị cho y
        tempScale.x = worldWidth / width; ////gán ngược lại giá trị cho x

        transform.localScale = tempScale;

    }
}
