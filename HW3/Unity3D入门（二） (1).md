参考链接：https://github.com/csr632/Priests-and-devils （老师的github仓库）

##### 1、简答并用程序验证
- 游戏对象运动的本质是什么？
游戏对象运动的本质就是使用矩阵变换（平移、旋转、缩放等）来改变游戏对象的空间属性。

- 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）
（1）运用重力来实现物体抛物线运动
效果图：
![在这里插入图片描述](https://img-blog.csdnimg.cn/20190922220452998.gif)
实现代码：

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3(10, 10, 0);
    }
}

```
(2)Transform.translate函数

```
public class GameObject2 : MonoBehaviour {    
    public float vY = 3.0f;
    public float aY = 1.0f;
    public float vX = 1.0f;
    
    void Start(){
        Debug.Log("Start!");
    }    
 
    void Update(){
        Vector3 change = new Vector3(Time.deltaTime * vX, Time.deltaTime * vY, 0);
        this.transform.Translate(change);
        vY -= aY * Time.deltaTime; 
    }
}
```
![在这里插入图片描述](https://img-blog.csdnimg.cn/20190922221047464.gif)

（3）使用Lerp

```
public class GameObject3: MonoBehaviour {    
    public float vY = 3.0f;
    public float aY = 1.0f;
    public float vX = 1.0f;
    
    void Start(){
        Debug.Log("Start!");
    }    
 
    void Update(){
        Vector3 change = new Vector3(Time.deltaTime * vX, Time.deltaTime * vY, 0);
        this.transform.postion = Vector3.Lerp(this.transform.position, this.transform.position + move, 1);
        vY -= aY * Time.deltaTime; 
    }
}
```

- 写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。
  
首先需要创建太阳系中所有的对象，同时由于题目要求转速不同且不在一个法平面上，所以，这里需要经可能为每个物体创建一个轴，使各物体绕轴旋转。
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Universe : MonoBehaviour
{
    GameObject Sun, Moon, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune;
    public Vector3 Ax1,
    public Vector3 Ax2;
    public Vector3 Ax3;
    public Vector3 Ax4;
    public Vector3 Ax5;
    public Vector3 Ax6;
    public Vector3 Ax7;
    public Vector3 Ax8;
    public Vector3 Ax9;
    
    void Start(){
        Sun = GameObject.Find("Sun");
        Moon = GameObject.Find("Moon");
        Mercury = GameObject.Find("Mercury");
        Venus = GameObject.Find("Venus");
        Earth = GameObject.Find("Earth");
        Mars = GameObject.Find("Mars");
        Jupiter = GameObject.Find("Jupiter");
        Saturn = GameObject.Find("Saturn");
        Uranus = GameObject.Find("Uranus");
        Neptune = GameObject.Find("Neptune");
    }
 
    void Update(){
        Sun.Rotate(Vector3.up * 10 * Time.deltaTime);
       
        Moon.RotateAround(Earth.position, Vector3.up, 359 * Time.deltaTime);
        Moon.Rotate(Vector3.up * 30 * Time.deltaTime);

        Mercury.RotateAround(Sun.position, Ax1, 47 * Time.deltaTime);
        Mercury.Rotate(Vector3.up * 50 * Time.deltaTime);

        Venus.RotateAround(Sun.position, Ax2, 35 * Time.deltaTime);
        Venus.Rotate(Vector3.up * 30 * Time.deltaTime);

        Earth.RotateAround(Sun.position, Ax3, 10 * Time.deltaTime);//公转
        Earth.Rotate(Vector3.up * 30 * Time.deltaTime);//自转

        Mars.RotateAround(Sun.position, Ax4, 24 * Time.deltaTime);
        Mars.Rotate(Vector3.up * 30 * Time.deltaTime);

        Jupiter.RotateAround(Sun.position, Ax5, 13 * Time.deltaTime);
        Jupiter.Rotate(Vector3.up * 30 * Time.deltaTime);

        Saturn.RotateAround(Sun.position, Ax6, 9 * Time.deltaTime);
        Saturn.Rotate(Vector3.up * 30 * Time.deltaTime);

        Uranus.RotateAround(Sun.position, Ax7, 6 * Time.deltaTime);
        Uranus.Rotate(Vector3.up * 30 * Time.deltaTime);

        Neptune.RotateAround(Sun.position, Ax8, 5 * Time.deltaTime);
        Neptune.Rotate(Vector3.up * 30 * Time.deltaTime);

        Pluto.RotateAround(Sun.position, Ax9, 3 * Time.deltaTime);
        Pluto.Rotate(Vector3.up * 30 * Time.deltaTime);
    }
}
```

