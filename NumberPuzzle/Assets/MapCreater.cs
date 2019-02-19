using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    public static MapCreater instance;
    public GameObject[] numbers;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        numbers[15] = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        Shuffle(numbers);
        for (int i = 0; i < 4; ++i) {
            for (int j = 0; j < 4; ++j) {
                if (numbers[i * 4 + j] == null) continue;
                Instantiate(numbers[i * 4 + j], new Vector3(i, j), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Shuffle(GameObject[] alpha) {
        for (int i = 0; i < alpha.Length; i++)
        {
            var temp = alpha[i];
            int randomIndex = Random.Range(i, alpha.Length);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
    }
}
