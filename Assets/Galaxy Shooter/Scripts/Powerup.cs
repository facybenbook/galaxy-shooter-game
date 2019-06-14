using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID; // 0: triple shot, 1: speed burst, 2: shields

    [SerializeField]
    private AudioClip _audioClip;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        
        if (transform.position.y < -7)
        {
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);

            Destroy(this.gameObject);
        }

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collied with: " + other.name);
        
        if(other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {

                if (this.powerupID == 0)
                {
                    //enable triple shot
                    player.TripleShotPowerupOn();
                }
                else if (this.powerupID == 1)
                {
                    //enable speed boost
                    player.SpeedBoostPowerupOn();
                }
                else if (this.powerupID == 2)
                {
                    //enable shields
                    player.ShieldPowerupOn();
                }

            }

            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);

            //destroy ourself
            Destroy(this.gameObject);

        }
       
    }

}
