using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioClip meatEaterDie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "meatEater")
        {
            // Destroy Meat Eater.
            AudioSource.PlayClipAtPoint(meatEaterDie, transform.position);
            GameObject.Find("Game").GetComponent<GameMain>().meatEaterList.Remove(col.transform);
            Destroy(col.gameObject);
            GameObject.Find("Game").GetComponent<GameMain>().meatEaters -= 1;

            if (GameObject.Find("Game").GetComponent<GameMain>().m1Unlocked && GameObject.Find("Game").GetComponent<GameMain>().m2Unlocked == false)
            {
                // Hide Projectile off screen.
                GameObject.Find("MilitaryOutpost(Clone)").GetComponent<MilitaryOutpost>().projectileIsActive = false;
            }
            
            else if (GameObject.Find("Game").GetComponent<GameMain>().m2Unlocked)
            {
                // Hide Projectile off screen.
                GameObject.Find("MilitaryOutpost2(Clone)").GetComponent<MilitaryOutpost>().projectileIsActive = false;
            }
        }


    }
}
