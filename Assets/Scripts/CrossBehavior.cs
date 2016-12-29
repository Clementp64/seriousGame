using UnityEngine;
using System.Collections;

public class CrossBehavior : MonoBehaviour {

    private float animationSpeed = -0.06f;

    void Start() {
        StartCoroutine(DestroyMyself());
    }

	void Update () {
        transform.localScale += new Vector3(animationSpeed, 0, 0);
        if (Mathf.Abs(transform.localScale.x) >= 1)
            animationSpeed *= -1;
	}

    IEnumerator DestroyMyself() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
