using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000042 RID: 66
	internal sealed class XmlaError : XmlaMessage
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x0001B871 File Offset: 0x00019A71
		internal XmlaError(int errorCode, string description, string source, string helpFile, XmlaMessageLocation location, string callStack, int errorType, bool isPrimary)
			: base(description, source, helpFile, location)
		{
			this.m_errorCode = errorCode;
			this.m_callStack = callStack;
			this.m_errorType = errorType;
			this.m_isPrimary = isPrimary;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001B8A0 File Offset: 0x00019AA0
		internal XmlaError(int errorCode, string description, string source, string helpFile, XmlaMessageLocation location)
			: this(errorCode, description, source, helpFile, location, null, 0, false)
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		public int ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001B8CC File Offset: 0x00019ACC
		public string CallStack
		{
			get
			{
				return this.m_callStack;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001B8D4 File Offset: 0x00019AD4
		public int ErrorType
		{
			get
			{
				return this.m_errorType;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0001B8DC File Offset: 0x00019ADC
		internal bool IsInvalidSession
		{
			get
			{
				return this.m_errorCode == -1056178166;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001B8EB File Offset: 0x00019AEB
		internal bool IsPrimary
		{
			get
			{
				return this.m_isPrimary;
			}
		}

		// Token: 0x0400039D RID: 925
		internal const int INVALID_SESSION_ERROR = -1056178166;

		// Token: 0x0400039E RID: 926
		private int m_errorCode;

		// Token: 0x0400039F RID: 927
		private string m_callStack;

		// Token: 0x040003A0 RID: 928
		private int m_errorType;

		// Token: 0x040003A1 RID: 929
		private bool m_isPrimary;
	}
}
