using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryOutpost : MonoBehaviour
{
    public Transform Boulder1;
    public Transform B;
    public int projectileSpeed; // Set at 30 for normal speed and 120 for fast speed.
    public Vector3 targetPOS;
    public Vector3 hideBoulderPOS;
    public bool projectileIsActive = false;
    private float timer = 0f;
    private int timeBetweenShots = 10;
    private Animator animator;
    



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        targetPOS = new Vector3(0f, 0f, 0f);

        // Create a Boulder and hide it off screen.
        B = Instantiate(Boulder1);
        hideBoulderPOS = new Vector3(100f, 100f, 100f);
        B.localPosition = hideBoulderPOS;
    }

    // Update is called once per frame
    void Update()
    {
        // Start the timer that keeps track of firing.
        timer += Time.deltaTime;

        // Check to see if it is time to fire.
        if (timer >= timeBetweenShots && GameObject.Find("Game").GetComponent<GameMain>().meatEaterList.Count > 0)
        {
            timer = 0f;

            // Find Target.
            targetPOS = GameObject.Find("Game").GetComponent<GameMain>().meatEaterList[0].position;

            Fire(targetPOS);
        }

        // Move Projectile
        if (projectileIsActive)
        {
            // Move our position a step closer to the target.
            float step = projectileSpeed * Time.deltaTime; // calculate distance to move
            B.position = Vector3.MoveTowards(B.position, targetPOS, step);
        }

        else if (projectileIsActive == false && Vector3.Distance(B.position, hideBoulderPOS) > 10)
        {
            B.position = hideBoulderPOS;
        }

    }

    public void UpdateOutpostPosition()
    {
        Vector3 outpostPosition = new Vector3(-2f, 1.8f, GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2);
        transform.localPosition = outpostPosition;
    }

    public void Fire(Vector3 target)
    {
        if (GameObject.Find("Game").GetComponent<GameMain>().m2Unlocked == false)
        {
            // Animate shot.
            animator.Play("OutpostFireAnim");

        }


        // Handles the motion of projectile.
        Vector3 outpostPosition = new Vector3(-3.2f, 2.4f, GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2);
        B.localPosition = outpostPosition;
        projectileIsActive = true;
    }

}
