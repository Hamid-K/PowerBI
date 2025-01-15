using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000017 RID: 23
	[ImmutableObject(true)]
	internal sealed class DefaultAnalyticsTracer : ITracer
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00003029 File Offset: 0x00001229
		private DefaultAnalyticsTracer()
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003031 File Offset: 0x00001231
		public void Trace(TraceLevel level, string format, params string[] args)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003033 File Offset: 0x00001233
		public void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003035 File Offset: 0x00001235
		public void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003037 File Offset: 0x00001237
		public void Trace(TraceLevel level, string format, object arg0, object arg1)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003039 File Offset: 0x00001239
		public void Trace(TraceLevel level, string format, object arg0)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000303B File Offset: 0x0000123B
		public void Trace(TraceLevel level, string message)
		{
		}

		// Token: 0x04000070 RID: 112
		internal static readonly DefaultAnalyticsTracer Instance = new DefaultAnalyticsTracer();
	}
}
