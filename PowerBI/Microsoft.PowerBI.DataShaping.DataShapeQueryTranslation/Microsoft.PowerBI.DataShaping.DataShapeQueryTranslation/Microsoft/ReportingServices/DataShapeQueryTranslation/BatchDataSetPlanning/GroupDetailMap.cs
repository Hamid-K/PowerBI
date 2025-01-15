using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000185 RID: 389
	internal abstract class GroupDetailMap
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00037FDA File Offset: 0x000361DA
		protected GroupDetailMap()
		{
			this.m_keyToDetails = new Dictionary<ExpressionId, List<ExpressionId>>();
			this.m_detailToKey = new Dictionary<ExpressionId, ExpressionId>();
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00037FF8 File Offset: 0x000361F8
		protected GroupDetailMap(GroupDetailMap map)
		{
			this.m_keyToDetails = map.m_keyToDetails;
			this.m_detailToKey = map.m_detailToKey;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00038018 File Offset: 0x00036218
		public ReadOnlyCollection<ExpressionId> GetDetails(ExpressionId groupKey)
		{
			return this.m_keyToDetails[groupKey].AsReadOnly();
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003802C File Offset: 0x0003622C
		public bool TryGetDetails(ExpressionId groupKey, out ReadOnlyCollection<ExpressionId> details)
		{
			List<ExpressionId> list;
			if (this.m_keyToDetails.TryGetValue(groupKey, out list))
			{
				details = list.AsReadOnly();
				return true;
			}
			details = null;
			return false;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00038057 File Offset: 0x00036257
		public ExpressionId GetGroupKey(ExpressionId detail)
		{
			return this.m_detailToKey[detail];
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00038065 File Offset: 0x00036265
		public bool TryGetGroupKey(ExpressionId detail, out ExpressionId groupKey)
		{
			return this.m_detailToKey.TryGetValue(detail, out groupKey);
		}

		// Token: 0x040006AB RID: 1707
		protected readonly Dictionary<ExpressionId, List<ExpressionId>> m_keyToDetails;

		// Token: 0x040006AC RID: 1708
		protected readonly Dictionary<ExpressionId, ExpressionId> m_detailToKey;
	}
}
