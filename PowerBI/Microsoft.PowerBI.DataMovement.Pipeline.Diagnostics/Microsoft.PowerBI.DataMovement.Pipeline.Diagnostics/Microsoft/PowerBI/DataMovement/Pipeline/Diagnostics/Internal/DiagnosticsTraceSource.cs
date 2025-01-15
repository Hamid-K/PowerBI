using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000D9 RID: 217
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal sealed class DiagnosticsTraceSource : TraceSourceBase<DiagnosticsTraceSource>
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00046492 File Offset: 0x00044692
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("DM.Pipeline.Diagnostics");
			}
		}
	}
}
