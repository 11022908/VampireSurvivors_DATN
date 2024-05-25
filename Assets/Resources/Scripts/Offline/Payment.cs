using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class PaymentManager : MonoBehaviour
{
    public void CreatePaymentLink()
    {
        StartCoroutine(CreatePaymentLinkCoroutine());
    }

    private IEnumerator CreatePaymentLinkCoroutine()
    {
        // URL endpoint của backend PHP
        string url = "http://localhost/phppayos/index.php";

        // Tạo form dữ liệu
        WWWForm form = new WWWForm();
        form.AddField("orderCode", Random.Range(100000, 1000000).ToString());
        form.AddField("amount", "2000");
        form.AddField("description", "Thanh toan don hang");
        form.AddField("returnUrl", "http://localhost:3030/success.html");
        form.AddField("cancelUrl", "http://localhost:3030/cancel.html");

        // Gửi yêu cầu POST đến backend PHP
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Lấy kết quả từ backend PHP
                PaymentLinkResponse response = JsonUtility.FromJson<PaymentLinkResponse>(request.downloadHandler.text);

                // Chuyển hướng người dùng đến URL thanh toán
                Application.OpenURL(response.checkoutUrl);
            }
            else
            {
                Debug.LogError("Error creating payment link: " + request.error);
            }
        }
    }

    [System.Serializable]
    public class PaymentLinkResponse
    {
        public string checkoutUrl;
    }
}