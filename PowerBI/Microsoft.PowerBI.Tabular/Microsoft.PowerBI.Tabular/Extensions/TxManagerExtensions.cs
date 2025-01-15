using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D9 RID: 473
	internal static class TxManagerExtensions
	{
		// Token: 0x06001C22 RID: 7202 RVA: 0x000C411C File Offset: 0x000C231C
		internal static bool IsInSavepoint(this TxManager txManager, string name)
		{
			TxSavepoint currentSavepoint = txManager.CurrentSavepoint;
			return currentSavepoint != null && currentSavepoint.Name == name;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000C4141 File Offset: 0x000C2341
		internal static IEnumerable<TxSavepoint> GetSavepoints(this TxManager txManager)
		{
			for (TxSavepoint current = txManager.CurrentSavepoint; current != null; current = current.Prev)
			{
				yield return current;
			}
			yield break;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000C4154 File Offset: 0x000C2354
		internal static TxSavepoint GetSavepointByName(this TxManager txManager, string savepointName)
		{
			TxSavepoint txSavepoint = txManager.CurrentSavepoint;
			while (txSavepoint != null && txSavepoint.Name != savepointName)
			{
				txSavepoint = txSavepoint.Prev;
			}
			return txSavepoint;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000C4183 File Offset: 0x000C2383
		internal static TxSavepoint GetSyncedSavepoint(this TxManager txManager)
		{
			return txManager.GetSavepointByName("Synced");
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000C4190 File Offset: 0x000C2390
		internal static TxSavepoint GetModifiedSavepoint(this TxManager txManager)
		{
			return txManager.GetSavepointByName("Modified");
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000C419D File Offset: 0x000C239D
		internal static TxSavepoint GetBeginTxSavepoint(this TxManager txManager)
		{
			return txManager.GetSavepointByName("BeginTransaction");
		}
	}
}
