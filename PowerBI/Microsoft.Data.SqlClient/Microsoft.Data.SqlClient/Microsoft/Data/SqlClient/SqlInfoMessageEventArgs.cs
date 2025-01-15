using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000082 RID: 130
	public sealed class SqlInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x00020653 File Offset: 0x0001E853
		internal SqlInfoMessageEventArgs(SqlException exception)
		{
			this._exception = exception;
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00020662 File Offset: 0x0001E862
		public SqlErrorCollection Errors
		{
			get
			{
				return this._exception.Errors;
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002066F File Offset: 0x0001E86F
		private bool ShouldSerializeErrors()
		{
			return this._exception != null && 0 < this._exception.Errors.Count;
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002068E File Offset: 0x0001E88E
		public string Message
		{
			get
			{
				return this._exception.Message;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002069B File Offset: 0x0001E89B
		public string Source
		{
			get
			{
				return this._exception.Source;
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000206A8 File Offset: 0x0001E8A8
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x040002AF RID: 687
		private readonly SqlException _exception;
	}
}
