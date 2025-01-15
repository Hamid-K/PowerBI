using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000078 RID: 120
	internal sealed class TraceableComponentLog : ITraceLog
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00010247 File Offset: 0x0000E447
		public TraceableComponentLog(RSTrace rsTracer, string prefix)
		{
			this.m_rsTracer = rsTracer;
			this.m_prefix = prefix;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001025D File Offset: 0x0000E45D
		bool ITraceLog.TraceError
		{
			get
			{
				return this.m_rsTracer.TraceError;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001026A File Offset: 0x0000E46A
		bool ITraceLog.TraceWarning
		{
			get
			{
				return this.m_rsTracer.TraceWarning;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00010277 File Offset: 0x0000E477
		bool ITraceLog.TraceInfo
		{
			get
			{
				return this.m_rsTracer.TraceInfo;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00010284 File Offset: 0x0000E484
		bool ITraceLog.TraceVerbose
		{
			get
			{
				return this.m_rsTracer.TraceVerbose;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00010294 File Offset: 0x0000E494
		void ITraceLog.WriteTrace(string message, TraceLevel level)
		{
			switch (level)
			{
			case TraceLevel.Error:
				if (!this.m_rsTracer.TraceError)
				{
					return;
				}
				break;
			case TraceLevel.Warning:
				if (!this.m_rsTracer.TraceWarning)
				{
					return;
				}
				break;
			case TraceLevel.Info:
				if (!this.m_rsTracer.TraceInfo)
				{
					return;
				}
				break;
			case TraceLevel.Verbose:
				if (!this.m_rsTracer.TraceVerbose)
				{
					return;
				}
				break;
			default:
				return;
			}
			this.m_rsTracer.Trace(level, this.m_prefix, new object[] { message });
		}

		// Token: 0x0400036B RID: 875
		private readonly RSTrace m_rsTracer;

		// Token: 0x0400036C RID: 876
		private readonly string m_prefix;
	}
}
