using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000AC RID: 172
	public sealed class ANCommonTrace : ANTraceSourceBase<ANCommonTrace>
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00010DCF File Offset: 0x0000EFCF
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("ANCommon");
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000FA6B File Offset: 0x0000DC6B
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Verbose;
			}
		}
	}
}
