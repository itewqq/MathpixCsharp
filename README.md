# MathpixCsharp: C#实现的Mathpix Windows开源客户端

应该不会有外国用户所以就只写中文README了（雾）

![使用演示](/images/demo1.gif)

## 功能支持：

- [x] 截图识别公式转换为Latex代码
- [x] 截图识别公式转换为Office Word公式
- [x] 手写公式识别支持
- [x] 多显示器支持
- [x] 最小化到系统托盘
- [x] 快捷键截图
- [x] 官方API支持，每个月可免费用1000次，需要**国外信用卡**
- [x] 第三方API支持，**无需信用卡**

## 客户端安装:

### 使用安装包：

下载最新的[安装包](https://github.com/itewqq/MathpixCsharp/releases/download/0.0.7/MathpixCsharpV0.0.7.zip)，解压之后双击Setup.exe，按照提示走即可安装好MathpixCsharp。

### 从源代码编译：

#### 环境准备：

- Windows 10
- .NET Framework >= 4.6.1
- Visual studio 2017，2019
  
将代码clone到本地

```sh
git clone https://github.com/itewqq/MathpixCsharp.git
```

使用Visual Studio打开项目，在Setup1项目上右键->重新生成，然后在目录```MathpixCsharp\Setup1\Release```下即可找到setup.exe，双击安装即可。

## 使用方法

- 如果你有国外的信用卡，例如Visa，MasterCard，推荐直接使用官方的API，每个月可以免费用1000次。详见 [官方API使用教程](https://github.com/itewqq/MathpixCsharp/blob/dev/OfficialApi.md)
- 如果你没有国外的信用卡，可以：
  - 使用第三方API的Key，详见 [第三方API使用教程](https://github.com/itewqq/MathpixCsharp/blob/dev/ThirdpartyApi.md)
  - 使用我开发的[Web版](https://mathf.itewqq.cn/)，没有快捷键支持，且Word公式转换效果较差。Web版介绍：https://zhuanlan.zhihu.com/p/208212375
  
  
当然，如果你不差钱的话使用官方的服务永远是最好的选择 :P

另外还有一些功能有待完善，但是现在大概没空。~~丑是丑了点又不是不能用~~ 也可能不能用，欢迎大家提issue，欢迎Pr。

<img  src="/images/goodbye.jpg" height="250" align=center />
