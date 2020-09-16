# netCore
.netCore3.1 下自定义Attribute授权验证Demo

获取Token接口地址 https://XXXXXX:XXXX/api/User/GetToken?userRole=TestRole

访问需要授权的接口需要在请求头中加入 Authorization：Bearer xxxx(你获得的token)