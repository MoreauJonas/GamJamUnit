using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimated : MonoBehaviour
{   
  public bool isInRange;

  public Animator animator;

  public GameObject door;

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.E) && isInRange)
    {
      if(door.GetComponent<BoxCollider2D>().enabled == true)
      {
        OpenDoor();
      } else
      {
        CloseDoor();
      }
    }
  }

  public void OpenDoor()
  {
    animator.ResetTrigger("Close");
    animator.SetTrigger("Open");
    door.GetComponent<BoxCollider2D>().enabled = false;
  }

  public void CloseDoor()
  {
    animator.ResetTrigger("Open");
    animator.SetTrigger("Close");
    door.GetComponent<BoxCollider2D>().enabled = true;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.CompareTag("Player"))
    {
      isInRange = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if(collision.CompareTag("Player"))
    {
      isInRange = false;
    }
  }
}
