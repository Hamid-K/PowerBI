using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	internal sealed class SchedulerNotRespondingPreventsPinningException : ReportCatalogException
	{
		// Token: 0x060001AA RID: 426 RVA: 0x000042F8 File Offset: 0x000024F8
		public SchedulerNotRespondingPreventsPinningException()
			: base(ErrorCode.rsSchedulerNotResponding, ErrorStringsWrapper.rsSchedulerNotRespondingPreventsPinning, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000430E File Offset: 0x0000250E
		private SchedulerNotRespondingPreventsPinningException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
