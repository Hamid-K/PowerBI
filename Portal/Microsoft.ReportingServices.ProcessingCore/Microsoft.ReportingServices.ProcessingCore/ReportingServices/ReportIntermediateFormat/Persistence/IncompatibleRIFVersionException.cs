using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000553 RID: 1363
	[Serializable]
	internal sealed class IncompatibleRIFVersionException : RSException
	{
		// Token: 0x060049DE RID: 18910 RVA: 0x00137A97 File Offset: 0x00135C97
		internal IncompatibleRIFVersionException(int documentCompatVersion, int codeCompatVersion)
			: base(ErrorCode.rsIncompatibleRIFVersion, string.Format(CultureInfo.InvariantCulture, "The RIF document is not compatible with this code version.  Document Version: {0} Code Version: {1}", documentCompatVersion, codeCompatVersion), null, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x00137ACB File Offset: 0x00135CCB
		internal IncompatibleRIFVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x00137AD5 File Offset: 0x00135CD5
		internal static void ThrowIfIncompatible(int documentCompatVersion, int codeCompatVersion)
		{
			if (documentCompatVersion != codeCompatVersion && documentCompatVersion != 0 && documentCompatVersion > codeCompatVersion)
			{
				throw new IncompatibleRIFVersionException(documentCompatVersion, codeCompatVersion);
			}
		}
	}
}
