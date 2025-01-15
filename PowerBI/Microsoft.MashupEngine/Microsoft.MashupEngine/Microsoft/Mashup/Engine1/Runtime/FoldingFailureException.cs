using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001306 RID: 4870
	[Serializable]
	internal class FoldingFailureException : NotSupportedException
	{
		// Token: 0x060080B6 RID: 32950 RVA: 0x001B71E2 File Offset: 0x001B53E2
		public FoldingFailureException(string message = "Folding failed. More details are available in the trace.")
			: base(message)
		{
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x001B71EB File Offset: 0x001B53EB
		public FoldingFailureException(Exception innerException)
			: base(null, innerException)
		{
		}

		// Token: 0x060080B8 RID: 32952 RVA: 0x001B71F5 File Offset: 0x001B53F5
		private FoldingFailureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060080B9 RID: 32953 RVA: 0x001B71FF File Offset: 0x001B53FF
		public void Trace(IHostTrace trace)
		{
			trace.Add(this, TraceEventType.Warning, true);
		}

		// Token: 0x0400461B RID: 17947
		private const string errorMessage = "Folding failed. More details are available in the trace.";
	}
}
