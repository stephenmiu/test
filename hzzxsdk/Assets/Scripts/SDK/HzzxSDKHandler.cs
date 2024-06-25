using LitJson;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YurenSDK
{
    /// <summary>
    /// js消息回调
    /// </summary>
    public class JsCallBack
    {
        /// <summary>
        /// success fail
        /// </summary>
        public string type;

        /// <summary>
        /// 返回的数据
        /// </summary>
        public string res;
    }

    /// <summary>
    /// js和unity互相通信
    /// </summary>
    public class HzzxSDKHandler : MonoBehaviour
    {
        private static HzzxSDKHandler instance = null;


        public static HzzxSDKHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    if (!Application.isPlaying)
                    {
                        Debug.LogError("不支持在非播放模式下调用WX接口");
                        return null;
                    }
                    instance = new GameObject(typeof(HzzxSDKHandler).Name).AddComponent<HzzxSDKHandler>();
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }

        [DllImport("__Internal")]
        private static extern void test();

        public void Test()
        {
            test();
        }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void WX_InitWithThirdUser(string pid, string openid, string unionid);
#else
        private static void WX_InitWithThirdUser(string pid, string openid, string unionid) { }
#endif

        public void InitWithThirdUser(string pid, string openid, string unionid)
        {
            WX_InitWithThirdUser(pid, openid, unionid);
        }


#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void WX_InitAndGetOpenId(string pid, bool isNeedUnionid);
#else
        private static void WX_InitAndGetOpenId(string pid, bool isNeedUnionid) { }
#endif

        public void InitAndGetOpenId(string pid, bool isNeedUnionid)
        {
            WX_InitAndGetOpenId(pid, isNeedUnionid);
        }

        /// <summary>
        /// 接收初始化回调
        /// </summary>
        /// <param name="msg"></param>
        public void GetInitCallback(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                //Debug.Log("GetRecommedAdListCallback ==>" + msg);
                var jsCallback = JsonMapper.ToObject<JsCallBack>(msg);
                var type = jsCallback.type;
                var res = jsCallback.res;

                //Debug.Log(res);
                if (type == "success")
                {
                    // Debug.Log("GetRecommedAdListCallback success ");
                    GetRecommedAdList(false, 0, 0);
                }
                else if (type == "fail")
                {
                    //Debug.Log("GetRecommedAdListCallback fail");
                }
            }
        }


#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void WX_ClickAndNavigate(string ad, int sceneCode);
#else
        private static void WX_ClickAndNavigate(string ad, int sceneCode) { }
#endif

        public void ClickAndNavigate(string ad, int sceneCode)
        {
            WX_ClickAndNavigate(ad, sceneCode);
        }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void WX_GetRecommedAdList(bool isRandomSort, int count, int score);
#else
        private static void WX_GetRecommedAdList(bool isRandomSort, int count, int score)
        {
        }
#endif
        public void GetRecommedAdList(bool isRandomSort, int count, int score)
        {
            // Debug.Log("GetRecommedAdList");
            WX_GetRecommedAdList(isRandomSort, count, score);
        }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void WX_GetBannerAdList(bool isRandomSort, int count, int score);
#else
        private static void WX_GetBannerAdList(bool isRandomSort, int count, int score)
        {
        }
#endif
        public void GetBannerAdList(bool isRandomSort, int count, int score)
        {
            //Debug.Log("GetBannerAdList");
            WX_GetBannerAdList(isRandomSort, count, score);
        }

        public List<IAd> RecommedAdList = new List<IAd>();
        /// <summary>
        /// 接收轮播广告回调
        /// </summary>
        /// <param name="msg"></param>
        public void GetRecommedAdListCallback(string msg)
        {
            if (!string.IsNullOrEmpty(msg) && RecommedAdList != null)
            {
                //Debug.Log("GetRecommedAdListCallback ==>" + msg);
                var jsCallback = JsonMapper.ToObject<JsCallBack>(msg);
                var type = jsCallback.type;
                var res = jsCallback.res;

                //Debug.Log(res);
                if (type == "success")
                {
                    // Debug.Log("GetRecommedAdListCallback success ");
                    if (res != null)
                        RecommedAdList = JsonMapper.ToObject<List<IAd>>(res);
                }
                else if (type == "fail")
                {
                    //Debug.Log("GetRecommedAdListCallback fail");
                }
            }
        }

        public List<IAd> BannerAdList = new List<IAd>();
        /// <summary>
        /// 接收轮播广告回调
        /// </summary>
        /// <param name="msg"></param>
        public void GetBannerAdListCallback(string msg)
        {
            if (!string.IsNullOrEmpty(msg) && BannerAdList != null)
            {
                //Debug.Log("GetBannerAdListCallback ==>" + msg);
                var jsCallback = JsonMapper.ToObject<JsCallBack>(msg);
                var type = jsCallback.type;
                var res = jsCallback.res;
                //Debug.Log(res);
                if (type == "success")
                {
                    //Debug.Log("GetBannerAdListCallback success");
                    if (res != null)
                        BannerAdList = JsonMapper.ToObject<List<IAd>>(res);
                }
                else if (type == "fail")
                {
                    //Debug.Log("GetBannerAdListCallback fail");
                }
            }
        }

    }

    public class IAd
    {
        public string id;
        public string pid;
        public string appid;
        public string image;
        public string icon;
        public string ad_image;
        public string banner;
        public string title;
        public string description;
        public string button;
        public string coins;
        public string page;
        public string count;
        public string category;
        public string plan_number;
        //public string table;
        public string start_time;
        public string end_time;
        public string platform;
        //public int sex;
        public string uservalue;
        public bool clicked;
        public string audi;
        public string classify;
        public string city_show;
        public string city_hide;
    }
}

