using System;
using System.Text;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200099D RID: 2461
	public class DrdaRequesterException : Exception
	{
		// Token: 0x06004C5B RID: 19547 RVA: 0x0013114B File Offset: 0x0012F34B
		public DrdaRequesterException(string msg)
			: this(msg, null, 0)
		{
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x00131156 File Offset: 0x0012F356
		public DrdaRequesterException(string msg, int sqlCode)
			: this(msg, null, sqlCode)
		{
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x00131161 File Offset: 0x0012F361
		public DrdaRequesterException(string errorMessage, string sqlState, int sqlCode)
		{
			this._errMsg = errorMessage;
			this._sqlState = sqlState;
			this._sqlCode = sqlCode;
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0013117E File Offset: 0x0012F37E
		internal DrdaRequesterException(string errorMessage, string sqlState, int sqlCode, Exception exception)
			: base(errorMessage, exception)
		{
			this._errMsg = errorMessage;
			this._sqlState = sqlState;
			this._sqlCode = sqlCode;
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x001311A0 File Offset: 0x0012F3A0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this._sqlState))
			{
				stringBuilder.Append("SQLSTATE=");
				stringBuilder.Append(this._sqlState);
			}
			if (this._sqlCode != 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append("SQLCODE=");
				stringBuilder.Append(this._sqlCode);
			}
			if (!string.IsNullOrWhiteSpace(this._errMsg))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(this._errMsg);
			}
			if (base.InnerException != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(base.InnerException.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06004C60 RID: 19552 RVA: 0x0013126E File Offset: 0x0012F46E
		public string SqlState
		{
			get
			{
				return this._sqlState;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06004C61 RID: 19553 RVA: 0x00131276 File Offset: 0x0012F476
		public int SqlCode
		{
			get
			{
				return this._sqlCode;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06004C62 RID: 19554 RVA: 0x0013127E File Offset: 0x0012F47E
		public override string Message
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x04003C4D RID: 15437
		private string _sqlState;

		// Token: 0x04003C4E RID: 15438
		private int _sqlCode;

		// Token: 0x04003C4F RID: 15439
		private string _errMsg;
	}
}
