using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	internal sealed class InternalDataSourceNotFoundException : ReportCatalogException
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00004555 File Offset: 0x00002755
		public InternalDataSourceNotFoundException()
			: base(ErrorCode.rsInternalDataSourceNotFound, ErrorStringsWrapper.internalDataSourceNotFound, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000456B File Offset: 0x0000276B
		private InternalDataSourceNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
