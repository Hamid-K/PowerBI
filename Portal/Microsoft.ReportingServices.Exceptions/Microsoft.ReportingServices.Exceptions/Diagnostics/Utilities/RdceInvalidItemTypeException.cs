using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	internal sealed class RdceInvalidItemTypeException : RSException
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00005553 File Offset: 0x00003753
		public RdceInvalidItemTypeException(string type)
			: base(ErrorCode.rsRdceInvalidItemTypeError, ErrorStringsWrapper.rsRdceInvalidItemTypeError(type), null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000557C File Offset: 0x0000377C
		private RdceInvalidItemTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
