using System;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000059 RID: 89
	internal sealed class ReducedSemanticQuery
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x000128FC File Offset: 0x00010AFC
		internal ReducedSemanticQuery(SemanticQuery smqlQuery)
		{
			if (smqlQuery.MeasureGroups.Count > 1 || smqlQuery.Hierarchies.Count > 1)
			{
				throw new NotImplementedException("Queries with more than one measure group or hierarchy are not implemented in SQL 2005.");
			}
			this.MeasureGroup = ((smqlQuery.MeasureGroups.Count != 0) ? smqlQuery.MeasureGroups[0] : null);
			this.Hierarchy = ((smqlQuery.Hierarchies.Count != 0) ? smqlQuery.Hierarchies[0] : null);
			if (this.MeasureGroup == null && this.Hierarchy == null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (this.MeasureGroup != null && this.Hierarchy != null && this.MeasureGroup.BaseEntity != this.Hierarchy.BaseEntity)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.BaseEntity = ((this.MeasureGroup != null) ? this.MeasureGroup.BaseEntity : this.Hierarchy.BaseEntity);
			this.BaseEntityFilter = ((this.Hierarchy != null) ? this.Hierarchy.Filter : null);
		}

		// Token: 0x040001DA RID: 474
		internal readonly MeasureGroup MeasureGroup;

		// Token: 0x040001DB RID: 475
		internal readonly Hierarchy Hierarchy;

		// Token: 0x040001DC RID: 476
		internal readonly IQueryEntity BaseEntity;

		// Token: 0x040001DD RID: 477
		internal readonly Expression BaseEntityFilter;
	}
}
