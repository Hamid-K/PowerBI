using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000DC RID: 220
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal sealed class NetworkCoreTraceSource : TraceSourceBase<NetworkCoreTraceSource>
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x000464A6 File Offset: 0x000446A6
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("DM.NetworkCore");
			}
		}
	}
}
