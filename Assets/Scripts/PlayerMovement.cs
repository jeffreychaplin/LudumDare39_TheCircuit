using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private bool reverse;

    [SerializeField]
    private bool isOpponent;

    private Transform targetNode;
    private int nodeIndex;
    private LoopNodeList[] loopNodeList;
    private int currentLoopIndex;
    private LoopNodeList currentLoop;

    private Text speedValue;

    private GameObject[] jumpLinks;
    //private GameObject[] players;

    private bool isCrashed;
    private bool isCheckPointed;
    private bool isOpponentChangedWires;

    // Use this for initialization
    void Start () {
        currentLoopIndex = 2;
        nodeIndex = 0;
        currentLoop = LevelManager.Instance.LoopNodeList[currentLoopIndex];
        targetNode = currentLoop.Node[nodeIndex];
        speedValue = LevelManager.Instance.SpeedValue.GetComponent<Text>();
        jumpLinks = GameObject.FindGameObjectsWithTag("JUMPLINK");
        //players = GameObject.FindGameObjectsWithTag("PLAYER");
        isCrashed = false;
        isCheckPointed = false;
        isOpponentChangedWires = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (targetNode != null) {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.position, GameManager.Instance.CurrentSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetNode.position) < 0.1f) {
                nodeIndex += reverse ? -1 : 1;
                if (nodeIndex < 0) {
                    nodeIndex = currentLoop.Node.Length - 1;
                }
                else if (nodeIndex >= currentLoop.Node.Length) {
                    nodeIndex = 0;
                }
                targetNode = currentLoop.Node[nodeIndex];
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!isOpponent && other.gameObject.tag == "CHECKPOINT" && !isCheckPointed) {
            isCheckPointed = true;
            SoundManager.Instance.PlayAudioClipFX("checkpoint");
            GameManager.Instance.CurrentSpeed += 0.1f;
            speedValue.text = GameManager.Instance.CurrentSpeed.ToString("0.0");
            Debug.Log("OnTriggerEnter2D CHECKPOINT! " + GameManager.Instance.CurrentSpeed);
        }

        if (isOpponent && !isOpponentChangedWires) {
            int randomAction = Random.Range(0, 10);
            if (other.gameObject.tag == "JUMPLINK_OPPONENT") {
                if (GameManager.Instance.PlayerLoopIndex != currentLoopIndex) {
                    if (randomAction <= 5) {
                        // move to same wire player is currently on.
                        currentLoopIndex = GameManager.Instance.PlayerLoopIndex < currentLoopIndex ? Mathf.Max(0, currentLoopIndex - 1) : Mathf.Min(2, currentLoopIndex + 1);
                        Debug.Log("OnTriggerStay2D move to player wire " + GameManager.Instance.PlayerLoopIndex + " < " + currentLoopIndex + " randomaction= " + randomAction);
                    }
                    else if (randomAction <= 10) {
                        // move toward random wire.
                        currentLoopIndex = Random.Range(0, 2) < currentLoopIndex ? Mathf.Max(0, currentLoopIndex - 1) : Mathf.Min(2, currentLoopIndex + 1);
                        Debug.Log("OnTriggerStay2D move toward wire " + GameManager.Instance.PlayerLoopIndex + " < " + currentLoopIndex + " randomaction= " + randomAction);
                    }
                    currentLoop = LevelManager.Instance.LoopNodeList[currentLoopIndex];
                    targetNode = currentLoop.Node[nodeIndex];
                }
                //Debug.Log("OnTriggerStay2D isOpponent " + currentLoopIndex + " randomaction= "  + randomAction);
            }
            isOpponentChangedWires = true;
        }

        if (!isOpponent && other.gameObject.tag == "OPPONENT" && !isCrashed) {
            Debug.Log("OnTriggerStay2D CRASH! " + currentLoopIndex);
            isCrashed = true;
            SoundManager.Instance.PlayAudioClipFX("crash");
            GameManager.Instance.PowerRemaining = Mathf.Max(0f, GameManager.Instance.PowerRemaining - 0.1f);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (!isOpponent && other.gameObject.layer == 8) {

            if (other.gameObject.tag == "JUMPLINK") {

                JumpLink jumpLink = other.GetComponent<JumpLink>();

                currentLoopIndex = jumpLink.NextLoopNumber;
                GameManager.Instance.PlayerLoopIndex = currentLoopIndex;
                currentLoop = LevelManager.Instance.LoopNodeList[currentLoopIndex];
                targetNode = currentLoop.Node[nodeIndex];
            }
            foreach (GameObject jumpLink in jumpLinks) {
                jumpLink.layer = 0; // Default;
            }
        }
        
        //batteryLevel = Mathf.Min(batteryLevel + rechargeRate * Time.deltaTime, 100.0F);

    }

    void OnTriggerExit2D(Collider2D other) {
        isCrashed = false;
        isCheckPointed = false;
        isOpponentChangedWires = false;
    }

 }
