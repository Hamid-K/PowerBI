using System;
using Microsoft.AnalysisServices.Tabular;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x0200002F RID: 47
	public interface IColumn
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000169 RID: 361
		bool HasErrors { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600016A RID: 362
		string ErrorMessage { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600016B RID: 363
		ColumnType Type { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600016C RID: 364
		bool IsCalculated { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600016D RID: 365
		bool CanDelete { get; }

		// Token: 0x0600016E RID: 366
		string GetAnnotation(string name);
	}
}
