using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E1 RID: 1505
	[Serializable]
	internal sealed class ReportProcessingException_NoRowsFieldAccess : Exception
	{
		// Token: 0x06005403 RID: 21507 RVA: 0x001618E8 File Offset: 0x0015FAE8
		internal ReportProcessingException_NoRowsFieldAccess()
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNoRowsFieldAccess, Array.Empty<object>()))
		{
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x00161904 File Offset: 0x0015FB04
		private ReportProcessingException_NoRowsFieldAccess(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
