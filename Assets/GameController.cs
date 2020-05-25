using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float difficultyMod = 15;
    float timer = 0;
    public GameObject badguy;

    // Update is called once per frame
    void Update()
    {
        if (timer <= difficultyMod)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            Instantiate(badguy);
        }

        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
