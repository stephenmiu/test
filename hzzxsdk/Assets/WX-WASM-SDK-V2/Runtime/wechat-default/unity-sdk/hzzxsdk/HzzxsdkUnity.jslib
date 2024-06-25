mergeInto(LibraryManager.library, {
    test:function()
    {
        window.WXWASMSDK.test();
    },

    WX_InitWithThirdUser:function(pid, openid, unionid){
        window.WXWASMSDK.WX_InitWithThirdUser(_WXPointer_stringify_adaptor(pid), _WXPointer_stringify_adaptor(openid), _WXPointer_stringify_adaptor(unionid));
    },

    WX_InitAndGetOpenId:function(pid, isNeedUnionid){
        window.WXWASMSDK.WX_InitAndGetOpenId(_WXPointer_stringify_adaptor(pid), _WXPointer_stringify_adaptor(isNeedUnionid));
    },

    WX_ClickAndNavigate:function(ad,sceneCode){
        window.WXWASMSDK.WX_ClickAndNavigate(_WXPointer_stringify_adaptor(ad),sceneCode);
    },

    WX_GetRecommedAdList:function(isRandomSort, count, score){    
        window.WXWASMSDK.WX_GetRecommedAdList(isRandomSort, count, score);
    },

    WX_GetBannerAdList:function(isRandomSort, count, score){    
        window.WXWASMSDK.WX_GetBannerAdList(isRandomSort, count, score);
    },
});