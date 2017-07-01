using UnityEngine;
using System.Collections;

public class ScrollScreen : MonoBehaviour
{
    RectTransform Scr;
    private Vector2 UpScreen;
    private Vector2 DownScreen;
    private Vector2 LeftScreen;
    private Vector2 RightScreen;

    public GameObject Cam;
    private const float z_cam = -400;

    //임시
    public GameObject C;
    public GameObject R1;
    public GameObject R2;
    public GameObject R3;
    public GameObject R4;
    public GameObject R5;

    
    public GameObject UIPanel; // 0621 상현 UI panel 따라오게 만들기 

    public float smoothTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    private GameObject[,] map = new GameObject[6, 6];
    private bool[,] ismap = new bool[6, 6];

    delegate void listener(string type, int id, float x, float y, float dx, float dy);

    event listener begin0, begin1, begin2, begin3, begin4, begin5, begin6, begin7, begin8, begin9;
    event listener move0, move1, move2, move3, move4, move5, move6, move7, move8, move9;
    event listener end0, end1, end2, end3, end4, end5, end6, end7, end8, end9;

    private Vector2[] delta = new Vector2[5];

    private bool isMoveScreen = false;

    private int now_x = 3;
    private int now_y = 3;

    private int x_c;
    private int y_c;
    public static int roomCount;


