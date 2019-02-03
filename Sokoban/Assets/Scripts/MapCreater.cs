using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    // 输入的地图。
    public string[] map;

    // 所有的预置体。
    public GameObject wall;
    public GameObject player;
    public GameObject box;
    public GameObject target;
    public GameObject ground;

    // 盒子，墙和目标点的位置。
    private Dictionary<int, GameObject> pos_box_map;
    private HashSet<int> wall_pos_set ;
    private List<int> tar_pos_list;

    // 用于2维转一维。
    // use to convert 2D position to 1D position.
    public const int SIZE = 1000;

    // 左上角地图起始点的位置。
    // Left top position
    public int left_top_x = -5;
    public int left_top_y = -4;
    
    private void Awake()
    {
        pos_box_map = new Dictionary<int, GameObject>();
        wall_pos_set = new HashSet<int>();
        tar_pos_list = new List<int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 从左到右，从上到下建图。
        int row_pos = left_top_x;
        foreach (var row in map)
        {
            int col_pos = left_top_y;
            for (int i = 0; i < row.Length; ++i)
            {
                Vector3 cell_pos = new Vector3(row_pos, col_pos);

                if (row[i] == '#')
                {
                    Instantiate(wall, cell_pos, Quaternion.identity);
                    wall_pos_set.Add(TwoDToOneD(row_pos, col_pos));
                }
                else if (row[i] == 'A')
                {
                    Instantiate(player, cell_pos, Quaternion.identity);
                }
                else if (row[i] == 'B')
                {
                    GameObject newbox = Instantiate(box, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == 'T')
                {
                    Instantiate(target, cell_pos, Quaternion.identity);
                    tar_pos_list.Add(TwoDToOneD(row_pos, col_pos));
                }

                // Ground.
                Instantiate(ground, cell_pos, Quaternion.identity);
                col_pos++;
            }
            row_pos++;
        }
    }

    public Dictionary<int, GameObject> getPosBoxMap() {
        return pos_box_map;
    }

    public HashSet<int> getWallPosSet() {
        return wall_pos_set;
    }

    public List<int> getTargetPosList() {
        return tar_pos_list;
    }

    public int TwoDToOneD(int x, int y) {
        return SIZE * x + y;
    }
}
