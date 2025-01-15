using System;

namespace Microsoft.OData
{
	// Token: 0x02000094 RID: 148
	public static class ODataMessageExtensions
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		public static ODataVersion GetODataVersion(this IODataResponseMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataResponseMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetODataVersion(odataMessage, defaultVersion);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000CE04 File Offset: 0x0000B004
		public static ODataVersion GetODataVersion(this IODataRequestMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataRequestMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetODataVersion(odataMessage, defaultVersion);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000CE2A File Offset: 0x0000B02A
		public static ODataPreferenceHeader PreferHeader(this IODataRequestMessage requestMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			return new ODataPreferenceHeader(requestMessage);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000CE3E File Offset: 0x0000B03E
		public static ODataPreferenceHeader PreferenceAppliedHeader(this IODataResponseMessage responseMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			return new ODataPreferenceHeader(responseMessage);
		}
	}
}
