## 更新：已经实现了完全免费无需信用卡和客户端的web版本，地址： https://mathcode.herokuapp.com/   Web版介绍：https://zhuanlan.zhihu.com/p/208212375

### 有任何疑问和建议请提[issue](https://github.com/itewqq/MathpixCsharp/issues)

P.S.：如果web版出现

Application error An error occurred in the application and your page could not be served. If you are the application owner, check your logs for details. You can do this from the Heroku CLI with the command

说明我本月的heroku免费额度用光了emmmmm（贫穷）：

H82 - Free dyno quota exhausted This indicates that an account’s free dyno hour quota is exhausted and that apps running free dynos are sleeping. You can view your app’s free dyno usage in the Heroku dashboard.

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

![使用测试](/images/test3.gif)

### Requirements:
- 信用卡一张(visa/mastercard，etc)
- .NET Framework >= 4.6.1
- Windows 10
- Visual studio 2017，2019(如果您想自己编译的话)

### 使用方法：
- 首先到 https://dashboard.mathpix.com/ 注册一个开发者账户，需要用到**信用卡**，可能会扣1美元验证信用卡有效性，会退回来。
- 注册登录之后就可以看到有一个API keys，在页面的最上面，包括两个字段 app_id 和 app_key，保存下来。
- 下载最新的[安装包](https://github.com/itewqq/MathpixCsharp/releases/download/0.0.4/MathpixCsharpV0.0.4.zip)，解压之后双击Setup.exe安装好MathpixCsharp。
- 打开之后在login的窗口里填入之前的app_id和app_key，只需要填一次之后就不用填了。如果填错了可以在主界面点击菜单->重置Key。
- 使用多个显示器时候，MathpixCsharp将自动选取**其窗体当前所在的屏幕**进行截屏。
- 截屏可以通过点击按钮截屏，也可以通过快捷键Ctrl+Alt+M来截屏，该快捷键与mathpix一样所以为了避免冲突请先退出mathpix。。
- 当点击关闭窗口的时候，MathpixCsharp会自动最小化到系统托盘，右键系统托盘可选退出。
- ~~丑是丑了点又不是不能用~~ 也可能不能用，欢迎大家提issue。
- **WORD**用户请点击**复制MathML**按钮，然后在word文档里**右键->粘贴->仅粘贴文本** 注意不能直接Ctrl+V！！！
- 新版Office也可以直接在Word里使用latex。

另外还有一些功能有待完善，但是现在大概没空

<img  src="/images/goodbye.jpg" height="250" align=center />
