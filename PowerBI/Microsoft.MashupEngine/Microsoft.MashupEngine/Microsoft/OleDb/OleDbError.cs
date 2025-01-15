using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F16 RID: 7958
	[Serializable]
	public class OleDbError
	{
		// Token: 0x0600C2E2 RID: 49890 RVA: 0x00271269 File Offset: 0x0026F469
		public OleDbError(string source, string message, string sqlState = null, int nativeError = -1)
		{
			this.message = message ?? string.Empty;
			this.source = source ?? string.Empty;
			this.sqlState = sqlState;
			this.nativeError = nativeError;
		}

		// Token: 0x17002FA1 RID: 12193
		// (get) Token: 0x0600C2E3 RID: 49891 RVA: 0x002712A0 File Offset: 0x0026F4A0
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17002FA2 RID: 12194
		// (get) Token: 0x0600C2E4 RID: 49892 RVA: 0x002712A8 File Offset: 0x0026F4A8
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17002FA3 RID: 12195
		// (get) Token: 0x0600C2E5 RID: 49893 RVA: 0x002712B0 File Offset: 0x0026F4B0
		public string SQLState
		{
			get
			{
				return this.sqlState;
			}
		}

		// Token: 0x17002FA4 RID: 12196
		// (get) Token: 0x0600C2E6 RID: 49894 RVA: 0x002712B8 File Offset: 0x0026F4B8
		public int NativeError
		{
			get
			{
				return this.nativeError;
			}
		}

		// Token: 0x0400645B RID: 25691
		public const int defaultError = -1;

		// Token: 0x0400645C RID: 25692
		public const string defaultState = null;

		// Token: 0x0400645D RID: 25693
		private readonly string source;

		// Token: 0x0400645E RID: 25694
		private readonly string message;

		// Token: 0x0400645F RID: 25695
		private readonly string sqlState;

		// Token: 0x04006460 RID: 25696
		private readonly int nativeError;
	}
}
