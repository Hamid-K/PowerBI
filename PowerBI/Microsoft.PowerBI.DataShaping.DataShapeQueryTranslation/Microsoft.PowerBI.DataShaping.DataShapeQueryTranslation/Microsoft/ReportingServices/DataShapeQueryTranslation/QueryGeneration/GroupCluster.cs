using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000074 RID: 116
	internal sealed class GroupCluster
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x00014E39 File Offset: 0x00013039
		internal GroupCluster(List<GroupKey> groupKeys, bool showAllItemsWithNoData)
		{
			this.m_groupKeys = groupKeys.AsReadOnly();
			this.m_showItemsWithNoData = showAllItemsWithNoData;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00014E54 File Offset: 0x00013054
		internal ReadOnlyCollection<GroupKey> GroupKeys
		{
			get
			{
				return this.m_groupKeys;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00014E5C File Offset: 0x0001305C
		internal bool ShowItemsWithNoData
		{
			get
			{
				return this.m_showItemsWithNoData;
			}
		}

		// Token: 0x040002EB RID: 747
		private readonly ReadOnlyCollection<GroupKey> m_groupKeys;

		// Token: 0x040002EC RID: 748
		private readonly bool m_showItemsWithNoData;
	}
}
