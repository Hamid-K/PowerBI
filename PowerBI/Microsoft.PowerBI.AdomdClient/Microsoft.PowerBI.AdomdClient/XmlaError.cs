using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000042 RID: 66
	internal sealed class XmlaError : XmlaMessage
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0001B541 File Offset: 0x00019741
		internal XmlaError(int errorCode, string description, string source, string helpFile, XmlaMessageLocation location, string callStack, int errorType, bool isPrimary)
			: base(description, source, helpFile, location)
		{
			this.m_errorCode = errorCode;
			this.m_callStack = callStack;
			this.m_errorType = errorType;
			this.m_isPrimary = isPrimary;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001B570 File Offset: 0x00019770
		internal XmlaError(int errorCode, string description, string source, string helpFile, XmlaMessageLocation location)
			: this(errorCode, description, source, helpFile, location, null, 0, false)
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001B594 File Offset: 0x00019794
		public int ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001B59C File Offset: 0x0001979C
		public string CallStack
		{
			get
			{
				return this.m_callStack;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0001B5A4 File Offset: 0x000197A4
		public int ErrorType
		{
			get
			{
				return this.m_errorType;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001B5AC File Offset: 0x000197AC
		internal bool IsInvalidSession
		{
			get
			{
				return this.m_errorCode == -1056178166;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001B5BB File Offset: 0x000197BB
		internal bool IsPrimary
		{
			get
			{
				return this.m_isPrimary;
			}
		}

		// Token: 0x04000390 RID: 912
		internal const int INVALID_SESSION_ERROR = -1056178166;

		// Token: 0x04000391 RID: 913
		private int m_errorCode;

		// Token: 0x04000392 RID: 914
		private string m_callStack;

		// Token: 0x04000393 RID: 915
		private int m_errorType;

		// Token: 0x04000394 RID: 916
		private bool m_isPrimary;
	}
}
