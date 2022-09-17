using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    Transform model;
    bool attacking;
    Vector3 defaulPos;
    Vector3 readyPos;
    float startRotation;
    float endRotation;
    float coroutineTime;
    float duration;
    float yRotation;
    Vector3 coroutineRotation;


    // Start is called before the first frame update
    void Start()
    {
        model = gameObject.transform.GetChild(0);
        defaulPos = new Vector3(.5f, 1, 0);
        readyPos = new Vector3(1, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Attack();

    }

    void Attack()
    {
        if (attacking == false)
            StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        attacking = true;
        model.localPosition = readyPos;
        model.Rotate(0, 0, 90);
        //Rotation Logic
        coroutineTime = 0.0f;
        duration = .5f;
        while (coroutineTime < duration)
        {
            coroutineTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation, endRotation, coroutineTime / duration);
            coroutineRotation.x = transform.eulerAngles.x;
            coroutineRotation.y = yRotation;
            coroutineRotation.z = transform.eulerAngles.z;
            transform.eulerAngles = coroutineRotation;
            yield return null;
        }
        //End Rotation Logic
        model.localPosition = defaulPos;
        model.Rotate(0, 0, -90);
        model.parent.Rotate(0, 180, 0);
        attacking = false;
    }
}
