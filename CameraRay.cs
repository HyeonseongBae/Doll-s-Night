using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public Vector2 ratio;
    public Animation handAnimation;
    Camera MC;

    public bool attack = false;


    void Awake()
    {
        MC = Camera.main;
    }

    void Start()
    {
        ratio = new Vector2(Screen.width / 2, Screen.height / 2);
        handAnimation = GameObject.Find("RightHands").GetComponent<Animation>();
    }

    void Update()
    {
        RayFoward();
    }

    void RayFoward()
    {
        if (attack) return;

        RaycastHit hit;
        Ray ray = MC.ScreenPointToRay(ratio);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            if (Vector3.Distance(objectHit.transform.position, MC.transform.position) < 5)
            {
                switch (objectHit.tag)
                {
                    case "Object":
                        handAnimation.Play("Armature|index");
                        if (Input.GetMouseButtonDown(0))
                        {
                            ItemManager.S.FindObject(objectHit.gameObject);
                        }
                        break;

                    case "Door":
                        handAnimation.Play("Armature|fig");
                        if (Input.GetMouseButtonDown(0))
                        {
                            objectHit.GetComponent<DoorHandleObject>().Action();
                        }
                        break;

                    case "Button":
                        handAnimation.Play("Armature|push");
                        if (Input.GetMouseButtonDown(0))
                        {
                            objectHit.GetComponent<ButtonObject>().Action();
                        }
                        break;

                    case "Target":
                        //handAnimation.Play("Armature|pistol");
                        //if (Input.GetMouseButtonDown(0))
                        //{
                        //    objectHit.GetComponent<TargetObject>().Action();
                        //}
                        break;

                    default:
                        handAnimation.Play("Armature|idle");
                        break;
                }
            }
            else handAnimation.Play("Armature|idle");
        }
        else handAnimation.Play("Armature|idle");
    }
}
