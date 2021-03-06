# tpns.serversdk.csharp
tpns.serversdk.csharp
腾讯移动推送服务端SDK的csharp版

测试通过，有一个bug:
发送中文时，安卓手机收到推送会看到%字符

2021.3.6 中文问题已经解决，
签名用utf8,原来的代码是从官网复制过来的，有问题
