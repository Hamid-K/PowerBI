using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x02000022 RID: 34
	public sealed class SubscriptionHistoryResult
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002921 File Offset: 0x00000B21
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00002929 File Offset: 0x00000B29
		public string SessionID { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00002932 File Offset: 0x00000B32
		public List<ErrorDetails> Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x040000B5 RID: 181
		private List<ErrorDetails> _errors = new List<ErrorDetails>();
	}
}
