using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008FF RID: 2303
	public interface ISqlStatement
	{
		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06004896 RID: 18582
		string CursorName { get; }

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06004897 RID: 18583
		int AffectedRowCount { get; }

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06004898 RID: 18584
		// (set) Token: 0x06004899 RID: 18585
		SqlAttribute SqlAttribute { get; set; }

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x0600489A RID: 18586
		IRequester Requester { get; }

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x0600489B RID: 18587
		IList<IResultSet> ResultSets { get; }

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x0600489C RID: 18588
		List<ISqlParameter> Parameters { get; }

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x0600489D RID: 18589
		// (set) Token: 0x0600489E RID: 18590
		long CommandSourceId { get; set; }

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x0600489F RID: 18591
		// (set) Token: 0x060048A0 RID: 18592
		Func<bool, bool> LiteralReplacementInvestigator { get; set; }

		// Token: 0x060048A1 RID: 18593
		Task ExecuteAsync(string statement, List<ISqlParameter> parameters, bool isExecReader, bool identityInsert, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048A2 RID: 18594
		Task InsertRowsAsync(List<object[]> rows, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048A3 RID: 18595
		Task PrepareAsync(string statement, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048A4 RID: 18596
		Task<IList<ISqlParameter>> GetParametersAsync(string statement, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048A5 RID: 18597
		Task CloseAsync(bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048A6 RID: 18598
		void Reset();
	}
}
