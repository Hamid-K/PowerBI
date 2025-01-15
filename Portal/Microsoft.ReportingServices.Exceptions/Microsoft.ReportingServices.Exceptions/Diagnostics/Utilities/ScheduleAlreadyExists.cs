using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	internal sealed class ScheduleAlreadyExists : ReportCatalogException
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000435A File Offset: 0x0000255A
		public ScheduleAlreadyExists(string name)
			: base(ErrorCode.rsScheduleAlreadyExists, ErrorStringsWrapper.rsScheduleAlreadyExists(name), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004371 File Offset: 0x00002571
		private ScheduleAlreadyExists(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
