using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	internal sealed class PBIServiceUnavailable : ReportCatalogException
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00003F16 File Offset: 0x00002116
		public PBIServiceUnavailable(string correlationId)
			: base(ErrorCode.rsPBIServiceUnavailable, ErrorStringsWrapper.rsPBIServiceUnavailable(correlationId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003F2D File Offset: 0x0000212D
		private PBIServiceUnavailable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
