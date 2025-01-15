using System;

namespace Microsoft.OData
{
	// Token: 0x0200006E RID: 110
	public static class ODataMessageExtensions
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0000A900 File Offset: 0x00008B00
		public static ODataVersion GetODataVersion(this IODataResponseMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataResponseMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetODataVersion(odataMessage, defaultVersion);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000A928 File Offset: 0x00008B28
		public static ODataVersion GetODataVersion(this IODataRequestMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataRequestMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetODataVersion(odataMessage, defaultVersion);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000A94E File Offset: 0x00008B4E
		public static ODataPreferenceHeader PreferHeader(this IODataRequestMessage requestMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			return new ODataPreferenceHeader(requestMessage);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000A962 File Offset: 0x00008B62
		public static ODataPreferenceHeader PreferenceAppliedHeader(this IODataResponseMessage responseMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			return new ODataPreferenceHeader(responseMessage);
		}
	}
}
