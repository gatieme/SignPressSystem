# SignPressClient

#build-003
实现播放语音的功能 
播放语音的几种方法包括

1.使用API函数。   winmm.dll
下边的类中使用了PlaySound、sndPlaySound、mciSendString  3个API函数来播放声音。

SpeechLib

2.使用SoundPlayer类播放。

3.使用DirectX进行播放。

一、使用API函数进行播放

http://www.cnblogs.com/net-study/archive/2013/07/10/3181674.html
http://www.sufeinet.com/thread-459-1-1.html
http://www.jb51.net/article/64864.htm
http://bbs.csdn.net/topics/390660311?page=1

另外为了让消费框能够显示中文的大写数字，
因此取消了提交管理时两个textbox的KeyPress对数字按键的监控


#WINXP
-------

WIN-XP上需要json-async以及补丁NDP40-KB2468871-v2
将.Net运行环境从4.5更换至4.0
重新安装json库
安装BCLasync以支持async/await特性
[如何在.net4.0中使用.net4.5的async/await实现异步](http://blog.csdn.net/gatieme/article/details/50571845)
那需要你去微软官网下载.net4.0的[KB2468871](https://www.microsoft.com/zh-cn/download/details.aspx?id=3556) 补丁来安装。

如果遇见其他问题，请参见
https://blogs.msdn.microsoft.com/bclteam/p/asynctargetingpackkb/
http://stackoverflow.com/questions/21214715/could-not-load-file-or-assembly-system-threading-tasks-version-2-5-19-0
