using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public sealed class AdomdError
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x00021B94 File Offset: 0x0001FD94
		internal AdomdError(int errorCode, string source, string message, string helpLink)
		{
			this.errorCode = errorCode;
			this.source = source;
			this.message = message;
			this.helpLink = helpLink;
			this.errorLocation = null;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00021BC0 File Offset: 0x0001FDC0
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

		// Token: 0x0600060F RID: 1551 RVA: 0x00021C21 File Offset: 0x0001FE21
		internal AdomdError(int errorCode, string source, string message, string helpLink, XmlaMessageLocation location)
			: this(errorCode, source, message, helpLink)
		{
			if (location != null)
			{
				this.errorLocation = new AdomdErrorLocation(location);
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00021C40 File Offset: 0x0001FE40
		internal AdomdError(XmlaError error)
			: this(error.ErrorCode, error.Source, error.Description, error.HelpFile, error.Location, error.CallStack, error.ErrorType, error.IsPrimary)
		{
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00021C83 File Offset: 0x0001FE83
		public override string ToString()
		{
			return this.message;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00021C8B File Offset: 0x0001FE8B
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00021C93 File Offset: 0x0001FE93
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00021C9B File Offset: 0x0001FE9B
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00021CA3 File Offset: 0x0001FEA3
		public string HelpLink
		{
			get
			{
				return this.helpLink;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00021CAB File Offset: 0x0001FEAB
		public AdomdErrorLocation Location
		{
			get
			{
				return this.errorLocation;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00021CB3 File Offset: 0x0001FEB3
		public string CallStack
		{
			get
			{
				return this.callStack;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00021CBB File Offset: 0x0001FEBB
		public ErrorType ErrorType
		{
			get
			{
				return this.errorType;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00021CC3 File Offset: 0x0001FEC3
		public bool IsPrimary
		{
			get
			{
				return this.isPrimary;
			}
		}

		// Token: 0x04000433 RID: 1075
		private int errorCode;

		// Token: 0x04000434 RID: 1076
		private string source;

		// Token: 0x04000435 RID: 1077
		private string message;

		// Token: 0x04000436 RID: 1078
		private string helpLink;

		// Token: 0x04000437 RID: 1079
		private AdomdErrorLocation errorLocation;

		// Token: 0x04000438 RID: 1080
		private string callStack;

		// Token: 0x04000439 RID: 1081
		private ErrorType errorType;

		// Token: 0x0400043A RID: 1082
		private bool isPrimary;
	}
}
