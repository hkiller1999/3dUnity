#### Unity3d作业（六）
本次作业的内容为：改进飞碟（Hit UFO）游戏

游戏内容要求：
1、按 adapter模式 设计图修改飞碟游戏
2、使它同时支持物理运动与运动学（变换）运动

1、按 adapter模式 设计图修改飞碟游戏
我们首先需要知道和了解Adapter模式：
Adapter模式分为两种:
1.类适配器模式
2.委托适配器
我们这里更多使用的是类适配器模式。
适配器模式（Adapter）包含以下主要角色。
目标（Target）接口：当前系统业务所期待的接口，它可以是抽象类或接口。
适配者（Adaptee）类：它是被访问和适配的现存组件库中的组件接口。
适配器（Adapter）类：它是一个转换器，通过继承或引用适配者的对象，把适配者接口转换成目标接口，让客户按目标接口的格式访问适配者。
我们如果要在原来的基础上实现物理效果，必须明确谁是适配器谁是适配者。我们过去已经将飞碟游戏的运动功能放在了CCActionManager.cs上，所以这里是非物理学运动，我们要进行添加。在不删除原有的非物理学变换运动动作管理器，可以通过适配器模式来实现。
关于本次游戏的改动具体如图所示：

![在这里插入图片描述](https://img-blog.csdnimg.cn/20191018212423755.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2hraWxsZXIxOTk5,size_16,color_FFFFFF,t_70)
在原有代码的基础上增加一个PhysisActionManager动作管理器类，

```
public class PhysicsActionManager : SSActionManager, ISSActionCallback, IActionManager {
    public FirstSceneController sceneController;
    public List<PhysicsEmitAction> seq = new List<PhysicsEmitAction>();
    public UserClickAction userClickAction;
    public DiskFactory disks;

    protected void Start()
    {
        sceneController = (FirstSceneController)SSDirector.getInstance().currentSceneController;
        sceneController.actionManager = this;
        disks = Singleton<DiskFactory>.Instance;
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
        ....
    }
    public void CheckEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
    {
    }
    public void Pause()
    {
       .......
    }
    public void PlayDisk()
    {
    ......
    }
}
```
这里使用了动作类为PhysisActionMove动作类。
同时将刚体Rigidbody组件为游戏对象提供物理属性，让游戏对象在场景中可以受到物理引擎的作用。当游戏对象添加了Rigidbody组件后，便使物体具有了相应的物理力。

同时这里我们使用IActionManger作为接口：

```
public interface IActionManager {
    void PlayDisk();
    void Pause();
}
```

CCActionManager修改Update函数为PlayDisk函数。只需要修改名字即可，其余不变。 
FirstSceneController函数修改三个地方：


##### FirstSceneController:
```
//public CCActionManager actionManager修改为:
public IActionManager actionManager;
```

```
this.gameObject.AddComponent<CCActionManager>()修改为：
this.gameObject.AddComponent<PhysicsActionManager>();
```

```
public void Update()
{
    if (times < 30 && flag == 0)
    {
        if (interval <= 0)
        {
            interval = Random.Range(3, 5);
            times++;
            df.GenDisk();
        }
        interval -= Time.deltaTime;
    }
    //ADD
    actionManager.PlayDisk();
}
```
接下来实现PhysicsEmitAction：

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEmitAction : SSAction {
    public Vector3 speed;

    public static PhysicsEmitAction GetSSAction()
    {
        PhysicsEmitAction action = CreateInstance<PhysicsEmitAction>();
        return action;
    }
    public override void Start()
    {
    }
    public override void Update()
    {
        if (transform.position.y < -10 || transform.position.x <= -20 || transform.position.x >= 20)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = Vector3.down;
            callback.SSActionEvent(this);
        }
    }
}
```
这里PhysicsEmitAction实现了对飞碟物理属性，例如速度等的控制。
具体实现请移步github。
[项目地址](https://github.com/hkiller1999/3dUnity/tree/master/HW6)
参考资料：
[pmlpml的github](https://github.com/pmlpml/unity3d-learning/tree/ex-physics/Assets)
[参考项目](https://github.com/liuwd8/unity/tree/master/Hit%20UFO%28Physics%29)
参考课件
