using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A6 RID: 422
	internal sealed class SubqueryDataShapeTableCache
	{
		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		public void Add(DataShape subqueryDataShape, PlanOperationContext table)
		{
			if (this._tablesByDataShapeId == null)
			{
				this._tablesByDataShapeId = new Dictionary<Identifier, PlanOperationContext>();
			}
			this._tablesByDataShapeId.Add(subqueryDataShape.Id, table);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003C703 File Offset: 0x0003A903
		public bool TryGetTable(DataShape subqueryDataShape, out PlanOperationContext table)
		{
			if (this._tablesByDataShapeId == null)
			{
				table = null;
				return false;
			}
			return this._tablesByDataShapeId.TryGetValue(subqueryDataShape.Id, out table);
		}

		// Token: 0x04000702 RID: 1794
		private Dictionary<Identifier, PlanOperationContext> _tablesByDataShapeId;
	}
}
