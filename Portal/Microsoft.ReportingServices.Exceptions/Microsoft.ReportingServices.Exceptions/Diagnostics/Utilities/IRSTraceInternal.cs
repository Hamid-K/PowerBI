using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000CE RID: 206
	public interface IRSTraceInternal
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600047B RID: 1147
		string CurrentTraceFilePath { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600047C RID: 1148
		// (set) Token: 0x0600047D RID: 1149
		bool BufferOutput { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600047E RID: 1150
		bool IsTraceInitialized { get; }

		// Token: 0x0600047F RID: 1151
		void ClearBuffer();

		// Token: 0x06000480 RID: 1152
		void WriteBuffer();

		// Token: 0x06000481 RID: 1153
		void Trace(string componentName, string message);

		// Token: 0x06000482 RID: 1154
		void Trace(string componentName, string format, params object[] arg);

		// Token: 0x06000483 RID: 1155
		void Trace(TraceLevel traceLevel, string componentName, string message);

		// Token: 0x06000484 RID: 1156
		void Trace(TraceLevel traceLevel, string componentName, string format, params object[] arg);

		// Token: 0x06000485 RID: 1157
		void TraceWithDetails(TraceLevel traceLevel, string componentName, string message, string details);

		// Token: 0x06000486 RID: 1158
		void TraceException(TraceLevel traceLevel, string componentName, string message);

		// Token: 0x06000487 RID: 1159
		void TraceWithNoEventLog(TraceLevel traceLevel, string componentName, string format, params object[] arg);

		// Token: 0x06000488 RID: 1160
		void Fail(string componentName);

		// Token: 0x06000489 RID: 1161
		void Fail(string componentName, string message);

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600048A RID: 1162
		string TraceDirectory { get; }

		// Token: 0x0600048B RID: 1163
		string GetDefaultTraceLevel();

		// Token: 0x0600048C RID: 1164
		bool GetTraceLevel(string componentName, out TraceLevel componentTraceLevel);
	}
}
