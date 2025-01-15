using System;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x020002A4 RID: 676
	public class ResultHandler : HandlerBase, IResultHandler2, IResultHandler
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x0005DDFC File Offset: 0x0005BFFC
		public virtual bool HasResult
		{
			get
			{
				return this._hasResult;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0005DE04 File Offset: 0x0005C004
		public virtual object Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x0005DE0C File Offset: 0x0005C00C
		public virtual string ErrorType
		{
			get
			{
				return this._errorType;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x0005DE14 File Offset: 0x0005C014
		public virtual string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0005DE1C File Offset: 0x0005C01C
		public virtual string ErrorStackTrace
		{
			get
			{
				return this._errorStackTrace;
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0005DE24 File Offset: 0x0005C024
		public virtual void SetResult(object value)
		{
			this._hasResult = true;
			this._result = value;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0005DE34 File Offset: 0x0005C034
		public virtual void SetError(string type, string message, string stackTrace)
		{
			this._errorType = type;
			this._errorMessage = message;
			this._errorStackTrace = stackTrace;
		}

		// Token: 0x04000BA4 RID: 2980
		private bool _hasResult;

		// Token: 0x04000BA5 RID: 2981
		private object _result;

		// Token: 0x04000BA6 RID: 2982
		private string _errorType;

		// Token: 0x04000BA7 RID: 2983
		private string _errorMessage;

		// Token: 0x04000BA8 RID: 2984
		private string _errorStackTrace;
	}
}
