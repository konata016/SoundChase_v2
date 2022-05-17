using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
    private const string Url = "https://script.google.com/macros/s/AKfycbypF3J3CbJFsuRe83Uu3xR2IsPBXqAKix1JycyDoeA_JiUWF1Pk/exec";

    private void Start()
    {
        StartCoroutine(Get());
    }

    public IEnumerator Get()
    {
        var id = "1vxEmnxYwg61VMURmkY8So5M-tmQJRdnY_avsfZdfyCM";
        var api = "AIzaSyDZNkGr0X0DoZygWpByfc_SPXZMpBPRJ3M";
        var key = "MckVbLgkjPPJLWRrY41f0jRsLJK_JktQW";
        var request = UnityWebRequest.Get($"{Url}?param=c");
        //UnityWebRequest.Get(Url + "?sheetName=" + "�V�[�g1");

        yield return request.SendWebRequest();

        //var result = request.downloadHandler.text;
        //var response = JsonUtility.FromJson<ResponseData>(result);
        //Debug.Log($"Rank�F{response.Key1} ID�F{response.Key2} Score�F{response.Key3}");
    }

    public IEnumerator Post()
    {
        var param = "c";
        var jsonBody = $"{{ \"param\" : \"{param}\" }}";
        var request = new UnityWebRequest(Url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        var result = request.downloadHandler.text;
        //var response = JsonUtility.FromJson<ResponseData>(result);
        //Debug.Log(response.message);
    }

    [System.Serializable]
    public class ResponseData
    {
        public int Key1;
        public string Key2;
        public int Key3;
    }

    //https://sheets.googleapis.com/v4/spreadsheets/�o�X�v���b�h�V�[�gID�p/values/�o�V�[�g���p?key=�oAPI�L�[�p
    //https://sheets.googleapis.com/v4/spreadsheets/1vxEmnxYwg61VMURmkY8So5M-tmQJRdnY_avsfZdfyCM/values/�V�[�g1?key=AIzaSyDZNkGr0X0DoZygWpByfc_SPXZMpBPRJ3M
}
