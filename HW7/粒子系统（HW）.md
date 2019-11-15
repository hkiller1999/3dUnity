#### 1、简单粒子制作

1、按参考资源要求，制作一个粒子系统，[参考资源](https://www.cnblogs.com/CaomaoUnity3d/p/5983730.html)
（1）打开unity3d，按照参考资源，将给的资源打包import进项目中，我们先创建光晕，粒子系统GuangQiu，对GuangQiu进行调参。
具体参数如下图所示：
![在这里插入图片描述](https://img-blog.csdnimg.cn/2019111516571393.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/2019111523355958.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191115233606639.png)

![在这里插入图片描述](https://img-blog.csdnimg.cn/20191115233615870.png)
设置完GuangQiu后我们再创建一个粒子系统来对光晕进行修饰效果。
整个的设置如图所示：
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191115233855495.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191115233904508.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191115233915590.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191115233922752.png)
接下来我们开始模拟星光，创建粒子系统XinGuang，继续进行设置：
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191115234009240.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/2019111523401717.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191115234025370.png)
![在这里插入图片描述](https://img-blog.csdnimg.cn/201911152340347.png)
这样我们的第一个粒子系统就做好了，这里我们并没有完全按照教程所给的设置，是因为我在使用了其给的设置后发现没有达到预期效果，所以这里我后面进行了不同的调整。

2、使用 3.3 节介绍，用代码控制使之在不同场景下效果不一样。
在3.3中我们可以看到一个 对汽车尾气进行模拟的例子，同时下面也给出了参考的代码段，我们按照指示设计出两个场景。
第一个场景是在模拟粒子坍缩：
具体代码如下：

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midlight : MonoBehaviour {
    ParticleSystem midLight;
    float size = 2000f;

    // Use this for initialization
    void Start()
        {
            midLight = GetComponent<ParticleSystem>();
        }


    // Update is called once per frame
    void Update()
    {
        size = size * 0.99f;
        var main = midLight.main;
        main.startSize = size;
    }

}

```
第二个场景
粒子系统逐渐消失/消亡

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class elimnation : MonoBehaviour
{
    ParticleSystem elimnate;
    public float size = 4f;
    void Start()
    {
        elimnate = GetComponent<ParticleSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        size = size * 0.99f;
        var main = elimnate.main;
        main.startSize = size;
    }

}

```

我的github：[Github](https://github.com/hkiller1999/3dUnity/tree/master/HW7)
视频地址：http://www.iqiyi.com/w_19saxevhfl.html
