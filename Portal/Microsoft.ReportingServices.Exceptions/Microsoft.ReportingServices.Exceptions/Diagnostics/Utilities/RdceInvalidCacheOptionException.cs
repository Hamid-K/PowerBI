using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AF RID: 175
	[Serializable]
	internal sealed class RdceInvalidCacheOptionException : RSException
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x000055B8 File Offset: 0x000037B8
		public RdceInvalidCacheOptionException()
			: base(ErrorCode.rsRdceInvalidCacheOptionError, ErrorStringsWrapper.rsRdceInvalidCacheOptionError, null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000055E0 File Offset: 0x000037E0
		private RdceInvalidCacheOptionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
