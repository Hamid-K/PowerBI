using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	internal sealed class MissingSessionIdException : ReportCatalogException
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00004040 File Offset: 0x00002240
		public MissingSessionIdException()
			: base(ErrorCode.rsMissingSessionId, ErrorStringsWrapper.rsMissingSessionId, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004056 File Offset: 0x00002256
		private MissingSessionIdException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
