using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    Vector2 newPosition;
    Animator animator;

    void Start()
    {
        ChangePosition();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        if (GameObject.Find("Player").GetComponentInChildren<Weapon>() != null)
        {
            Debug.Log("Player has weapon, activating portal.");

            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;

        }
        else
        {
            Debug.Log("Player does not have weapon, deactivating portal.");

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in GameManager.Instance.transform)
            {
                if (child.GetComponent<Canvas>() != null || child.GetComponent<UnityEngine.UI.Image>() != null)
                {
                    child.gameObject.SetActive(true);
                }
            }
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }
}