using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeBindingResolution
{
	// Token: 0x02000121 RID: 289
	public sealed class DataShapeBindingResolver
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0000F72F File Offset: 0x0000D92F
		public DataShapeBindingResolver(IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext)
		{
			this._federatedSchema = federatedSchema;
			this._errorContext = errorContext;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000F745 File Offset: 0x0000D945
		public static bool TryResolve(DataShapeBinding binding, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, string rootQueryName, out ResolvedDataShapeBinding resolvedBinding)
		{
			return new DataShapeBindingResolver(federatedSchema, errorContext).TryResolve(binding, out resolvedBinding);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000F758 File Offset: 0x0000D958
		private bool TryResolve(DataShapeBinding binding, out ResolvedDataShapeBinding resolvedBinding)
		{
			ResolvedDataReduction resolvedDataReduction;
			if (!this.TryResolve(binding.DataReduction, out resolvedDataReduction))
			{
				resolvedBinding = null;
				return false;
			}
			resolvedBinding = new ResolvedDataShapeBinding(resolvedDataReduction);
			return true;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000F784 File Offset: 0x0000D984
		private bool TryResolve(DataReduction dataReduction, out ResolvedDataReduction resolvedDataReduction)
		{
			if (dataReduction == null)
			{
				resolvedDataReduction = null;
				return true;
			}
			ResolvedDataReductionLimit resolvedDataReductionLimit;
			ResolvedDataReductionLimit resolvedDataReductionLimit2;
			ResolvedDataReductionLimit resolvedDataReductionLimit3;
			List<ResolvedScopedDataReduction> list;
			if (!this.TryResolve(dataReduction.Primary, out resolvedDataReductionLimit) | !this.TryResolve(dataReduction.Secondary, out resolvedDataReductionLimit2) | !this.TryResolve(dataReduction.Intersection, out resolvedDataReductionLimit3) | !this.TryResolveList<ScopedDataReduction, ResolvedScopedDataReduction>(dataReduction.Scoped, new QueryResolutionUtils.TryResolveItem<ScopedDataReduction, ResolvedScopedDataReduction>(this.TryResolve), out list))
			{
				resolvedDataReduction = null;
				return false;
			}
			resolvedDataReduction = new ResolvedDataReduction(dataReduction.DataVolume, resolvedDataReductionLimit, resolvedDataReductionLimit2, resolvedDataReductionLimit3, list);
			return true;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000F80C File Offset: 0x0000DA0C
		private bool TryResolve(DataReductionAlgorithm algorithm, out ResolvedDataReductionLimit resolvedAlgorithm)
		{
			if (algorithm == null)
			{
				resolvedAlgorithm = null;
				return true;
			}
			if (algorithm.Top != null)
			{
				return this.TryResolve(algorithm.Top, out resolvedAlgorithm);
			}
			if (algorithm.Sample != null)
			{
				return this.TryResolve(algorithm.Sample, out resolvedAlgorithm);
			}
			if (algorithm.Bottom != null)
			{
				return this.TryResolve(algorithm.Bottom, out resolvedAlgorithm);
			}
			if (algorithm.Window != null)
			{
				return this.TryResolve(algorithm.Window, out resolvedAlgorithm);
			}
			if (algorithm.BinnedLineSample != null)
			{
				return this.TryResolve(algorithm.BinnedLineSample, out resolvedAlgorithm);
			}
			if (algorithm.OverlappingPointsSample != null)
			{
				return this.TryResolve(algorithm.OverlappingPointsSample, out resolvedAlgorithm);
			}
			if (algorithm.TopNPerLevel != null)
			{
				return this.TryResolve(algorithm.TopNPerLevel, out resolvedAlgorithm);
			}
			throw new InvalidOperationException("Unknown DataReductionAlgorithm");
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		private bool TryResolve(ScopedDataReduction scoped, out ResolvedScopedDataReduction resolvedScoped)
		{
			ResolvedDataReductionScope resolvedDataReductionScope;
			ResolvedDataReductionLimit resolvedDataReductionLimit;
			if (!this.TryResolve(scoped.Scope, out resolvedDataReductionScope) | !this.TryResolve(scoped.Algorithm, out resolvedDataReductionLimit))
			{
				resolvedScoped = null;
				return false;
			}
			resolvedScoped = new ResolvedScopedDataReduction(resolvedDataReductionScope, resolvedDataReductionLimit);
			return true;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000F939 File Offset: 0x0000DB39
		private bool TryResolve(DataReductionScope scope, out ResolvedDataReductionScope resolvedScope)
		{
			resolvedScope = new ResolvedDataReductionScope(scope.Primary.AsReadOnlyList<int>(), scope.Secondary.AsReadOnlyList<int>());
			return true;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000F959 File Offset: 0x0000DB59
		private bool TryResolve(DataReductionTopLimit limit, out ResolvedDataReductionLimit resolvedLimit)
		{
			resolvedLimit = new ResolvedDataReductionTopLimit(limit.Count);
			return true;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000F969 File Offset: 0x0000DB69
		private bool TryResolve(DataReductionSampleLimit limit, out ResolvedDataReductionLimit resolvedLimit)
		{
			resolvedLimit = new ResolvedDataReductionSampleLimit(limit.Count);
			return true;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000F979 File Offset: 0x0000DB79
		private bool TryResolve(DataReductionBottomLimit limit, out ResolvedDataReductionLimit resolvedLimit)
		{
			resolvedLimit = new ResolvedDataReductionBottomLimit(limit.Count);
			return true;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000F98C File Offset: 0x0000DB8C
		private bool TryResolve(DataReductionDataWindow limit, out ResolvedDataReductionLimit resolvedLimit)
		{
			IReadOnlyList<IReadOnlyList<PrimitiveValue>> readOnlyList;
			if (!this.TryResolveRestartTokens(limit.RestartTokens, out readOnlyList))
			{
				resolvedLimit = null;
				return false;
			}
			resolvedLimit = new ResolvedDataReductionDataWindow(limit.Count, readOnlyList, limit.RestartMatchingBehavior);
			return true;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		internal bool TryResolveRestartTokens(IList<IList<string>> stringTokens, out IReadOnlyList<IReadOnlyList<PrimitiveValue>> resolvedTokens)
		{
			if (stringTokens.IsNullOrEmptyCollection<IList<string>>())
			{
				resolvedTokens = null;
				return true;
			}
			List<IReadOnlyList<PrimitiveValue>> list = new List<IReadOnlyList<PrimitiveValue>>(stringTokens.Count);
			foreach (IList<string> list2 in stringTokens)
			{
				IReadOnlyList<PrimitiveValue> readOnlyList;
				if (!this.TryParseRestartToken(list2, out readOnlyList))
				{
					this._errorContext.RegisterWarning(DataShapeBindingResolutionMessages.CouldNotParseRestartToken());
					resolvedTokens = null;
					return true;
				}
				list.Add(readOnlyList);
			}
			resolvedTokens = list;
			return true;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000FA50 File Offset: 0x0000DC50
		internal bool TryParseRestartToken(IList<string> stringToken, out IReadOnlyList<PrimitiveValue> resolvedToken)
		{
			List<PrimitiveValue> list = new List<PrimitiveValue>(stringToken.Count);
			foreach (string text in stringToken)
			{
				PrimitiveValue @null;
				if (text == null)
				{
					@null = PrimitiveValue.Null;
				}
				else if (!PrimitiveValueEncoding.TryParseTypeEncodedString(text, out @null))
				{
					resolvedToken = null;
					return false;
				}
				list.Add(@null);
			}
			resolvedToken = list;
			return true;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		private bool TryResolve(DataReductionBinnedLineSampleLimit limit, out ResolvedDataReductionLimit resolvedLimit)
		{
			resolvedLimit = new ResolvedDataReductionBinnedLineSampleLimit(limit.Count, limit.MinPointsPerSeries, limit.MaxDynamicSeriesCount, limit.PrimaryScalarKey, limit.WarningCount);
			return true;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		private bool TryResolve(DataReductionOverlappingPointsSampleLimit limit, out ResolvedDataReductionLimit resolvedLimit)
		{
			ResolvedDataReductionPlotAxisBinding resolvedDataReductionPlotAxisBinding;
			ResolvedDataReductionPlotAxisBinding resolvedDataReductionPlotAxisBinding2;
			if (!this.TryResolve(limit.X, out resolvedDataReductionPlotAxisBinding) | !this.TryResolve(limit.Y, out resolvedDataReductionPlotAxisBinding2))
			{
				resolvedLimit = null;
				return false;
			}
			resolvedLimit = new ResolvedDataReductionOverlappingPointsSampleLimit(limit.Count, resolvedDataReductionPlotAxisBinding, resolvedDataReductionPlotAxisBinding2);
			return true;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000FB37 File Offset: 0x0000DD37
		private bool TryResolve(DataReductionPlotAxisBinding plotAxis, out ResolvedDataReductionPlotAxisBinding resolvedPlotAxis)
		{
			if (plotAxis == null)
			{
				resolvedPlotAxis = null;
				return true;
			}
			resolvedPlotAxis = new ResolvedDataReductionPlotAxisBinding(plotAxis.Index, plotAxis.Transform);
			return true;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		private bool TryResolve(DataReductionTopNPerLevelSampleLimit limit, out ResolvedDataReductionLimit resolvedLimit)
		{
			ResolvedDataReductionWindowExpansionState resolvedDataReductionWindowExpansionState;
			if (!this.TryResolve(limit.WindowExpansion, out resolvedDataReductionWindowExpansionState))
			{
				resolvedLimit = null;
				return false;
			}
			resolvedLimit = new ResolvedDataReductionTopNPerLevelSampleLimit(limit.Count, resolvedDataReductionWindowExpansionState);
			return true;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000FB90 File Offset: 0x0000DD90
		private bool TryResolve(DataReductionWindowExpansionState state, out ResolvedDataReductionWindowExpansionState resolvedState)
		{
			if (state == null)
			{
				resolvedState = null;
				return true;
			}
			IReadOnlyList<ResolvedQuerySource> readOnlyList;
			QueryExpressionResolver queryExpressionResolver;
			List<ResolvedDataShapeBindingAxisExpansionLevel> list;
			ResolvedDataReductionWindowExpansionInstance resolvedDataReductionWindowExpansionInstance;
			if (!this.TryResolveSources(state.From, out readOnlyList, out queryExpressionResolver) || !this.TryResolveList<DataShapeBindingAxisExpansionLevel, QueryExpressionResolver, ResolvedDataShapeBindingAxisExpansionLevel>(state.Levels, queryExpressionResolver, new QueryResolutionUtils.TryResolveItem<DataShapeBindingAxisExpansionLevel, QueryExpressionResolver, ResolvedDataShapeBindingAxisExpansionLevel>(this.TryResolve), out list) || !this.TryResolve(state.WindowInstances, queryExpressionResolver, out resolvedDataReductionWindowExpansionInstance))
			{
				resolvedState = null;
				return false;
			}
			resolvedState = new ResolvedDataReductionWindowExpansionState(readOnlyList, list, resolvedDataReductionWindowExpansionInstance);
			return true;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		private bool TryResolve(DataShapeBindingAxisExpansionLevel level, QueryExpressionResolver expressionResolver, out ResolvedDataShapeBindingAxisExpansionLevel resolvedLevel)
		{
			List<ResolvedQueryExpression> list;
			if (!this.TryResolveExpressionList(level.Expressions, expressionResolver, out list))
			{
				resolvedLevel = null;
				return false;
			}
			resolvedLevel = new ResolvedDataShapeBindingAxisExpansionLevel(list, level.Default);
			return true;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000FC30 File Offset: 0x0000DE30
		private bool TryResolve(DataReductionWindowExpansionInstance instance, QueryExpressionResolver expressionResolver, out ResolvedDataReductionWindowExpansionInstance resolvedInstance)
		{
			if (instance == null)
			{
				resolvedInstance = null;
				return true;
			}
			List<ResolvedQueryExpression> list;
			List<ResolvedDataReductionWindowExpansionInstance> list2;
			List<ResolvedDataReductionWindowExpansionInstanceValue> list3;
			if (!this.TryResolveExpressionList(instance.Values, expressionResolver, out list) || !this.TryResolveList<DataReductionWindowExpansionInstance, QueryExpressionResolver, ResolvedDataReductionWindowExpansionInstance>(instance.Children, expressionResolver, new QueryResolutionUtils.TryResolveItem<DataReductionWindowExpansionInstance, QueryExpressionResolver, ResolvedDataReductionWindowExpansionInstance>(this.TryResolve), out list2) || !this.TryResolveList<DataReductionWindowExpansionInstanceValue, QueryExpressionResolver, ResolvedDataReductionWindowExpansionInstanceValue>(instance.WindowExpansionInstanceWindowValue, expressionResolver, new QueryResolutionUtils.TryResolveItem<DataReductionWindowExpansionInstanceValue, QueryExpressionResolver, ResolvedDataReductionWindowExpansionInstanceValue>(this.TryResolve), out list3))
			{
				resolvedInstance = null;
				return false;
			}
			resolvedInstance = new ResolvedDataReductionWindowExpansionInstance(list, list2, list3);
			return true;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		private bool TryResolve(DataReductionWindowExpansionInstanceValue value, QueryExpressionResolver expressionResolver, out ResolvedDataReductionWindowExpansionInstanceValue resolvedValue)
		{
			List<ResolvedQueryExpression> list;
			if (!this.TryResolveExpressionList(value.Values, expressionResolver, out list))
			{
				resolvedValue = null;
				return false;
			}
			resolvedValue = new ResolvedDataReductionWindowExpansionInstanceValue(list, value.WindowStartKind);
			return true;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000FCDA File Offset: 0x0000DEDA
		private bool TryResolveExpression(QueryExpressionContainer expression, QueryExpressionResolver resolver, out ResolvedQueryExpression resolvedExpression)
		{
			return QueryResolutionUtils.TryResolveQueryExpression(resolver, this._errorContext, expression, out resolvedExpression);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		private bool TryResolveSources(List<EntitySource> entitySources, out IReadOnlyList<ResolvedQuerySource> resolvedSources, out QueryExpressionResolver expressionResolver)
		{
			ReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> readOnlyDictionary = Util.EmptyReadOnlyDictionary<string, ResolvedQueryParameterDeclaration>();
			ImmutableDictionary<string, ResolvedQueryLetBinding> empty = ImmutableDictionary<string, ResolvedQueryLetBinding>.Empty;
			QueryTransformTableContext queryTransformTableContext = new QueryTransformTableContext();
			QueryDefinitionNameRegistrar queryDefinitionNameRegistrar = new QueryDefinitionNameRegistrar();
			HashSet<string> hashSet = new HashSet<string>(QueryNameComparer.Instance);
			QuerySourceContext querySourceContext;
			if (!QueryResolutionUtils.TryResolveQuerySources(entitySources, this._federatedSchema, this._errorContext, hashSet, queryDefinitionNameRegistrar, queryTransformTableContext, empty, readOnlyDictionary, out querySourceContext))
			{
				resolvedSources = null;
				expressionResolver = null;
				return false;
			}
			resolvedSources = querySourceContext.SourceMap.Values.ToList<ResolvedQuerySource>();
			expressionResolver = new QueryExpressionResolver(querySourceContext, this._errorContext, queryTransformTableContext, hashSet, queryDefinitionNameRegistrar, empty, readOnlyDictionary);
			return true;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000FD6A File Offset: 0x0000DF6A
		private bool TryResolveExpressionList(IList<QueryExpressionContainer> expressions, QueryExpressionResolver resolver, out List<ResolvedQueryExpression> resolvedExpressions)
		{
			return this.TryResolveList<QueryExpressionContainer, QueryExpressionResolver, ResolvedQueryExpression>(expressions, resolver, new QueryResolutionUtils.TryResolveItem<QueryExpressionContainer, QueryExpressionResolver, ResolvedQueryExpression>(this.TryResolveExpression), out resolvedExpressions);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000FD81 File Offset: 0x0000DF81
		private bool TryResolveList<TInput, TOutput>(IList<TInput> input, QueryResolutionUtils.TryResolveItem<TInput, TOutput> resolver, out List<TOutput> output)
		{
			if (input == null)
			{
				output = null;
				return true;
			}
			return QueryResolutionUtils.TryResolveEach<TInput, TOutput>(input, resolver, out output);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000FD93 File Offset: 0x0000DF93
		private bool TryResolveList<TInput, TArg, TOutput>(IList<TInput> input, TArg arg, QueryResolutionUtils.TryResolveItem<TInput, TArg, TOutput> resolver, out List<TOutput> output)
		{
			if (input == null)
			{
				output = null;
				return true;
			}
			return QueryResolutionUtils.TryResolveEach<TInput, TArg, TOutput>(input, arg, resolver, out output);
		}

		// Token: 0x0400037A RID: 890
		private readonly IFederatedConceptualSchema _federatedSchema;

		// Token: 0x0400037B RID: 891
		private readonly QueryResolutionErrorContext _errorContext;
	}
}
