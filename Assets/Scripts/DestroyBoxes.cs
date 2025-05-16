using System;
using System.Linq;
using UnityEngine;

public class DestroyBoxes : MonoBehaviour
{
    private Collider2D[] colliders;

    public delegate void OnDestroyed(GameObject box);
    public static event OnDestroyed Destroyed;

    //private void OnEnable() => TapEvent.OnTapped += DestroyObject;
    
    //private void OnDestroy() => TapEvent.OnTapped -= DestroyObject;

    private void Update()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, transform.rotation.eulerAngles.z);
    }

    private void DestroyObject()
    {
        if (colliders.FirstOrDefault(collider => collider.gameObject.name == "RightGear") && colliders.FirstOrDefault(collider => collider.gameObject.name == "LeftGear"))
        {

            Destroyed?.Invoke(gameObject);
            //if (gameObject.TryGetComponent(out Box currentBox))
            {
                //ScoreSystem.Instance.AddScore(currentBox.cost);
            }

            
            Destroy(gameObject);
        }
    }

}
