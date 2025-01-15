using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	internal sealed class WindowsAuthz1355ApiException : ReportCatalogException
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00004E89 File Offset: 0x00003089
		public WindowsAuthz1355ApiException(string methodName, string username)
			: base(ErrorCode.rsWinAuthzError1355, ErrorStringsWrapper.rsWinAuthz1355(methodName, username), null, null, Array.Empty<object>())
		{
		}
	}
}
