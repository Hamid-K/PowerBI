using System;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A2 RID: 418
	internal sealed class PlanNamedTableContext : PlanNamedTable
	{
		// Token: 0x06000EBF RID: 3775 RVA: 0x0003BC73 File Offset: 0x00039E73
		internal PlanNamedTableContext(string name, PlanOperationContext tableContext, bool canExpandToMultiTables = false, bool isReusable = false, bool isFragmentOfExistingDeclaration = false)
			: base(name, tableContext.Table, canExpandToMultiTables, isReusable, isFragmentOfExistingDeclaration, null, false)
		{
			this.TableContext = tableContext;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0003BC90 File Offset: 0x00039E90
		public PlanOperationContext TableContext { get; }
	}
}
