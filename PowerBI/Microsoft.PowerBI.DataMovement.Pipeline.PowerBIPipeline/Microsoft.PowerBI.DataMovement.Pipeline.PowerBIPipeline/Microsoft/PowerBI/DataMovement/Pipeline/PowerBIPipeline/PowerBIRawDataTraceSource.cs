using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000013 RID: 19
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	internal sealed class PowerBIRawDataTraceSource : TraceSourceBase<PowerBIRawDataTraceSource>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003D7F File Offset: 0x00001F7F
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("DM.PowerBIPipeline");
			}
		}
	}
}
