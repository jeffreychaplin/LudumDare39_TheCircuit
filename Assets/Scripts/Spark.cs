using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {

    private Electricity electricity;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        electricity = transform.parent.gameObject.GetComponent<Electricity>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderer.enabled = electricity.ShowSpark;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (electricity.ShowSpark) {
            if (other.gameObject.tag == "PLAYER") {
                electricity.ShowSpark = false;
                SoundManager.Instance.PlayAudioClipFX("spark_pickup");
                GameManager.Instance.PowerRemaining = Mathf.Min(1f, GameManager.Instance.PowerRemaining + 0.05f);
                GameManager.Instance.RandomlyTurnOnSparks(1);
            }
            else if (other.gameObject.tag == "OPPONENT") {
                electricity.ShowSpark = false;
                SoundManager.Instance.PlayAudioClipFX("spark_pickup");
                GameManager.Instance.RandomlyTurnOnSparks(1);
            }
        }
    }
}
