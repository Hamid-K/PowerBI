using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	internal sealed class WindowsAuthz5ApiException : ReportCatalogException
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00004E6E File Offset: 0x0000306E
		public WindowsAuthz5ApiException(string methodName, string username)
			: base(ErrorCode.rsWinAuthzError5, ErrorStringsWrapper.rsWinAuthz5(methodName, username), null, null, Array.Empty<object>())
		{
		}
	}
}
