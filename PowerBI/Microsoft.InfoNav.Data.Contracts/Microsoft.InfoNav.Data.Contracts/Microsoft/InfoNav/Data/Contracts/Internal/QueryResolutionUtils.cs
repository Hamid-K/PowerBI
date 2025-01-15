using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200026E RID: 622
	internal static class QueryResolutionUtils
	{
		// Token: 0x060012F6 RID: 4854 RVA: 0x00021B58 File Offset: 0x0001FD58
		internal static bool TryResolveQuerySources(List<EntitySource> from, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, HashSet<string> querySourceUsedNames, IQueryDefinitionNameRegistrar queryDefinitionNames, QueryTransformTableContext transformContext, ImmutableDictionary<string, ResolvedQueryLetBinding> letMap, IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> parameterMap, out QuerySourceContext sourceContext)
		{
			sourceContext = null;
			Dictionary<string, ResolvedQuerySource> dictionary = new Dictionary<string, ResolvedQuerySource>();
			foreach (EntitySource entitySource in from)
			{
				EntitySourceType type = entitySource.Type;
				ResolvedQuerySource resolvedQuerySource;
				if (type > EntitySourceType.Pod)
				{
					if (type != EntitySourceType.Expression)
					{
						return false;
					}
					QueryExpressionContainer expression = entitySource.Expression;
					QueryExpressionResolver queryExpressionResolver = new QueryExpressionResolver(new QuerySourceContext(new Dictionary<string, ResolvedQuerySource>(QueryNameComparer.Instance), federatedSchema), errorContext, transformContext, querySourceUsedNames, queryDefinitionNames, letMap, parameterMap);
					if (expression.Subquery != null)
					{
						queryDefinitionNames.PushName(entitySource.Name, true);
					}
					ResolvedQueryExpression resolvedQueryExpression;
					if (!QueryResolutionUtils.TryResolveQueryExpression(queryExpressionResolver, errorContext, entitySource.Expression, out resolvedQueryExpression))
					{
						errorContext.RegisterError(QueryResolutionMessages.CouldNotResolveSourceExpression(entitySource.Name));
						return false;
					}
					if (expression.Subquery != null)
					{
						queryDefinitionNames.PopName(entitySource.Name);
					}
					resolvedQuerySource = resolvedQueryExpression.ExpressionSource(entitySource.Name);
				}
				else
				{
					IConceptualSchema conceptualSchema;
					if (!federatedSchema.TryGetSchema(entitySource.Schema, out conceptualSchema))
					{
						errorContext.RegisterError(QueryResolutionMessages.CouldNotResolveSourceSchemaReference(entitySource.Schema));
						return false;
					}
					IConceptualEntity conceptualEntity;
					if (!conceptualSchema.TryGetEntity(entitySource.Entity, out conceptualEntity))
					{
						errorContext.RegisterError(QueryResolutionMessages.CouldNotResolveSourceEntityReference(entitySource.Entity));
						return false;
					}
					resolvedQuerySource = conceptualEntity.EntitySource(entitySource.Name, entitySource.Schema);
				}
				if (!querySourceUsedNames.Add(entitySource.Name))
				{
					errorContext.RegisterError(QueryResolutionMessages.DuplicateSourceName(entitySource.Name));
					return false;
				}
				dictionary.Add(entitySource.Name, resolvedQuerySource);
			}
			sourceContext = new QuerySourceContext(dictionary, federatedSchema);
			return true;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00021D20 File Offset: 0x0001FF20
		internal static bool TryResolveQueryParameterDeclarations(IReadOnlyList<QueryExpressionContainer> declarations, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, HashSet<string> querySourceUsedNames, IQueryDefinitionNameRegistrar queryDefinitionNames, IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> parameterMap, out IReadOnlyList<ResolvedQueryParameterDeclaration> resultDeclarations, out IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> resultParameterMap)
		{
			if (declarations.IsNullOrEmpty<QueryExpressionContainer>())
			{
				resultDeclarations = Util.EmptyReadOnlyCollection<ResolvedQueryParameterDeclaration>();
				resultParameterMap = parameterMap ?? Util.EmptyReadOnlyDictionary<string, ResolvedQueryParameterDeclaration>();
				return true;
			}
			if (parameterMap != null)
			{
				resultDeclarations = null;
				resultParameterMap = null;
				errorContext.RegisterError(QueryResolutionMessages.QueryParameterDeclaredOnSubquery());
				return false;
			}
			List<ResolvedQueryParameterDeclaration> list = new List<ResolvedQueryParameterDeclaration>(declarations.Count);
			resultDeclarations = list;
			Dictionary<string, ResolvedQueryParameterDeclaration> dictionary = new Dictionary<string, ResolvedQueryParameterDeclaration>(declarations.Count, QueryNameComparer.Instance);
			resultParameterMap = dictionary;
			ReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> readOnlyDictionary = Util.EmptyReadOnlyDictionary<string, ResolvedQueryParameterDeclaration>();
			ImmutableDictionary<string, ResolvedQueryLetBinding> immutableDictionary = QueryResolutionUtils.CreateEmptyLetMap();
			QueryTransformTableContext queryTransformTableContext = new QueryTransformTableContext();
			QueryExpressionResolver queryExpressionResolver = new QueryExpressionResolver(new QuerySourceContext(new Dictionary<string, ResolvedQuerySource>(QueryNameComparer.Instance), federatedSchema), errorContext, queryTransformTableContext, querySourceUsedNames, queryDefinitionNames, immutableDictionary, readOnlyDictionary);
			foreach (QueryExpressionContainer queryExpressionContainer in declarations)
			{
				if (resultParameterMap.ContainsKey(queryExpressionContainer.Name))
				{
					errorContext.RegisterError(QueryResolutionMessages.DuplicateParameterName(queryExpressionContainer.Name));
					return false;
				}
				ResolvedQueryExpression resolvedQueryExpression;
				if (!QueryResolutionUtils.TryResolveQueryExpression(queryExpressionResolver, errorContext, queryExpressionContainer.Expression, out resolvedQueryExpression))
				{
					errorContext.RegisterError(QueryResolutionMessages.CouldNotResolveParameterExpression(queryExpressionContainer.Name));
					return false;
				}
				ResolvedQueryParameterDeclaration resolvedQueryParameterDeclaration = new ResolvedQueryParameterDeclaration(queryExpressionContainer.Name, resolvedQueryExpression);
				list.Add(resolvedQueryParameterDeclaration);
				dictionary.Add(resolvedQueryParameterDeclaration.Name, resolvedQueryParameterDeclaration);
			}
			return true;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00021E80 File Offset: 0x00020080
		internal static bool TryResolveLetBindings(IReadOnlyList<QueryExpressionContainer> bindings, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, HashSet<string> querySourceUsedNames, IQueryDefinitionNameRegistrar queryDefinitionNames, ImmutableDictionary<string, ResolvedQueryLetBinding> letMap, IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> parameterMap, out IReadOnlyList<ResolvedQueryLetBinding> resultBindings, out ImmutableDictionary<string, ResolvedQueryLetBinding> resultLetMap)
		{
			resultLetMap = letMap;
			if (bindings.IsNullOrEmpty<QueryExpressionContainer>())
			{
				resultBindings = Util.EmptyReadOnlyCollection<ResolvedQueryLetBinding>();
				return true;
			}
			List<ResolvedQueryLetBinding> list = new List<ResolvedQueryLetBinding>(bindings.Count);
			resultBindings = list;
			foreach (QueryExpressionContainer queryExpressionContainer in bindings)
			{
				if (resultLetMap.ContainsKey(queryExpressionContainer.Name))
				{
					errorContext.RegisterError(QueryResolutionMessages.DuplicateLetName(queryExpressionContainer.Name));
					return false;
				}
				resultLetMap = resultLetMap.Add(queryExpressionContainer.Name, null);
				QueryTransformTableContext queryTransformTableContext = new QueryTransformTableContext();
				QueryExpressionResolver queryExpressionResolver = new QueryExpressionResolver(new QuerySourceContext(new Dictionary<string, ResolvedQuerySource>(QueryNameComparer.Instance), federatedSchema), errorContext, queryTransformTableContext, querySourceUsedNames, queryDefinitionNames, resultLetMap, parameterMap);
				queryDefinitionNames.PushName(queryExpressionContainer.Name, false);
				ResolvedQueryExpression resolvedQueryExpression;
				if (!QueryResolutionUtils.TryResolveQueryExpression(queryExpressionResolver, errorContext, queryExpressionContainer.Expression, out resolvedQueryExpression))
				{
					errorContext.RegisterError(QueryResolutionMessages.CouldNotResolveLetExpression(queryExpressionContainer.Name));
					return false;
				}
				queryDefinitionNames.PopName(queryExpressionContainer.Name);
				ResolvedQueryLetBinding resolvedQueryLetBinding = new ResolvedQueryLetBinding(queryExpressionContainer.Name, resolvedQueryExpression);
				list.Add(resolvedQueryLetBinding);
				resultLetMap = resultLetMap.SetItem(queryExpressionContainer.Name, resolvedQueryLetBinding);
			}
			return true;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00021FC4 File Offset: 0x000201C4
		internal static ImmutableDictionary<string, ResolvedQueryLetBinding> CreateEmptyLetMap()
		{
			return ImmutableDictionary<string, ResolvedQueryLetBinding>.Empty.WithComparers(QueryNameComparer.Instance, ReferenceEqualityComparer<ResolvedQueryLetBinding>.Instance);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00021FDA File Offset: 0x000201DA
		internal static void EnsureQueryDefinitionResolverContract(QueryResolutionErrorContext errorContext, bool resolutionSucceeded)
		{
			if (!resolutionSucceeded && !errorContext.HasError)
			{
				errorContext.RegisterError(QueryResolutionMessages.CouldNotResolveSemanticQueryDefinition());
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00021FF4 File Offset: 0x000201F4
		internal static bool TryResolveEach<InputType, OutputType>(IEnumerable<InputType> inputList, QueryResolutionUtils.TryResolveItem<InputType, OutputType> tryResolve, out List<OutputType> outputList)
		{
			bool flag = true;
			outputList = new List<OutputType>();
			foreach (InputType inputType in inputList)
			{
				OutputType outputType;
				if (tryResolve(inputType, out outputType))
				{
					outputList.Add(outputType);
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00022058 File Offset: 0x00020258
		internal static bool TryResolveEach<InputType, TArg, OutputType>(IEnumerable<InputType> inputList, TArg arg, QueryResolutionUtils.TryResolveItem<InputType, TArg, OutputType> tryResolve, out List<OutputType> outputList)
		{
			bool flag = true;
			outputList = new List<OutputType>();
			foreach (InputType inputType in inputList)
			{
				OutputType outputType;
				if (tryResolve(inputType, arg, out outputType))
				{
					outputList.Add(outputType);
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000220BC File Offset: 0x000202BC
		internal static bool TryResolveQueryExpression(QueryExpressionResolver queryExpressionResolver, QueryResolutionErrorContext errorContext, QueryExpressionContainer expressionContainer, out ResolvedQueryExpression resolvedExpression)
		{
			try
			{
				resolvedExpression = expressionContainer.Expression.Accept<ResolvedQueryExpression>(queryExpressionResolver);
			}
			catch (QueryResolutionException)
			{
				resolvedExpression = null;
				return false;
			}
			return !errorContext.HasError;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000220FC File Offset: 0x000202FC
		internal static bool TryResolveStandaloneExpression(QueryExpressionContainer expression, IConceptualSchema schema, IErrorContext errorContext, out ResolvedQueryExpression resolvedExpression)
		{
			QueryResolutionErrorContext queryResolutionErrorContext = new QueryResolutionErrorContext(errorContext);
			QueryExpressionResolver queryExpressionResolver = new QueryExpressionResolver(new QuerySourceContext(Util.EmptyReadOnlyDictionary<string, ResolvedQuerySource>(), schema.ToFederatedSchema()), queryResolutionErrorContext, null, new HashSet<string>(QueryNameComparer.Instance), NoOpQueryDefinitionNameRegistrar.Instance, QueryResolutionUtils.CreateEmptyLetMap(), Util.EmptyReadOnlyDictionary<string, ResolvedQueryParameterDeclaration>());
			try
			{
				resolvedExpression = expression.Expression.Accept<ResolvedQueryExpression>(queryExpressionResolver);
			}
			catch (QueryResolutionException)
			{
				resolvedExpression = null;
				return false;
			}
			return resolvedExpression != null && !errorContext.HasError;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00022180 File Offset: 0x00020380
		internal static bool TryValidateSelectNames(QueryResolutionErrorContext errorContext, IReadOnlyList<ResolvedQuerySelect> selects, out IReadOnlyList<ResolvedQuerySelect> repairedSelects)
		{
			HashSet<string> hashSet = new HashSet<string>(QueryNameComparer.Instance);
			HashSet<string> hashSet2 = new HashSet<string>(QueryNameComparer.Instance);
			bool flag = true;
			bool flag2 = false;
			foreach (ResolvedQuerySelect resolvedQuerySelect in selects)
			{
				if (!string.IsNullOrEmpty(resolvedQuerySelect.Name) && !hashSet.Add(resolvedQuerySelect.Name))
				{
					errorContext.RegisterWarning(QueryResolutionMessages.DuplicateExpressionName(resolvedQuerySelect.Name));
					flag = false;
				}
				if (!string.IsNullOrEmpty(resolvedQuerySelect.NativeReferenceName) && !hashSet2.Add(resolvedQuerySelect.NativeReferenceName))
				{
					errorContext.RegisterError(QueryResolutionMessages.DuplicateNativeReferenceName(resolvedQuerySelect.NativeReferenceName));
					flag2 = true;
				}
			}
			if (flag2)
			{
				repairedSelects = null;
				return false;
			}
			IReadOnlyList<ResolvedQuerySelect> readOnlyList2;
			if (!flag)
			{
				IReadOnlyList<ResolvedQuerySelect> readOnlyList = selects.Select((ResolvedQuerySelect select) => new ResolvedQuerySelect(select.Expression, null, select.NativeReferenceName)).ToList<ResolvedQuerySelect>();
				readOnlyList2 = readOnlyList;
			}
			else
			{
				readOnlyList2 = selects;
			}
			repairedSelects = readOnlyList2;
			return true;
		}

		// Token: 0x0200033E RID: 830
		// (Invoke) Token: 0x06001A1F RID: 6687
		internal delegate bool TryResolveItem<I, O>(I input, out O output);

		// Token: 0x0200033F RID: 831
		// (Invoke) Token: 0x06001A23 RID: 6691
		internal delegate bool TryResolveItem<I, TArg, O>(I input, TArg arg, out O output);
	}
}
