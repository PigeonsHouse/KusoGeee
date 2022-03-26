using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public GameObject scope;
    private GameObject instant_scope;
    private Vector3 pos;
    private RaycastHit hit;
    void Start() {
        instant_scope = (GameObject)Instantiate(scope, new Vector3(0, -10, 0), Quaternion.identity);
    }
    void Update() {
        int layorMask = ~ (1 << 6);
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 40f, layorMask)) {
            instant_scope.transform.position = hit.point;
        }
    }

    void OnDestroy() {
        Destroy(instant_scope);
    }
}
