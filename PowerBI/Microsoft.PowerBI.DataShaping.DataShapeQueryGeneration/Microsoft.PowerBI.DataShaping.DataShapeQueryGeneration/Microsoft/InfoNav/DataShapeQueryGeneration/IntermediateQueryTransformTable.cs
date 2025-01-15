using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000DD RID: 221
	internal sealed class IntermediateQueryTransformTable
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x0001CC44 File Offset: 0x0001AE44
		internal IntermediateQueryTransformTable(Identifier id, string queryName, List<IntermediateQueryTransformTableColumn> columns, IntermediateQueryTransformTable sourceTable, QuerySourceExpressionReferenceContext sourceRefContext, DataShapeGenerationErrorContext errorContext)
		{
			this._id = id;
			this._queryName = queryName;
			this._columns = columns;
			this._sourceTable = sourceTable;
			this._sourceRefContext = sourceRefContext;
			this._errorContext = errorContext;
			foreach (IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn in this._columns)
			{
				intermediateQueryTransformTableColumn.SetTable(this);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
		internal Identifier Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		internal string QueryName
		{
			get
			{
				return this._queryName;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001CCD8 File Offset: 0x0001AED8
		internal IReadOnlyList<IntermediateQueryTransformTableColumn> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001CCE0 File Offset: 0x0001AEE0
		internal bool TryGetColumn(ResolvedQueryExpression underlyingExpression, out IntermediateQueryTransformTableColumn column)
		{
			for (int i = 0; i < this._columns.Count; i++)
			{
				IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn = this._columns[i];
				if (intermediateQueryTransformTableColumn.UnderlyingExpression.Equals(underlyingExpression))
				{
					column = intermediateQueryTransformTableColumn;
					return true;
				}
			}
			column = null;
			return false;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001CD28 File Offset: 0x0001AF28
		private void PropagateRoleAndOmitFromOutput(IntermediateQueryTransformTableColumn existingTransformColumn, IntermediateQueryTransformTableColumn column)
		{
			if (this._sourceTable != null)
			{
				IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
				this._sourceTable.TryGetColumn(existingTransformColumn.UnderlyingExpression, out intermediateQueryTransformTableColumn);
				IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn2;
				this._sourceTable.TryGetColumn(column.UnderlyingExpression, out intermediateQueryTransformTableColumn2);
				this._sourceTable.PropagateRoleAndOmitFromOutput(intermediateQueryTransformTableColumn, intermediateQueryTransformTableColumn2);
			}
			if (existingTransformColumn.Role != null && column.Role != existingTransformColumn.Role)
			{
				column.SetRole(existingTransformColumn.Role);
			}
			existingTransformColumn.OmitFromOutput = true;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001CDA0 File Offset: 0x0001AFA0
		internal static IntermediateQueryTransformTableColumn GetOrCreateColumn(IConceptualProperty newConceptualProperty, TransformTableColumnActAs actAs, IntermediateQueryTransformTableColumn existingTransformColumn, DsqExpressionGenerator expressionGenerator, bool propagateRoleAndOmitFromOutput)
		{
			ResolvedQueryColumnReferenceExpression resolvedQueryColumnReferenceExpression = existingTransformColumn.UnderlyingExpression as ResolvedQueryColumnReferenceExpression;
			if (resolvedQueryColumnReferenceExpression != null)
			{
				return existingTransformColumn.Table.GetOrCreateColumn(expressionGenerator, resolvedQueryColumnReferenceExpression, actAs, newConceptualProperty, existingTransformColumn, propagateRoleAndOmitFromOutput);
			}
			ResolvedQueryPropertyExpression resolvedQueryPropertyExpression = (existingTransformColumn.UnderlyingExpression as ResolvedQueryPropertyExpression).Expression.Property(newConceptualProperty);
			return existingTransformColumn.Table.GetOrCreateColumn(expressionGenerator, resolvedQueryPropertyExpression, actAs, newConceptualProperty.FormatString, existingTransformColumn, propagateRoleAndOmitFromOutput);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001CE00 File Offset: 0x0001B000
		private IntermediateQueryTransformTableColumn GetOrCreateColumn(DsqExpressionGenerator expressionGenerator, ResolvedQueryExpression underlyingExpression, TransformTableColumnActAs actAs, string formatString, IntermediateQueryTransformTableColumn existingTransformColumn, bool propagateRoleAndOmitFromOutput)
		{
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (this.TryGetColumn(underlyingExpression, out intermediateQueryTransformTableColumn))
			{
				if (propagateRoleAndOmitFromOutput)
				{
					this.PropagateRoleAndOmitFromOutput(existingTransformColumn, intermediateQueryTransformTableColumn);
				}
				return intermediateQueryTransformTableColumn;
			}
			string text = DataShapeIdGenerator.CreateTransformColumnId(this._columns.Count);
			string role = this.GetRole(existingTransformColumn, propagateRoleAndOmitFromOutput);
			if (this._sourceTable == null)
			{
				HashSet<string> hashSet;
				GeneratedDsqExpression generatedDsqExpression;
				if (!ResolvedQueryExpressionValidator.Validate(underlyingExpression, this._errorContext, AllowedExpressionContent.Transform, new ExpressionContext(this._queryName, SemanticQueryObjectType.TransformColumn, text), out hashSet) || !expressionGenerator.TryGenerate(underlyingExpression, out generatedDsqExpression))
				{
					throw new InvalidOperationException(StringUtil.FormatInvariant("Could not create column from expression: {0}", underlyingExpression.ToTraceString()));
				}
				IConceptualColumn conceptualColumn;
				underlyingExpression.TryGetAsProperty(out conceptualColumn);
				intermediateQueryTransformTableColumn = new IntermediateQueryTransformTableColumn(text, generatedDsqExpression.Expression, role, actAs, formatString, conceptualColumn, underlyingExpression, generatedDsqExpression.IsScalar);
			}
			else
			{
				IntermediateQueryTransformTableColumn orCreateColumn = this._sourceTable.GetOrCreateColumn(expressionGenerator, existingTransformColumn.UnderlyingExpression, actAs, formatString, existingTransformColumn, false);
				IntermediateQueryTransformTableColumn orCreateColumn2 = this._sourceTable.GetOrCreateColumn(expressionGenerator, underlyingExpression, actAs, formatString, orCreateColumn, propagateRoleAndOmitFromOutput);
				intermediateQueryTransformTableColumn = new IntermediateQueryTransformTableColumn(text, orCreateColumn2.DsqExpression(), role, actAs, orCreateColumn2.FormatString, orCreateColumn2.UnderlyingConceptualColumn, orCreateColumn2.UnderlyingExpression, orCreateColumn2.IsScalar);
			}
			this._columns.Add(intermediateQueryTransformTableColumn);
			intermediateQueryTransformTableColumn.SetTable(this);
			return intermediateQueryTransformTableColumn;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001CF30 File Offset: 0x0001B130
		private IntermediateQueryTransformTableColumn GetOrCreateColumn(DsqExpressionGenerator expressionGenerator, ResolvedQueryColumnReferenceExpression subqueryColumnRef, TransformTableColumnActAs actAs, IConceptualProperty newConceptualProperty, IntermediateQueryTransformTableColumn existingTransformColumn, bool propagateRoleAndOmitFromOutput)
		{
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (this.TryGetColumnWithProperty(newConceptualProperty, out intermediateQueryTransformTableColumn))
			{
				if (propagateRoleAndOmitFromOutput)
				{
					this.PropagateRoleAndOmitFromOutput(existingTransformColumn, intermediateQueryTransformTableColumn);
				}
				return intermediateQueryTransformTableColumn;
			}
			string text = DataShapeIdGenerator.CreateTransformColumnId(this._columns.Count);
			string role = this.GetRole(existingTransformColumn, propagateRoleAndOmitFromOutput);
			if (this._sourceTable == null)
			{
				IntermediateTableSchemaColumn intermediateTableSchemaColumn;
				IIntermediateTableSchemaItem intermediateTableSchemaItem;
				if (!this._sourceRefContext.TryGetColumnInSource(subqueryColumnRef, this._errorContext, out intermediateTableSchemaColumn) || !intermediateTableSchemaColumn.TryGetRelatedItem(newConceptualProperty, out intermediateTableSchemaItem))
				{
					throw new InvalidOperationException(StringUtil.FormatInvariant("Could not create column from expression: {0}", subqueryColumnRef.ToTraceString()));
				}
				IConceptualColumn conceptualColumn = intermediateTableSchemaItem.LineageProperty as IConceptualColumn;
				intermediateQueryTransformTableColumn = new IntermediateQueryTransformTableColumn(text, intermediateTableSchemaItem.ValueCalculationId.StructureReference(), role, actAs, intermediateTableSchemaItem.FormatString, conceptualColumn, subqueryColumnRef, (conceptualColumn != null) ? new bool?(conceptualColumn.ConceptualDataType.IsScalar()) : null);
			}
			else
			{
				IntermediateQueryTransformTableColumn orCreateColumn = this._sourceTable.GetOrCreateColumn(expressionGenerator, subqueryColumnRef, actAs, newConceptualProperty, existingTransformColumn, propagateRoleAndOmitFromOutput);
				intermediateQueryTransformTableColumn = new IntermediateQueryTransformTableColumn(text, orCreateColumn.DsqExpression(), role, actAs, orCreateColumn.FormatString, orCreateColumn.UnderlyingConceptualColumn, orCreateColumn.UnderlyingExpression, orCreateColumn.IsScalar);
			}
			this._columns.Add(intermediateQueryTransformTableColumn);
			intermediateQueryTransformTableColumn.SetTable(this);
			return intermediateQueryTransformTableColumn;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001D068 File Offset: 0x0001B268
		private bool TryGetColumnWithProperty(IConceptualProperty conceptualProperty, out IntermediateQueryTransformTableColumn column)
		{
			for (int i = 0; i < this._columns.Count; i++)
			{
				IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn = this._columns[i];
				if (intermediateQueryTransformTableColumn.UnderlyingConceptualColumn != null && intermediateQueryTransformTableColumn.UnderlyingConceptualColumn.Equals(conceptualProperty))
				{
					column = intermediateQueryTransformTableColumn;
					return true;
				}
			}
			column = null;
			return false;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001D0B7 File Offset: 0x0001B2B7
		private string GetRole(IntermediateQueryTransformTableColumn correspondingTransformColumn, bool propagateCorrespondingRole)
		{
			if (propagateCorrespondingRole)
			{
				return correspondingTransformColumn.Role;
			}
			return null;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001D0C4 File Offset: 0x0001B2C4
		internal void RemoveColumn(IntermediateQueryTransformTableColumn column)
		{
			this._columns.Remove(column);
		}

		// Token: 0x040003FD RID: 1021
		private readonly Identifier _id;

		// Token: 0x040003FE RID: 1022
		private readonly string _queryName;

		// Token: 0x040003FF RID: 1023
		private readonly List<IntermediateQueryTransformTableColumn> _columns;

		// Token: 0x04000400 RID: 1024
		private readonly IntermediateQueryTransformTable _sourceTable;

		// Token: 0x04000401 RID: 1025
		private readonly QuerySourceExpressionReferenceContext _sourceRefContext;

		// Token: 0x04000402 RID: 1026
		private readonly DataShapeGenerationErrorContext _errorContext;
	}
}
