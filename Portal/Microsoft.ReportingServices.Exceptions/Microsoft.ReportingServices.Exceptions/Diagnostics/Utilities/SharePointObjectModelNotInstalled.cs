using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	internal sealed class SharePointObjectModelNotInstalled : ReportCatalogException
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00004BA3 File Offset: 0x00002DA3
		public SharePointObjectModelNotInstalled(Exception sharePointObjectModelLoadException)
			: base(ErrorCode.rsSharePointObjectModelNotInstalled, ErrorStringsWrapper.rsSharePointObjectModelNotInstalled((sharePointObjectModelLoadException != null) ? sharePointObjectModelLoadException.ToString() : string.Empty), sharePointObjectModelLoadException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00004BCC File Offset: 0x00002DCC
		private SharePointObjectModelNotInstalled(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
