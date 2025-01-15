using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x0200000C RID: 12
	internal sealed class NoOpDataShapeDefaultValueContextManager : IDataShapeDefaultValueContextManager
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000295D File Offset: 0x00000B5D
		private NoOpDataShapeDefaultValueContextManager()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002965 File Offset: 0x00000B65
		public PlanOperationClearDefaultContext ToPlanOperation(DataShape dataShape)
		{
			return null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002968 File Offset: 0x00000B68
		public void AddGrouping(DataMember dataMember)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000296A File Offset: 0x00000B6A
		public void AddFilter(DataShape dataShape, FilterCondition filterCondition)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000296C File Offset: 0x00000B6C
		public void AddDefaultValueFilters(DataShape dataShape)
		{
		}

		// Token: 0x04000032 RID: 50
		internal static readonly NoOpDataShapeDefaultValueContextManager Instance = new NoOpDataShapeDefaultValueContextManager();
	}
}
