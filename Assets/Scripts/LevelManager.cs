using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PrefabColor {
    [SerializeField]
    private Color32 color;

    [SerializeField]
    private GameObject prefab;

    public Color32 Color { get { return color; } set { color = value; } }
    public GameObject Prefab { get { return prefab; } set { prefab = value; } }

}

public struct IntPoint2 {
    public int x;
    public int y;

    public IntPoint2(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

[System.Serializable]
public class LoopNodeList {
    public Transform[] Node;
}

public class LevelManager : Singleton<LevelManager> {

    [SerializeField]
    private Texture2D levelData;

    [SerializeField]
    private GameObject speedValue;

    [SerializeField]
    private PrefabColor[] prefabColors;

    [SerializeField]    
    private LoopNodeList[] loopNodeList;

    public LoopNodeList[] LoopNodeList { get { return loopNodeList; } private set { loopNodeList = value; } }
    public GameObject SpeedValue { get { return speedValue; } private set { speedValue = value; } }

    public void clearLevel() {
        while(transform.childCount > 0) {
            Transform child = transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
    public void loadLevel() {
        clearLevel();

        Color32[] levelPixels = levelData.GetPixels32();
        IntPoint2 dimensions = new IntPoint2(levelData.width, levelData.height);

        for (int x = 0; x < dimensions.x; x++) {
            for (int y = 0; y < dimensions.y; y++) {
                createPrefab(levelPixels[(y * dimensions.x) + x], x, y);
            }
        }
    }

    public void createPrefab(Color32 color, int x, int y) {
        if (color.a <= 0) {
            return; // when pixel transparent.
        }

        foreach (PrefabColor prefabColor in prefabColors) {
            if (prefabColor.Color.Equals(color)) {
                // located an existing prefab color.
                Debug.Log("prefab color located !" + color);
                if (prefabColor.Prefab) {
                    GameObject go = (GameObject)Instantiate(prefabColor.Prefab, new Vector3(x, y, 0), Quaternion.identity);
                    go.transform.SetParent(this.transform);
                    if (prefabColor.Prefab.tag == "PLAYER") {
                        GameManager.Instance.Player = go;
                    }
                    return;
                }
                Debug.Log("unable to locate prefab!");
                return;
            }
        }
        Debug.Log("unable to locate prefab color! " + color);
    }

    // Use this for initialization
    void Start() {
        loadLevel();
    }
}
