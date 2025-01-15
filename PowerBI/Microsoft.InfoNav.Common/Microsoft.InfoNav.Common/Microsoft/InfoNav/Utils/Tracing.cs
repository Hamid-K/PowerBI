using System;

namespace Microsoft.InfoNav.Utils
{
	// Token: 0x02000037 RID: 55
	public static class Tracing
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000081F2 File Offset: 0x000063F2
		public static string TraceSourceInstancePropertyName
		{
			get
			{
				return "Tracer";
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000081F9 File Offset: 0x000063F9
		public static string TraceSourceIdentification
		{
			get
			{
				return "ID";
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00008200 File Offset: 0x00006400
		public static string TraceSourceVerbosityPropertyName
		{
			get
			{
				return "DefaultVerbosity";
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00008207 File Offset: 0x00006407
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000820E File Offset: 0x0000640E
		public static TraceVerbosity ForcedTracesTraceLevel
		{
			get
			{
				return Tracing.s_forcedTracesTraceLevel;
			}
			set
			{
				Tracing.s_forcedTracesTraceLevel = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00008216 File Offset: 0x00006416
		public static bool RemovePersonallyIdentifiableInformationFromTraces
		{
			get
			{
				return Tracing.s_removePersonallyIdentifiableInformationFromTraces;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000821D File Offset: 0x0000641D
		public static char Delimiter
		{
			get
			{
				return '\t';
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008221 File Offset: 0x00006421
		internal static void ConfigurePII(bool markPII)
		{
			Tracing.s_removePersonallyIdentifiableInformationFromTraces = markPII;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008229 File Offset: 0x00006429
		private static int SetHeaderLength()
		{
			Tracing.s_headerLength = Tracing.s_instanceId.Length + 1 + 36 + 1 + 36 + 1 + 36 + 1 + 4;
			return Tracing.s_headerLength;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00008252 File Offset: 0x00006452
		internal static int HeaderLength
		{
			get
			{
				return Tracing.s_headerLength;
			}
		}

		// Token: 0x04000088 RID: 136
		internal const string c_removePersonallyIdentifiableInformationFromTracesSuppressedName = "Microsoft.Cloud.Platform.Utils.Tracing.RemovePIIFromTracesSuppressed";

		// Token: 0x04000089 RID: 137
		private const char c_delimiter = '\t';

		// Token: 0x0400008A RID: 138
		private const string c_traceSourceResourceName = "Microsoft.Cloud.Platform.TraceSource.Defined";

		// Token: 0x0400008B RID: 139
		private static string s_instanceId = string.Empty;

		// Token: 0x0400008C RID: 140
		private static TraceVerbosity s_forcedTracesTraceLevel = TraceVerbosity.Verbose;

		// Token: 0x0400008D RID: 141
		private static bool s_removePersonallyIdentifiableInformationFromTraces = true;

		// Token: 0x0400008E RID: 142
		private static int s_headerLength = Tracing.SetHeaderLength();
	}
}
