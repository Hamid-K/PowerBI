using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public sealed class AdomdError
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00021EC4 File Offset: 0x000200C4
		internal AdomdError(int errorCode, string source, string message, string helpLink)
		{
			this.errorCode = errorCode;
			this.source = source;
			this.message = message;
			this.helpLink = helpLink;
			this.errorLocation = null;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00021EF0 File Offset: 0x000200F0
		internal AdomdError(int errorCode, string source, string message, string helpLink, XmlaMessageLocation location, string callStack, int errorType, bool isPrimary)
			: this(errorCode, source, message, helpLink, location)
		{
			if (!Enum.IsDefined(typeof(ErrorType), errorType))
			{
				throw new ArgumentException(string.Format("Value {0} is not a valid ErrorType value", errorType));
			}
			this.callStack = callStack;
			this.errorType = (ErrorType)errorType;
			this.isPrimary = isPrimary;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00021F51 File Offset: 0x00020151
		internal AdomdError(int errorCode, string source, string message, string helpLink, XmlaMessageLocation location)
			: this(errorCode, source, message, helpLink)
		{
			if (location != null)
			{
				this.errorLocation = new AdomdErrorLocation(location);
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00021F70 File Offset: 0x00020170
		internal AdomdError(XmlaError error)
			: this(error.ErrorCode, error.Source, error.Description, error.HelpFile, error.Location, error.CallStack, error.ErrorType, error.IsPrimary)
		{
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00021FB3 File Offset: 0x000201B3
		public override string ToString()
		{
			return this.message;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00021FBB File Offset: 0x000201BB
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00021FC3 File Offset: 0x000201C3
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00021FCB File Offset: 0x000201CB
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00021FD3 File Offset: 0x000201D3
		public string HelpLink
		{
			get
			{
				return this.helpLink;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00021FDB File Offset: 0x000201DB
		public AdomdErrorLocation Location
		{
			get
			{
				return this.errorLocation;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00021FE3 File Offset: 0x000201E3
		public string CallStack
		{
			get
			{
				return this.callStack;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00021FEB File Offset: 0x000201EB
		public ErrorType ErrorType
		{
			get
			{
				return this.errorType;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00021FF3 File Offset: 0x000201F3
		public bool IsPrimary
		{
			get
			{
				return this.isPrimary;
			}
		}

		// Token: 0x04000440 RID: 1088
		private int errorCode;

		// Token: 0x04000441 RID: 1089
		private string source;

		// Token: 0x04000442 RID: 1090
		private string message;

		// Token: 0x04000443 RID: 1091
		private string helpLink;

		// Token: 0x04000444 RID: 1092
		private AdomdErrorLocation errorLocation;

		// Token: 0x04000445 RID: 1093
		private string callStack;

		// Token: 0x04000446 RID: 1094
		private ErrorType errorType;

		// Token: 0x04000447 RID: 1095
		private bool isPrimary;
	}
}
