using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	internal sealed class CannotPrepareQueryException : ReportCatalogException
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000486F File Offset: 0x00002A6F
		public CannotPrepareQueryException(Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsCannotPrepareQuery, ErrorStringsWrapper.rsCannotPrepareQuery, innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00004885 File Offset: 0x00002A85
		public CannotPrepareQueryException(Exception innerException)
			: this(innerException, null)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000488F File Offset: 0x00002A8F
		public CannotPrepareQueryException(string additionalTraceMessage)
			: this(null, additionalTraceMessage)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00004899 File Offset: 0x00002A99
		private CannotPrepareQueryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
