using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	internal sealed class WindowsAuthzApiException : ReportCatalogException
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00004E52 File Offset: 0x00003052
		public WindowsAuthzApiException(string methodName, string errorCode, string username)
			: base(ErrorCode.rsWinAuthzError, ErrorStringsWrapper.rsWinAuthz(methodName, errorCode, username), null, null, Array.Empty<object>())
		{
		}
	}
}
