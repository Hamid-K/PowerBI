using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	internal sealed class ScheduleDateTimeRangeException : ReportCatalogException
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000439D File Offset: 0x0000259D
		public ScheduleDateTimeRangeException()
			: base(ErrorCode.rsScheduleDateTimeRangeException, ErrorStringsWrapper.rsScheduleDateTimeRangeException, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000043B3 File Offset: 0x000025B3
		private ScheduleDateTimeRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
