using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000007 RID: 7
	public sealed class DataTransformMessage
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000217E File Offset: 0x0000037E
		public DataTransformMessage(string code, DataTransformMessageSeverity severity, string message)
		{
			this._code = code;
			this._severity = severity;
			this._message = message;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000219B File Offset: 0x0000039B
		public string Code
		{
			get
			{
				return this._code;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021A3 File Offset: 0x000003A3
		public DataTransformMessageSeverity Severity
		{
			get
			{
				return this._severity;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021AB File Offset: 0x000003AB
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x0400002F RID: 47
		private readonly string _code;

		// Token: 0x04000030 RID: 48
		private readonly DataTransformMessageSeverity _severity;

		// Token: 0x04000031 RID: 49
		private readonly string _message;
	}
}
