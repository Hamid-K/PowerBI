using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000133 RID: 307
	public static class ODataMessageExtensions
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x0001A0DC File Offset: 0x000182DC
		public static ODataVersion GetDataServiceVersion(this IODataResponseMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataResponseMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetDataServiceVersion(odataMessage, defaultVersion);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001A104 File Offset: 0x00018304
		public static ODataVersion GetDataServiceVersion(this IODataRequestMessage message, ODataVersion defaultVersion)
		{
			ODataMessage odataMessage = new ODataRequestMessage(message, false, false, long.MaxValue);
			return ODataUtilsInternal.GetDataServiceVersion(odataMessage, defaultVersion);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001A12A File Offset: 0x0001832A
		public static ODataPreferenceHeader PreferHeader(this IODataRequestMessage requestMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			return new ODataPreferenceHeader(requestMessage);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001A13D File Offset: 0x0001833D
		public static ODataPreferenceHeader PreferenceAppliedHeader(this IODataResponseMessage responseMessage)
		{
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			return new ODataPreferenceHeader(responseMessage);
		}
	}
}
