using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000BA RID: 186
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	internal sealed class OleDbBaseTraceSource : TraceSourceBase<OleDbBaseTraceSource>
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00008FBA File Offset: 0x000071BA
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("DataMovement.PipeLine.OleDbBase");
			}
		}
	}
}
