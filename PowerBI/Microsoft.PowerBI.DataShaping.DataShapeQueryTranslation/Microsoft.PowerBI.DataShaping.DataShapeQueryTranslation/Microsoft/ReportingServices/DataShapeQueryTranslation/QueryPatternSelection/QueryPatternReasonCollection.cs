using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Reporting;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x0200006C RID: 108
	internal sealed class QueryPatternReasonCollection
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00013F05 File Offset: 0x00012105
		public bool HasSingleResultPatternReason
		{
			get
			{
				return QueryPatternReasonCollection.HasReason<QueryPatternReason>(this.m_singleResultTableReasons);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00013F12 File Offset: 0x00012112
		public bool HasBatchPatternReason
		{
			get
			{
				return QueryPatternReasonCollection.HasReason<QueryPatternReason>(this.m_batchReasons);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00013F1F File Offset: 0x0001211F
		public ReadOnlyCollection<QueryPatternReason> SingleResultPatternReasons
		{
			get
			{
				return this.m_singleResultTableReasons.ToReadOnlyCollection<QueryPatternReason>();
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00013F2C File Offset: 0x0001212C
		public ReadOnlyCollection<QueryPatternReason> BatchPatternReasons
		{
			get
			{
				return this.m_batchReasons.ToReadOnlyCollection<QueryPatternReason>();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00013F3C File Offset: 0x0001213C
		public ReadOnlyCollection<QueryPatternReason> AllReasons
		{
			get
			{
				HashSet<QueryPatternReason> hashSet = new HashSet<QueryPatternReason>();
				if (this.m_singleResultTableReasons != null)
				{
					hashSet.UnionWith(this.m_singleResultTableReasons);
				}
				if (this.m_batchReasons != null)
				{
					hashSet.UnionWith(this.m_batchReasons);
				}
				return hashSet.ToReadOnlyCollection<QueryPatternReason>();
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00013F7D File Offset: 0x0001217D
		public bool CheckBatchPrerequisite(bool condition, QueryPatternReason reason)
		{
			if (!condition)
			{
				QueryPatternReasonCollection.AddReason<QueryPatternReason>(ref this.m_singleResultTableReasons, reason);
			}
			return condition;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00013F8F File Offset: 0x0001218F
		public void RegisterSingleResultTablePatternOnlyReason(QueryPatternReason reason)
		{
			QueryPatternReasonCollection.AddReason<QueryPatternReason>(ref this.m_singleResultTableReasons, reason);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00013F9D File Offset: 0x0001219D
		public void RegisterBatchPatternOnlyReason(QueryPatternReason reason)
		{
			QueryPatternReasonCollection.AddReason<QueryPatternReason>(ref this.m_batchReasons, reason);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00013FAB File Offset: 0x000121AB
		private static void AddReason<T>(ref HashSet<T> reasons, T reason)
		{
			if (reasons == null)
			{
				reasons = new HashSet<T>();
			}
			reasons.Add(reason);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00013FC1 File Offset: 0x000121C1
		private static bool HasReason<T>(HashSet<T> reasons)
		{
			return reasons != null && reasons.Count > 0;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00013FD1 File Offset: 0x000121D1
		public static string CreateReasonsString<T>(IList<T> reasons, string separator)
		{
			if (reasons == null || reasons.Count == 0)
			{
				return null;
			}
			return string.Join(separator, reasons.Select((T r) => r.ToString()));
		}

		// Token: 0x040002C9 RID: 713
		private HashSet<QueryPatternReason> m_singleResultTableReasons;

		// Token: 0x040002CA RID: 714
		private HashSet<QueryPatternReason> m_batchReasons;
	}
}
