using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000187 RID: 391
	internal sealed class WritableGroupDetailMap : GroupDetailMap
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0003807D File Offset: 0x0003627D
		internal WritableGroupDetailMap()
		{
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00038088 File Offset: 0x00036288
		public void AddDetail(ExpressionId groupKey, ExpressionId detail)
		{
			List<ExpressionId> list;
			if (!this.m_keyToDetails.TryGetValue(groupKey, out list))
			{
				list = new List<ExpressionId>();
				this.m_keyToDetails.Add(groupKey, list);
			}
			list.Add(detail);
			this.m_detailToKey.Add(detail, groupKey);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000380CC File Offset: 0x000362CC
		public ReadOnlyGroupDetailMap AsReadOnly()
		{
			return new ReadOnlyGroupDetailMap(this);
		}
	}
}
