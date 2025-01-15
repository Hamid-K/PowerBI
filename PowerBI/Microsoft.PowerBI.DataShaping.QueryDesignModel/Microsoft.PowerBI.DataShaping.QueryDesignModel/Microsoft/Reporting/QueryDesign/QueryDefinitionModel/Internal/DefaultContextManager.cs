using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D0 RID: 208
	internal sealed class DefaultContextManager : IEquatable<DefaultContextManager>, IReadOnlyDefaultContextManager
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x0002218C File Offset: 0x0002038C
		internal DefaultContextManager()
		{
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000221F8 File Offset: 0x000203F8
		internal DefaultContextManager(FieldRelationshipAnnotations relationshipAnnotations, ColumnGroupingAnnotations columnGroupingAnnotations)
		{
			this._fieldRelationshipAnnotations = relationshipAnnotations;
			this._columnGroupingAnnotations = columnGroupingAnnotations;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00022274 File Offset: 0x00020474
		public bool IsEmpty
		{
			get
			{
				return this._fieldsWithImplicitClearedContext.Count == 0 && this._columnsWithImplicitClearedContext.Count == 0 && this._fieldsRequiringClearedContext.Count == 0 && this._columnsRequiringClearedContext.Count == 0 && this._fieldsRequiringImplicitDefaultContext.Count == 0 && this._columnsRequiringImplicitDefaultContext.Count == 0 && this._fieldsRequiringExplicitDefaultContext.Count == 0 && this._columnsRequiringExplicitDefaultContext.Count == 0;
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000222EC File Offset: 0x000204EC
		public override bool Equals(object other)
		{
			DefaultContextManager defaultContextManager = other as DefaultContextManager;
			return defaultContextManager != null && this.Equals(defaultContextManager);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002230C File Offset: 0x0002050C
		public override int GetHashCode()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(this._fieldsWithImplicitClearedContext.GetHashCode(), this._columnsWithImplicitClearedContext.GetHashCode(), this._fieldsRequiringClearedContext.GetHashCode(), this._columnsRequiringClearedContext.GetHashCode(), this._fieldsRequiringImplicitDefaultContext.GetHashCode(), this._columnsRequiringImplicitDefaultContext.GetHashCode(), this._fieldsRequiringExplicitDefaultContext.GetHashCode(), this._columnsRequiringExplicitDefaultContext.GetHashCode());
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00022378 File Offset: 0x00020578
		public bool Equals(DefaultContextManager other)
		{
			return other != null && (this._fieldsWithImplicitClearedContext.SetEquals(other._fieldsWithImplicitClearedContext) && this._columnsWithImplicitClearedContext.SetEquals(other._columnsWithImplicitClearedContext) && this._fieldsRequiringClearedContext.SetEquals(other._fieldsRequiringClearedContext) && this._columnsRequiringClearedContext.SetEquals(other._columnsRequiringClearedContext) && this._fieldsRequiringImplicitDefaultContext.SetEquals(other._fieldsRequiringImplicitDefaultContext) && this._columnsRequiringImplicitDefaultContext.SetEquals(other._columnsRequiringImplicitDefaultContext) && this._fieldsRequiringExplicitDefaultContext.SetEquals(other._fieldsRequiringExplicitDefaultContext)) && this._columnsRequiringExplicitDefaultContext.SetEquals(other._columnsRequiringExplicitDefaultContext);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00022425 File Offset: 0x00020625
		public IEnumerable<IEdmFieldInstance> GetFieldsRequiringClearDefaultFilterContext()
		{
			return this._fieldsWithImplicitClearedContext.Except(this._fieldsRequiringImplicitDefaultContext).Union(this._fieldsRequiringClearedContext).Except(this._fieldsRequiringExplicitDefaultContext);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002244E File Offset: 0x0002064E
		public IEnumerable<IConceptualColumn> GetColumnsRequiringClearDefaultFilterContext()
		{
			return this._columnsWithImplicitClearedContext.Except(this._columnsRequiringImplicitDefaultContext).Union(this._columnsRequiringClearedContext).Except(this._columnsRequiringExplicitDefaultContext);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00022478 File Offset: 0x00020678
		public void AddFieldRequiringDefaultContext(IEnumerable<IEdmFieldInstance> fields)
		{
			foreach (IEdmFieldInstance edmFieldInstance in fields)
			{
				this.AddFieldRequiringDefaultContext(edmFieldInstance);
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000224C0 File Offset: 0x000206C0
		public void AddColumnRequiringDefaultContext(IEnumerable<IConceptualColumn> columns)
		{
			foreach (IConceptualColumn conceptualColumn in columns)
			{
				this.AddColumnRequiringDefaultContext(conceptualColumn);
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00022508 File Offset: 0x00020708
		public void AddFieldRequiringDefaultContext(IEdmFieldInstance field)
		{
			IEnumerable<IEdmFieldInstance> identityFieldsWithDefaultValues = QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(field);
			IEnumerable<IEdmFieldInstance> identityFieldsWithDefaultValues2 = QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(identityFieldsWithDefaultValues.GetFieldsForExplicitDefaultFilterRetain());
			this._fieldsRequiringImplicitDefaultContext.UnionWith(identityFieldsWithDefaultValues2);
			this._fieldsRequiringImplicitDefaultContext.ExceptWith(identityFieldsWithDefaultValues);
			this._fieldsRequiringExplicitDefaultContext.UnionWith(identityFieldsWithDefaultValues);
			IEnumerable<IEdmFieldInstance> fieldsForImplicitDefaultFilterContextExclusions = identityFieldsWithDefaultValues.GetFieldsForImplicitDefaultFilterContextExclusions();
			if (fieldsForImplicitDefaultFilterContextExclusions != null)
			{
				this._fieldsWithImplicitClearedContext.UnionWith(QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(fieldsForImplicitDefaultFilterContextExclusions));
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00022568 File Offset: 0x00020768
		public void AddColumnRequiringDefaultContext(IConceptualColumn column)
		{
			IEnumerable<IConceptualColumn> identityColumnsWithDefaultValues = QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(column);
			IEnumerable<IConceptualColumn> identityColumnsWithDefaultValues2 = QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(identityColumnsWithDefaultValues.GetColumnsForExplicitDefaultFilterRetain(this._fieldRelationshipAnnotations, this._columnGroupingAnnotations));
			this._columnsRequiringImplicitDefaultContext.UnionWith(identityColumnsWithDefaultValues2);
			this._columnsRequiringImplicitDefaultContext.ExceptWith(identityColumnsWithDefaultValues);
			this._columnsRequiringExplicitDefaultContext.UnionWith(identityColumnsWithDefaultValues);
			IEnumerable<IConceptualColumn> columnsForImplicitDefaultFilterContextExclusions = identityColumnsWithDefaultValues.GetColumnsForImplicitDefaultFilterContextExclusions(this._fieldRelationshipAnnotations, this._columnGroupingAnnotations);
			if (columnsForImplicitDefaultFilterContextExclusions != null)
			{
				this._columnsWithImplicitClearedContext.UnionWith(QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(columnsForImplicitDefaultFilterContextExclusions));
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000225E0 File Offset: 0x000207E0
		public void AddFieldsRequiringClearedContext(FilterCondition filterCondition)
		{
			IReadOnlyList<IEdmFieldInstance> referencedModelFields = filterCondition.GetReferencedModelFields();
			if (referencedModelFields.IsNullOrEmpty<IEdmFieldInstance>())
			{
				return;
			}
			this.AddFieldsRequiringClearedContext(referencedModelFields);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00022604 File Offset: 0x00020804
		public void AddColumnsRequiringClearedContext(FilterCondition filterCondition)
		{
			IReadOnlyList<IConceptualColumn> referencedModelColumns = filterCondition.GetReferencedModelColumns();
			if (referencedModelColumns.IsNullOrEmpty<IConceptualColumn>())
			{
				return;
			}
			this.AddColumnsRequiringClearedContext(referencedModelColumns);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00022628 File Offset: 0x00020828
		public void AddFieldsRequiringClearedContext(IReadOnlyList<IEdmFieldInstance> referencedFields)
		{
			IEnumerable<IEdmFieldInstance> identityFields = QdmExpressionBuilder.GetIdentityFields(referencedFields);
			this.AddFieldsRequiringClearedContext(QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(identityFields.GetFieldsForDefaultGroupAndFilterExclusions(false)));
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00022650 File Offset: 0x00020850
		public void AddColumnsRequiringClearedContext(IReadOnlyList<IConceptualColumn> referencedFields)
		{
			IEnumerable<IConceptualColumn> identityColumns = QdmExpressionBuilder.GetIdentityColumns(referencedFields);
			this.AddColumnsRequiringClearedContext(QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(identityColumns.GetColumnsForDefaultGroupAndFilterExclusions(this._fieldRelationshipAnnotations, this._columnGroupingAnnotations, false)));
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00022684 File Offset: 0x00020884
		public void AddFieldRequiringClearedContext(IEdmFieldInstance field)
		{
			IEnumerable<IEdmFieldInstance> identityFieldsWithDefaultValues = QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(field);
			this.AddFieldsRequiringClearedContext(QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(identityFieldsWithDefaultValues.GetFieldsForDefaultGroupAndFilterExclusions(false)));
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000226AC File Offset: 0x000208AC
		public void AddColumnRequiringClearedContext(IConceptualColumn column)
		{
			IEnumerable<IConceptualColumn> identityColumnsWithDefaultValues = QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(column);
			this.AddColumnsRequiringClearedContext(QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(identityColumnsWithDefaultValues.GetColumnsForDefaultGroupAndFilterExclusions(this._fieldRelationshipAnnotations, this._columnGroupingAnnotations, false)));
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000226E0 File Offset: 0x000208E0
		public void AddImplicitGroupingDefaultFilterExclusions(Group group, bool useConceptualSchema)
		{
			foreach (GroupKey groupKey in group.Keys)
			{
				this.AddImplicitGroupingDefaultFilterExclusions(groupKey.Expression, useConceptualSchema);
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00022734 File Offset: 0x00020934
		public void AddImplicitGroupingDefaultFilterExclusions(QueryExpression groupKeyExpression, bool useConceptualSchema)
		{
			if (useConceptualSchema)
			{
				IEnumerable<IConceptualColumn> identityColumns = QdmExpressionBuilder.GetIdentityColumns(groupKeyExpression.GetReferencedColumns());
				this.AddColumnRequiringImplicitGroupingClearedContext(identityColumns);
				return;
			}
			IEnumerable<IEdmFieldInstance> identityFields = QdmExpressionBuilder.GetIdentityFields(groupKeyExpression.GetReferencedFields());
			this.AddFieldRequiringImplicitGroupingClearedContext(identityFields);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002276C File Offset: 0x0002096C
		public void AddFieldRequiringImplicitGroupingClearedContext(IEnumerable<IEdmFieldInstance> fields)
		{
			IEnumerable<IEdmFieldInstance> fieldsForDefaultGroupAndFilterExclusions = fields.GetFieldsForDefaultGroupAndFilterExclusions(false);
			if (!fieldsForDefaultGroupAndFilterExclusions.IsNullOrEmpty<IEdmFieldInstance>())
			{
				this._fieldsWithImplicitClearedContext.UnionWith(QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(fieldsForDefaultGroupAndFilterExclusions));
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002279C File Offset: 0x0002099C
		public void AddColumnRequiringImplicitGroupingClearedContext(IEnumerable<IConceptualColumn> columns)
		{
			IEnumerable<IConceptualColumn> columnsForDefaultGroupAndFilterExclusions = columns.GetColumnsForDefaultGroupAndFilterExclusions(this._fieldRelationshipAnnotations, this._columnGroupingAnnotations, false);
			if (!columnsForDefaultGroupAndFilterExclusions.IsNullOrEmpty<IConceptualColumn>())
			{
				this._columnsWithImplicitClearedContext.UnionWith(QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(columnsForDefaultGroupAndFilterExclusions));
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000227D6 File Offset: 0x000209D6
		private void AddFieldsRequiringClearedContext(IEnumerable<IEdmFieldInstance> fields)
		{
			if (fields.IsNullOrEmpty<IEdmFieldInstance>())
			{
				return;
			}
			this._fieldsRequiringClearedContext.UnionWith(fields.Where((IEdmFieldInstance f) => f.Field.DefaultMember != null));
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00022811 File Offset: 0x00020A11
		private void AddColumnsRequiringClearedContext(IEnumerable<IConceptualColumn> columns)
		{
			if (columns.IsNullOrEmpty<IConceptualColumn>())
			{
				return;
			}
			this._columnsRequiringClearedContext.UnionWith(columns.Where((IConceptualColumn c) => c.DefaultValue != null));
		}

		// Token: 0x04000974 RID: 2420
		private readonly HashSet<IEdmFieldInstance> _fieldsWithImplicitClearedContext = new HashSet<IEdmFieldInstance>();

		// Token: 0x04000975 RID: 2421
		private readonly HashSet<IConceptualColumn> _columnsWithImplicitClearedContext = new HashSet<IConceptualColumn>();

		// Token: 0x04000976 RID: 2422
		private readonly HashSet<IEdmFieldInstance> _fieldsRequiringClearedContext = new HashSet<IEdmFieldInstance>();

		// Token: 0x04000977 RID: 2423
		private readonly HashSet<IConceptualColumn> _columnsRequiringClearedContext = new HashSet<IConceptualColumn>();

		// Token: 0x04000978 RID: 2424
		private readonly HashSet<IEdmFieldInstance> _fieldsRequiringImplicitDefaultContext = new HashSet<IEdmFieldInstance>();

		// Token: 0x04000979 RID: 2425
		private readonly HashSet<IConceptualColumn> _columnsRequiringImplicitDefaultContext = new HashSet<IConceptualColumn>();

		// Token: 0x0400097A RID: 2426
		private readonly HashSet<IEdmFieldInstance> _fieldsRequiringExplicitDefaultContext = new HashSet<IEdmFieldInstance>();

		// Token: 0x0400097B RID: 2427
		private readonly HashSet<IConceptualColumn> _columnsRequiringExplicitDefaultContext = new HashSet<IConceptualColumn>();

		// Token: 0x0400097C RID: 2428
		private readonly FieldRelationshipAnnotations _fieldRelationshipAnnotations;

		// Token: 0x0400097D RID: 2429
		private readonly ColumnGroupingAnnotations _columnGroupingAnnotations;
	}
}
