using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008FC RID: 2300
	public interface IResultSet
	{
		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06004882 RID: 18562
		IColumnInfo[] ColumnInfos { get; }

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06004883 RID: 18563
		bool IsCursorScrollable { get; }

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06004884 RID: 18564
		int RowsCount { get; }

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06004885 RID: 18565
		bool EndOfQuery { get; }

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06004886 RID: 18566
		int CurrentRowIndex { get; }

		// Token: 0x06004887 RID: 18567
		Task<bool> ReadRowAsync(QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x06004888 RID: 18568
		Task<object> GetColumnData(int columnOrdina, bool isAsync, CancellationToken cancellationTokenl);
	}
}
