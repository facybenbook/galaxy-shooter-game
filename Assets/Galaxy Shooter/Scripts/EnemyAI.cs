using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private UIManager _UIManager;

    [SerializeField]
    private AudioClip _audioClip;

    // Use this for initialization
    void Start () {

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -6)
        {
            float randomX = Random.Range(-7.0f, 7.0f);
            transform.position = new Vector3(randomX, 6, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy collied with: " + other.name);

        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            //destroy laser
            Destroy(other.gameObject);

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

            _UIManager.UpdateScore();

            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);

            //destroy ourself
            Destroy(this.gameObject);

        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

            _UIManager.UpdateScore();

            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);

            Destroy(this.gameObject);
        }

    }


}
