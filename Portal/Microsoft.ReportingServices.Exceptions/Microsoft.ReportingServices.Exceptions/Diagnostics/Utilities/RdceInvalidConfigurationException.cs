using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	internal sealed class RdceInvalidConfigurationException : RSException
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x00005521 File Offset: 0x00003721
		public RdceInvalidConfigurationException()
			: base(ErrorCode.rsRdceInvalidConfigurationError, ErrorStringsWrapper.rsRdceInvalidConfigurationError, null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00005549 File Offset: 0x00003749
		private RdceInvalidConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
