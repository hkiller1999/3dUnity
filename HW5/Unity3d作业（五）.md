## 与游戏世界交互
参考博客及github：
https://blog.csdn.net/jc2474223242/article/details/79975137
https://github.com/liuwd8/unity

#### 作业与练习
##### 1、编写一个简单的鼠标打飞碟（Hit UFO）游戏

 - 游戏内容要求：
 	
 	1、游戏有 n 个 round，每个 round 都包括10 次 trial；
	2、每个 trial 的飞碟的色彩、大小、发射位置、速度、角度、同时出现的个数都可能不同。它们由该 round 的 ruler 控制；
	3、每个 trial 的飞碟有随机性，总体难度随 round 上升；
	4、鼠标点中得分，得分规则按色彩、大小、速度不同计算，规则可自由设定。
	
 -  游戏要求
	
 - [ ] 使用带缓存的工厂模式管理不同飞碟的生产与回收，该工厂必须是场景单实例的！具体实现见参考资源 Singleton 模板类
       
 - [ ] 近可能使用前面 MVC 结构实现人机交互与游戏模型分离

#### 实现
首先创建GameObject disk，将其拖入预设之中，其次从网上下载相关Scence 将其添加到unity中。接下来开始编写code

这次作业使用工厂模式制作，由于上次作业牧师与魔鬼为我们提供了较好的思考方式。以下：
工厂类的实现：
DiskFactory:.cs:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour {
    public List<GameObject> used = new List<GameObject>();
    public List<GameObject> free = new List<GameObject>();

	// Use this for initialization
	void Start () { }

    public void GenDisk()
    {
        GameObject disk;
        if(free.Count == 0)
        {
            disk = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk"), Vector3.zero, Quaternion.identity);
        }
        else
        {
            disk = free[0];
            free.RemoveAt(0);
        }
        float x = Random.Range(-10.0f, 10.0f);
        disk.transform.position = new Vector3(x, 0, 0);
        disk.transform.Rotate(new Vector3(x < 0? -x*9 : x*9, 0, 0));
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color color = new Color(r, g, b);
        disk.transform.GetComponent<Renderer>().material.color = color;
        used.Add(disk);
    }
    public void RecycleDisk(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        free.Add(obj);
    }
}
```

场景控制器FirstScenceContorller:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private FirstSceneController action;
    private GUIStyle fontstyle1 = new GUIStyle();
    // Use this for initialization
    void Start () {
        action = SSDirector.getInstance().currentSceneController as FirstSceneController;
        fontstyle1.fontSize = 50;
    }

    // Update is called once per frame
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 80, 80, 60), "RESTART"))
        {
            action.Restart();
        }
        if (GUI.Button(new Rect(0, 160, 80, 60), "Pause"))
        {
            action.Pause();
        }
        if (action.flag == 0)
        {
            fontstyle1.normal.textColor = Color.green;
            GUI.Label(new Rect(Screen.width / 2 - 150, 0, 300, 100), "Score: " +
                action.score + ", Round: " + (Mathf.CeilToInt(FirstSceneController.times / 10) + 1), fontstyle1);
        }
        else if (action.flag == 1)
        {
            fontstyle1.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 150, 0, 300, 100), "Your score is : " + action.score, fontstyle1);
        }
        else
        {
            fontstyle1.normal.textColor = Color.green;
            GUI.Label(new Rect(Screen.width / 2 - 150, 0, 300, 100), "Score: " +
                action.score + ", Round: " + (Mathf.CeilToInt(FirstSceneController.times / 10) + 1), fontstyle1);
            fontstyle1.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height/2-50, 300, 100), "Pause!", fontstyle1);
        }
    }
}

```
动作管理之CCActionManager.cs:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {
    public FirstSceneController sceneController;
    public List<CCMoveToAction> seq = new List<CCMoveToAction>();
    public UserClickAction userClickAction;
    public DiskFactory disks;
    
    protected new void Start()
    {
        sceneController = (FirstSceneController)SSDirector.getInstance().currentSceneController;
        sceneController.actionManager = this;
        disks = Singleton<DiskFactory>.Instance;
    }
    protected new void Update()
    {
        if(disks.used.Count > 0)
        {
            GameObject disk = disks.used[0];
            float x = Random.Range(-10, 10);
            CCMoveToAction moveToAction = CCMoveToAction.GetSSAction(new Vector3(x, 12, 0), 3 * (Mathf.CeilToInt(FirstSceneController.times / 10) + 1) * Time.deltaTime);
            seq.Add(moveToAction);
            this.RunAction(disk, moveToAction, this);
            disks.used.RemoveAt(0);
        }
        if (Input.GetMouseButtonDown(0) && sceneController.flag == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitGameObject;
            if (Physics.Raycast(ray, out hitGameObject))
            {
                GameObject gameObject = hitGameObject.collider.gameObject;
                if (gameObject.tag == "disk")
                {
                    foreach(var k in seq)
                    {
                        if (k.gameObject == gameObject)
                            k.transform.position = k.target;
                    }
                    userClickAction = UserClickAction.GetSSAction();
                    this.RunAction(gameObject, userClickAction, this);
                }/*
                else if (gameObject.transform.parent.name == "boat" && sceneController.boatCapacity < 2 && (moveToAction == null || !moveToAction.enable))
                {
                    moveToAction = CCMoveToAction.GetSSAction(-gameObject.transform.parent.transform.position, 10*Time.deltaTime);
                    this.RunAction(gameObject.transform.parent.gameObject, moveToAction, this);
                }*/
            }
        }
        base.Update();
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
        disks.RecycleDisk(source.gameObject);
        seq.Remove(source as CCMoveToAction);
        source.destory = true;
        if (FirstSceneController.times >= 30)
            sceneController.flag = 1;
    }
    public void CheckEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
    }
    public void Pause()
    {
        if(sceneController.flag == 0)
        {
            foreach (var k in seq)
            {
                k.enable = false;
            }
            sceneController.flag = 2;
        }
        else if(sceneController.flag == 2)
        {
            foreach (var k in seq)
            {
                k.enable = true;
            }
            sceneController.flag = 0;
        }
    }
}

```

动作管理之SSAction: SSAction是所有动作的基类，通过实现SSAciton来指定不同的动作：
SSAction.cs:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject
{
    public bool enable = true;
    public bool destory = false;

    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public ISSActionCallback callback { get; set; }

    protected SSAction() { }

	// Use this for initialization
    public virtual void Start () {
        throw new System.NotImplementedException();
	}

    // Update is called once per frame
    public virtual void Update() {
        throw new System.NotImplementedException();
    }
}
```


[我的github地址](https://github.com/hkiller1999/3dUnity/upload/master/HW5)
