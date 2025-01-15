using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	internal sealed class IncompatibleRIFVersionException : RSException
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00008877 File Offset: 0x00006A77
		internal IncompatibleRIFVersionException(int documentCompatVersion, int codeCompatVersion)
			: base(ErrorCode.rsIncompatibleRIFVersion, string.Format(CultureInfo.InvariantCulture, "The RIF document is not compatible with this code version.  Document Version: {0} Code Version: {1}", documentCompatVersion, codeCompatVersion), null, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000088AB File Offset: 0x00006AAB
		internal IncompatibleRIFVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000088B5 File Offset: 0x00006AB5
		internal static void ThrowIfIncompatible(int documentCompatVersion, int codeCompatVersion)
		{
			if (documentCompatVersion != codeCompatVersion && documentCompatVersion != 0 && documentCompatVersion > codeCompatVersion)
			{
				throw new IncompatibleRIFVersionException(documentCompatVersion, codeCompatVersion);
			}
		}
	}
}
