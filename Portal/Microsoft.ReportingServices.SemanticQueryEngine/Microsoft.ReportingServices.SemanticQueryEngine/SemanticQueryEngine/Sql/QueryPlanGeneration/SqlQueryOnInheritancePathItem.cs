using System;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000062 RID: 98
	internal sealed class SqlQueryOnInheritancePathItem : SqlQueryOnRelationBase
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0001414C File Offset: 0x0001234C
		internal SqlQueryOnInheritancePathItem(QueryPlanBuilder qpBuilder, NestedQueryKey key)
			: base(qpBuilder, key)
		{
			if (base.Key == null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (!(base.Key.FilteredPathItem.ExpressionPathItem is InheritancePathItem))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (this.Entity.ModelEntity == null || base.Key.FilteredPathItem.TargetEntity.ModelEntity == null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.InitializeJoinColumns(base.Key.FilteredPathItem.SourceEntity.ModelEntity, this.Entity.ModelEntity);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000141DC File Offset: 0x000123DC
		private void InitializeJoinColumns(ModelEntity sourceEntity, ModelEntity targetEntity)
		{
			if (sourceEntity.Inheritance != null && sourceEntity.Inheritance.InheritsFrom == targetEntity)
			{
				if (sourceEntity.Inheritance.Binding == null)
				{
					throw SQEAssert.AssertFalseAndThrow("sourceEntity inheritance has no binding.", Array.Empty<object>());
				}
				base.InitializeJoinColumns(sourceEntity.Inheritance.Binding.GetRelation(), RelationEnd.Target);
				return;
			}
			else
			{
				if (targetEntity.Inheritance == null || targetEntity.Inheritance.InheritsFrom != sourceEntity)
				{
					throw SQEAssert.AssertFalseAndThrow("Invalid InheritancePathItem: source and target entities are not related thru inheritance.", Array.Empty<object>());
				}
				if (targetEntity.Inheritance.Binding == null)
				{
					throw SQEAssert.AssertFalseAndThrow("targetEntity inheritance has no binding.", Array.Empty<object>());
				}
				base.InitializeJoinColumns(targetEntity.Inheritance.Binding.GetRelation(), RelationEnd.Source);
				return;
			}
		}
	}
}
