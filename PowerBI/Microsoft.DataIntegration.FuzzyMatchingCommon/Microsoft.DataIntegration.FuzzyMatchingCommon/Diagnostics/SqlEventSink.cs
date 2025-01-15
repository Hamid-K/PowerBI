using System;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal class SqlEventSink : IEventSink
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00013104 File Offset: 0x00011304
		public void Assert(bool condition)
		{
			SqlContext.Pipe.Send("Fuzzy Match Assert failed.");
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00013115 File Offset: 0x00011315
		public void Assert(bool condition, string message)
		{
			SqlContext.Pipe.Send("Fuzzy Match Assert failed : " + message);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0001312C File Offset: 0x0001132C
		public void Trace(string message)
		{
			SqlContext.Pipe.Send("Trace : " + message);
		}
	}
}
