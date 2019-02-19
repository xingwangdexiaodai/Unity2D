using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndMove : MonoBehaviour
{
    private void OnMouseDown()
    {
        var mx = Input.mousePosition.x;
        var my = Input.mousePosition.y;

        var mousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(mx, my));

        int i = Mathf.RoundToInt(mousePosWorld.x);
        int j = Mathf.RoundToInt(mousePosWorld.y);

        Debug.Log(i + " " + j);

        Move(i * 4 + j, (i - 1) * 4 + j);
        Move(i * 4 + j, (i + 1) * 4 + j);
        Move(i * 4 + j, i * 4 + j + 1);
        Move(i * 4 + j, i* 4 + j - 1);

        int cur = 1;
        bool flag = true;
        foreach(var num in MapCreater.instance.numbers) {
            if (num == null) Debug.Log("NULL");
            else Debug.Log(num.tag);
           //if(cur.ToString() != num.tag) {
           //     flag = false;
           //     break;
           //}
            //cur++;
        }
        Debug.Log("sssssssssss");

        if (flag) Debug.Log("win");
    }

    void Move(int pos, int nxtPos) {
        if (pos < 0 || pos >= 16) return;
        if (nxtPos < 0 || nxtPos >= 16) return;
        if (MapCreater.instance.numbers[nxtPos] != null) return;

        int i = nxtPos / 4;
        int j = nxtPos % 4;

        transform.position = new Vector3(i, j);
        MapCreater.instance.numbers[nxtPos] = MapCreater.instance.numbers[pos];
        MapCreater.instance.numbers[pos] = null;
    }
}
