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
