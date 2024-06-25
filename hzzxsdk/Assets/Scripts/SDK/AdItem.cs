using System.Collections;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using WeChatWASM;
using YurenSDK;

public class AdItem : MonoBehaviour
{
    public Image adIcon;

    /// <summary>
    /// ����
    /// </summary>
    public Text adName;

    private IAd adComponent;

    public CanvasGroup canvasGroup;

    [Tooltip("������棬�ܶ���")]
    public bool isSingleAd = true;

    /// <summary>
    /// ����
    /// </summary>
    private int adIndex;

    public Image redImg;

    private void Awake()
    {
        //
    }

    private void Start()
    {
        initItem(0, true, 0);
    }

    /// <summary>
    /// ��ʼ�����item
    /// </summary>
    /// <param name="index">����</param>
    /// <param name="isSingleAd">�Ƿ�Ϊ�������</param>
    public void initItem(int index = 0, bool isSingleAd = false, int delaySecond = 3)
    {
        StopAllCoroutines();
        adIndex = index;
        StartCoroutine(DelayInit(0));
    }

    /// <summary>
    /// ��ʱ��ȡ�����Ϣ
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayInit(int second)
    {
        yield return new WaitForSeconds(second);
        if (Application.platform != RuntimePlatform.WindowsEditor)
        {
            SetItemInfo();
        }
    }

    /// <summary>
    /// ���������Ϣ
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayChangeItem(int second)
    {
        yield return new WaitForSeconds(second);
        if (adIndex > HzzxSDKHandler.Instance.RecommedAdList.Count - 2)
        {
            adIndex = 0;
        }
        else
        {
            adIndex++;
        }
        SetItemInfo();
    }

    /// <summary>
    /// ����item��Ϣ
    /// </summary>
    public void SetItemInfo()
    {
        if (adIndex > 100)
        {
            adIndex = 0;
        }
        adComponent = HzzxSDKHandler.Instance.RecommedAdList[adIndex];
        Debug.Log($"aditem count {adIndex} {HzzxSDKHandler.Instance.RecommedAdList.Count} " + adComponent.title + " " + adComponent.icon);
        if (adComponent.title == null || adComponent.icon == null)
        {
            canvasGroup.alpha = 0f;
            return;
        }
        else
        {
            canvasGroup.alpha = 1f;
        }

        StartCoroutine(this.GetTexture(adIndex, adIcon));

        string nameStr = adComponent.title;
        if (nameStr.Length > 5)
        {
            nameStr = nameStr.Remove(5, nameStr.Length - 5);
            nameStr += "...";
        }
        adName.text = nameStr;
    }

    public IEnumerator GetTexture(int adIndex, Image imageComponent)
    {
        if (HzzxSDKHandler.Instance.RecommedAdList.Count <= 0)
        {
            yield break;
        }

        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(HzzxSDKHandler.Instance.RecommedAdList[adIndex].icon))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log(uwr.error);
            }
            else
            {
                Texture2D texture2D = DownloadHandlerTexture.GetContent(uwr);
                //if (texture2D == null)
                //{
                //    //Debug.Log($"���ص�ͼƬΪ��! {uwr.uri}");
                //}
                Sprite adSp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * 0.5f);
                imageComponent.sprite = adSp;
                imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 80);
                imageComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80);
                //imageComponent.SetNativeSize();
                //imageComponent.transform.localScale = Vector3.one * 0.35f;
            }
        }
    }

    /// <summary>
    /// �����ת
    /// </summary>
    public void OnClick()
    {
        redImg.gameObject.SetActive(false);
        HzzxSDKHandler.Instance.ClickAndNavigate(JsonMapper.ToJson(adComponent), 0);
        //Debug.Log("�����ת");
    }
}