    // initial map making
    public void makeMap()
    {
        // 임시
        // 맵만드는걸 여기다라 하자
        //1. ismap 배열으로 생성하여 map 배열에 적용 
        //2. map 배열에 먼저 만들어 생성한뒤 ismap 에 적용

        //현재는 맵이 이미 만들어져 있으므로 2번 방법으로 함 1번으로 하면 조켔당 랜덤으로 첨에 만들어서

        map[3, 3] = C;
        map[2, 3] = R1; // up
        map[3, 4] = R2; // right
        map[3, 2] = R3; // left
        map[4, 3] = R4; // down
        map[3, 5] = R5; // another room

        // 임시

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (map[i, j] != null)
                {
                    ismap[i, j] = true;
                }
            }
        }


    }


    // Use this for initialization
    void Start()
    {


        UpScreen = new Vector2(0, 1330);
        DownScreen = new Vector2(0, -1330);
        LeftScreen = new Vector2(800, 0);
        RightScreen = new Vector2(-800, 0);



        begin0 += onTouch;
        end0 += onTouch;
        move0 += onTouch;


        // 맵 두개 초기화
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                ismap[i, j] = false;
                map[i, j] = null;
            }
        }

        makeMap();

    }

    void initailizeMap()
    {

        x_c = Random.Range(0, 5);
        y_c = Random.Range(0, 5);
        now_x = x_c;
        now_y = y_c;

        ismap[x_c, y_c] = true;

        int count = 0;
        while (count < roomCount)
        {
            int tmp = Random.Range(0, 4);
            bool check = true;

            switch (tmp)
            {
                // 왼쪽
                case 0:
                    check = checkIsMap(now_x - 1, now_y);
                    break;
                // 오른쪽
                case 1:
                    check = checkIsMap(now_x + 1, now_y);
                    break;
                // 위쪽
                case 2:
                    check = checkIsMap(now_x, now_y + 1);
                    break;
                // 아래쪽
                case 3:
                    check = checkIsMap(now_x, now_y - 1);
                    break;
                // 처음맵으로 돌아가 다시 initialize
                case 4:
                    check = false;
                    now_x = x_c;
                    now_y = y_c;
                    break;
            }

            if (check)
            {
                count++;
            }
        }

    }

    bool checkIsMap(int x, int y)
    {

        // 배열 범위를 벗어났을때
        if (x < 0 || x > 5 || y < 0 || y > 5)
        {
            return false;
        }
        // 생성시키려는 장소에 이미 생성되어 있을때
        else if (ismap[x, y] == true)
        {
            // 좌표만 옮기고 방 count는 올리지 않는다
            now_x = x;
            now_y = y;
            return false;
        }
        // 정상적인 맵 생성
        else
        {
            ismap[x, y] = true;
            now_x = x;
            now_y = y;
        }
        return true;
    }

    IEnumerator smoothMove(float currentX, float currentY, float targetX, float targetY)
    {
        Vector3 taget = new Vector3(targetX, targetY, -400);
        if (isMoveScreen)
            yield break;
        while (true)
        {
            isMoveScreen = true;

            Vector3 tmp = Vector3.SmoothDamp(Cam.transform.position, taget, ref velocity, smoothTime);
            Cam.transform.position = tmp;

            // 중간에 update 멈추고 화면 움직이기만 하는건 어떨까
            float dis = Vector3.Distance(Cam.transform.position, taget);
            if (dis <= 1)
            {
                tmp = taget;
                Cam.transform.position = tmp;

                Debug.Log("완료");
                isMoveScreen = false;
                yield break;
            }
            yield return 0.01f;
        }
    }
    public void moveScreen(int where)
    {
        float x_current;
        float y_current;
        float x_target;
        float y_target;

        switch (where)
        {
            case 0: // 위로 쓸어넘기기 ( ↑ )
                if ((now_x - 1) >= 0 && ismap[now_x - 1, now_y] == true)
                {
                    now_x--;
                    //Scr.Translate(UpScreen);
                    x_current = Cam.transform.position.x;
                    y_current = Cam.transform.position.y;

                    x_target = Cam.transform.position.x;
                    y_target = Cam.transform.position.y - 1330;
                    
                    
                    StartCoroutine(smoothMove(x_current, y_current, x_target, y_target));
                    UIPanel.transform.position = UIPanel.transform.position + new Vector3(0, -1330, 0); // 0621 상현
                }
                break;
            case 1: // 아래로 쓸어넘기기 ( ↓ )
                if ((now_x + 1) < 6 && ismap[now_x + 1, now_y] == true)
                {
                    now_x++;
                    //Scr.Translate(DownScreen);

                    x_current = Cam.transform.position.x;
                    y_current = Cam.transform.position.y;

                    x_target = Cam.transform.position.x;
                    y_target = Cam.transform.position.y + 1330;
                    

                    StartCoroutine(smoothMove(x_current, y_current, x_target, y_target));
                    UIPanel.transform.position = UIPanel.transform.position + new Vector3(0, 1330, 0); // 0621 상현
                }
                break;
            case 2: // 왼쪽으로 쓸어넘기기 ( <- )
                if ((now_y - 1) >= 0 && ismap[now_x, now_y - 1] == true)
                {
                    now_y--;
                    //Scr.Translate(LeftScreen);

                    x_current = Cam.transform.position.x;
                    y_current = Cam.transform.position.y;

                    x_target = Cam.transform.position.x - 800;
                    y_target = Cam.transform.position.y;
                    

                    StartCoroutine(smoothMove(x_current, y_current, x_target, y_target));
                    UIPanel.transform.position = UIPanel.transform.position + new Vector3(-800, 0, 0); // 0621 상현
                }
                break;
            case 3: // 오른쪽으로 쓸어넘기기 ( -> )
                if ((now_y + 1) < 6 && ismap[now_x, now_y + 1] == true)
                {
                    now_y++;
                    //Scr.Translate(RightScreen);

                    x_current = Cam.transform.position.x;
                    y_current = Cam.transform.position.y;

                    x_target = Cam.transform.position.x + 800;
                    y_target = Cam.transform.position.y;
                    

                    StartCoroutine(smoothMove(x_current, y_current, x_target, y_target));
                    UIPanel.transform.position = UIPanel.transform.position + new Vector3(800, 0, 0); // 0621 상현
                }
                break;
        }

    }
    // TODO 아래에서 위로 안되고 가장 오른쪽으로 안넘어감

    void onTouch(string type, int id, float x, float y, float dx, float dy)
    {
        switch (type)
        {
            case "begin":
                //Debug.Log("down:" + x + "," + y);
                break;
            case "end":
                //Debug.Log("end:" + x + "," + y + ", d:" + dx + "," + dy);
                if (!isMoveScreen)
                {
                    if (dy > 300)
                    {
                        moveScreen(0);
                    }
                    else if (dy < -300)
                    {
                        moveScreen(1);
                    }
                    else if (dx > 200)
                    {
                        moveScreen(2);
                    }
                    else if (dx < -200)
                    {
                        moveScreen(3);
                    }
                }
                break;
            case "move":
                //Debug.Log("move:" + x + "," + y + ", d:" + dx + "," + dy);
                break;
        }
    }


    //Update is called once per frame
    void Update()
    {
        int count = Input.touchCount;
        if (count == 0) return;

        for (int i = 0; i < count; i++)
        {
            Touch touch = Input.GetTouch(i);
            int id = touch.fingerId;

            // 터치좌표
            Vector2 pos = touch.position;

            //begin이라면 무조건 delta에 넣어주자.
            if (touch.phase == TouchPhase.Began) delta[id] = touch.position;

            //좌표계 정리
            float x, y, dx, dy;
            x = pos.x;
            y = pos.y;
            if (touch.phase == TouchPhase.Began)
            {
                dx = dy = 0;
            }
            else {
                dx = pos.x - delta[id].x;
                dy = pos.y - delta[id].y;
            }

            //상태에 따라 이벤트를 호출하자
            if (touch.phase == TouchPhase.Began)
            {
                switch (id)
                {
                    case 0:
                        if (begin0 != null) begin0("begin", id, x, y, dx, dy);
                        break;
                    case 1:
                        if (begin1 != null) begin1("begin", id, x, y, dx, dy);
                        break;
                    case 2:
                        if (begin2 != null) begin2("begin", id, x, y, dx, dy);
                        break;
                    case 3:
                        if (begin3 != null) begin3("begin", id, x, y, dx, dy);
                        break;
                    case 4:
                        if (begin4 != null) begin4("begin", id, x, y, dx, dy);
                        break;
                    case 5:
                        if (begin5 != null) begin0("begin", id, x, y, dx, dy);
                        break;
                    case 6:
                        if (begin6 != null) begin1("begin", id, x, y, dx, dy);
                        break;
                    case 7:
                        if (begin7 != null) begin2("begin", id, x, y, dx, dy);
                        break;
                    case 8:
                        if (begin8 != null) begin3("begin", id, x, y, dx, dy);
                        break;
                    case 9:
                        if (begin9 != null) begin4("begin", id, x, y, dx, dy);
                        break;
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                switch (id)
                {

                    case 0:
                        if (move0 != null) move0("move", id, x, y, dx, dy);
                        break;
                    case 1:
                        if (move1 != null) move1("move", id, x, y, dx, dy);
                        break;
                    case 2:
                        if (move2 != null) move2("move", id, x, y, dx, dy);
                        break;
                    case 3:
                        if (move3 != null) move3("move", id, x, y, dx, dy);
                        break;
                    case 4:
                        if (move4 != null) move4("move", id, x, y, dx, dy);
                        break;
                    case 5:
                        if (move5 != null) move0("move", id, x, y, dx, dy);
                        break;
                    case 6:
                        if (move6 != null) move1("move", id, x, y, dx, dy);
                        break;
                    case 7:
                        if (move7 != null) move2("move", id, x, y, dx, dy);
                        break;
                    case 8:
                        if (move8 != null) move3("move", id, x, y, dx, dy);
                        break;
                    case 9:
                        if (move9 != null) move4("move", id, x, y, dx, dy);
                        break;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                switch (id)
                {
                    case 0:
                        if (end0 != null) end0("end", id, x, y, dx, dy);
                        break;
                    case 1:
                        if (end1 != null) end1("end", id, x, y, dx, dy);
                        break;
                    case 2:
                        if (end2 != null) end2("end", id, x, y, dx, dy);
                        break;
                    case 3:
                        if (end3 != null) end3("end", id, x, y, dx, dy);
                        break;
                    case 4:
                        if (end4 != null) end4("end", id, x, y, dx, dy);
                        break;
                    case 5:
                        if (end5 != null) end0("end", id, x, y, dx, dy);
                        break;
                    case 6:
                        if (end6 != null) end1("end", id, x, y, dx, dy);
                        break;
                    case 7:
                        if (end7 != null) end2("end", id, x, y, dx, dy);
                        break;
                    case 8:
                        if (end8 != null) end3("end", id, x, y, dx, dy);
                        break;
                    case 9:
                        if (end9 != null) end4("end", id, x, y, dx, dy);
                        break;
                }
            }
        }
    }


}