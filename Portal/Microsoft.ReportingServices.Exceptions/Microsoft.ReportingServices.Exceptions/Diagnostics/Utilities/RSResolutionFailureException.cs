using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	internal sealed class RSResolutionFailureException : ReportCatalogException
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x000047C0 File Offset: 0x000029C0
		public RSResolutionFailureException(string databaseFullName)
			: this(null, databaseFullName)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000047CA File Offset: 0x000029CA
		public RSResolutionFailureException(Exception innerException, string databaseFullName)
			: base(ErrorCode.rsResolutionFailureException, ErrorStringsWrapper.rsResolutionFailureException(databaseFullName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000047E4 File Offset: 0x000029E4
		private RSResolutionFailureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
