using System;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000065 RID: 101
	internal sealed class SqlQueryOnRolePathItem : SqlQueryOnRelationBase
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x000145E4 File Offset: 0x000127E4
		internal SqlQueryOnRolePathItem(QueryPlanBuilder qpBuilder, NestedQueryKey key)
			: base(qpBuilder, key)
		{
			if (base.Key == null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (!(base.Key.FilteredPathItem.ExpressionPathItem is RolePathItem))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (this.Entity.ModelEntity == null || base.Key.FilteredPathItem.SourceEntity.ModelEntity == null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.InitializeJoinColumns(base.Key.FilteredPathItem.SourceEntity.ModelEntity, this.Entity.ModelEntity);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00014674 File Offset: 0x00012874
		internal static bool CanSkipPathPoint(RolePathItem rolePathItem)
		{
			ModelEntity modelEntity = rolePathItem.SourceEntity.ModelEntity;
			TableBinding tableBinding = ((modelEntity != null) ? (modelEntity.Binding as TableBinding) : null);
			ModelEntity modelEntity2 = rolePathItem.TargetEntity.ModelEntity;
			ColumnBinding columnBinding = ((modelEntity2 != null) ? (modelEntity2.Binding as ColumnBinding) : null);
			return rolePathItem.Cardinality == Cardinality.One && tableBinding != null && columnBinding != null && tableBinding.GetTable() == columnBinding.GetColumn().Table;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000146E0 File Offset: 0x000128E0
		private void InitializeJoinColumns(ModelEntity sourceEntity, ModelEntity targetEntity)
		{
			RelationBinding binding = ((RolePathItem)base.Key.FilteredPathItem.ExpressionPathItem).Role.Binding;
			if (binding != null)
			{
				base.InitializeJoinColumns(binding.GetRelation(), binding.RelationEnd);
				return;
			}
			Binding binding2 = sourceEntity.Binding;
			Binding binding3 = targetEntity.Binding;
			if (binding2 == null || binding3 == null)
			{
				throw new NotImplementedException("Calculated entities are not supported in SQL 2005 (sourceEntity.Binding == null || targetEntity.Binding == null).");
			}
			if ((binding2 is TableBinding && binding3 is TableBinding) || (binding2 is ColumnBinding && binding3 is ColumnBinding))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			DsvTable dsvTable;
			DsvColumn dsvColumn;
			if (binding2 is TableBinding)
			{
				dsvTable = ((TableBinding)binding2).GetTable();
				dsvColumn = ((ColumnBinding)binding3).GetColumn();
			}
			else
			{
				dsvTable = ((TableBinding)binding3).GetTable();
				dsvColumn = ((ColumnBinding)binding2).GetColumn();
			}
			if (dsvTable == null || dsvColumn == null || dsvColumn.Table != dsvTable)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			DsvColumn[] array = new DsvColumn[] { dsvColumn };
			base.InitializeJoinColumns(array, array);
		}
	}
}
