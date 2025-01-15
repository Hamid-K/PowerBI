using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000007 RID: 7
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	internal sealed class RowsetSerializationTraceSource : TraceSourceBase<RowsetSerializationTraceSource>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("DM.Pipeline.RowsetSerialization");
			}
		}
	}
}
