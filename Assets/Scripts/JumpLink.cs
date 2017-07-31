using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLink : MonoBehaviour {

    [SerializeField]
    private int loopNumber;

    private KeyCode keyCode;
    private int nextLoopNumber;

    public int LoopNumber { get { return loopNumber; } private set { loopNumber = value; } }
    public KeyCode KeyCode { get { return keyCode; } private set { keyCode = value; } }
    public int NextLoopNumber { get { return nextLoopNumber; } private set { nextLoopNumber = value; } }

    // Use this for initialization
    void Start() {
        gameObject.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        if (gameObject.CompareTag("JUMPLINK")) {
            if (loopNumber == 0 && (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))) {
                KeyCode = KeyCode.Alpha2;
                NextLoopNumber = 1;
                gameObject.layer = 8; // ACTIVE
            }

            if (loopNumber == 1 && (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))) {
                KeyCode = KeyCode.Alpha1;
                NextLoopNumber = 0;
                gameObject.layer = 8; // ACTIVE
            }

            if (loopNumber == 1 && (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))) {
                KeyCode = KeyCode.Alpha3;
                NextLoopNumber = 2;
                gameObject.layer = 8; // ACTIVE
            }

            if (loopNumber == 2 && (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))) {
                KeyCode = KeyCode.Alpha2;
                NextLoopNumber = 1;
                gameObject.layer = 8; // ACTIVE
            }
        }
    }
}
