using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	internal sealed class ScheduleNotFoundException : ReportCatalogException
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00004339 File Offset: 0x00002539
		public ScheduleNotFoundException(string idOrData)
			: base(ErrorCode.rsScheduleNotFound, ErrorStringsWrapper.rsScheduleNotFound(idOrData), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004350 File Offset: 0x00002550
		private ScheduleNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
