using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	internal sealed class InternalRepublishingException : ReportCatalogException
	{
		// Token: 0x0600028B RID: 651 RVA: 0x000052AC File Offset: 0x000034AC
		public InternalRepublishingException(string itemPath, Exception innerException, byte[] contents)
			: base(ErrorCode.rsInternalRepublishingFailed, string.Format(CultureInfo.CurrentCulture, "Report upgrade failed for item '{0}'.", itemPath), innerException, null, new object[] { contents })
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000052E0 File Offset: 0x000034E0
		private InternalRepublishingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
