using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200017D RID: 381
	public static class ODataMessageExtensions
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x00031B20 File Offset: 0x0002FD20
		public static ODataVersion GetODataVersion(this IODataResponseMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataResponseMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetODataVersion(odataMessage, defaultVersion);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00031B48 File Offset: 0x0002FD48
		public static ODataVersion GetODataVersion(this IODataRequestMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataRequestMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetODataVersion(odataMessage, defaultVersion);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00031B6E File Offset: 0x0002FD6E
		public static ODataPreferenceHeader PreferHeader(this IODataRequestMessage requestMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			return new ODataPreferenceHeader(requestMessage);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00031B81 File Offset: 0x0002FD81
		public static ODataPreferenceHeader PreferenceAppliedHeader(this IODataResponseMessage responseMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			return new ODataPreferenceHeader(responseMessage);
		}
	}
}
