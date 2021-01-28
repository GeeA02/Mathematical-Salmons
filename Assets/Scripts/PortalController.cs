using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class PortalController : MonoBehaviour
    {
        private Collider portalCollider;

        //      v   MAP NAME    v
        public string mapName;

        //      v   REQUIRED POINTS    v
        public int requiredPoints;

        //      v   POINTS TEXT    v
        public Text pointsText;

        public AudioSource successSound;

        // Start is called before the first frame update
        void Start()
        {
            portalCollider = GetComponent<Collider>();
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                if (CheckPoints())
                {
                    successSound.Play();
                    Invoke("success", 3.0f);
                }
                else
                    DisplayMesseage("You need more points \n to complete level!");
            }
            else
                DisplayMesseage("");
        }

        private bool CheckPoints()
        {
            uint points = PlayerController.pointsPi + PlayerController.pointsE + PlayerController.pointsFi;
            return (requiredPoints - points) <= 0;
        }

        private void DisplayMesseage(string text)
        {
            pointsText.text = text;
        }
        
        private void success()
        {
                SceneManager.LoadScene(mapName);
        }
    }
}