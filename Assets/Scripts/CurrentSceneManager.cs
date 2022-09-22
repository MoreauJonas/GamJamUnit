using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;
    public static CurrentSceneManager instance;
  private void Awake()
  {
    if (instance !=null)
    {
      Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dnas la scéne");
      return;
    }
    instance = this;
  }
}
