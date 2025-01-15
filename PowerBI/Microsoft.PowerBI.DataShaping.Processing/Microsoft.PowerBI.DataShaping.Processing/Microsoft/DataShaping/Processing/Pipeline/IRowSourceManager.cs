using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200009D RID: 157
	internal interface IRowSourceManager
	{
		// Token: 0x06000418 RID: 1048
		bool TrySetupResultSet(int resultSetIndex);

		// Token: 0x06000419 RID: 1049
		bool TryRestoreResultSet(int resultSetIndex);

		// Token: 0x0600041A RID: 1050
		IDataRow ReadRow();

		// Token: 0x0600041B RID: 1051
		IReadOnlyList<Type> GetActiveResultSetColumnTypes();

		// Token: 0x0600041C RID: 1052
		bool WasSetup(int resultSetIndex);

		// Token: 0x0600041D RID: 1053
		bool HasRows(int resultSetIndex);

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600041E RID: 1054
		int ActiveResultSetIndex { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600041F RID: 1055
		long TotalCachedRowsCount { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000420 RID: 1056
		IReadOnlyRowCache ActiveRowCache { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000421 RID: 1057
		IReadOnlyList<Message> Messages { get; }
	}
}
