using System;
using System.Globalization;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x02000203 RID: 515
	internal class DeviceAuthJWTResponse
	{
		// Token: 0x060015C2 RID: 5570 RVA: 0x00047E87 File Offset: 0x00046087
		public DeviceAuthJWTResponse(string audience, string nonce, string base64EncodedCertificate)
		{
			this._header = new DeviceAuthHeader(base64EncodedCertificate);
			this._payload = new DeviceAuthPayload(audience, nonce);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00047EA8 File Offset: 0x000460A8
		public string GetResponseToSign()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", Base64UrlHelpers.Encode(JsonHelper.SerializeToJson<DeviceAuthHeader>(this._header).ToByteArray()), Base64UrlHelpers.Encode(JsonHelper.SerializeToJson<DeviceAuthPayload>(this._payload).ToByteArray()));
		}

		// Token: 0x040008F0 RID: 2288
		private readonly DeviceAuthHeader _header;

		// Token: 0x040008F1 RID: 2289
		private readonly DeviceAuthPayload _payload;
	}
}
