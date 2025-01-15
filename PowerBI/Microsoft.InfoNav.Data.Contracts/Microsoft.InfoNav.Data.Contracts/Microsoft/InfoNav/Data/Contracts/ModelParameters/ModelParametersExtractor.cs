using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.ModelParameters
{
	// Token: 0x020000E9 RID: 233
	public static class ModelParametersExtractor
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x0000C900 File Offset: 0x0000AB00
		public static ParameterMappings ExtractParameterMappings(IReadOnlyList<ResolvedQueryFilter> resolvedQueryFilters, IErrorContext errorContext)
		{
			WriteableParameterMappings writeableParameterMappings = new WriteableParameterMappings();
			foreach (ResolvedQueryFilter resolvedQueryFilter in resolvedQueryFilters)
			{
				FilterAnnotations annotations = resolvedQueryFilter.Annotations;
				if (annotations == null || annotations.MParameterBehavior != MParameterBehavior.Ignore)
				{
					bool flag = !resolvedQueryFilter.Target.IsNullOrEmpty<ResolvedQueryExpression>();
					IConceptualColumn conceptualColumn = ModelParametersExtractor.ExtractMappedColumnFromTarget(resolvedQueryFilter.Target, errorContext);
					ParameterMapping parameterMapping = ModelParametersExtractor.ExtractMappingsFromExpression(resolvedQueryFilter.Condition, errorContext, flag, conceptualColumn);
					if (parameterMapping != null)
					{
						writeableParameterMappings.AddMapping(parameterMapping);
					}
				}
			}
			return writeableParameterMappings;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000C99C File Offset: 0x0000AB9C
		private static ParameterMapping ExtractMappingsFromExpression(ResolvedQueryExpression resolvedQueryExpression, IErrorContext errorContext, bool hasTarget, IConceptualColumn mappedColumnInTarget)
		{
			ResolvedQueryComparisonExpression resolvedQueryComparisonExpression = resolvedQueryExpression as ResolvedQueryComparisonExpression;
			if (resolvedQueryComparisonExpression != null)
			{
				return ModelParametersExtractor.ExtractFromQueryComparisonExpression(resolvedQueryComparisonExpression, errorContext, hasTarget, mappedColumnInTarget);
			}
			ResolvedQueryInExpression resolvedQueryInExpression = resolvedQueryExpression as ResolvedQueryInExpression;
			if (resolvedQueryInExpression != null)
			{
				return ModelParametersExtractor.ExtractFromQueryInExpression(resolvedQueryInExpression, errorContext, hasTarget, mappedColumnInTarget);
			}
			ResolvedQueryOrExpression resolvedQueryOrExpression = resolvedQueryExpression as ResolvedQueryOrExpression;
			if (resolvedQueryOrExpression == null)
			{
				if (MappedColumnExtractor.HasMappedParameter(resolvedQueryExpression, errorContext))
				{
					errorContext.RegisterError(QueryValidationMessages.FoundParameterMappingOnUnsupportedFilter, new object[0]);
				}
				return null;
			}
			return ModelParametersExtractor.ExtractFromQueryOrExpression(resolvedQueryOrExpression, errorContext, hasTarget, mappedColumnInTarget);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000CA02 File Offset: 0x0000AC02
		public static bool AnyFilterHasParameterMapping(IReadOnlyList<ResolvedQueryFilter> resolvedQueryFilters, IErrorContext errorContext)
		{
			ParameterMappings parameterMappings = ModelParametersExtractor.ExtractParameterMappings(resolvedQueryFilters, errorContext);
			return (parameterMappings != null && parameterMappings.Count > 0) || errorContext.HasError;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000CA24 File Offset: 0x0000AC24
		private static IConceptualColumn ExtractMappedColumnFromTarget(IReadOnlyList<ResolvedQueryExpression> targets, IErrorContext errorContext)
		{
			if (targets != null && targets.Count > 1)
			{
				using (IEnumerator<ResolvedQueryExpression> enumerator = targets.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ResolvedQueryExpression resolvedQueryExpression = enumerator.Current;
						IReadOnlyList<IConceptualColumn> readOnlyList = MappedColumnExtractor.ExtractMappedColumns(resolvedQueryExpression, errorContext);
						if (readOnlyList != null && readOnlyList.Count > 0)
						{
							errorContext.RegisterError(QueryValidationMessages.FoundMultipleTargetsWithAtLeastOneMapping, new object[0]);
							break;
						}
					}
					goto IL_0093;
				}
			}
			if (targets != null && targets.Count == 1)
			{
				IReadOnlyList<IConceptualColumn> readOnlyList2 = MappedColumnExtractor.ExtractMappedColumns(targets[0], errorContext);
				if (readOnlyList2 != null && readOnlyList2.Count == 1)
				{
					return readOnlyList2[0];
				}
				if (readOnlyList2 != null)
				{
					int count = readOnlyList2.Count;
				}
			}
			IL_0093:
			return null;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		private static ParameterMapping ExtractFromQueryComparisonExpression(ResolvedQueryComparisonExpression resolvedQueryComparisonExpression, IErrorContext errorContext, bool hasTarget, IConceptualColumn mappedColumnInTarget)
		{
			string text;
			ResolvedQueryLiteralExpression resolvedQueryLiteralExpression;
			bool flag;
			bool flag2;
			ModelParametersExtractor.ExtractParameterNameAndLiteralFromComparisonExpression(resolvedQueryComparisonExpression, errorContext, hasTarget, mappedColumnInTarget, out text, out resolvedQueryLiteralExpression, out flag, out flag2);
			if (text == null || resolvedQueryLiteralExpression == null)
			{
				return null;
			}
			HashSet<ResolvedQueryLiteralExpression> hashSet = new HashSet<ResolvedQueryLiteralExpression>(DefaultResolvedQueryExpressionEqualityComparer.Instance) { resolvedQueryLiteralExpression };
			return new ParameterMapping(text, hashSet, flag, flag2);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000CB24 File Offset: 0x0000AD24
		private static ParameterMapping ExtractFromQueryOrExpression(ResolvedQueryOrExpression resolvedQueryOrExpression, IErrorContext errorContext, bool hasTarget, IConceptualColumn mappedColumnInTarget)
		{
			ParameterMapping parameterMapping = ModelParametersExtractor.ExtractMappingsFromExpression(resolvedQueryOrExpression.Left, errorContext, hasTarget, mappedColumnInTarget);
			ParameterMapping parameterMapping2 = ModelParametersExtractor.ExtractMappingsFromExpression(resolvedQueryOrExpression.Right, errorContext, hasTarget, mappedColumnInTarget);
			if (parameterMapping == null && parameterMapping2 == null)
			{
				return null;
			}
			if (parameterMapping != null && parameterMapping2 != null)
			{
				if (parameterMapping.ParameterName != parameterMapping2.ParameterName)
				{
					errorContext.RegisterError(QueryValidationMessages.MultipleParameterAssignmentsWithinORUnsupported, new object[0]);
				}
				parameterMapping.Values.UnionWith(parameterMapping2.Values);
				return new ParameterMapping(parameterMapping.ParameterName, parameterMapping.Values, parameterMapping.IsListType, parameterMapping.IsSelectAllFilter);
			}
			errorContext.RegisterError(QueryValidationMessages.MixOfAssignedAndUnassignedColumnsWithinORExpression, new object[0]);
			return null;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		private static ParameterMapping ExtractFromQueryInExpression(ResolvedQueryInExpression resolvedQueryInExpression, IErrorContext errorContext, bool hasTarget, IConceptualColumn mappedColumnInTarget)
		{
			IEnumerable<ResolvedQueryExpression> expressions = resolvedQueryInExpression.Expressions;
			string text = null;
			ResolvedQueryColumnExpression resolvedQueryColumnExpression = null;
			foreach (ResolvedQueryExpression resolvedQueryExpression in expressions)
			{
				resolvedQueryColumnExpression = resolvedQueryExpression as ResolvedQueryColumnExpression;
				text = ModelParametersExtractor.ExtractMappedParameter(resolvedQueryColumnExpression, errorContext);
				if (text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				if (mappedColumnInTarget != null)
				{
					errorContext.RegisterError(QueryValidationMessages.MappedTargetMustMatchMappedCondition, new object[0]);
				}
				if (MappedColumnExtractor.HasMappedParameter(resolvedQueryInExpression, errorContext))
				{
					errorContext.RegisterError(QueryValidationMessages.FoundParameterMappingOnUnsupportedFilter, new object[0]);
				}
				return null;
			}
			if (hasTarget && mappedColumnInTarget != ((resolvedQueryColumnExpression != null) ? resolvedQueryColumnExpression.Column : null))
			{
				errorContext.RegisterError(QueryValidationMessages.MappedTargetMustMatchMappedCondition, new object[0]);
			}
			if (resolvedQueryInExpression.HasTable)
			{
				errorContext.RegisterError(QueryValidationMessages.InTableConditionNotSupportedWithMappedParameters, new object[0]);
				return null;
			}
			if (resolvedQueryInExpression.Expressions.Count > 1)
			{
				errorContext.RegisterError(QueryValidationMessages.CannotUseMultiColumnFilteringWithMappedColumns, new object[0]);
				return null;
			}
			if (resolvedQueryInExpression.Values.Count > ModelParametersExtractor.MaxValuesWithinInFilter)
			{
				errorContext.RegisterError(QueryValidationMessages.TooManyInValuesForMappedParameterFilter(ModelParametersExtractor.MaxValuesWithinInFilter), new object[0]);
				return null;
			}
			HashSet<ResolvedQueryLiteralExpression> hashSet = new HashSet<ResolvedQueryLiteralExpression>(DefaultResolvedQueryExpressionEqualityComparer.Instance);
			foreach (IReadOnlyList<ResolvedQueryExpression> readOnlyList in resolvedQueryInExpression.Values)
			{
				ResolvedQueryLiteralExpression resolvedQueryLiteralExpression = readOnlyList[0] as ResolvedQueryLiteralExpression;
				if (resolvedQueryLiteralExpression == null)
				{
					errorContext.RegisterError(QueryValidationMessages.InvalidMappedParameterValueExpression, new object[0]);
					return null;
				}
				hashSet.Add(resolvedQueryLiteralExpression);
			}
			return new ParameterMapping(text, hashSet, resolvedQueryColumnExpression.Column.IsMappedParameterListType(), false);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		private static void ExtractParameterNameAndLiteralFromComparisonExpression(ResolvedQueryComparisonExpression resolvedQueryComparisonExpression, IErrorContext errorContext, bool hasTarget, IConceptualColumn mappedColumnInTarget, out string mappedParameterName, out ResolvedQueryLiteralExpression resolvedQueryLiteralExpression, out bool isListType, out bool isSelectAllFilter)
		{
			resolvedQueryLiteralExpression = null;
			isListType = false;
			isSelectAllFilter = false;
			ResolvedQueryColumnExpression resolvedQueryColumnExpression = resolvedQueryComparisonExpression.Left as ResolvedQueryColumnExpression;
			mappedParameterName = ModelParametersExtractor.ExtractMappedParameter(resolvedQueryColumnExpression, errorContext);
			if (mappedParameterName == null)
			{
				if (mappedColumnInTarget != null)
				{
					errorContext.RegisterError(QueryValidationMessages.MappedTargetMustMatchMappedCondition, new object[0]);
				}
				if (MappedColumnExtractor.HasMappedParameter(resolvedQueryComparisonExpression, errorContext))
				{
					errorContext.RegisterError(QueryValidationMessages.FoundParameterMappingOnUnsupportedFilter, new object[0]);
				}
				return;
			}
			isListType = resolvedQueryColumnExpression.Column.IsMappedParameterListType();
			if (hasTarget && mappedColumnInTarget != resolvedQueryColumnExpression.Column)
			{
				errorContext.RegisterError(QueryValidationMessages.MappedTargetMustMatchMappedCondition, new object[0]);
			}
			if (resolvedQueryComparisonExpression.ComparisonKind != QueryComparisonKind.Equal)
			{
				errorContext.RegisterError(QueryValidationMessages.FoundParameterMappingOnUnsupportedFilter, new object[0]);
				return;
			}
			if (resolvedQueryComparisonExpression.Right is ResolvedQueryAnyValueExpression)
			{
				string text = resolvedQueryColumnExpression.Column.MappedParameterSelectAllValue();
				if (!string.IsNullOrWhiteSpace(text))
				{
					isSelectAllFilter = true;
					resolvedQueryLiteralExpression = text.Literal();
				}
			}
			else
			{
				resolvedQueryLiteralExpression = resolvedQueryComparisonExpression.Right as ResolvedQueryLiteralExpression;
			}
			if (resolvedQueryLiteralExpression == null)
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidMappedParameterValueExpression, new object[0]);
				return;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000CE74 File Offset: 0x0000B074
		private static string ExtractMappedParameter(ResolvedQueryColumnExpression resolvedQueryColumnExpression, IErrorContext errorContext)
		{
			if (resolvedQueryColumnExpression == null)
			{
				return null;
			}
			IReadOnlyList<ConceptualMParameter> mappedParameter = resolvedQueryColumnExpression.Column.GetMappedParameter();
			if (mappedParameter != null && mappedParameter.Count > 1)
			{
				errorContext.RegisterError(QueryValidationMessages.MultipleParameterMappingsUnsupported, new object[0]);
				return null;
			}
			if (mappedParameter == null)
			{
				return null;
			}
			return mappedParameter.FirstOrDefault<ConceptualMParameter>().Name;
		}

		// Token: 0x040002A1 RID: 673
		internal static readonly int MaxValuesWithinInFilter = 10000;
	}
}
