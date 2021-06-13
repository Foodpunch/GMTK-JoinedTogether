using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{

    public CanvasGroup fade;
    bool gameOver;


    public Text text;
    public Text birdGuided;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            fade.alpha += Time.deltaTime;
            PlayerScript.instance.gameObject.SetActive(false);
            int birdCount = PlayerScript.instance.playerBirdManager.birdList.Count;
            birdGuided.text = "Birds Guided : " + birdCount.ToString();
            if(birdCount >= 80)
            {
                text.text = "GRADE: A \n Hoooly crap!You're a bona fide bird herder! Someone get this robo-bird a medal.";
            }
            else if(birdCount >21 && birdCount <80)
            {
                text.text = "GRADE: B \n Not a bad turnout if I do say so myself! Great job.";
            }
            else if(birdCount >=1 && birdCount <20)
            {
                text.text = "GRADE: C \n What.. You read the manual, right? There really should be more birds getting here...";
            }
            else if (birdCount ==0)
            {
                text.text = "GRADE: F \n How... Is the robo-bird malfunctioning? Don't tell me... Are you weaponizing the birds?! You're no different than those nasty poachers...";
            }
                
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            //Game over sequence
            gameOver = true;
        }
    }
}
