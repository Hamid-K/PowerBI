using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x0200001C RID: 28
	public class TransformException : Exception
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002265 File Offset: 0x00000465
		public TransformException()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000226D File Offset: 0x0000046D
		public TransformException(string message)
			: base(message)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002276 File Offset: 0x00000476
		public TransformException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002280 File Offset: 0x00000480
		public TransformException(string message, string errorCode, ErrorSource category)
			: base(message)
		{
			this._errorCode = errorCode;
			this._source = category;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002297 File Offset: 0x00000497
		public TransformException(string message, string errorCode, ErrorSource category, Exception inner)
			: base(message, inner)
		{
			this._errorCode = errorCode;
			this._source = category;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000022B0 File Offset: 0x000004B0
		public string ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000022B8 File Offset: 0x000004B8
		public ErrorSource Category
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x04000047 RID: 71
		private readonly string _errorCode;

		// Token: 0x04000048 RID: 72
		private readonly ErrorSource _source;
	}
}
