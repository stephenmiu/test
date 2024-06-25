import { formatResponse } from '../sdk';

require('../hzzxsdk/hzzxsdk');

export default {
    test() {
        console.log("js test");
    },

    WX_InitWithThirdUser(pid, openid, unionid) {
        let user = {
            openid: openid,
            unionid: unionid
        };
        //console.log('initWithThirdUser ', user, pid);
        g_hzzxsdk.initWithThirdUser(pid, user).then(openid => {
            //console.log('initWithThirdUser 获得openid', openid);
        }).catch(err => {
            //console.log('initWithThirdUser 初始化失败.', err);
        });
    },


    WX_InitAndGetOpenId(pid, isNeedUnionid = false) {
        //console.log('InitAndGetOpenId ', pid);
        g_hzzxsdk.initAndGetOpenId(pid, isNeedUnionid).then(openid => {
            console.log('initAndGetOpenId 获得openid', openid);
            GameGlobal.Module.SendMessage("HzzxSDKHandler", 'GetInitCallback', JSON.stringify({ type: "success", res: JSON.stringify("null") }));
        }).catch(err => {
            GameGlobal.Module.SendMessage("HzzxSDKHandler", 'GetInitCallback', JSON.stringify({ type: "fail", res: JSON.stringify("null") }));
            console.log('initAndGetOpenId 初始化失败.', err);
        });
    },

    WX_ClickAndNavigate(ad, sceneCode) {
        //ad = formatJsonStr(ad);
        //console.log('跳转：', ad, sceneCode);
        // 广告数据
        // 点击广告的位置
        g_hzzxsdk.clickAndNavigate(JSON.parse(ad), sceneCode).then(res => {
            //console.log('跳转是否成功 = ',res.navigateTo);
            //console.log('是否是第一次点击 = ',res.isFristClick);
        });
    },

    WX_GetBannerAdList(isRandomSort, count, score) {
        g_hzzxsdk.getBannerAdList(isRandomSort, count, score).then(adList => {
            //这里返回的是一个ad列表
            //console.log('刷新banner分享广告列表:', adList, adList.length);
            //formatResponse('getBannerAdList', adList);
            //formatResponse('getBannerAdList', adList);
            if (adList == undefined) {
                GameGlobal.Module.SendMessage("HzzxSDKHandler", 'GetBannerAdListCallback', JSON.stringify({ type: "fail", res: JSON.stringify("null") }));
            } else {
                var data = new Array(adList.length);
                for (var i = 0; i < adList.length; i++) {
                    data[i] = adList[i];
                }
                //js通信unity 
                GameGlobal.Module.SendMessage("HzzxSDKHandler", 'GetBannerAdListCallback', JSON.stringify({ type: "success", res: JSON.stringify(data) }));

            }
        });
    },

    WX_GetRecommedAdList(isRandomSort, count, score) {
        //conf = formatJsonStr(conf);
        //console.log('推荐广告列表列表：',conf.isRandomSort,conf.count, conf.score);
        g_hzzxsdk.getRecommedAdList(isRandomSort, count, score).then(adList => {
            //这里返回的是一个ad列表
            //console.log('刷新推荐广告列表:', adList);
            //formatResponse('GetRecommedAdListCallback', adList);
            //formatResponse('GetRecommedAdListCallback', adList);
            if (adList == undefined) {
                GameGlobal.Module.SendMessage("HzzxSDKHandler", 'GetRecommedAdListCallback', JSON.stringify({ type: "fail", res: JSON.stringify("null") }));
            } else {
                var data = new Array(adList.length);
                for (var i = 0; i < adList.length; i++) {
                    data[i] = adList[i];
                }
                //js通信unity 
                GameGlobal.Module.SendMessage("HzzxSDKHandler", 'GetRecommedAdListCallback', JSON.stringify({ type: "success", res: JSON.stringify(data) }));
            }
        });
    },
}