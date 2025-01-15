using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	internal sealed class OperationNotSupportedException : ReportCatalogException
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00004D8F File Offset: 0x00002F8F
		public OperationNotSupportedException(string operation)
			: base(ErrorCode.rsOperationNotSupported, ErrorStringsWrapper.rsOperationNotSupported(operation), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00004DA9 File Offset: 0x00002FA9
		private OperationNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
