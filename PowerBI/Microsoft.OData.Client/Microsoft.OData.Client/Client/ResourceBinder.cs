using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Client.ALinq.UriParser;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A3 RID: 163
	internal class ResourceBinder : DataServiceALinqExpressionVisitor
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000135AB File Offset: 0x000117AB
		private ClientEdmModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000135B8 File Offset: 0x000117B8
		private ResourceBinder(DataServiceContext context)
		{
			this.context = context;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000135C8 File Offset: 0x000117C8
		internal static Expression Bind(Expression e, DataServiceContext context)
		{
			ResourceBinder resourceBinder = new ResourceBinder(context);
			Expression expression = resourceBinder.Visit(e);
			ResourceBinder.VerifyKeyPredicates(expression);
			ResourceBinder.VerifyNotSelectManyProjection(expression);
			return expression;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000135F4 File Offset: 0x000117F4
		internal static bool IsMissingKeyPredicates(Expression expression)
		{
			ResourceExpression resourceExpression = expression as ResourceExpression;
			if (resourceExpression != null)
			{
				if (ResourceBinder.IsMissingKeyPredicates(resourceExpression.Source))
				{
					return true;
				}
				if (resourceExpression.Source != null && !resourceExpression.IsOperationInvocation)
				{
					ResourceSetExpression resourceSetExpression = resourceExpression.Source as ResourceSetExpression;
					if (resourceSetExpression != null && !resourceSetExpression.HasKeyPredicate)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013644 File Offset: 0x00011844
		internal static void VerifyKeyPredicates(Expression e)
		{
			if (ResourceBinder.IsMissingKeyPredicates(e))
			{
				throw new NotSupportedException(Strings.ALinq_CantNavigateWithoutKeyPredicate);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001365C File Offset: 0x0001185C
		internal static void VerifyNotSelectManyProjection(Expression expression)
		{
			QueryableResourceExpression queryableResourceExpression = expression as QueryableResourceExpression;
			if (queryableResourceExpression != null)
			{
				ProjectionQueryOptionExpression projection = queryableResourceExpression.Projection;
				if (projection != null)
				{
					MethodCallExpression methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(projection.Selector.Body);
					if (methodCallExpression != null && methodCallExpression.Method.Name == "SelectMany")
					{
						throw new NotSupportedException(Strings.ALinq_UnsupportedExpression(methodCallExpression));
					}
				}
				else if (queryableResourceExpression.HasTransparentScope)
				{
					throw new NotSupportedException(Strings.ALinq_UnsupportedExpression(queryableResourceExpression));
				}
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000136C8 File Offset: 0x000118C8
		private static Expression AnalyzePredicate(MethodCallExpression mce, ClientEdmModel model)
		{
			QueryableResourceExpression queryableResourceExpression;
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.TryGetResourceSetMethodArguments(mce, out queryableResourceExpression, out lambdaExpression))
			{
				ResourceBinder.ValidationRules.RequireNonSingleton(mce.Arguments[0]);
				return mce;
			}
			ResourceBinder.ValidationRules.CheckPredicate(lambdaExpression.Body, model);
			List<Expression> list = new List<Expression>();
			ResourceBinder.AddConjuncts(lambdaExpression.Body, list);
			Dictionary<QueryableResourceExpression, List<Expression>> dictionary = new Dictionary<QueryableResourceExpression, List<Expression>>(ReferenceEqualityComparer<QueryableResourceExpression>.Instance);
			List<ResourceExpression> list2 = new List<ResourceExpression>();
			foreach (Expression expression in list)
			{
				ResourceBinder.ValidateFilter(expression, mce.Method.Name);
				Expression expression2 = InputBinder.Bind(expression, queryableResourceExpression, lambdaExpression.Parameters[0], list2);
				if (list2.Count > 1)
				{
					return mce;
				}
				QueryableResourceExpression queryableResourceExpression2 = ((list2.Count == 0) ? queryableResourceExpression : (list2[0] as QueryableResourceExpression));
				if (queryableResourceExpression2 == null)
				{
					return mce;
				}
				List<Expression> list3 = null;
				if (!dictionary.TryGetValue(queryableResourceExpression2, out list3))
				{
					list3 = new List<Expression>();
					dictionary[queryableResourceExpression2] = list3;
				}
				list3.Add(expression2);
				list2.Clear();
			}
			List<Expression> list4;
			if (dictionary.TryGetValue(queryableResourceExpression, out list4))
			{
				dictionary.Remove(queryableResourceExpression);
			}
			else
			{
				list4 = null;
			}
			foreach (KeyValuePair<QueryableResourceExpression, List<Expression>> keyValuePair in dictionary)
			{
				QueryableResourceExpression key = keyValuePair.Key;
				List<Expression> value = keyValuePair.Value;
				List<Expression> list6;
				List<Expression> list5 = ResourceBinder.ExtractKeyPredicate(key, value, model, out list6);
				if (list5 == null || (list6 != null && list6.Count > 0))
				{
					return mce;
				}
				key.SetKeyPredicate(list5);
			}
			if (list4 != null)
			{
				List<Expression> list7 = null;
				List<Expression> list8 = null;
				List<Expression> list9 = queryableResourceExpression.KeyPredicateConjuncts.ToList<Expression>();
				if (queryableResourceExpression.Filter != null && queryableResourceExpression.Filter.PredicateConjuncts.Count > 0)
				{
					list9 = list9.Union(queryableResourceExpression.Filter.PredicateConjuncts.Union(list4)).ToList<Expression>();
				}
				else
				{
					list9 = list9.Union(list4).ToList<Expression>();
				}
				if (!queryableResourceExpression.UseFilterAsPredicate)
				{
					list7 = ResourceBinder.ExtractKeyPredicate(queryableResourceExpression, list9, model, out list8);
				}
				if (list7 != null)
				{
					queryableResourceExpression.SetKeyPredicate(list7);
					queryableResourceExpression.RemoveFilterExpression();
				}
				if (list7 != null && queryableResourceExpression.HasSequenceQueryOptions)
				{
					queryableResourceExpression.ConvertKeyToFilterExpression();
				}
				if (list7 == null)
				{
					queryableResourceExpression.ConvertKeyToFilterExpression();
					queryableResourceExpression.AddFilter(list4);
				}
			}
			return queryableResourceExpression;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00013944 File Offset: 0x00011B44
		private static void ValidateFilter(Expression exp, string method)
		{
			BinaryExpression binaryExpression = ResourceBinder.StripTo<BinaryExpression>(exp);
			if (binaryExpression != null)
			{
				ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(binaryExpression.Left, method);
				ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(binaryExpression.Right, method);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013974 File Offset: 0x00011B74
		private static List<Expression> ExtractKeyPredicate(QueryableResourceExpression target, List<Expression> predicates, ClientEdmModel edmModel, out List<Expression> nonKeyPredicates)
		{
			Dictionary<PropertyInfo, ConstantExpression> dictionary = null;
			nonKeyPredicates = null;
			List<Expression> list = null;
			foreach (Expression expression in predicates)
			{
				PropertyInfo propertyInfo;
				ConstantExpression constantExpression;
				if (ResourceBinder.PatternRules.MatchKeyComparison(expression, out propertyInfo, out constantExpression))
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<PropertyInfo, ConstantExpression>(EqualityComparer<PropertyInfo>.Default);
						list = new List<Expression>();
					}
					else if (dictionary.ContainsKey(propertyInfo))
					{
						throw Error.NotSupported(Strings.ALinq_CanOnlyApplyOneKeyPredicate);
					}
					dictionary.Add(propertyInfo, constantExpression);
					list.Add(expression);
				}
				else
				{
					if (nonKeyPredicates == null)
					{
						nonKeyPredicates = new List<Expression>();
					}
					nonKeyPredicates.Add(expression);
				}
			}
			if (nonKeyPredicates != null && nonKeyPredicates.Count > 0)
			{
				target.UseFilterAsPredicate = true;
				list = null;
			}
			else if (dictionary != null)
			{
				IEdmEntityType edmEntityType = (IEdmEntityType)edmModel.GetOrCreateEdmType(target.CreateReference().Type);
				if (edmEntityType.Key().Count<IEdmStructuralProperty>() != dictionary.Keys.Count)
				{
					dictionary = null;
					list = null;
				}
			}
			return list;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013A78 File Offset: 0x00011C78
		private static void AddConjuncts(Expression e, List<Expression> conjuncts)
		{
			if (ResourceBinder.PatternRules.MatchAnd(e))
			{
				BinaryExpression binaryExpression = (BinaryExpression)e;
				ResourceBinder.AddConjuncts(binaryExpression.Left, conjuncts);
				ResourceBinder.AddConjuncts(binaryExpression.Right, conjuncts);
				return;
			}
			conjuncts.Add(e);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013AB4 File Offset: 0x00011CB4
		internal bool AnalyzeProjection(MethodCallExpression mce, SequenceMethod sequenceMethod, out Expression e)
		{
			e = mce;
			bool flag = sequenceMethod == SequenceMethod.SelectManyResultSelector;
			ResourceExpression resourceExpression = this.Visit(mce.Arguments[0]) as ResourceExpression;
			if (resourceExpression == null)
			{
				return false;
			}
			if (sequenceMethod == SequenceMethod.SelectManyResultSelector)
			{
				Expression expression = mce.Arguments[1];
				if (!ResourceBinder.PatternRules.MatchParameterMemberAccess(expression))
				{
					return false;
				}
				Expression expression2 = mce.Arguments[2];
				LambdaExpression lambdaExpression;
				if (!ResourceBinder.PatternRules.MatchDoubleArgumentLambda(expression2, out lambdaExpression))
				{
					return false;
				}
				if (ResourceBinder.ExpressionPresenceVisitor.IsExpressionPresent(lambdaExpression.Parameters[0], lambdaExpression.Body))
				{
					return false;
				}
				List<ResourceExpression> list = new List<ResourceExpression>();
				LambdaExpression lambdaExpression2 = ResourceBinder.StripTo<LambdaExpression>(expression);
				Expression expression3 = InputBinder.Bind(lambdaExpression2.Body, resourceExpression, lambdaExpression2.Parameters[0], list);
				expression3 = ResourceBinder.StripCastMethodCalls(expression3);
				MemberExpression memberExpression;
				if (!ResourceBinder.PatternRules.MatchPropertyProjectionRelatedSet(resourceExpression, expression3, this.context, out memberExpression))
				{
					return false;
				}
				ResourceExpression resourceExpression2 = ResourceBinder.CreateResourceSetExpression(mce.Method.ReturnType, resourceExpression, memberExpression, TypeSystem.GetElementType(memberExpression.Type));
				if (!ResourceBinder.PatternRules.MatchMemberInitExpressionWithDefaultConstructor(resourceExpression2, lambdaExpression) && !ResourceBinder.PatternRules.MatchNewExpression(resourceExpression2, lambdaExpression))
				{
					return false;
				}
				lambdaExpression = Expression.Lambda(lambdaExpression.Body, new ParameterExpression[] { lambdaExpression.Parameters[1] });
				ResourceExpression resourceExpression3 = resourceExpression2.CreateCloneWithNewType(mce.Type);
				bool flag2;
				try
				{
					flag2 = ProjectionAnalyzer.Analyze(lambdaExpression, resourceExpression3, false, this.context);
				}
				catch (NotSupportedException)
				{
					flag2 = false;
				}
				if (!flag2)
				{
					return false;
				}
				e = resourceExpression3;
				ResourceBinder.ValidationRules.RequireCanProject(resourceExpression2);
			}
			else
			{
				LambdaExpression lambdaExpression3;
				if (!ResourceBinder.PatternRules.MatchSingleArgumentLambda(mce.Arguments[1], out lambdaExpression3))
				{
					return false;
				}
				lambdaExpression3 = ProjectionRewriter.TryToRewrite(lambdaExpression3, resourceExpression);
				ResourceExpression resourceExpression4 = resourceExpression.CreateCloneWithNewType(mce.Type);
				if (!ProjectionAnalyzer.Analyze(lambdaExpression3, resourceExpression4, flag, this.context))
				{
					return false;
				}
				ResourceBinder.ValidationRules.RequireCanProject(resourceExpression);
				e = resourceExpression4;
			}
			return true;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013C7C File Offset: 0x00011E7C
		internal static Expression AnalyzeNavigation(MethodCallExpression mce, DataServiceContext context)
		{
			Expression expression = mce.Arguments[0];
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.PatternRules.MatchSingleArgumentLambda(mce.Arguments[1], out lambdaExpression))
			{
				return mce;
			}
			ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(lambdaExpression.Body, mce.Method.Name);
			if (ResourceBinder.PatternRules.MatchIdentitySelector(lambdaExpression))
			{
				return expression;
			}
			if (ResourceBinder.PatternRules.MatchTransparentIdentitySelector(expression, lambdaExpression, context))
			{
				return ResourceBinder.RemoveTransparentScope(mce.Method.ReturnType, (QueryableResourceExpression)expression);
			}
			ResourceExpression resourceExpression;
			Expression expression2;
			MemberExpression memberExpression;
			if (ResourceBinder.IsValidNavigationSource(expression, out resourceExpression) && ResourceBinder.TryBindToInput(resourceExpression, lambdaExpression, out expression2) && ResourceBinder.PatternRules.MatchPropertyProjectionSingleton(resourceExpression, expression2, context, out memberExpression))
			{
				ResourceBinder.ValidationRules.DisallowMemberAccessInNavigation(memberExpression, context.Model);
				expression2 = memberExpression;
				return ResourceBinder.CreateNavigationPropertySingletonExpression(mce.Method.ReturnType, resourceExpression, expression2);
			}
			return mce;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013D33 File Offset: 0x00011F33
		private static bool IsValidNavigationSource(Expression input, out ResourceExpression sourceExpression)
		{
			ResourceBinder.ValidationRules.RequireCanNavigate(input);
			sourceExpression = input as ResourceExpression;
			return sourceExpression != null;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013D48 File Offset: 0x00011F48
		internal static Expression AnalyzeSelectMany(MethodCallExpression mce, DataServiceContext context)
		{
			if (mce.Arguments.Count != 2 && mce.Arguments.Count != 3)
			{
				return mce;
			}
			ResourceExpression resourceExpression;
			if (!ResourceBinder.IsValidNavigationSource(mce.Arguments[0], out resourceExpression))
			{
				return mce;
			}
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.PatternRules.MatchSingleArgumentLambda(mce.Arguments[1], out lambdaExpression))
			{
				return mce;
			}
			ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(lambdaExpression.Body, mce.Method.Name);
			List<ResourceExpression> list = new List<ResourceExpression>();
			Expression expression = InputBinder.Bind(lambdaExpression.Body, resourceExpression, lambdaExpression.Parameters[0], list);
			QueryableResourceExpression queryableResourceExpression;
			if (!ResourceBinder.TryAnalyzeSelectManyCollector(resourceExpression, expression, context, out queryableResourceExpression))
			{
				return mce;
			}
			if (queryableResourceExpression.Type != mce.Method.ReturnType)
			{
				queryableResourceExpression = queryableResourceExpression.CreateCloneForTransparentScope(mce.Method.ReturnType);
			}
			if (mce.Arguments.Count == 3)
			{
				return ResourceBinder.AnalyzeSelectManySelector(mce, queryableResourceExpression, context);
			}
			return queryableResourceExpression;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00013E2C File Offset: 0x0001202C
		private static bool TryAnalyzeSelectManyCollector(ResourceExpression input, Expression navPropRef, DataServiceContext context, out QueryableResourceExpression result)
		{
			MethodCallExpression methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(navPropRef);
			SequenceMethod sequenceMethod;
			MemberExpression memberExpression;
			if (methodCallExpression != null && ReflectionUtil.TryIdentifySequenceMethod(methodCallExpression.Method, out sequenceMethod) && (sequenceMethod == SequenceMethod.Cast || sequenceMethod == SequenceMethod.OfType))
			{
				if (ResourceBinder.TryAnalyzeSelectManyCollector(input, methodCallExpression.Arguments[0], context, out result))
				{
					methodCallExpression = Expression.Call(methodCallExpression.Method, result);
					if (sequenceMethod == SequenceMethod.Cast)
					{
						result = ResourceBinder.AnalyzeCast(methodCallExpression) as QueryableResourceExpression;
					}
					else
					{
						result = ResourceBinder.AnalyzeOfType(methodCallExpression) as QueryableResourceExpression;
					}
				}
				else
				{
					result = null;
				}
			}
			else if (ResourceBinder.PatternRules.MatchPropertyProjectionRelatedSet(input, navPropRef, context, out memberExpression))
			{
				Type elementType = TypeSystem.GetElementType(memberExpression.Type);
				result = ResourceBinder.CreateResourceSetExpression(memberExpression.Type, input, memberExpression, elementType);
			}
			else
			{
				result = null;
			}
			return result != null;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013ED8 File Offset: 0x000120D8
		private static Expression AnalyzeSelectManySelector(MethodCallExpression selectManyCall, QueryableResourceExpression sourceResource, DataServiceContext context)
		{
			LambdaExpression lambdaExpression = ResourceBinder.StripTo<LambdaExpression>(selectManyCall.Arguments[2]);
			QueryableResourceExpression.TransparentAccessors transparentAccessors;
			Expression expression;
			if (ResourceBinder.PatternRules.MatchTransparentScopeSelector(sourceResource, lambdaExpression, out transparentAccessors))
			{
				sourceResource.TransparentScope = transparentAccessors;
				expression = sourceResource;
			}
			else if (ResourceBinder.PatternRules.MatchIdentityProjectionResultSelector(lambdaExpression))
			{
				expression = sourceResource;
			}
			else if (ResourceBinder.PatternRules.MatchMemberInitExpressionWithDefaultConstructor(sourceResource, lambdaExpression) || ResourceBinder.PatternRules.MatchNewExpression(sourceResource, lambdaExpression))
			{
				lambdaExpression = Expression.Lambda(lambdaExpression.Body, new ParameterExpression[] { lambdaExpression.Parameters[1] });
				if (!ProjectionAnalyzer.Analyze(lambdaExpression, sourceResource, false, context))
				{
					expression = selectManyCall;
				}
				else
				{
					expression = sourceResource;
				}
			}
			else
			{
				expression = selectManyCall;
			}
			return expression;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013F64 File Offset: 0x00012164
		internal static Expression ApplyOrdering(MethodCallExpression mce, bool descending, bool thenBy, ClientEdmModel model)
		{
			ResourceBinder.ValidationRules.CheckOrderBy(mce, model);
			QueryableResourceExpression queryableResourceExpression;
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.TryGetResourceSetMethodArguments(mce, out queryableResourceExpression, out lambdaExpression))
			{
				return mce;
			}
			ResourceBinder.ValidationRules.DisallowExpressionEndWithTypeAs(lambdaExpression.Body, mce.Method.Name);
			Expression expression;
			if (!ResourceBinder.TryBindToInput(queryableResourceExpression, lambdaExpression, out expression))
			{
				return mce;
			}
			List<OrderByQueryOptionExpression.Selector> list;
			if (!thenBy)
			{
				list = new List<OrderByQueryOptionExpression.Selector>();
				ResourceBinder.AddSequenceQueryOption(queryableResourceExpression, new OrderByQueryOptionExpression(mce.Type, list));
			}
			else
			{
				list = queryableResourceExpression.OrderBy.Selectors;
			}
			list.Add(new OrderByQueryOptionExpression.Selector(expression, descending));
			if (queryableResourceExpression.Type != mce.Method.ReturnType)
			{
				return queryableResourceExpression.CreateCloneWithNewType(mce.Method.ReturnType);
			}
			return queryableResourceExpression;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001400C File Offset: 0x0001220C
		private static Expression LimitCardinality(MethodCallExpression mce, int maxCardinality)
		{
			if (mce.Arguments.Count != 1)
			{
				return mce;
			}
			ResourceSetExpression resourceSetExpression = mce.Arguments[0] as ResourceSetExpression;
			if (resourceSetExpression != null)
			{
				if (!resourceSetExpression.HasKeyPredicate && resourceSetExpression.NodeType != (ExpressionType)10002 && (resourceSetExpression.Take == null || (int)resourceSetExpression.Take.TakeAmount.Value > maxCardinality))
				{
					ResourceBinder.AddSequenceQueryOption(resourceSetExpression, new TakeQueryOptionExpression(mce.Type, Expression.Constant(maxCardinality)));
				}
				return mce.Arguments[0];
			}
			if (mce.Arguments[0] is NavigationPropertySingletonExpression || mce.Arguments[0] is SingletonResourceExpression)
			{
				return mce.Arguments[0];
			}
			return mce;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000140D0 File Offset: 0x000122D0
		private static Expression AnalyzeCast(MethodCallExpression mce)
		{
			ResourceExpression resourceExpression = mce.Arguments[0] as ResourceExpression;
			if (resourceExpression != null)
			{
				return resourceExpression.CreateCloneWithNewType(mce.Method.ReturnType);
			}
			return mce;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00014108 File Offset: 0x00012308
		private static Expression AnalyzeOfType(MethodCallExpression mce)
		{
			QueryableResourceExpression queryableResourceExpression = mce.Arguments[0] as QueryableResourceExpression;
			if (queryableResourceExpression != null)
			{
				Type type = mce.Method.GetGenericArguments().SingleOrDefault<Type>();
				if (type == null)
				{
					throw new InvalidOperationException(Strings.ALinq_OfTypeArgumentNotAvailable);
				}
				if (type.IsAssignableFrom(queryableResourceExpression.ResourceType))
				{
					return queryableResourceExpression;
				}
				if (queryableResourceExpression.ResourceType.IsAssignableFrom(type))
				{
					if (queryableResourceExpression.ResourceTypeAs != null)
					{
						throw new NotSupportedException(Strings.ALinq_CannotUseTypeFiltersMultipleTimes);
					}
					queryableResourceExpression.ResourceTypeAs = type;
					return queryableResourceExpression.CreateCloneWithNewType(mce.Method.ReturnType);
				}
			}
			return mce;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000141A0 File Offset: 0x000123A0
		private static bool IsTypeOfGenericBaseType(Type baseType, Type derivedType)
		{
			while (derivedType != null && derivedType != typeof(object))
			{
				Type type = (derivedType.IsGenericType() ? derivedType.GetGenericTypeDefinition() : derivedType);
				if (type == baseType)
				{
					return true;
				}
				derivedType = derivedType.GetBaseType();
			}
			return false;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000141F0 File Offset: 0x000123F0
		private static Expression StripConvert(Expression e, Type selfDefinedConvertType)
		{
			UnaryExpression unaryExpression = e as UnaryExpression;
			if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert && unaryExpression.Type.GetBaseType() == typeof(DataServiceContext))
			{
				e = unaryExpression.Operand;
			}
			else if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert && ResourceBinder.IsTypeOfGenericBaseType(typeof(DataServiceQuery<>), unaryExpression.Type))
			{
				e = unaryExpression.Operand;
				ResourceExpression resourceExpression = e as ResourceExpression;
				if (resourceExpression != null && selfDefinedConvertType != null)
				{
					e = resourceExpression.CreateCloneWithNewType(selfDefinedConvertType);
				}
				else if (resourceExpression != null)
				{
					e = resourceExpression.CreateCloneWithNewType(unaryExpression.Type);
				}
			}
			return e;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00014294 File Offset: 0x00012494
		private static Expression AnalyzeExpand(MethodCallExpression mce, DataServiceContext context)
		{
			Expression expression = ResourceBinder.StripConvert(mce.Object, null);
			ResourceExpression resourceExpression = expression as ResourceExpression;
			if (resourceExpression == null)
			{
				return mce;
			}
			ResourceBinder.ValidationRules.RequireCanExpand(resourceExpression);
			Expression expression2 = ResourceBinder.StripTo<Expression>(mce.Arguments[0]);
			string text = null;
			if (expression2.NodeType == ExpressionType.Constant)
			{
				text = (string)((ConstantExpression)expression2).Value;
			}
			else if (expression2.NodeType == ExpressionType.Lambda)
			{
				Version version;
				ResourceBinder.ValidationRules.ValidateExpandPath(expression2, context, out text, out version);
				resourceExpression.RaiseUriVersion(version);
			}
			if (!resourceExpression.ExpandPaths.Contains(text))
			{
				resourceExpression.ExpandPaths.Add(text);
			}
			return resourceExpression;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001432C File Offset: 0x0001252C
		private static Expression AnalyzeFunc(MethodCallExpression mce, bool isExtensionMethod)
		{
			Expression expression;
			if (isExtensionMethod)
			{
				expression = ResourceBinder.StripConvert(mce.Arguments[0], mce.Method.ReturnType);
			}
			else
			{
				expression = ResourceBinder.StripConvert(mce.Object, mce.Method.ReturnType);
			}
			ResourceExpression resourceExpression = expression as ResourceExpression;
			if (resourceExpression == null)
			{
				return mce;
			}
			if (resourceExpression.OperationName == null)
			{
				resourceExpression.OperationName = mce.Method.Name;
			}
			resourceExpression.IsAction = false;
			string[] array = (from p in mce.Method.GetParameters()
				select p.Name).ToArray<string>();
			for (int i = (isExtensionMethod ? 1 : 0); i < mce.Arguments.Count - 1; i++)
			{
				Expression expression2 = ResourceBinder.StripTo<Expression>(mce.Arguments[i]);
				string text = null;
				if (expression2.NodeType == ExpressionType.Constant)
				{
					ConstantExpression constantExpression = (ConstantExpression)expression2;
					text = ODataUriUtils.ConvertToUriLiteral(constantExpression.Value, ODataVersion.V4);
				}
				KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(array[i], text);
				if (!resourceExpression.OperationParameters.Contains(keyValuePair))
				{
					resourceExpression.OperationParameters.Add(array[i], text);
				}
			}
			return resourceExpression;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00014458 File Offset: 0x00012658
		private static Expression AnalyzeAddCustomQueryOption(MethodCallExpression mce)
		{
			Expression expression = ResourceBinder.StripConvert(mce.Object, null);
			ResourceExpression resourceExpression = expression as ResourceExpression;
			if (resourceExpression == null)
			{
				return mce;
			}
			ResourceBinder.ValidationRules.RequireCanAddCustomQueryOption(resourceExpression);
			ConstantExpression constantExpression = ResourceBinder.StripTo<ConstantExpression>(mce.Arguments[0]);
			ConstantExpression constantExpression2 = ResourceBinder.StripTo<ConstantExpression>(mce.Arguments[1]);
			if (((string)constantExpression.Value).Trim() == "$expand")
			{
				ResourceBinder.ValidationRules.RequireCanExpand(resourceExpression);
				resourceExpression.ExpandPaths = resourceExpression.ExpandPaths.Union(new string[] { (string)constantExpression2.Value }, StringComparer.Ordinal).ToList<string>();
			}
			else
			{
				ResourceBinder.ValidationRules.RequireLegalCustomQueryOption(mce.Arguments[0], resourceExpression);
				resourceExpression.CustomQueryOptions.Add(constantExpression, constantExpression2);
			}
			return resourceExpression;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001451C File Offset: 0x0001271C
		private static Expression AnalyzeAddCountOption(MethodCallExpression mce, CountOption countOption)
		{
			Expression expression = ResourceBinder.StripConvert(mce.Object, null);
			QueryableResourceExpression queryableResourceExpression = expression as QueryableResourceExpression;
			if (queryableResourceExpression == null)
			{
				return mce;
			}
			ResourceBinder.ValidationRules.RequireCanAddCount(queryableResourceExpression);
			queryableResourceExpression.ConvertKeyToFilterExpression();
			queryableResourceExpression.CountOption = countOption;
			return queryableResourceExpression;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00014558 File Offset: 0x00012758
		private static QueryableResourceExpression CreateResourceSetExpression(Type type, ResourceExpression source, Expression memberExpression, Type resourceType)
		{
			Type elementType = TypeSystem.GetElementType(type);
			Type type2 = typeof(IOrderedQueryable<>).MakeGenericType(new Type[] { elementType });
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(type2, source, memberExpression, resourceType, source.ExpandPaths.ToList<string>(), source.CountOption, source.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), null, null, source.UriVersion);
			source.ExpandPaths.Clear();
			source.CountOption = CountOption.None;
			source.CustomQueryOptions.Clear();
			return resourceSetExpression;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00014610 File Offset: 0x00012810
		private static NavigationPropertySingletonExpression CreateNavigationPropertySingletonExpression(Type type, ResourceExpression source, Expression memberExpression)
		{
			NavigationPropertySingletonExpression navigationPropertySingletonExpression = new NavigationPropertySingletonExpression(type, source, memberExpression, memberExpression.Type, source.ExpandPaths.ToList<string>(), source.CountOption, source.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), null, null, source.UriVersion);
			source.ExpandPaths.Clear();
			source.CountOption = CountOption.None;
			source.CustomQueryOptions.Clear();
			return navigationPropertySingletonExpression;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000146AC File Offset: 0x000128AC
		private static QueryableResourceExpression RemoveTransparentScope(Type expectedResultType, QueryableResourceExpression input)
		{
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(expectedResultType, input.Source, input.MemberExpression, input.ResourceType, input.ExpandPaths, input.CountOption, input.CustomQueryOptions, input.Projection, input.ResourceTypeAs, input.UriVersion);
			resourceSetExpression.SetKeyPredicate(input.KeyPredicateConjuncts);
			foreach (QueryOptionExpression queryOptionExpression in input.SequenceQueryOptions)
			{
				resourceSetExpression.AddSequenceQueryOption(queryOptionExpression);
			}
			resourceSetExpression.OverrideInputReference(input);
			return resourceSetExpression;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001474C File Offset: 0x0001294C
		internal static Expression StripConvertToAssignable(Expression e)
		{
			UnaryExpression unaryExpression = e as UnaryExpression;
			Expression expression;
			if (unaryExpression != null && ResourceBinder.PatternRules.MatchConvertToAssignable(unaryExpression))
			{
				expression = unaryExpression.Operand;
			}
			else
			{
				expression = e;
			}
			return expression;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00014778 File Offset: 0x00012978
		internal static T StripTo<T>(Expression expression) where T : Expression
		{
			Expression expression2;
			do
			{
				expression2 = expression;
				expression = ((expression.NodeType == ExpressionType.Quote) ? ((UnaryExpression)expression).Operand : expression);
				expression = ResourceBinder.StripConvertToAssignable(expression);
			}
			while (expression2 != expression);
			return expression2 as T;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000147B8 File Offset: 0x000129B8
		internal static T StripTo<T>(Expression expression, out Type convertedType) where T : Expression
		{
			convertedType = null;
			Expression expression2;
			for (;;)
			{
				expression2 = expression;
				expression = ((expression.NodeType == ExpressionType.Quote) ? ((UnaryExpression)expression).Operand : expression);
				if (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked || expression.NodeType == ExpressionType.TypeAs)
				{
					UnaryExpression unaryExpression = expression as UnaryExpression;
					if (unaryExpression != null)
					{
						if (ResourceBinder.PatternRules.MatchConvertToAssignable(unaryExpression))
						{
							expression = unaryExpression.Operand;
						}
						else if (expression.NodeType == ExpressionType.TypeAs)
						{
							if (convertedType != null)
							{
								break;
							}
							expression = unaryExpression.Operand;
							convertedType = unaryExpression.Type;
						}
					}
				}
				if (expression2 == expression)
				{
					goto Block_8;
				}
			}
			throw new NotSupportedException(Strings.ALinq_CannotUseTypeFiltersMultipleTimes);
			Block_8:
			return expression2 as T;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00014860 File Offset: 0x00012A60
		internal override Expression VisitQueryableResourceExpression(QueryableResourceExpression rse)
		{
			if (rse.NodeType == (ExpressionType)10000 || rse.NodeType == (ExpressionType)10001)
			{
				return QueryableResourceExpression.CreateNavigationResourceExpression(rse.NodeType, rse.Type, rse.Source, rse.MemberExpression, rse.ResourceType, null, CountOption.None, null, null, rse.ResourceTypeAs, rse.UriVersion, rse.OperationName, rse.OperationParameters);
			}
			return rse;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000148C8 File Offset: 0x00012AC8
		private static bool TryGetResourceSetMethodArguments(MethodCallExpression mce, out QueryableResourceExpression input, out LambdaExpression lambda)
		{
			input = null;
			lambda = null;
			input = mce.Arguments[0] as QueryableResourceExpression;
			return input != null && ResourceBinder.PatternRules.MatchSingleArgumentLambda(mce.Arguments[1], out lambda);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00014900 File Offset: 0x00012B00
		private static bool TryBindToInput(ResourceExpression input, LambdaExpression le, out Expression bound)
		{
			List<ResourceExpression> list = new List<ResourceExpression>();
			bound = InputBinder.Bind(le.Body, input, le.Parameters[0], list);
			if (list.Count > 1 || (list.Count == 1 && list[0] != input))
			{
				bound = null;
			}
			return bound != null;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00014954 File Offset: 0x00012B54
		private static Expression AnalyzeResourceSetConstantMethod(MethodCallExpression mce, Func<MethodCallExpression, ResourceExpression, ConstantExpression, Expression> constantMethodAnalyzer)
		{
			ResourceExpression resourceExpression = (ResourceExpression)mce.Arguments[0];
			ConstantExpression constantExpression = ResourceBinder.StripTo<ConstantExpression>(mce.Arguments[1]);
			if (constantExpression == null)
			{
				return mce;
			}
			return constantMethodAnalyzer(mce, resourceExpression, constantExpression);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00014994 File Offset: 0x00012B94
		private static Expression AnalyzeCountMethod(MethodCallExpression mce)
		{
			QueryableResourceExpression queryableResourceExpression = mce.Arguments[0] as QueryableResourceExpression;
			if (queryableResourceExpression == null)
			{
				return mce;
			}
			ResourceBinder.ValidationRules.RequireCanAddCount(queryableResourceExpression);
			queryableResourceExpression.ConvertKeyToFilterExpression();
			queryableResourceExpression.CountOption = CountOption.CountSegment;
			return queryableResourceExpression;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000149CC File Offset: 0x00012BCC
		private static void AddSequenceQueryOption(ResourceExpression target, QueryOptionExpression qoe)
		{
			QueryableResourceExpression queryableResourceExpression = (QueryableResourceExpression)target;
			queryableResourceExpression.ConvertKeyToFilterExpression();
			switch (qoe.NodeType)
			{
			case (ExpressionType)10005:
				if (queryableResourceExpression.Take != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("skip", "top"));
				}
				break;
			case (ExpressionType)10006:
				if (queryableResourceExpression.Skip != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("orderby", "skip"));
				}
				if (queryableResourceExpression.Take != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("orderby", "top"));
				}
				if (queryableResourceExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("orderby", "select"));
				}
				break;
			case (ExpressionType)10007:
				if (queryableResourceExpression.Skip != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "skip"));
				}
				if (queryableResourceExpression.Take != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "top"));
				}
				if (queryableResourceExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "select"));
				}
				break;
			}
			queryableResourceExpression.AddSequenceQueryOption(qoe);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00014AE0 File Offset: 0x00012CE0
		internal override Expression VisitBinary(BinaryExpression b)
		{
			Expression expression = base.VisitBinary(b);
			if (ResourceBinder.PatternRules.MatchStringAddition(expression))
			{
				BinaryExpression binaryExpression = ResourceBinder.StripTo<BinaryExpression>(expression);
				MethodInfo method = typeof(string).GetMethod("Concat", new Type[]
				{
					typeof(string),
					typeof(string)
				});
				return Expression.Call(method, new Expression[] { binaryExpression.Left, binaryExpression.Right });
			}
			return expression;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00014B5C File Offset: 0x00012D5C
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = base.VisitMemberAccess(m);
			MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(expression);
			PropertyInfo propertyInfo;
			Expression expression2;
			MethodInfo methodInfo;
			if (memberExpression != null && ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(memberExpression, out propertyInfo, out expression2) && TypeSystem.TryGetPropertyAsMethod(propertyInfo, out methodInfo))
			{
				return Expression.Call(memberExpression.Expression, methodInfo);
			}
			return expression;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00014BA0 File Offset: 0x00012DA0
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Large switch is necessary")]
		internal override Expression VisitMethodCall(MethodCallExpression mce)
		{
			SequenceMethod sequenceMethod;
			Expression expression;
			if (ReflectionUtil.TryIdentifySequenceMethod(mce.Method, out sequenceMethod) && (sequenceMethod == SequenceMethod.Select || sequenceMethod == SequenceMethod.SelectManyResultSelector) && this.AnalyzeProjection(mce, sequenceMethod, out expression))
			{
				return expression;
			}
			expression = base.VisitMethodCall(mce);
			mce = expression as MethodCallExpression;
			if (mce == null)
			{
				return expression;
			}
			if (ReflectionUtil.TryIdentifySequenceMethod(mce.Method, out sequenceMethod))
			{
				if (sequenceMethod > SequenceMethod.First)
				{
					if (sequenceMethod <= SequenceMethod.Single)
					{
						if (sequenceMethod == SequenceMethod.FirstOrDefault)
						{
							goto IL_01C1;
						}
						if (sequenceMethod != SequenceMethod.Single)
						{
							goto IL_01E8;
						}
					}
					else if (sequenceMethod != SequenceMethod.SingleOrDefault)
					{
						switch (sequenceMethod)
						{
						case SequenceMethod.Any:
						case SequenceMethod.AnyPredicate:
						case SequenceMethod.All:
							return mce;
						case SequenceMethod.Count:
						case SequenceMethod.LongCount:
							return ResourceBinder.AnalyzeCountMethod(mce);
						case SequenceMethod.CountPredicate:
							goto IL_01E8;
						default:
							goto IL_01E8;
						}
					}
					return ResourceBinder.LimitCardinality(mce, 2);
				}
				if (sequenceMethod <= SequenceMethod.Take)
				{
					switch (sequenceMethod)
					{
					case SequenceMethod.Where:
						return ResourceBinder.AnalyzePredicate(mce, this.Model);
					case SequenceMethod.WhereOrdinal:
					case SequenceMethod.SelectOrdinal:
					case SequenceMethod.SelectManyOrdinal:
						goto IL_01E8;
					case SequenceMethod.OfType:
						return ResourceBinder.AnalyzeOfType(mce);
					case SequenceMethod.Cast:
						return ResourceBinder.AnalyzeCast(mce);
					case SequenceMethod.Select:
						return ResourceBinder.AnalyzeNavigation(mce, this.context);
					case SequenceMethod.SelectMany:
					case SequenceMethod.SelectManyResultSelector:
						return ResourceBinder.AnalyzeSelectMany(mce, this.context);
					default:
						switch (sequenceMethod)
						{
						case SequenceMethod.OrderBy:
							return ResourceBinder.ApplyOrdering(mce, false, false, this.Model);
						case SequenceMethod.OrderByComparer:
						case SequenceMethod.OrderByDescendingComparer:
						case SequenceMethod.ThenByComparer:
						case SequenceMethod.ThenByDescendingComparer:
							goto IL_01E8;
						case SequenceMethod.OrderByDescending:
							return ResourceBinder.ApplyOrdering(mce, true, false, this.Model);
						case SequenceMethod.ThenBy:
							return ResourceBinder.ApplyOrdering(mce, false, true, this.Model);
						case SequenceMethod.ThenByDescending:
							return ResourceBinder.ApplyOrdering(mce, true, true, this.Model);
						case SequenceMethod.Take:
							return ResourceBinder.AnalyzeResourceSetConstantMethod(mce, delegate(MethodCallExpression callExp, ResourceExpression resource, ConstantExpression takeCount)
							{
								ResourceBinder.AddSequenceQueryOption(resource, new TakeQueryOptionExpression(callExp.Type, takeCount));
								return resource;
							});
						default:
							goto IL_01E8;
						}
						break;
					}
				}
				else
				{
					if (sequenceMethod == SequenceMethod.Skip)
					{
						return ResourceBinder.AnalyzeResourceSetConstantMethod(mce, delegate(MethodCallExpression callExp, ResourceExpression resource, ConstantExpression skipCount)
						{
							ResourceBinder.AddSequenceQueryOption(resource, new SkipQueryOptionExpression(callExp.Type, skipCount));
							return resource;
						});
					}
					if (sequenceMethod != SequenceMethod.First)
					{
						goto IL_01E8;
					}
				}
				IL_01C1:
				return ResourceBinder.LimitCardinality(mce, 1);
				IL_01E8:
				throw Error.MethodNotSupported(mce);
			}
			if ((mce.Method.DeclaringType.IsGenericType() && ResourceBinder.IsTypeOfGenericBaseType(typeof(DataServiceQuery<>), mce.Method.DeclaringType)) || (mce.Method.GetParameters().Any<ParameterInfo>() && ResourceBinder.IsTypeOfGenericBaseType(typeof(DataServiceQuery<>), mce.Method.GetParameters()[0].ParameterType)))
			{
				Type type;
				if (mce.Method.DeclaringType.IsGenericType() && ResourceBinder.IsTypeOfGenericBaseType(typeof(DataServiceQuery<>), mce.Method.DeclaringType))
				{
					type = typeof(DataServiceQuery<>).MakeGenericType(new Type[] { mce.Method.DeclaringType.GetGenericArguments()[0] });
				}
				else
				{
					type = typeof(DataServiceQuery<>).MakeGenericType(new Type[] { mce.Method.GetParameters()[0].ParameterType.GetGenericArguments()[0] });
				}
				if (mce.Method.Name == "Expand" && mce.Method.DeclaringType == type)
				{
					return ResourceBinder.AnalyzeExpand(mce, this.context);
				}
				if (mce.Method.GetParameters().Any<ParameterInfo>() && mce.Method.GetParameters()[0].ParameterType == type)
				{
					return ResourceBinder.AnalyzeFunc(mce, true);
				}
				if (mce.Method.Name == "AddQueryOption" && mce.Method.DeclaringType == type)
				{
					return ResourceBinder.AnalyzeAddCustomQueryOption(mce);
				}
				if (mce.Method.Name == "IncludeTotalCount" && mce.Method.DeclaringType == type)
				{
					return ResourceBinder.AnalyzeAddCountOption(mce, CountOption.CountQuery);
				}
				throw Error.MethodNotSupported(mce);
			}
			else
			{
				if (mce.Method.DeclaringType != null && mce.Method.DeclaringType.GetBaseType() == typeof(DataServiceContext))
				{
					return ResourceBinder.AnalyzeFunc(mce, false);
				}
				return mce;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00014FA8 File Offset: 0x000131A8
		private static Expression StripCastMethodCalls(Expression expression)
		{
			MethodCallExpression methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(expression);
			while (methodCallExpression != null && ReflectionUtil.IsSequenceMethod(methodCallExpression.Method, SequenceMethod.Cast))
			{
				expression = methodCallExpression.Arguments[0];
				methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(expression);
			}
			return expression;
		}

		// Token: 0x04000227 RID: 551
		private const string AddQueryOptionMethodName = "AddQueryOption";

		// Token: 0x04000228 RID: 552
		private const string ExpandMethodName = "Expand";

		// Token: 0x04000229 RID: 553
		private const string IncludeTotalCountMethodName = "IncludeTotalCount";

		// Token: 0x0400022A RID: 554
		private readonly DataServiceContext context;

		// Token: 0x02000180 RID: 384
		internal static class PatternRules
		{
			// Token: 0x06000DCF RID: 3535 RVA: 0x0002F99B File Offset: 0x0002DB9B
			internal static bool MatchConvertToAssignable(UnaryExpression expression)
			{
				return (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked || expression.NodeType == ExpressionType.TypeAs) && expression.Type.IsAssignableFrom(expression.Operand.Type);
			}

			// Token: 0x06000DD0 RID: 3536 RVA: 0x0002F9D4 File Offset: 0x0002DBD4
			internal static bool MatchParameterMemberAccess(Expression expression)
			{
				LambdaExpression lambdaExpression = ResourceBinder.StripTo<LambdaExpression>(expression);
				if (lambdaExpression == null || lambdaExpression.Parameters.Count != 1)
				{
					return false;
				}
				ParameterExpression parameterExpression = lambdaExpression.Parameters[0];
				Expression expression2 = ResourceBinder.StripCastMethodCalls(lambdaExpression.Body);
				for (MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(expression2); memberExpression != null; memberExpression = ResourceBinder.StripTo<MemberExpression>(memberExpression.Expression))
				{
					if (memberExpression.Expression == parameterExpression)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000DD1 RID: 3537 RVA: 0x0002FA38 File Offset: 0x0002DC38
			internal static bool MatchPropertyAccess(Expression e, DataServiceContext context, out MemberExpression member, out Expression instance, out PathSegmentToken propertyPath, out Version uriVersion)
			{
				instance = null;
				propertyPath = null;
				uriVersion = Util.ODataVersion4;
				MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(e);
				member = memberExpression;
				while (memberExpression != null)
				{
					PropertyInfo propertyInfo;
					Expression expression;
					if (ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(memberExpression, out propertyInfo, out expression))
					{
						string serverDefinedName = ClientTypeUtil.GetServerDefinedName(propertyInfo);
						NonSystemToken nonSystemToken = new NonSystemToken(serverDefinedName, null, null);
						if (propertyPath == null)
						{
							propertyPath = nonSystemToken;
						}
						else
						{
							nonSystemToken.NextToken = propertyPath;
							propertyPath = nonSystemToken;
						}
						e = memberExpression.Expression;
						Type type;
						memberExpression = ResourceBinder.StripTo<MemberExpression>(e, out type);
						if (type != null)
						{
							propertyPath = new NonSystemToken(UriHelper.GetEntityTypeNameForUriAndValidateMaxProtocolVersion(type, context, ref uriVersion), null, null)
							{
								NextToken = propertyPath
							};
						}
					}
					else
					{
						memberExpression = null;
					}
				}
				if (propertyPath != null)
				{
					instance = e;
					return true;
				}
				return false;
			}

			// Token: 0x06000DD2 RID: 3538 RVA: 0x0002FAE8 File Offset: 0x0002DCE8
			internal static bool MatchPropertyAccess(Expression e, DataServiceContext context, out MemberExpression member, out Expression instance, out List<string> propertyPath, out Version uriVersion)
			{
				instance = null;
				propertyPath = null;
				uriVersion = Util.ODataVersion4;
				MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(e);
				member = memberExpression;
				while (memberExpression != null)
				{
					PropertyInfo propertyInfo;
					Expression expression;
					if (ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(memberExpression, out propertyInfo, out expression))
					{
						if (propertyPath == null)
						{
							propertyPath = new List<string>();
						}
						propertyPath.Insert(0, propertyInfo.Name);
						e = memberExpression.Expression;
						Type type;
						memberExpression = ResourceBinder.StripTo<MemberExpression>(e, out type);
						if (type != null)
						{
							propertyPath.Insert(0, UriHelper.GetTypeNameForUri(type, context));
						}
					}
					else
					{
						memberExpression = null;
					}
				}
				if (propertyPath != null)
				{
					instance = e;
					return true;
				}
				return false;
			}

			// Token: 0x06000DD3 RID: 3539 RVA: 0x0002FB74 File Offset: 0x0002DD74
			internal static bool MatchConstant(Expression e, out ConstantExpression constExpr)
			{
				constExpr = e as ConstantExpression;
				return constExpr != null;
			}

			// Token: 0x06000DD4 RID: 3540 RVA: 0x0002FB84 File Offset: 0x0002DD84
			internal static bool MatchAnd(Expression e)
			{
				BinaryExpression binaryExpression = e as BinaryExpression;
				return binaryExpression != null && (binaryExpression.NodeType == ExpressionType.And || binaryExpression.NodeType == ExpressionType.AndAlso);
			}

			// Token: 0x06000DD5 RID: 3541 RVA: 0x0002FBB4 File Offset: 0x0002DDB4
			internal static bool MatchNonPrivateReadableProperty(Expression e, out PropertyInfo propInfo, out Expression target)
			{
				propInfo = null;
				target = null;
				MemberExpression memberExpression = e as MemberExpression;
				if (memberExpression == null)
				{
					return false;
				}
				if (PlatformHelper.IsProperty(memberExpression.Member))
				{
					PropertyInfo propertyInfo = (PropertyInfo)memberExpression.Member;
					if (propertyInfo.CanRead && !TypeSystem.IsPrivate(propertyInfo))
					{
						propInfo = propertyInfo;
						target = memberExpression.Expression;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000DD6 RID: 3542 RVA: 0x0002FC0C File Offset: 0x0002DE0C
			internal static bool MatchKeyProperty(Expression expression, out PropertyInfo property)
			{
				property = null;
				PropertyInfo propertyInfo;
				Expression expression2;
				if (!ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(expression, out propertyInfo, out expression2))
				{
					return false;
				}
				Type reflectedType = propertyInfo.ReflectedType;
				if ((ClientTypeUtil.GetKeyPropertiesOnType(reflectedType) ?? ClientTypeUtil.EmptyPropertyInfoArray).Contains(propertyInfo, ResourceBinder.PropertyInfoEqualityComparer.Instance) && expression2 is InputReferenceExpression)
				{
					property = propertyInfo;
					return true;
				}
				return false;
			}

			// Token: 0x06000DD7 RID: 3543 RVA: 0x0002FC5C File Offset: 0x0002DE5C
			internal static bool MatchKeyComparison(Expression e, out PropertyInfo keyProperty, out ConstantExpression keyValue)
			{
				if (ResourceBinder.PatternRules.MatchBinaryEquality(e))
				{
					BinaryExpression binaryExpression = (BinaryExpression)e;
					if ((ResourceBinder.PatternRules.MatchKeyProperty(binaryExpression.Left, out keyProperty) && ResourceBinder.PatternRules.MatchConstant(binaryExpression.Right, out keyValue)) || (ResourceBinder.PatternRules.MatchKeyProperty(binaryExpression.Right, out keyProperty) && ResourceBinder.PatternRules.MatchConstant(binaryExpression.Left, out keyValue)))
					{
						return keyValue.Value != null;
					}
				}
				keyProperty = null;
				keyValue = null;
				return false;
			}

			// Token: 0x06000DD8 RID: 3544 RVA: 0x0002FCC4 File Offset: 0x0002DEC4
			internal static bool MatchReferenceEquals(Expression expression)
			{
				MethodCallExpression methodCallExpression = expression as MethodCallExpression;
				return methodCallExpression != null && methodCallExpression.Method == typeof(object).GetMethod("ReferenceEquals");
			}

			// Token: 0x06000DD9 RID: 3545 RVA: 0x0002FCFC File Offset: 0x0002DEFC
			internal static bool MatchResource(Expression expression, out ResourceExpression resource)
			{
				resource = expression as ResourceExpression;
				return resource != null;
			}

			// Token: 0x06000DDA RID: 3546 RVA: 0x0002FD0B File Offset: 0x0002DF0B
			internal static bool MatchDoubleArgumentLambda(Expression expression, out LambdaExpression lambda)
			{
				return ResourceBinder.PatternRules.MatchNaryLambda(expression, 2, out lambda);
			}

			// Token: 0x06000DDB RID: 3547 RVA: 0x0002FD18 File Offset: 0x0002DF18
			internal static bool MatchIdentitySelector(LambdaExpression lambda)
			{
				ParameterExpression parameterExpression = lambda.Parameters[0];
				return parameterExpression == ResourceBinder.StripTo<ParameterExpression>(lambda.Body);
			}

			// Token: 0x06000DDC RID: 3548 RVA: 0x0002FD40 File Offset: 0x0002DF40
			internal static bool MatchSingleArgumentLambda(Expression expression, out LambdaExpression lambda)
			{
				return ResourceBinder.PatternRules.MatchNaryLambda(expression, 1, out lambda);
			}

			// Token: 0x06000DDD RID: 3549 RVA: 0x0002FD4C File Offset: 0x0002DF4C
			internal static bool MatchTransparentIdentitySelector(Expression input, LambdaExpression selector, DataServiceContext context)
			{
				if (selector.Parameters.Count != 1)
				{
					return false;
				}
				QueryableResourceExpression queryableResourceExpression = input as QueryableResourceExpression;
				if (queryableResourceExpression == null || queryableResourceExpression.TransparentScope == null)
				{
					return false;
				}
				Expression body = selector.Body;
				ParameterExpression parameterExpression = selector.Parameters[0];
				MemberExpression memberExpression;
				Expression expression;
				List<string> list;
				Version version;
				return ResourceBinder.PatternRules.MatchPropertyAccess(body, context, out memberExpression, out expression, out list, out version) && (expression == parameterExpression && list.Count == 1) && list[0] == queryableResourceExpression.TransparentScope.Accessor;
			}

			// Token: 0x06000DDE RID: 3550 RVA: 0x0002FDD0 File Offset: 0x0002DFD0
			internal static bool MatchIdentityProjectionResultSelector(Expression e)
			{
				LambdaExpression lambdaExpression = (LambdaExpression)e;
				return lambdaExpression.Body == lambdaExpression.Parameters[1];
			}

			// Token: 0x06000DDF RID: 3551 RVA: 0x0002FDF8 File Offset: 0x0002DFF8
			internal static bool MatchTransparentScopeSelector(QueryableResourceExpression input, LambdaExpression resultSelector, out QueryableResourceExpression.TransparentAccessors transparentScope)
			{
				transparentScope = null;
				if (resultSelector.Body.NodeType != ExpressionType.New)
				{
					return false;
				}
				NewExpression newExpression = (NewExpression)resultSelector.Body;
				if (newExpression.Arguments.Count < 2)
				{
					return false;
				}
				if (newExpression.Type.GetBaseType() != typeof(object))
				{
					return false;
				}
				ParameterInfo[] parameters = newExpression.Constructor.GetParameters();
				if (newExpression.Members.Count != parameters.Length)
				{
					return false;
				}
				QueryableResourceExpression queryableResourceExpression = input.Source as QueryableResourceExpression;
				int num = -1;
				ParameterExpression parameterExpression = resultSelector.Parameters[0];
				ParameterExpression parameterExpression2 = resultSelector.Parameters[1];
				MemberInfo[] array = new MemberInfo[newExpression.Members.Count];
				IEnumerable<PropertyInfo> publicProperties = newExpression.Type.GetPublicProperties(true);
				Dictionary<string, Expression> dictionary = new Dictionary<string, Expression>(parameters.Length - 1, StringComparer.Ordinal);
				for (int i = 0; i < newExpression.Arguments.Count; i++)
				{
					Expression expression = newExpression.Arguments[i];
					MemberInfo member = newExpression.Members[i];
					if (!ResourceBinder.PatternRules.ExpressionIsSimpleAccess(expression, resultSelector.Parameters))
					{
						return false;
					}
					if (PlatformHelper.IsMethod(member))
					{
						member = publicProperties.Where((PropertyInfo property) => PlatformHelper.AreMembersEqual(member, property.GetGetMethod())).FirstOrDefault<PropertyInfo>();
						if (member == null)
						{
							return false;
						}
					}
					if (member.Name != parameters[i].Name)
					{
						return false;
					}
					array[i] = member;
					ParameterExpression parameterExpression3 = ResourceBinder.StripTo<ParameterExpression>(expression);
					if (parameterExpression2 == parameterExpression3)
					{
						if (num != -1)
						{
							return false;
						}
						num = i;
					}
					else if (parameterExpression == parameterExpression3)
					{
						dictionary[member.Name] = queryableResourceExpression.CreateReference();
					}
					else
					{
						List<ResourceExpression> list = new List<ResourceExpression>();
						InputBinder.Bind(expression, queryableResourceExpression, resultSelector.Parameters[0], list);
						if (list.Count != 1)
						{
							return false;
						}
						dictionary[member.Name] = list[0].CreateReference();
					}
				}
				if (num == -1)
				{
					return false;
				}
				string name = array[num].Name;
				transparentScope = new QueryableResourceExpression.TransparentAccessors(name, dictionary);
				return true;
			}

			// Token: 0x06000DE0 RID: 3552 RVA: 0x00030032 File Offset: 0x0002E232
			internal static bool MatchPropertyProjectionRelatedSet(ResourceExpression input, Expression potentialPropertyRef, DataServiceContext context, out MemberExpression setNavigationMember)
			{
				return ResourceBinder.PatternRules.MatchPropertyProjection(input, potentialPropertyRef, true, context, out setNavigationMember);
			}

			// Token: 0x06000DE1 RID: 3553 RVA: 0x0003003E File Offset: 0x0002E23E
			internal static bool MatchPropertyProjectionSingleton(ResourceExpression input, Expression potentialPropertyRef, DataServiceContext context, out MemberExpression propertyMember)
			{
				return ResourceBinder.PatternRules.MatchPropertyProjection(input, potentialPropertyRef, false, context, out propertyMember);
			}

			// Token: 0x06000DE2 RID: 3554 RVA: 0x0003004C File Offset: 0x0002E24C
			private static bool MatchPropertyProjection(ResourceExpression input, Expression potentialPropertyRef, bool matchSetNavigationProperty, DataServiceContext context, out MemberExpression propertyMember)
			{
				Expression operand;
				List<string> list;
				Version version;
				if (ResourceBinder.PatternRules.MatchPropertyAccess(potentialPropertyRef, context, out propertyMember, out operand, out list, out version))
				{
					UnaryExpression unaryExpression = operand as UnaryExpression;
					if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.TypeAs)
					{
						operand = unaryExpression.Operand;
					}
					if (operand == input.CreateReference() && ResourceBinder.PatternRules.MatchSetNavigationProperty(propertyMember, context.Model) == matchSetNavigationProperty)
					{
						return true;
					}
				}
				propertyMember = null;
				return false;
			}

			// Token: 0x06000DE3 RID: 3555 RVA: 0x000300A8 File Offset: 0x0002E2A8
			internal static bool MatchMemberInitExpressionWithDefaultConstructor(Expression source, LambdaExpression e)
			{
				MemberInitExpression memberInitExpression = ResourceBinder.StripTo<MemberInitExpression>(e.Body);
				ResourceExpression resourceExpression;
				return ResourceBinder.PatternRules.MatchResource(source, out resourceExpression) && memberInitExpression != null && memberInitExpression.NewExpression.Arguments.Count == 0;
			}

			// Token: 0x06000DE4 RID: 3556 RVA: 0x000300E4 File Offset: 0x0002E2E4
			internal static bool MatchNewExpression(Expression source, LambdaExpression e)
			{
				ResourceExpression resourceExpression;
				return ResourceBinder.PatternRules.MatchResource(source, out resourceExpression) && e.Body is NewExpression;
			}

			// Token: 0x06000DE5 RID: 3557 RVA: 0x0003010B File Offset: 0x0002E30B
			internal static bool MatchNot(Expression expression)
			{
				return expression.NodeType == ExpressionType.Not;
			}

			// Token: 0x06000DE6 RID: 3558 RVA: 0x00030118 File Offset: 0x0002E318
			internal static bool MatchSetNavigationProperty(Expression e, ClientEdmModel model)
			{
				return TypeSystem.FindIEnumerable(e.Type) != null && e.Type != typeof(char[]) && e.Type != typeof(byte[]) && !WebUtil.IsCLRTypeCollection(e.Type, model);
			}

			// Token: 0x06000DE7 RID: 3559 RVA: 0x00030178 File Offset: 0x0002E378
			internal static ResourceBinder.PatternRules.MatchNullCheckResult MatchNullCheck(Expression entityInScope, ConditionalExpression conditional)
			{
				ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = default(ResourceBinder.PatternRules.MatchNullCheckResult);
				ResourceBinder.PatternRules.MatchEqualityCheckResult matchEqualityCheckResult = ResourceBinder.PatternRules.MatchEquality(conditional.Test);
				if (!matchEqualityCheckResult.Match)
				{
					return matchNullCheckResult;
				}
				Expression expression;
				if (matchEqualityCheckResult.EqualityYieldsTrue)
				{
					if (!ResourceBinder.PatternRules.MatchNullConstant(conditional.IfTrue))
					{
						return matchNullCheckResult;
					}
					expression = conditional.IfFalse;
				}
				else
				{
					if (!ResourceBinder.PatternRules.MatchNullConstant(conditional.IfFalse))
					{
						return matchNullCheckResult;
					}
					expression = conditional.IfTrue;
				}
				Expression expression2;
				if (ResourceBinder.PatternRules.MatchNullConstant(matchEqualityCheckResult.TestLeft))
				{
					expression2 = matchEqualityCheckResult.TestRight;
				}
				else
				{
					if (!ResourceBinder.PatternRules.MatchNullConstant(matchEqualityCheckResult.TestRight))
					{
						return matchNullCheckResult;
					}
					expression2 = matchEqualityCheckResult.TestLeft;
				}
				MemberAssignmentAnalysis memberAssignmentAnalysis = MemberAssignmentAnalysis.Analyze(entityInScope, expression);
				if (memberAssignmentAnalysis.MultiplePathsFound)
				{
					return matchNullCheckResult;
				}
				MemberAssignmentAnalysis memberAssignmentAnalysis2 = MemberAssignmentAnalysis.Analyze(entityInScope, expression2);
				if (memberAssignmentAnalysis2.MultiplePathsFound)
				{
					return matchNullCheckResult;
				}
				Expression[] expressionsToTargetEntity = memberAssignmentAnalysis.GetExpressionsToTargetEntity();
				Expression[] expressionsToTargetEntity2 = memberAssignmentAnalysis2.GetExpressionsToTargetEntity();
				if (expressionsToTargetEntity2.Length > expressionsToTargetEntity.Length)
				{
					return matchNullCheckResult;
				}
				for (int i = 0; i < expressionsToTargetEntity2.Length; i++)
				{
					Expression expression3 = expressionsToTargetEntity[i];
					Expression expression4 = expressionsToTargetEntity2[i];
					if (expression3 != expression4)
					{
						if (expression3.NodeType != expression4.NodeType || expression3.NodeType != ExpressionType.MemberAccess)
						{
							return matchNullCheckResult;
						}
						if (((MemberExpression)expression3).Member != ((MemberExpression)expression4).Member)
						{
							return matchNullCheckResult;
						}
					}
				}
				matchNullCheckResult.AssignExpression = expression;
				matchNullCheckResult.Match = true;
				matchNullCheckResult.TestToNullExpression = expression2;
				return matchNullCheckResult;
			}

			// Token: 0x06000DE8 RID: 3560 RVA: 0x000302CC File Offset: 0x0002E4CC
			internal static bool MatchNullConstant(Expression expression)
			{
				ConstantExpression constantExpression = expression as ConstantExpression;
				return constantExpression != null && constantExpression.Value == null;
			}

			// Token: 0x06000DE9 RID: 3561 RVA: 0x000302EE File Offset: 0x0002E4EE
			internal static bool MatchBinaryExpression(Expression e)
			{
				return e is BinaryExpression;
			}

			// Token: 0x06000DEA RID: 3562 RVA: 0x000302F9 File Offset: 0x0002E4F9
			internal static bool MatchBinaryEquality(Expression e)
			{
				return ResourceBinder.PatternRules.MatchBinaryExpression(e) && ((BinaryExpression)e).NodeType == ExpressionType.Equal;
			}

			// Token: 0x06000DEB RID: 3563 RVA: 0x00030314 File Offset: 0x0002E514
			internal static bool MatchStringAddition(Expression e)
			{
				if (e.NodeType == ExpressionType.Add)
				{
					BinaryExpression binaryExpression = e as BinaryExpression;
					return binaryExpression != null && binaryExpression.Left.Type == typeof(string) && binaryExpression.Right.Type == typeof(string);
				}
				return false;
			}

			// Token: 0x06000DEC RID: 3564 RVA: 0x0003036D File Offset: 0x0002E56D
			internal static bool MatchNewDataServiceCollectionOfT(NewExpression nex)
			{
				return nex.Type.IsGenericType() && WebUtil.IsDataServiceCollectionType(nex.Type.GetGenericTypeDefinition());
			}

			// Token: 0x06000DED RID: 3565 RVA: 0x00030390 File Offset: 0x0002E590
			internal static bool MatchNewCollectionOfT(NewExpression nex)
			{
				Type type = nex.Type;
				return type.GetInterfaces().Any((Type t) => t.GetGenericTypeDefinition() == typeof(ICollection<>));
			}

			// Token: 0x06000DEE RID: 3566 RVA: 0x000303D0 File Offset: 0x0002E5D0
			internal static ResourceBinder.PatternRules.MatchEqualityCheckResult MatchEquality(Expression expression)
			{
				ResourceBinder.PatternRules.MatchEqualityCheckResult matchEqualityCheckResult = default(ResourceBinder.PatternRules.MatchEqualityCheckResult);
				matchEqualityCheckResult.Match = false;
				matchEqualityCheckResult.EqualityYieldsTrue = true;
				while (!ResourceBinder.PatternRules.MatchReferenceEquals(expression))
				{
					if (!ResourceBinder.PatternRules.MatchNot(expression))
					{
						BinaryExpression binaryExpression = expression as BinaryExpression;
						if (binaryExpression != null)
						{
							if (binaryExpression.NodeType == ExpressionType.NotEqual)
							{
								matchEqualityCheckResult.EqualityYieldsTrue = !matchEqualityCheckResult.EqualityYieldsTrue;
							}
							else if (binaryExpression.NodeType != ExpressionType.Equal)
							{
								return matchEqualityCheckResult;
							}
							matchEqualityCheckResult.TestLeft = binaryExpression.Left;
							matchEqualityCheckResult.TestRight = binaryExpression.Right;
							matchEqualityCheckResult.Match = true;
						}
						return matchEqualityCheckResult;
					}
					matchEqualityCheckResult.EqualityYieldsTrue = !matchEqualityCheckResult.EqualityYieldsTrue;
					expression = ((UnaryExpression)expression).Operand;
				}
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
				matchEqualityCheckResult.Match = true;
				matchEqualityCheckResult.TestLeft = methodCallExpression.Arguments[0];
				matchEqualityCheckResult.TestRight = methodCallExpression.Arguments[1];
				return matchEqualityCheckResult;
			}

			// Token: 0x06000DEF RID: 3567 RVA: 0x000304B0 File Offset: 0x0002E6B0
			private static bool ExpressionIsSimpleAccess(Expression argument, ReadOnlyCollection<ParameterExpression> expressions)
			{
				Expression expression = argument;
				MemberExpression memberExpression;
				do
				{
					memberExpression = expression as MemberExpression;
					if (memberExpression != null)
					{
						expression = memberExpression.Expression;
					}
				}
				while (memberExpression != null);
				ParameterExpression parameterExpression = expression as ParameterExpression;
				return parameterExpression != null && expressions.Contains(parameterExpression);
			}

			// Token: 0x06000DF0 RID: 3568 RVA: 0x000304E8 File Offset: 0x0002E6E8
			private static bool MatchNaryLambda(Expression expression, int parameterCount, out LambdaExpression lambda)
			{
				lambda = null;
				LambdaExpression lambdaExpression = ResourceBinder.StripTo<LambdaExpression>(expression);
				if (lambdaExpression != null && lambdaExpression.Parameters.Count == parameterCount)
				{
					lambda = lambdaExpression;
				}
				return lambda != null;
			}

			// Token: 0x020001EB RID: 491
			internal struct MatchNullCheckResult
			{
				// Token: 0x04000858 RID: 2136
				internal Expression AssignExpression;

				// Token: 0x04000859 RID: 2137
				internal bool Match;

				// Token: 0x0400085A RID: 2138
				internal Expression TestToNullExpression;
			}

			// Token: 0x020001EC RID: 492
			internal struct MatchEqualityCheckResult
			{
				// Token: 0x0400085B RID: 2139
				internal bool EqualityYieldsTrue;

				// Token: 0x0400085C RID: 2140
				internal bool Match;

				// Token: 0x0400085D RID: 2141
				internal Expression TestLeft;

				// Token: 0x0400085E RID: 2142
				internal Expression TestRight;
			}
		}

		// Token: 0x02000181 RID: 385
		internal static class ValidationRules
		{
			// Token: 0x06000DF1 RID: 3569 RVA: 0x00030518 File Offset: 0x0002E718
			internal static void RequireCanNavigate(Expression e)
			{
				QueryableResourceExpression queryableResourceExpression = e as QueryableResourceExpression;
				if (queryableResourceExpression != null && queryableResourceExpression.HasSequenceQueryOptions)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionsOnlyAllowedOnLeafNodes);
				}
				ResourceExpression resourceExpression;
				if (ResourceBinder.PatternRules.MatchResource(e, out resourceExpression) && resourceExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_ProjectionOnlyAllowedOnLeafNodes);
				}
			}

			// Token: 0x06000DF2 RID: 3570 RVA: 0x00030560 File Offset: 0x0002E760
			internal static void RequireCanProject(Expression e)
			{
				ResourceExpression resourceExpression = (ResourceExpression)e;
				if (!ResourceBinder.PatternRules.MatchResource(e, out resourceExpression))
				{
					throw new NotSupportedException(Strings.ALinq_CanOnlyProjectTheLeaf);
				}
				if (resourceExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_ProjectionCanOnlyHaveOneProjection);
				}
				if (resourceExpression.ExpandPaths.Count > 0)
				{
					throw new NotSupportedException(Strings.ALinq_CannotProjectWithExplicitExpansion);
				}
			}

			// Token: 0x06000DF3 RID: 3571 RVA: 0x000305B8 File Offset: 0x0002E7B8
			internal static void RequireCanExpand(Expression e)
			{
				ResourceExpression resourceExpression = (ResourceExpression)e;
				if (!ResourceBinder.PatternRules.MatchResource(e, out resourceExpression))
				{
					throw new NotSupportedException(Strings.ALinq_CantExpand);
				}
				if (resourceExpression.Projection != null)
				{
					throw new NotSupportedException(Strings.ALinq_CannotProjectWithExplicitExpansion);
				}
			}

			// Token: 0x06000DF4 RID: 3572 RVA: 0x000305F4 File Offset: 0x0002E7F4
			internal static void RequireCanAddCount(Expression e)
			{
				ResourceExpression resourceExpression = (ResourceExpression)e;
				if (!ResourceBinder.PatternRules.MatchResource(e, out resourceExpression))
				{
					throw new NotSupportedException(Strings.ALinq_CannotAddCountOption);
				}
				if (resourceExpression.CountOption != CountOption.None)
				{
					throw new NotSupportedException(Strings.ALinq_CannotAddCountOptionConflict);
				}
			}

			// Token: 0x06000DF5 RID: 3573 RVA: 0x00030630 File Offset: 0x0002E830
			internal static void RequireCanAddCustomQueryOption(Expression e)
			{
				ResourceExpression resourceExpression = (ResourceExpression)e;
				if (!ResourceBinder.PatternRules.MatchResource(e, out resourceExpression))
				{
					throw new NotSupportedException(Strings.ALinq_CantAddQueryOption);
				}
			}

			// Token: 0x06000DF6 RID: 3574 RVA: 0x0003065C File Offset: 0x0002E85C
			internal static void RequireNonSingleton(Expression e)
			{
				ResourceExpression resourceExpression = e as ResourceExpression;
				if (resourceExpression != null && resourceExpression.IsSingleton)
				{
					throw new NotSupportedException(Strings.ALinq_QueryOptionsOnlyAllowedOnSingletons);
				}
			}

			// Token: 0x06000DF7 RID: 3575 RVA: 0x00030688 File Offset: 0x0002E888
			internal static void RequireLegalCustomQueryOption(Expression e, ResourceExpression target)
			{
				string name = ((string)(e as ConstantExpression).Value).Trim();
				if (name[0] == '$')
				{
					if (target.CustomQueryOptions.Any((KeyValuePair<ConstantExpression, ConstantExpression> c) => (string)c.Key.Value == name))
					{
						throw new NotSupportedException(Strings.ALinq_CantAddDuplicateQueryOption(name));
					}
					QueryableResourceExpression queryableResourceExpression = target as QueryableResourceExpression;
					if (queryableResourceExpression != null)
					{
						string text = name.Substring(1);
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
						if (num <= 2802900028U)
						{
							if (num <= 967958004U)
							{
								if (num != 297952813U)
								{
									if (num == 967958004U)
									{
										if (text == "count")
										{
											if (queryableResourceExpression.CountOption != CountOption.None)
											{
												throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
											}
											return;
										}
									}
								}
								else if (text == "select")
								{
									if (queryableResourceExpression.Projection != null)
									{
										throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
									}
									return;
								}
							}
							else if (num != 1097563074U)
							{
								if (num == 2802900028U)
								{
									if (text == "top")
									{
										if (queryableResourceExpression.Take != null)
										{
											throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
										}
										return;
									}
								}
							}
							else if (text == "skip")
							{
								if (queryableResourceExpression.Skip != null)
								{
									throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
								}
								return;
							}
						}
						else if (num <= 3353438327U)
						{
							if (num != 3114108242U)
							{
								if (num == 3353438327U)
								{
									if (text == "filter")
									{
										if (queryableResourceExpression.Filter != null)
										{
											throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
										}
										return;
									}
								}
							}
							else if (text == "format")
							{
								ResourceBinder.ValidationRules.ThrowNotSupportedExceptionForTheFormatOption();
								return;
							}
						}
						else if (num != 3826381794U)
						{
							if (num == 4160401121U)
							{
								if (text == "expand")
								{
									return;
								}
							}
						}
						else if (text == "orderby")
						{
							if (queryableResourceExpression.OrderBy != null)
							{
								throw new NotSupportedException(Strings.ALinq_CantAddAstoriaQueryOption(name));
							}
							return;
						}
						throw new NotSupportedException(Strings.ALinq_CantAddQueryOptionStartingWithDollarSign(name));
					}
				}
			}

			// Token: 0x06000DF8 RID: 3576 RVA: 0x000308EA File Offset: 0x0002EAEA
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DataServiceContext", Justification = "The spelling is correct.")]
			private static void ThrowNotSupportedExceptionForTheFormatOption()
			{
				throw new NotSupportedException(Strings.ALinq_FormatQueryOptionNotSupported);
			}

			// Token: 0x06000DF9 RID: 3577 RVA: 0x000308F8 File Offset: 0x0002EAF8
			internal static void CheckPredicate(Expression e, ClientEdmModel model)
			{
				ResourceBinder.ValidationRules.WhereAndOrderByChecker whereAndOrderByChecker = new ResourceBinder.ValidationRules.WhereAndOrderByChecker(model, SequenceMethod.Where);
				whereAndOrderByChecker.Visit(e);
			}

			// Token: 0x06000DFA RID: 3578 RVA: 0x00030918 File Offset: 0x0002EB18
			internal static void CheckOrderBy(Expression e, ClientEdmModel model)
			{
				ResourceBinder.ValidationRules.WhereAndOrderByChecker whereAndOrderByChecker = new ResourceBinder.ValidationRules.WhereAndOrderByChecker(model, SequenceMethod.OrderBy);
				whereAndOrderByChecker.Visit(e);
			}

			// Token: 0x06000DFB RID: 3579 RVA: 0x00030938 File Offset: 0x0002EB38
			internal static void DisallowMemberAccessInNavigation(Expression e, ClientEdmModel model)
			{
				for (MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(e); memberExpression != null; memberExpression = ResourceBinder.StripTo<MemberExpression>(memberExpression.Expression))
				{
					if (WebUtil.IsCLRTypeCollection(memberExpression.Expression.Type, model))
					{
						throw new NotSupportedException(Strings.ALinq_CollectionMemberAccessNotSupportedInNavigation(memberExpression.Member.Name));
					}
				}
			}

			// Token: 0x06000DFC RID: 3580 RVA: 0x00030988 File Offset: 0x0002EB88
			internal static void DisallowExpressionEndWithTypeAs(Expression exp, string method)
			{
				Expression expression = ResourceBinder.StripTo<UnaryExpression>(exp);
				if (expression != null && expression.NodeType == ExpressionType.TypeAs)
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionCannotEndWithTypeAs(exp.ToString(), method));
				}
			}

			// Token: 0x06000DFD RID: 3581 RVA: 0x000309BC File Offset: 0x0002EBBC
			internal static void ValidateExpandPath(Expression input, DataServiceContext context, out string expandPath, out Version uriVersion)
			{
				expandPath = null;
				uriVersion = Util.ODataVersion4;
				LambdaExpression lambdaExpression;
				if (ResourceBinder.PatternRules.MatchSingleArgumentLambda(input, out lambdaExpression))
				{
					MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(lambdaExpression.Body);
					MemberExpression memberExpression2;
					Expression operand;
					PathSegmentToken pathSegmentToken;
					if (memberExpression != null && ResourceBinder.PatternRules.MatchPropertyAccess(memberExpression, context, out memberExpression2, out operand, out pathSegmentToken, out uriVersion))
					{
						UnaryExpression unaryExpression = operand as UnaryExpression;
						if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.TypeAs)
						{
							operand = unaryExpression.Operand;
						}
						Type reflectedType = memberExpression2.Member.ReflectedType;
						if (operand == lambdaExpression.Parameters[0] && ClientTypeUtil.TypeOrElementTypeIsEntity(reflectedType))
						{
							ExpandOnlyPathToStringVisitor expandOnlyPathToStringVisitor = new ExpandOnlyPathToStringVisitor();
							expandPath = pathSegmentToken.Accept<string>(expandOnlyPathToStringVisitor);
							return;
						}
					}
				}
				throw new NotSupportedException(Strings.ALinq_InvalidExpressionInNavigationPath(input));
			}

			// Token: 0x020001EF RID: 495
			internal class WhereAndOrderByChecker : DataServiceALinqExpressionVisitor
			{
				// Token: 0x06000F93 RID: 3987 RVA: 0x00032EA6 File Offset: 0x000310A6
				internal WhereAndOrderByChecker(ClientEdmModel model, SequenceMethod checkedMethod)
				{
					this.model = model;
					this.checkedMethod = checkedMethod;
				}

				// Token: 0x06000F94 RID: 3988 RVA: 0x00032EBC File Offset: 0x000310BC
				internal override Expression VisitMethodCall(MethodCallExpression mce)
				{
					SequenceMethod sequenceMethod;
					if (!ReflectionUtil.TryIdentifySequenceMethod(mce.Method, out sequenceMethod) || !ReflectionUtil.IsAnyAllMethod(sequenceMethod))
					{
						return base.VisitMethodCall(mce);
					}
					if (this.checkedMethod == SequenceMethod.OrderBy)
					{
						throw new NotSupportedException(Strings.ALinq_AnyAllNotSupportedInOrderBy(mce.Method.Name));
					}
					Type type = mce.Method.GetGenericArguments().SingleOrDefault<Type>();
					if (!ClientTypeUtil.TypeOrElementTypeIsEntity(type))
					{
						Expression expression = mce.Arguments[0];
						MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(expression);
						PropertyInfo propertyInfo;
						Expression expression2;
						if (memberExpression == null || !ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(memberExpression, out propertyInfo, out expression2) || !WebUtil.IsCLRTypeCollection(propertyInfo.PropertyType, this.model))
						{
							throw new NotSupportedException(Strings.ALinq_InvalidSourceForAnyAll(mce.Method.Name));
						}
					}
					if (mce.Arguments.Count == 2)
					{
						base.Visit(mce.Arguments[1]);
					}
					return mce;
				}

				// Token: 0x06000F95 RID: 3989 RVA: 0x00032F98 File Offset: 0x00031198
				internal override Expression VisitMemberAccess(MemberExpression m)
				{
					if (PlatformHelper.IsProperty(m.Member))
					{
						PropertyInfo propertyInfo = (PropertyInfo)m.Member;
						if (propertyInfo.Name.Equals("Count"))
						{
							MemberExpression memberExpression = ResourceBinder.StripTo<MemberExpression>(m.Expression);
							if (memberExpression != null && !PrimitiveType.IsKnownNullableType(memberExpression.Type))
							{
								Type implementationType = ClientTypeUtil.GetImplementationType(memberExpression.Type, typeof(ICollection<>));
								if (implementationType != null)
								{
									return m;
								}
							}
						}
						if (WebUtil.IsCLRTypeCollection(propertyInfo.PropertyType, this.model))
						{
							if (this.checkedMethod == SequenceMethod.Where)
							{
								throw new NotSupportedException(Strings.ALinq_CollectionPropertyNotSupportedInWhere(propertyInfo.Name));
							}
							throw new NotSupportedException(Strings.ALinq_CollectionPropertyNotSupportedInOrderBy(propertyInfo.Name));
						}
						else if (typeof(DataServiceStreamLink).IsAssignableFrom(propertyInfo.PropertyType))
						{
							throw new NotSupportedException(Strings.ALinq_LinkPropertyNotSupportedInExpression(propertyInfo.Name));
						}
					}
					return base.VisitMemberAccess(m);
				}

				// Token: 0x04000862 RID: 2146
				private readonly SequenceMethod checkedMethod;

				// Token: 0x04000863 RID: 2147
				private readonly ClientEdmModel model;
			}
		}

		// Token: 0x02000182 RID: 386
		private sealed class PropertyInfoEqualityComparer : IEqualityComparer<PropertyInfo>
		{
			// Token: 0x06000DFE RID: 3582 RVA: 0x0000347F File Offset: 0x0000167F
			private PropertyInfoEqualityComparer()
			{
			}

			// Token: 0x06000DFF RID: 3583 RVA: 0x00030A60 File Offset: 0x0002EC60
			public bool Equals(PropertyInfo left, PropertyInfo right)
			{
				return left == right || (!(null == left) && !(null == right) && left.DeclaringType == right.DeclaringType && left.Name.Equals(right.Name));
			}

			// Token: 0x06000E00 RID: 3584 RVA: 0x00030A9D File Offset: 0x0002EC9D
			public int GetHashCode(PropertyInfo obj)
			{
				if (!(null != obj))
				{
					return 0;
				}
				return obj.GetHashCode();
			}

			// Token: 0x04000748 RID: 1864
			internal static readonly ResourceBinder.PropertyInfoEqualityComparer Instance = new ResourceBinder.PropertyInfoEqualityComparer();
		}

		// Token: 0x02000183 RID: 387
		private sealed class ExpressionPresenceVisitor : DataServiceALinqExpressionVisitor
		{
			// Token: 0x06000E02 RID: 3586 RVA: 0x00030ABC File Offset: 0x0002ECBC
			private ExpressionPresenceVisitor(Expression target)
			{
				this.target = target;
			}

			// Token: 0x06000E03 RID: 3587 RVA: 0x00030ACC File Offset: 0x0002ECCC
			internal static bool IsExpressionPresent(Expression target, Expression tree)
			{
				ResourceBinder.ExpressionPresenceVisitor expressionPresenceVisitor = new ResourceBinder.ExpressionPresenceVisitor(target);
				expressionPresenceVisitor.Visit(tree);
				return expressionPresenceVisitor.found;
			}

			// Token: 0x06000E04 RID: 3588 RVA: 0x00030AF0 File Offset: 0x0002ECF0
			internal override Expression Visit(Expression exp)
			{
				Expression expression;
				if (this.found || this.target == exp)
				{
					this.found = true;
					expression = exp;
				}
				else
				{
					expression = base.Visit(exp);
				}
				return expression;
			}

			// Token: 0x04000749 RID: 1865
			private readonly Expression target;

			// Token: 0x0400074A RID: 1866
			private bool found;
		}
	}
}
