using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;

namespace secureacceptance
{
    public static class Security
    {
        //private const String SECRET_KEY = "26f2cbf6892e49318247c09ca490a20e2cd21ee4c81a405c817c6f21202a892fb69c4ad0956a426a91097ec9791efa429f6f53c5a6614c7fa0af06dae98dfad3d1aba074967c402fa4759967e66ed8400ce02ada39fe4264b57af0a385a541b0ebe50f78416e433087b168873b76adcca2713ee4253f4e9ea8cd715a0aca8801";       // Test Key
        private const String SECRET_KEY = "586894b8f0854c2d972fbafd382a9d28dd9e63e2d3344a659357d9fb3f52740ce236f6164479485b88e987275ad95f118ab2bcfdd9d14719bff74099dd4a749b688f8d450d174710a8f479897f03128e33d1d8003a7b49f39bb3a1684686c94ee1ffca13263942fd868f7abde60a734816df86d6d5fc419e89384ccbe9e61088";     // Live Key
        
        public static String sign(IDictionary<string, string> paramsArray)  {
            return sign(buildDataToSign(paramsArray), SECRET_KEY);
        }

        private static String sign(String data, String secretKey) {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
						byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static String buildDataToSign(IDictionary<string,string> paramsArray) {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

	        foreach (String signedFieldName in signedFieldNames)
	        {
	             dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
	        }

            return commaSeparate(dataToSign);
        }

        private static String commaSeparate(IList<string> dataToSign) {
            return String.Join(",", dataToSign);                         
        }
    }
}
