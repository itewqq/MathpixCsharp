# MathpixCsharp

## 每个月可以免费用1000次的Mathpix Windows客户端，支持图片转word公式，图片转latex代码

鉴于一个月5美元对于国外友人的消费水平来说应该不是问题。。我就只写中文的readme了。。。

这个项目的起因就是偶然有一天发现[mathpix给开发者用的api](https://mathpix.com/ocr)的定价策略很神奇。。。

- First 1000 requests free
- $0.004/request (1-100K requests)
- $0.002/request (100-300K requests)
- $0.001/request (300K+ requests)

emm照这个价格原来每个月5美刀的钱能用到秃头了。

于是就自己写了一个客户端？把API key填进去就可以用了。（我愿称之为木兰mathipix）

![使用测试](/images/test2.gif)

### Requirements:
- 信用卡一张(visa/mastercard，etc)
- .NET Framework >= 4.6.1
- Windows 10
- Visual studio 2017，2019(如果您想自己编译的话)

### 使用方法：
- 首先到 https://dashboard.mathpix.com/ 注册一个开发者账户，需要用到**信用卡**，可能会扣1美元验证信用卡有效性，会退回来。
- 注册登录之后就可以看到有一个API keys，在页面的最上面，包括两个字段 app_id 和 app_key，保存下来。
- 通过[msi](https://github.com/itewqq/MathpixCsharp/releases/download/0.0.1/MathpixCsharp.msi)安装好MathpixCsharp
- 打开之后在login的form里填入之前的app_id和app_key，只需要第一次打开程序的时候填写。
- 之后的界面应该能看懂什么意思了~~丑是丑了点又不是不能用~~ 也可能不能用，欢迎大家提issue
- **WORD**用户请点击**复制MathML**按钮，然后在word文档里**右键->粘贴->仅粘贴文本** 注意不能直接Ctrl+V！！！

如果需求比较大的话，以后可以考虑把5美元每月的api自己包装一下来做个全免费的。。毕竟使用次数不限，更重要的是对很多学生党来说信用卡可能不太好搞

另外还有一些功能有待完善，但是现在大概没空

<img  src="/images/goodbye.jpg" height="250" align=center />
