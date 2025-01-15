using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers
{
	// Token: 0x020001B4 RID: 436
	internal sealed class SubqueryTableManager
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x0003E473 File Offset: 0x0003C673
		internal SubqueryTableManager()
		{
			this.m_subqueryTables = new List<SubqueryIntermediatePlan>();
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003E486 File Offset: 0x0003C686
		internal void AddTable(SubqueryIntermediatePlan table)
		{
			this.m_subqueryTables.Add(table);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003E494 File Offset: 0x0003C694
		internal bool TryGetTable(IScope outerScope, IScope innerScope, ScopeTree scopeTree, out SubqueryIntermediatePlan table)
		{
			foreach (SubqueryIntermediatePlan subqueryIntermediatePlan in this.m_subqueryTables)
			{
				if (scopeTree.AreSameScope(subqueryIntermediatePlan.InnerScope, innerScope) && scopeTree.AreSameScope(subqueryIntermediatePlan.OuterScope, outerScope))
				{
					table = subqueryIntermediatePlan;
					return true;
				}
			}
			table = null;
			return false;
		}

		// Token: 0x04000737 RID: 1847
		private readonly List<SubqueryIntermediatePlan> m_subqueryTables;
	}
}
