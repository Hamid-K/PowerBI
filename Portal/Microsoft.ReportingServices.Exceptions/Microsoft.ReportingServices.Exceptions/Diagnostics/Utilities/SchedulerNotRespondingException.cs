using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	internal sealed class SchedulerNotRespondingException : ReportCatalogException
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x000042D8 File Offset: 0x000024D8
		public SchedulerNotRespondingException()
			: base(ErrorCode.rsSchedulerNotResponding, ErrorStringsWrapper.rsSchedulerNotResponding, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000042EE File Offset: 0x000024EE
		private SchedulerNotRespondingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
