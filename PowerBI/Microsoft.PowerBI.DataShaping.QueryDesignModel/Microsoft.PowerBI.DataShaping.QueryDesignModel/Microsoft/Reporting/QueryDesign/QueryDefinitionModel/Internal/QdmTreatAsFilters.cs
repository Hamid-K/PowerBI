using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000103 RID: 259
	internal static class QdmTreatAsFilters
	{
		// Token: 0x06000EE2 RID: 3810 RVA: 0x00028391 File Offset: 0x00026591
		private static bool CanUseTreatAsForFilters(this DaxCapabilities daxCapabilities)
		{
			return daxCapabilities.IsSupported(DaxFunctionKind.TreatAs) && daxCapabilities.IsSupported(ModelCapabilitiesKind.TableConstructor);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000283A8 File Offset: 0x000265A8
		public static bool TryHandlePredicateUsingTreatAs(QueryExpression predicate, IEnumerable<IEdmFieldInstance> predicatesGroupingFields, IEnumerable<IConceptualColumn> predicatesGroupingColumns, DaxCapabilities daxCapabilities, IDataComparer comparer, CancellationToken cancellationToken, bool useConceptualSchema, out QueryExpression treatAsPredicate)
		{
			if (!daxCapabilities.CanUseTreatAsForFilters())
			{
				treatAsPredicate = null;
				return false;
			}
			treatAsPredicate = predicate as QueryTreatAsExpression;
			if (treatAsPredicate != null)
			{
				return true;
			}
			QueryInExpression queryInExpression;
			bool flag;
			if (QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInExpression>(predicate, out queryInExpression, out flag) && !flag)
			{
				if (useConceptualSchema ? (!QdmTreatAsFilters.ArePredicateColumnsCompatibleWithTreatAs(predicate, predicatesGroupingColumns, daxCapabilities)) : (!QdmTreatAsFilters.ArePredicateFieldsCompatibleWithTreatAs(predicate, predicatesGroupingFields, daxCapabilities)))
				{
					return false;
				}
				if (!queryInExpression.CanBePreservedAsTuples)
				{
					return false;
				}
				treatAsPredicate = QdmTreatAsFilters.TranslateInToTreatAs(queryInExpression, comparer, cancellationToken);
				return true;
			}
			else
			{
				QueryInTableExpression queryInTableExpression;
				bool flag2;
				if (!QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInTableExpression>(predicate, out queryInTableExpression, out flag2) || flag2)
				{
					return false;
				}
				if (useConceptualSchema ? (!QdmTreatAsFilters.ArePredicateColumnsCompatibleWithTreatAs(predicate, predicatesGroupingColumns, daxCapabilities)) : (!QdmTreatAsFilters.ArePredicateFieldsCompatibleWithTreatAs(predicate, predicatesGroupingFields, daxCapabilities)))
				{
					return false;
				}
				treatAsPredicate = QdmTreatAsFilters.TranslateInTableToTreatAs(queryInTableExpression);
				return true;
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00028458 File Offset: 0x00026658
		private static bool ArePredicateFieldsCompatibleWithTreatAs(QueryExpression predicate, IEnumerable<IEdmFieldInstance> predicatesGroupingFields, DaxCapabilities daxCapabilities)
		{
			IEnumerable<IEdmFieldInstance> referencedFields = predicate.GetReferencedFields();
			if (!daxCapabilities.IsSupported(ModelCapabilitiesKind.TreatAsSupportForAllColumns))
			{
				if (!referencedFields.All((IEdmFieldInstance f) => f.SupportsTreatAs()))
				{
					return false;
				}
			}
			if (!predicatesGroupingFields.ToSet<IEdmFieldInstance>().SetEquals(QdmExpressionBuilder.GetIdentityFields(referencedFields)))
			{
				return false;
			}
			HashSet<EdmField> hashSet = referencedFields.Select((IEdmFieldInstance s) => s.Field).ToSet<EdmField>();
			foreach (EdmField edmField in hashSet)
			{
				if (!edmField.Grouping.IsIdentityOnEntityKey)
				{
					foreach (EdmField edmField2 in edmField.Grouping.IdentityFields)
					{
						if (!hashSet.Contains(edmField2))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00028578 File Offset: 0x00026778
		private static bool ArePredicateColumnsCompatibleWithTreatAs(QueryExpression predicate, IEnumerable<IConceptualColumn> predicatesGroupingColumns, DaxCapabilities daxCapabilities)
		{
			IEnumerable<IConceptualColumn> referencedColumns = predicate.GetReferencedColumns();
			if (!daxCapabilities.IsSupported(ModelCapabilitiesKind.TreatAsSupportForAllColumns))
			{
				if (!referencedColumns.All((IConceptualColumn f) => f.SupportsTreatAs()))
				{
					return false;
				}
			}
			if (!predicatesGroupingColumns.ToSet<IConceptualColumn>().SetEquals(QdmExpressionBuilder.GetIdentityColumns(referencedColumns)))
			{
				return false;
			}
			foreach (IConceptualColumn conceptualColumn in referencedColumns)
			{
				if (!conceptualColumn.Grouping.IsIdentityOnEntityKey)
				{
					foreach (IConceptualColumn conceptualColumn2 in conceptualColumn.Grouping.IdentityColumns)
					{
						if (!referencedColumns.Contains(conceptualColumn2))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00028664 File Offset: 0x00026864
		private static QueryExpression TranslateInToTreatAs(QueryInExpression inExpression, IDataComparer comparer, CancellationToken cancellationToken)
		{
			inExpression = QueryAlgorithms.DedupeValues(inExpression, comparer);
			if (!inExpression.IsStrict)
			{
				inExpression = QueryAlgorithms.RewriteWithEqualitySemantics(inExpression, cancellationToken);
			}
			IReadOnlyList<QueryExpression> expressions = inExpression.Expressions;
			IReadOnlyList<IReadOnlyList<QueryExpression>> values = inExpression.Values;
			TreatAsTableBuilder treatAsTableBuilder = new TreatAsTableBuilder(expressions.Count);
			foreach (QueryExpression queryExpression in expressions)
			{
				treatAsTableBuilder.AddColumn(queryExpression);
			}
			foreach (IReadOnlyList<QueryExpression> readOnlyList in values)
			{
				treatAsTableBuilder.AddRow(readOnlyList);
			}
			return treatAsTableBuilder.ToQueryTable().Expression;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00028728 File Offset: 0x00026928
		private static QueryExpression TranslateInTableToTreatAs(QueryInTableExpression inTableExpression)
		{
			QueryExpression queryExpression = inTableExpression.LeftExpression.RewriteEntityPlaceholdersToScalarEntityReferences(null, null);
			QueryTupleExpression queryTupleExpression = queryExpression as QueryTupleExpression;
			IReadOnlyList<KeyValuePair<string, QueryExpression>> readOnlyList;
			if (queryTupleExpression != null)
			{
				readOnlyList = queryTupleExpression.NamedColumns;
			}
			else
			{
				readOnlyList = new KeyValuePair<string, QueryExpression>[] { queryExpression.As("Column") };
			}
			return inTableExpression.RightExpression.TreatAs(readOnlyList);
		}
	}
}
