using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public sealed class XmlaError : XmlaMessage
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x0001F229 File Offset: 0x0001D429
		internal XmlaError(int errorCode, string description, string source, string helpFile, XmlaMessageLocation location, string callStack, int errorType, bool isPrimary)
			: base(description, source, helpFile, location)
		{
			this.m_errorCode = errorCode;
			this.m_callStack = callStack;
			this.m_errorType = errorType;
			this.m_isPrimary = isPrimary;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001F258 File Offset: 0x0001D458
		internal XmlaError(int errorCode, string description, string source, string helpFile, XmlaMessageLocation location)
			: this(errorCode, description, source, helpFile, location, null, 0, false)
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001F27C File Offset: 0x0001D47C
		public int ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0001F284 File Offset: 0x0001D484
		public string CallStack
		{
			get
			{
				return this.m_callStack;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0001F28C File Offset: 0x0001D48C
		public int ErrorType
		{
			get
			{
				return this.m_errorType;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001F294 File Offset: 0x0001D494
		internal bool IsInvalidSession
		{
			get
			{
				return this.m_errorCode == -1056178166;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001F2A3 File Offset: 0x0001D4A3
		internal bool IsPrimary
		{
			get
			{
				return this.m_isPrimary;
			}
		}

		// Token: 0x040003CC RID: 972
		internal const int INVALID_SESSION_ERROR = -1056178166;

		// Token: 0x040003CD RID: 973
		private int m_errorCode;

		// Token: 0x040003CE RID: 974
		private string m_callStack;

		// Token: 0x040003CF RID: 975
		private int m_errorType;

		// Token: 0x040003D0 RID: 976
		private bool m_isPrimary;
	}
}
