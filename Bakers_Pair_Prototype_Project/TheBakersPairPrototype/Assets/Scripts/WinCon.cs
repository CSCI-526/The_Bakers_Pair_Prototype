using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCon : MonoBehaviour
{
        public GameObject tower;

        public GameObject victoryUI;
        public GameObject loseUI;               // Reference to the "You Lose" UI

    // Start is called before the first frame update
    void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.transform.IsChildOf(tower.transform) && !loseUI.activeSelf)
            {
                victoryUI.SetActive(true);
            }
        }
}
