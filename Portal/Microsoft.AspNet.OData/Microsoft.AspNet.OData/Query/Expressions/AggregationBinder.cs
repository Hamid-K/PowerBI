using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000E4 RID: 228
	internal class AggregationBinder : ExpressionBinderBase
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		internal AggregationBinder(ODataQuerySettings settings, IWebApiAssembliesResolver assembliesResolver, Type elementType, IEdmModel model, TransformationNode transformation)
			: base(model, assembliesResolver, settings)
		{
			this._elementType = elementType;
			this._transformation = transformation;
			this._lambdaParameter = Expression.Parameter(this._elementType, "$it");
			TransformationNodeKind kind = transformation.Kind;
			if (kind != TransformationNodeKind.Aggregate)
			{
				if (kind != TransformationNodeKind.GroupBy)
				{
					throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, SRResources.NotSupportedTransformationKind, new object[] { transformation.Kind }));
				}
				GroupByTransformationNode groupByTransformationNode = this._transformation as GroupByTransformationNode;
				this._groupingProperties = groupByTransformationNode.GroupingProperties;
				if (groupByTransformationNode.ChildTransformations != null)
				{
					if (groupByTransformationNode.ChildTransformations.Kind != TransformationNodeKind.Aggregate)
					{
						throw new NotImplementedException();
					}
					AggregateTransformationNode aggregateTransformationNode = (AggregateTransformationNode)groupByTransformationNode.ChildTransformations;
					this._aggregateExpressions = this.FixCustomMethodReturnTypes(aggregateTransformationNode.AggregateExpressions);
				}
				this._groupByClrType = typeof(GroupByWrapper);
				this.ResultClrType = typeof(AggregationWrapper);
			}
			else
			{
				AggregateTransformationNode aggregateTransformationNode2 = this._transformation as AggregateTransformationNode;
				this._aggregateExpressions = this.FixCustomMethodReturnTypes(aggregateTransformationNode2.AggregateExpressions);
				this.ResultClrType = typeof(NoGroupByAggregationWrapper);
			}
			this._groupByClrType = this._groupByClrType ?? typeof(NoGroupByWrapper);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001BC3B File Offset: 0x00019E3B
		private static Expression WrapDynamicCastIfNeeded(Expression propertyAccessor)
		{
			if (propertyAccessor.Type == typeof(object))
			{
				return Expression.Call(null, ExpressionHelperMethods.ConvertToDecimal, new Expression[] { propertyAccessor });
			}
			return propertyAccessor;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001BC6B File Offset: 0x00019E6B
		private IEnumerable<AggregateExpressionBase> FixCustomMethodReturnTypes(IEnumerable<AggregateExpressionBase> aggregateExpressions)
		{
			return aggregateExpressions.Select(delegate(AggregateExpressionBase x)
			{
				AggregateExpression aggregateExpression = x as AggregateExpression;
				if (aggregateExpression == null)
				{
					return x;
				}
				return this.FixCustomMethodReturnType(aggregateExpression);
			});
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001BC80 File Offset: 0x00019E80
		private AggregateExpression FixCustomMethodReturnType(AggregateExpression expression)
		{
			if (expression.Method != AggregationMethod.Custom)
			{
				return expression;
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReferenceOrNull = EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(this.GetCustomMethod(expression).ReturnType);
			return new AggregateExpression(expression.Expression, expression.MethodDefinition, expression.Alias, edmPrimitiveTypeReferenceOrNull);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001BCC4 File Offset: 0x00019EC4
		private MethodInfo GetCustomMethod(AggregateExpression expression)
		{
			Type type = Expression.Lambda(this.BindAccessor(expression.Expression, null), new ParameterExpression[] { this._lambdaParameter }).Body.Type;
			string methodLabel = expression.MethodDefinition.MethodLabel;
			MethodInfo methodInfo;
			if (!base.Model.GetAnnotationValue(base.Model).GetMethodInfo(methodLabel, type, out methodInfo))
			{
				throw new ODataException(Error.Format(SRResources.AggregationNotSupportedForType, new object[] { expression.Method, expression.Expression, type }));
			}
			return methodInfo;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001BD57 File Offset: 0x00019F57
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0001BD5F File Offset: 0x00019F5F
		public Type ResultClrType { get; private set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001BD68 File Offset: 0x00019F68
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0001BD70 File Offset: 0x00019F70
		public IEdmTypeReference ResultType { get; private set; }

		// Token: 0x060007AB RID: 1963 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public IQueryable Bind(IQueryable query)
		{
			this._classicEF = this.IsClassicEF(query);
			this.BaseQuery = query;
			base.EnsureFlattenedPropertyContainer(this._lambdaParameter);
			query = this.FlattenReferencedProperties(query);
			IQueryable queryable = this.BindGroupBy(query);
			return this.BindSelect(queryable);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001BDC4 File Offset: 0x00019FC4
		private IQueryable FlattenReferencedProperties(IQueryable query)
		{
			if (this._aggregateExpressions != null)
			{
				if (this._aggregateExpressions.OfType<AggregateExpression>().Any((AggregateExpression e) => e.Method != AggregationMethod.VirtualPropertyCount) && this._groupingProperties != null && this._groupingProperties.Any<GroupByPropertyNode>() && (this.FlattenedPropertyContainer == null || !this.FlattenedPropertyContainer.Any<KeyValuePair<string, Expression>>()))
				{
					Type type = typeof(FlatteningWrapper<>).MakeGenericType(new Type[] { this._elementType });
					PropertyInfo property = type.GetProperty("Source");
					List<MemberAssignment> list = new List<MemberAssignment>();
					list.Add(Expression.Bind(property, this._lambdaParameter));
					List<AggregateExpression> list2 = (from e in this._aggregateExpressions.OfType<AggregateExpression>()
						where e.Method != AggregationMethod.VirtualPropertyCount
						select e).ToList<AggregateExpression>();
					NamedPropertyExpression[] array = new NamedPropertyExpression[list2.Count];
					int num = list2.Count - 1;
					ParameterExpression parameterExpression = Expression.Parameter(type, "$it");
					MemberExpression memberExpression = Expression.Property(parameterExpression, "GroupByContainer");
					foreach (AggregateExpression aggregateExpression in list2)
					{
						string text = "Property" + num.ToString(CultureInfo.CurrentCulture);
						Expression expression = this.BindAccessor(aggregateExpression.Expression, null);
						Type type2 = expression.Type;
						expression = this.WrapConvert(expression);
						array[num] = new NamedPropertyExpression(Expression.Constant(text), expression);
						UnaryExpression unaryExpression = Expression.Convert(Expression.Property(memberExpression, "Value"), type2);
						memberExpression = Expression.Property(memberExpression, "Next");
						this._preFlattenedMap.Add(aggregateExpression.Expression, unaryExpression);
						num--;
					}
					PropertyInfo property2 = this.ResultClrType.GetProperty("GroupByContainer");
					list.Add(Expression.Bind(property2, AggregationPropertyContainer.CreateNextNamedPropertyContainer(array)));
					LambdaExpression lambdaExpression = Expression.Lambda(Expression.MemberInit(Expression.New(type), list), new ParameterExpression[] { this._lambdaParameter });
					query = ExpressionHelpers.Select(query, lambdaExpression, this._elementType);
					this._lambdaParameter = parameterExpression;
					this._elementType = type;
				}
			}
			return query;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001C020 File Offset: 0x0001A220
		internal virtual bool IsClassicEF(IQueryable query)
		{
			string @namespace = query.Provider.GetType().Namespace;
			return @namespace == "System.Data.Entity.Core.Objects.ELinq" || @namespace == "System.Data.Entity.Internal.Linq";
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001C058 File Offset: 0x0001A258
		private IQueryable BindSelect(IQueryable grouping)
		{
			Type type = typeof(IGrouping<, >).MakeGenericType(new Type[] { this._groupByClrType, this._elementType });
			ParameterExpression parameterExpression = Expression.Parameter(type, "$it");
			List<MemberAssignment> list = new List<MemberAssignment>();
			if (this._groupingProperties != null && this._groupingProperties.Any<GroupByPropertyNode>())
			{
				PropertyInfo property = this.ResultClrType.GetProperty("GroupByContainer");
				list.Add(Expression.Bind(property, Expression.Property(Expression.Property(parameterExpression, "Key"), "GroupByContainer")));
			}
			if (this._aggregateExpressions != null)
			{
				List<NamedPropertyExpression> list2 = new List<NamedPropertyExpression>();
				foreach (AggregateExpressionBase aggregateExpressionBase in this._aggregateExpressions)
				{
					list2.Add(new NamedPropertyExpression(Expression.Constant(aggregateExpressionBase.Alias), this.CreateAggregationExpression(parameterExpression, aggregateExpressionBase, this._elementType)));
				}
				PropertyInfo property2 = this.ResultClrType.GetProperty("Container");
				list.Add(Expression.Bind(property2, AggregationPropertyContainer.CreateNextNamedPropertyContainer(list2)));
			}
			LambdaExpression lambdaExpression = Expression.Lambda(Expression.MemberInit(Expression.New(this.ResultClrType), list), new ParameterExpression[] { parameterExpression });
			return ExpressionHelpers.Select(grouping, lambdaExpression, type);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		private List<MemberAssignment> CreateSelectMemberAssigments(Type type, MemberExpression propertyAccessor, IEnumerable<GroupByPropertyNode> properties)
		{
			List<MemberAssignment> list = new List<MemberAssignment>();
			if (this._groupingProperties != null)
			{
				foreach (GroupByPropertyNode groupByPropertyNode in properties)
				{
					MemberExpression memberExpression = Expression.Property(propertyAccessor, groupByPropertyNode.Name);
					MemberInfo memberInfo = type.GetMember(groupByPropertyNode.Name).Single<MemberInfo>();
					if (groupByPropertyNode.Expression != null)
					{
						list.Add(Expression.Bind(memberInfo, memberExpression));
					}
					else
					{
						Type propertyType = (memberInfo as PropertyInfo).PropertyType;
						MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(propertyType), this.CreateSelectMemberAssigments(propertyType, memberExpression, groupByPropertyNode.ChildTransformations));
						list.Add(Expression.Bind(memberInfo, memberInitExpression));
					}
				}
			}
			return list;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001C278 File Offset: 0x0001A478
		private Expression CreateAggregationExpression(ParameterExpression accum, AggregateExpressionBase expression, Type baseType)
		{
			AggregateExpressionKind aggregateKind = expression.AggregateKind;
			if (aggregateKind == AggregateExpressionKind.PropertyAggregate)
			{
				return this.CreatePropertyAggregateExpression(accum, expression as AggregateExpression, baseType);
			}
			if (aggregateKind != AggregateExpressionKind.EntitySetAggregate)
			{
				throw new ODataException(Error.Format(SRResources.AggregateKindNotSupported, new object[] { expression.AggregateKind }));
			}
			return this.CreateEntitySetAggregateExpression(accum, expression as EntitySetAggregateExpression, baseType);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		private Expression CreateEntitySetAggregateExpression(ParameterExpression accum, EntitySetAggregateExpression expression, Type baseType)
		{
			List<MemberAssignment> list = new List<MemberAssignment>();
			MethodInfo methodInfo = ExpressionHelperMethods.QueryableAsQueryable.MakeGenericMethod(new Type[] { baseType });
			Expression expression2 = Expression.Call(null, methodInfo, new Expression[] { accum });
			Expression expression3 = this.BindAccessor(expression.Expression.Source, null);
			string clrPropertyName = EdmLibHelpers.GetClrPropertyName(expression.Expression.NavigationProperty, base.Model);
			MemberExpression memberExpression = Expression.Property(expression3, clrPropertyName);
			Type type = expression3.Type;
			Type type2 = memberExpression.Type.GenericTypeArguments.Single<Type>();
			MethodInfo methodInfo2 = ExpressionHelperMethods.EnumerableSelectManyGeneric.MakeGenericMethod(new Type[] { type, type2 });
			ParameterExpression parameterExpression = Expression.Parameter(type, "$it");
			LambdaExpression lambdaExpression = Expression.Lambda(Expression.Property(parameterExpression, expression.Expression.NavigationProperty.Name), new ParameterExpression[] { parameterExpression });
			MethodCallExpression methodCallExpression = Expression.Call(null, methodInfo2, expression2, lambdaExpression);
			Type typeFromHandle = typeof(object);
			MethodInfo methodInfo3 = ExpressionHelperMethods.EnumerableGroupByGeneric.MakeGenericMethod(new Type[] { type2, typeFromHandle });
			LambdaExpression lambdaExpression2 = Expression.Lambda(Expression.New(typeFromHandle), new ParameterExpression[] { Expression.Parameter(type2, "$gr") });
			MethodCallExpression methodCallExpression2 = Expression.Call(null, methodInfo3, methodCallExpression, lambdaExpression2);
			Type type3 = typeof(IGrouping<, >).MakeGenericType(new Type[] { typeFromHandle, type2 });
			ParameterExpression parameterExpression2 = Expression.Parameter(type3, "$p");
			List<NamedPropertyExpression> list2 = new List<NamedPropertyExpression>();
			foreach (AggregateExpressionBase aggregateExpressionBase in expression.Children)
			{
				list2.Add(new NamedPropertyExpression(Expression.Constant(aggregateExpressionBase.Alias), this.CreateAggregationExpression(parameterExpression2, aggregateExpressionBase, type2)));
			}
			Type typeFromHandle2 = typeof(EntitySetAggregationWrapper);
			PropertyInfo property = typeFromHandle2.GetProperty("Container");
			list.Add(Expression.Bind(property, AggregationPropertyContainer.CreateNextNamedPropertyContainer(list2)));
			LambdaExpression lambdaExpression3 = Expression.Lambda(Expression.MemberInit(Expression.New(typeFromHandle2), list), new ParameterExpression[] { parameterExpression2 });
			MethodInfo methodInfo4 = ExpressionHelperMethods.EnumerableSelectGeneric.MakeGenericMethod(new Type[]
			{
				type3,
				lambdaExpression3.Body.Type
			});
			return Expression.Call(null, methodInfo4, methodCallExpression2, lambdaExpression3);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001C52C File Offset: 0x0001A72C
		private Expression CreatePropertyAggregateExpression(ParameterExpression accum, AggregateExpression expression, Type baseType)
		{
			Expression expression2;
			if (this._classicEF)
			{
				MethodInfo methodInfo = ExpressionHelperMethods.QueryableAsQueryable.MakeGenericMethod(new Type[] { baseType });
				expression2 = Expression.Call(null, methodInfo, new Expression[] { accum });
			}
			else
			{
				Type type = typeof(IEnumerable<>).MakeGenericType(new Type[] { baseType });
				expression2 = Expression.Convert(accum, type);
			}
			if (expression.Method == AggregationMethod.VirtualPropertyCount)
			{
				MethodInfo methodInfo2 = (this._classicEF ? ExpressionHelperMethods.QueryableCountGeneric : ExpressionHelperMethods.EnumerableCountGeneric).MakeGenericMethod(new Type[] { baseType });
				return this.WrapConvert(Expression.Call(null, methodInfo2, new Expression[] { expression2 }));
			}
			ParameterExpression parameterExpression = ((baseType == this._elementType) ? this._lambdaParameter : Expression.Parameter(baseType, "$it"));
			Expression expression3;
			if (!this._preFlattenedMap.TryGetValue(expression.Expression, out expression3))
			{
				expression3 = this.BindAccessor(expression.Expression, parameterExpression);
			}
			LambdaExpression lambdaExpression = Expression.Lambda(expression3, new ParameterExpression[] { parameterExpression });
			Expression expression5;
			switch (expression.Method)
			{
			case AggregationMethod.Sum:
			{
				Expression expression4 = AggregationBinder.WrapDynamicCastIfNeeded(expression3);
				lambdaExpression = Expression.Lambda(expression4, new ParameterExpression[] { parameterExpression });
				MethodInfo methodInfo3;
				if (!(this._classicEF ? ExpressionHelperMethods.QueryableSumGenerics : ExpressionHelperMethods.EnumerableSumGenerics).TryGetValue(expression4.Type, out methodInfo3))
				{
					throw new ODataException(Error.Format(SRResources.AggregationNotSupportedForType, new object[] { expression.Method, expression.Expression, expression4.Type }));
				}
				MethodInfo methodInfo4 = methodInfo3.MakeGenericMethod(new Type[] { baseType });
				expression5 = Expression.Call(null, methodInfo4, expression2, lambdaExpression);
				if (lambdaExpression.Type == typeof(object))
				{
					expression5 = Expression.Convert(expression5, typeof(object));
					goto IL_0493;
				}
				goto IL_0493;
			}
			case AggregationMethod.Min:
			{
				MethodInfo methodInfo5 = (this._classicEF ? ExpressionHelperMethods.QueryableMin : ExpressionHelperMethods.EnumerableMin).MakeGenericMethod(new Type[]
				{
					baseType,
					lambdaExpression.Body.Type
				});
				expression5 = Expression.Call(null, methodInfo5, expression2, lambdaExpression);
				goto IL_0493;
			}
			case AggregationMethod.Max:
			{
				MethodInfo methodInfo6 = (this._classicEF ? ExpressionHelperMethods.QueryableMax : ExpressionHelperMethods.EnumerableMax).MakeGenericMethod(new Type[]
				{
					baseType,
					lambdaExpression.Body.Type
				});
				expression5 = Expression.Call(null, methodInfo6, expression2, lambdaExpression);
				goto IL_0493;
			}
			case AggregationMethod.Average:
			{
				Expression expression6 = AggregationBinder.WrapDynamicCastIfNeeded(expression3);
				lambdaExpression = Expression.Lambda(expression6, new ParameterExpression[] { parameterExpression });
				MethodInfo methodInfo7;
				if (!(this._classicEF ? ExpressionHelperMethods.QueryableAverageGenerics : ExpressionHelperMethods.EnumerableAverageGenerics).TryGetValue(expression6.Type, out methodInfo7))
				{
					throw new ODataException(Error.Format(SRResources.AggregationNotSupportedForType, new object[] { expression.Method, expression.Expression, expression6.Type }));
				}
				MethodInfo methodInfo8 = methodInfo7.MakeGenericMethod(new Type[] { baseType });
				expression5 = Expression.Call(null, methodInfo8, expression2, lambdaExpression);
				if (lambdaExpression.Type == typeof(object))
				{
					expression5 = Expression.Convert(expression5, typeof(object));
					goto IL_0493;
				}
				goto IL_0493;
			}
			case AggregationMethod.CountDistinct:
			{
				MethodInfo methodInfo9 = (this._classicEF ? ExpressionHelperMethods.QueryableSelectGeneric : ExpressionHelperMethods.EnumerableSelectGeneric).MakeGenericMethod(new Type[]
				{
					this._elementType,
					lambdaExpression.Body.Type
				});
				Expression expression7 = Expression.Call(null, methodInfo9, expression2, lambdaExpression);
				MethodInfo methodInfo10 = (this._classicEF ? ExpressionHelperMethods.QueryableDistinct : ExpressionHelperMethods.EnumerableDistinct).MakeGenericMethod(new Type[] { lambdaExpression.Body.Type });
				Expression expression8 = Expression.Call(null, methodInfo10, new Expression[] { expression7 });
				MethodInfo methodInfo11 = (this._classicEF ? ExpressionHelperMethods.QueryableCountGeneric : ExpressionHelperMethods.EnumerableCountGeneric).MakeGenericMethod(new Type[] { lambdaExpression.Body.Type });
				expression5 = Expression.Call(null, methodInfo11, new Expression[] { expression8 });
				goto IL_0493;
			}
			case AggregationMethod.Custom:
			{
				MethodInfo customMethod = this.GetCustomMethod(expression);
				MethodInfo methodInfo12 = (this._classicEF ? ExpressionHelperMethods.QueryableSelectGeneric : ExpressionHelperMethods.EnumerableSelectGeneric).MakeGenericMethod(new Type[]
				{
					this._elementType,
					lambdaExpression.Body.Type
				});
				MethodCallExpression methodCallExpression = Expression.Call(null, methodInfo12, expression2, lambdaExpression);
				expression5 = Expression.Call(null, customMethod, new Expression[] { methodCallExpression });
				goto IL_0493;
			}
			}
			throw new ODataException(Error.Format(SRResources.AggregationMethodNotSupported, new object[] { expression.Method }));
			IL_0493:
			return this.WrapConvert(expression5);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001C9D4 File Offset: 0x0001ABD4
		private Expression WrapConvert(Expression expression)
		{
			if (!this._classicEF && expression.Type.IsValueType)
			{
				return Expression.Convert(expression, typeof(object));
			}
			return expression;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001CA00 File Offset: 0x0001AC00
		private Expression BindAccessor(QueryNode node, Expression baseElement = null)
		{
			QueryNodeKind kind = node.Kind;
			if (kind <= QueryNodeKind.ResourceRangeVariableReference)
			{
				switch (kind)
				{
				case QueryNodeKind.None:
				case QueryNodeKind.SingleNavigationNode:
				{
					SingleNavigationNode singleNavigationNode = (SingleNavigationNode)node;
					return this.CreatePropertyAccessExpression(this.BindAccessor(singleNavigationNode.Source, null), singleNavigationNode.NavigationProperty, base.GetFullPropertyPath(singleNavigationNode));
				}
				case QueryNodeKind.Constant:
				case QueryNodeKind.NonResourceRangeVariableReference:
				case QueryNodeKind.UnaryOperator:
				case QueryNodeKind.CollectionPropertyAccess:
				case QueryNodeKind.SingleValueFunctionCall:
				case QueryNodeKind.Any:
					break;
				case QueryNodeKind.Convert:
				{
					ConvertNode convertNode = (ConvertNode)node;
					return base.CreateConvertExpression(convertNode, this.BindAccessor(convertNode.Source, baseElement));
				}
				case QueryNodeKind.BinaryOperator:
				{
					BinaryOperatorNode binaryOperatorNode = (BinaryOperatorNode)node;
					Expression expression = this.BindAccessor(binaryOperatorNode.Left, baseElement);
					Expression expression2 = this.BindAccessor(binaryOperatorNode.Right, baseElement);
					return base.CreateBinaryExpression(binaryOperatorNode.OperatorKind, expression, expression2, true);
				}
				case QueryNodeKind.SingleValuePropertyAccess:
				{
					SingleValuePropertyAccessNode singleValuePropertyAccessNode = node as SingleValuePropertyAccessNode;
					return this.CreatePropertyAccessExpression(this.BindAccessor(singleValuePropertyAccessNode.Source, baseElement), singleValuePropertyAccessNode.Property, base.GetFullPropertyPath(singleValuePropertyAccessNode));
				}
				case QueryNodeKind.CollectionNavigationNode:
					return baseElement ?? this._lambdaParameter;
				case QueryNodeKind.SingleValueOpenPropertyAccess:
				{
					SingleValueOpenPropertyAccessNode singleValueOpenPropertyAccessNode = node as SingleValueOpenPropertyAccessNode;
					return base.GetFlattenedPropertyExpression(singleValueOpenPropertyAccessNode.Name) ?? this.CreateOpenPropertyAccessExpression(singleValueOpenPropertyAccessNode);
				}
				default:
					if (kind == QueryNodeKind.ResourceRangeVariableReference)
					{
						if (!this._lambdaParameter.Type.IsGenericType || !(this._lambdaParameter.Type.GetGenericTypeDefinition() == typeof(FlatteningWrapper<>)))
						{
							return this._lambdaParameter;
						}
						return Expression.Property(this._lambdaParameter, "Source");
					}
					break;
				}
			}
			else
			{
				if (kind == QueryNodeKind.SingleComplexNode)
				{
					SingleComplexNode singleComplexNode = node as SingleComplexNode;
					return this.CreatePropertyAccessExpression(this.BindAccessor(singleComplexNode.Source, baseElement), singleComplexNode.Property, base.GetFullPropertyPath(singleComplexNode));
				}
				if (kind == QueryNodeKind.AggregatedCollectionPropertyNode)
				{
					AggregatedCollectionPropertyNode aggregatedCollectionPropertyNode = node as AggregatedCollectionPropertyNode;
					return this.CreatePropertyAccessExpression(this.BindAccessor(aggregatedCollectionPropertyNode.Source, baseElement), aggregatedCollectionPropertyNode.Property, null);
				}
			}
			throw Error.NotSupported(SRResources.QueryNodeBindingNotSupported, new object[]
			{
				node.Kind,
				typeof(AggregationBinder).Name
			});
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001CC14 File Offset: 0x0001AE14
		private Expression CreatePropertyAccessExpression(Expression source, IEdmProperty property, string propertyPath = null)
		{
			string clrPropertyName = EdmLibHelpers.GetClrPropertyName(property, base.Model);
			propertyPath = propertyPath ?? clrPropertyName;
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && ExpressionBinderBase.IsNullable(source.Type) && source != this._lambdaParameter)
			{
				Expression expression = base.RemoveInnerNullPropagation(source);
				Expression expression2 = base.GetFlattenedPropertyExpression(propertyPath) ?? Expression.Property(expression, clrPropertyName);
				Expression expression3 = ExpressionBinderBase.ToNullable(base.ConvertNonStandardPrimitives(expression2));
				return Expression.Condition(Expression.Equal(source, ExpressionBinderBase.NullConstant), Expression.Constant(null, expression3.Type), expression3);
			}
			return base.GetFlattenedPropertyExpression(propertyPath) ?? base.ConvertNonStandardPrimitives(Expression.Property(source, clrPropertyName));
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		private Expression CreateOpenPropertyAccessExpression(SingleValueOpenPropertyAccessNode openNode)
		{
			Expression expression = this.BindAccessor(openNode.Source, null);
			if (expression.Type.GetProperty(openNode.Name) != null)
			{
				return Expression.Property(expression, openNode.Name);
			}
			PropertyInfo dynamicPropertyContainer = base.GetDynamicPropertyContainer(openNode);
			MemberExpression memberExpression = Expression.Property(expression, dynamicPropertyContainer.Name);
			IndexExpression indexExpression = Expression.Property(memberExpression, ExpressionBinderBase.DictionaryStringObjectIndexerName, new Expression[] { Expression.Constant(openNode.Name) });
			MethodCallExpression methodCallExpression = Expression.Call(memberExpression, memberExpression.Type.GetMethod("ContainsKey"), new Expression[] { Expression.Constant(openNode.Name) });
			ConstantExpression constantExpression = Expression.Constant(null);
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				return Expression.Condition(Expression.AndAlso(Expression.NotEqual(memberExpression, Expression.Constant(null)), methodCallExpression), indexExpression, constantExpression);
			}
			return Expression.Condition(methodCallExpression, indexExpression, constantExpression);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001CD9C File Offset: 0x0001AF9C
		private IQueryable BindGroupBy(IQueryable query)
		{
			Type elementType = query.ElementType;
			LambdaExpression lambdaExpression;
			if (this._groupingProperties != null && this._groupingProperties.Any<GroupByPropertyNode>())
			{
				List<NamedPropertyExpression> list = this.CreateGroupByMemberAssignments(this._groupingProperties);
				PropertyInfo property = typeof(GroupByWrapper).GetProperty("GroupByContainer");
				List<MemberAssignment> list2 = new List<MemberAssignment>();
				list2.Add(Expression.Bind(property, AggregationPropertyContainer.CreateNextNamedPropertyContainer(list)));
				lambdaExpression = Expression.Lambda(Expression.MemberInit(Expression.New(typeof(GroupByWrapper)), list2), new ParameterExpression[] { this._lambdaParameter });
			}
			else
			{
				lambdaExpression = Expression.Lambda(Expression.New(this._groupByClrType), new ParameterExpression[] { this._lambdaParameter });
			}
			return ExpressionHelpers.GroupBy(query, lambdaExpression, elementType, this._groupByClrType);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001CE60 File Offset: 0x0001B060
		private List<NamedPropertyExpression> CreateGroupByMemberAssignments(IEnumerable<GroupByPropertyNode> nodes)
		{
			List<NamedPropertyExpression> list = new List<NamedPropertyExpression>();
			foreach (GroupByPropertyNode groupByPropertyNode in nodes)
			{
				string name = groupByPropertyNode.Name;
				if (groupByPropertyNode.Expression != null)
				{
					list.Add(new NamedPropertyExpression(Expression.Constant(name), this.WrapConvert(this.BindAccessor(groupByPropertyNode.Expression, null))));
				}
				else
				{
					PropertyInfo property = typeof(GroupByWrapper).GetProperty("GroupByContainer");
					List<MemberAssignment> list2 = new List<MemberAssignment>();
					list2.Add(Expression.Bind(property, AggregationPropertyContainer.CreateNextNamedPropertyContainer(this.CreateGroupByMemberAssignments(groupByPropertyNode.ChildTransformations))));
					list.Add(new NamedPropertyExpression(Expression.Constant(name), Expression.MemberInit(Expression.New(typeof(GroupByWrapper)), list2)));
				}
			}
			return list;
		}

		// Token: 0x0400023F RID: 575
		private const string GroupByContainerProperty = "GroupByContainer";

		// Token: 0x04000240 RID: 576
		private Type _elementType;

		// Token: 0x04000241 RID: 577
		private TransformationNode _transformation;

		// Token: 0x04000242 RID: 578
		private ParameterExpression _lambdaParameter;

		// Token: 0x04000243 RID: 579
		private IEnumerable<AggregateExpressionBase> _aggregateExpressions;

		// Token: 0x04000244 RID: 580
		private IEnumerable<GroupByPropertyNode> _groupingProperties;

		// Token: 0x04000245 RID: 581
		private Type _groupByClrType;

		// Token: 0x04000246 RID: 582
		private bool _classicEF;

		// Token: 0x04000249 RID: 585
		private Dictionary<SingleValueNode, Expression> _preFlattenedMap = new Dictionary<SingleValueNode, Expression>();
	}
}
