using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour {

    [SerializeField]
    private bool showSpark;

    private SpriteRenderer[] spriteRenderers;
    private GameObject spark;

    public bool ShowSpark { get { return showSpark; } set { showSpark = value; } }

    // Use this for initialization
    void Start () {
        spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (showSpark && Time.timeScale > 0) {
            StartCoroutine(RandomizeElectricity());
        }
    }

    IEnumerator RandomizeElectricity() {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
            spriteRenderer.flipX = GameManager.Instance.RandomizeBool();
            spriteRenderer.flipY = GameManager.Instance.RandomizeBool();
            yield return new WaitForSeconds(5.0f);
        }
    }
}
