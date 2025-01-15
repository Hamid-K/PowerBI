using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000008 RID: 8
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	internal sealed class ExceptionUtilsTraceSource : TraceSourceBase<ExceptionUtilsTraceSource>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000216F File Offset: 0x0000036F
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("DM.Pipeline.ExceptionUtils");
			}
		}
	}
}
