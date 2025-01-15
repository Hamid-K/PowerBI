using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000FA RID: 250
	public class FilterBinder : ExpressionBinderBase
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x000217D8 File Offset: 0x0001F9D8
		public FilterBinder(IServiceProvider requestContainer)
			: base(requestContainer)
		{
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000217EC File Offset: 0x0001F9EC
		internal FilterBinder(ODataQuerySettings settings, IWebApiAssembliesResolver assembliesResolver, IEdmModel model)
			: base(model, assembliesResolver, settings)
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00021804 File Offset: 0x0001FA04
		internal static Expression Bind(IQueryable baseQuery, FilterClause filterClause, Type filterType, ODataQueryContext context, ODataQuerySettings querySettings)
		{
			if (filterClause == null)
			{
				throw Error.ArgumentNull("filterClause");
			}
			if (filterType == null)
			{
				throw Error.ArgumentNull("filterType");
			}
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			FilterBinder orCreateFilterBinder = FilterBinder.GetOrCreateFilterBinder(context, querySettings);
			orCreateFilterBinder._filterType = filterType;
			orCreateFilterBinder.BaseQuery = baseQuery;
			return FilterBinder.BindFilterClause(orCreateFilterBinder, filterClause, filterType);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002185E File Offset: 0x0001FA5E
		internal static LambdaExpression Bind(IQueryable baseQuery, OrderByClause orderBy, Type elementType, ODataQueryContext context, ODataQuerySettings querySettings)
		{
			FilterBinder orCreateFilterBinder = FilterBinder.GetOrCreateFilterBinder(context, querySettings);
			orCreateFilterBinder._filterType = elementType;
			orCreateFilterBinder.BaseQuery = baseQuery;
			return FilterBinder.BindOrderByClause(orCreateFilterBinder, orderBy, elementType);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00021880 File Offset: 0x0001FA80
		private static FilterBinder GetOrCreateFilterBinder(ODataQueryContext context, ODataQuerySettings querySettings)
		{
			FilterBinder filterBinder = null;
			if (context.RequestContainer != null)
			{
				filterBinder = ServiceProviderServiceExtensions.GetRequiredService<FilterBinder>(context.RequestContainer);
				if (filterBinder != null && filterBinder.Model != context.Model && filterBinder.Model == EdmCoreModel.Instance)
				{
					filterBinder.Model = context.Model;
				}
			}
			return filterBinder ?? new FilterBinder(querySettings, WebApiAssembliesResolver.Default, context.Model);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000218E3 File Offset: 0x0001FAE3
		private FilterBinder(IEdmModel model, IWebApiAssembliesResolver assembliesResolver, ODataQuerySettings querySettings, Type filterType)
			: base(model, assembliesResolver, querySettings)
		{
			this._filterType = filterType;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00021901 File Offset: 0x0001FB01
		internal static Expression<Func<TEntityType, bool>> Bind<TEntityType>(FilterClause filterClause, IEdmModel model, IWebApiAssembliesResolver assembliesResolver, ODataQuerySettings querySettings)
		{
			return FilterBinder.Bind(filterClause, typeof(TEntityType), model, assembliesResolver, querySettings) as Expression<Func<TEntityType, bool>>;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002191C File Offset: 0x0001FB1C
		internal static Expression Bind(FilterClause filterClause, Type filterType, IEdmModel model, IWebApiAssembliesResolver assembliesResolver, ODataQuerySettings querySettings)
		{
			if (filterClause == null)
			{
				throw Error.ArgumentNull("filterClause");
			}
			if (filterType == null)
			{
				throw Error.ArgumentNull("filterType");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (assembliesResolver == null)
			{
				throw Error.ArgumentNull("assembliesResolver");
			}
			return FilterBinder.BindFilterClause(new FilterBinder(model, assembliesResolver, querySettings, filterType), filterClause, filterType);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00021978 File Offset: 0x0001FB78
		private static LambdaExpression BindFilterClause(FilterBinder binder, FilterClause filterClause, Type filterType)
		{
			LambdaExpression lambdaExpression = binder.BindExpression(filterClause.Expression, filterClause.RangeVariable, filterType);
			lambdaExpression = Expression.Lambda(binder.ApplyNullPropagationForFilterBody(lambdaExpression.Body), lambdaExpression.Parameters);
			Type type = typeof(Func<, >).MakeGenericType(new Type[]
			{
				filterType,
				typeof(bool)
			});
			if (lambdaExpression.Type != type)
			{
				throw Error.Argument("filterType", SRResources.CannotCastFilter, new object[]
				{
					lambdaExpression.Type.FullName,
					type.FullName
				});
			}
			return lambdaExpression;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00021A14 File Offset: 0x0001FC14
		private static LambdaExpression BindOrderByClause(FilterBinder binder, OrderByClause orderBy, Type elementType)
		{
			return binder.BindExpression(orderBy.Expression, orderBy.RangeVariable, elementType);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00021A2C File Offset: 0x0001FC2C
		public virtual Expression Bind(QueryNode node)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			CollectionNode collectionNode = node as CollectionNode;
			SingleValueNode singleValueNode = node as SingleValueNode;
			if (collectionNode != null)
			{
				return this.BindCollectionNode(collectionNode);
			}
			if (singleValueNode != null)
			{
				return this.BindSingleValueNode(singleValueNode);
			}
			throw Error.NotSupported(SRResources.QueryNodeBindingNotSupported, new object[]
			{
				node.Kind,
				typeof(FilterBinder).Name
			});
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00021A94 File Offset: 0x0001FC94
		private Expression BindCountNode(CountNode node)
		{
			Expression expression = this.Bind(node.Source);
			Expression expression2 = Expression.Constant(null, typeof(long?));
			Type type;
			if (!TypeHelper.IsCollection(expression.Type, out type))
			{
				return expression2;
			}
			MethodInfo methodInfo;
			if (typeof(IQueryable).IsAssignableFrom(expression.Type))
			{
				methodInfo = ExpressionHelperMethods.QueryableCountGeneric.MakeGenericMethod(new Type[] { type });
			}
			else
			{
				methodInfo = ExpressionHelperMethods.EnumerableCountGeneric.MakeGenericMethod(new Type[] { type });
			}
			expression2 = Expression.Call(null, methodInfo, new Expression[] { expression });
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				return Expression.Condition(Expression.Equal(expression, Expression.Constant(null)), Expression.Constant(null, typeof(long?)), ExpressionHelpers.ToNullable(expression2));
			}
			return expression2;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00021B5C File Offset: 0x0001FD5C
		public virtual Expression BindDynamicPropertyAccessQueryNode(SingleValueOpenPropertyAccessNode openNode)
		{
			if (EdmLibHelpers.IsDynamicTypeWrapper(this._filterType))
			{
				return base.GetFlattenedPropertyExpression(openNode.Name) ?? Expression.Property(this.Bind(openNode.Source), openNode.Name);
			}
			PropertyInfo dynamicPropertyContainer = base.GetDynamicPropertyContainer(openNode);
			Expression expression = this.BindPropertyAccessExpression(openNode, dynamicPropertyContainer);
			IndexExpression indexExpression = Expression.Property(expression, ExpressionBinderBase.DictionaryStringObjectIndexerName, new Expression[] { Expression.Constant(openNode.Name) });
			MethodCallExpression methodCallExpression = Expression.Call(expression, expression.Type.GetMethod("ContainsKey"), new Expression[] { Expression.Constant(openNode.Name) });
			ConstantExpression constantExpression = Expression.Constant(null);
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				return Expression.Condition(Expression.AndAlso(Expression.NotEqual(expression, Expression.Constant(null)), methodCallExpression), indexExpression, constantExpression);
			}
			return Expression.Condition(methodCallExpression, indexExpression, constantExpression);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00021C34 File Offset: 0x0001FE34
		private Expression BindPropertyAccessExpression(SingleValueOpenPropertyAccessNode openNode, PropertyInfo prop)
		{
			Expression expression = this.Bind(openNode.Source);
			Expression expression2;
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && ExpressionBinderBase.IsNullable(expression.Type) && expression != this._lambdaParameters["$it"])
			{
				expression2 = Expression.Property(base.RemoveInnerNullPropagation(expression), prop.Name);
			}
			else
			{
				expression2 = Expression.Property(expression, prop.Name);
			}
			return expression2;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00021CA0 File Offset: 0x0001FEA0
		public virtual Expression BindSingleResourceFunctionCallNode(SingleResourceFunctionCallNode node)
		{
			string name = node.Name;
			if (name != null && name == "cast")
			{
				return this.BindSingleResourceCastFunctionCall(node);
			}
			throw Error.NotSupported(SRResources.ODataFunctionNotSupported, new object[] { node.Name });
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		private Expression BindSingleResourceCastFunctionCall(SingleResourceFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			string text = (string)((ConstantNode)node.Parameters.Last<QueryNode>()).Value;
			IEdmType edmType = base.Model.FindType(text);
			Type type = null;
			if (edmType != null)
			{
				type = EdmLibHelpers.GetClrType(edmType.ToEdmTypeReference(false), base.Model);
			}
			if (array[0].Type == type)
			{
				return array[0];
			}
			return ExpressionBinderBase.NullConstant;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00021D5C File Offset: 0x0001FF5C
		public virtual Expression BindSingleResourceCastNode(SingleResourceCastNode node)
		{
			Type clrType = EdmLibHelpers.GetClrType(node.StructuredTypeReference, base.Model);
			return Expression.TypeAs(this.BindCastSourceNode(node.Source), clrType);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00021D90 File Offset: 0x0001FF90
		public virtual Expression BindCollectionResourceCastNode(CollectionResourceCastNode node)
		{
			Type clrType = EdmLibHelpers.GetClrType(node.ItemStructuredType, base.Model);
			return FilterBinder.OfType(this.BindCastSourceNode(node.Source), clrType);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00021DC4 File Offset: 0x0001FFC4
		public virtual Expression BindCollectionConstantNode(CollectionConstantNode node)
		{
			ConstantNode constantNode = node.Collection.FirstOrDefault<ConstantNode>();
			object obj = null;
			if (constantNode != null)
			{
				obj = constantNode.Value;
			}
			Type type = this.RetrieveClrTypeForConstant(node.ItemType, ref obj);
			IList list = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { type })) as IList;
			foreach (ConstantNode constantNode2 in node.Collection)
			{
				object obj2 = (type.IsEnum ? EnumDeserializationHelpers.ConvertEnumValue(constantNode2.Value, type) : constantNode2.Value);
				list.Add(obj2);
			}
			return Expression.Constant(list);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00021E8C File Offset: 0x0002008C
		private Expression BindCastSourceNode(QueryNode sourceNode)
		{
			Expression expression;
			if (sourceNode == null)
			{
				expression = this._lambdaParameters["$it"];
			}
			else
			{
				expression = this.Bind(sourceNode);
			}
			return expression;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00021EB8 File Offset: 0x000200B8
		private static Expression OfType(Expression source, Type elementType)
		{
			if (ExpressionBinderBase.IsIQueryable(source.Type))
			{
				return Expression.Call(null, ExpressionHelperMethods.QueryableOfType.MakeGenericMethod(new Type[] { elementType }), new Expression[] { source });
			}
			return Expression.Call(null, ExpressionHelperMethods.EnumerableOfType.MakeGenericMethod(new Type[] { elementType }), new Expression[] { source });
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00021F1B File Offset: 0x0002011B
		public virtual Expression BindNavigationPropertyNode(QueryNode sourceNode, IEdmNavigationProperty navigationProperty)
		{
			return this.BindNavigationPropertyNode(sourceNode, navigationProperty, null);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00021F28 File Offset: 0x00020128
		public virtual Expression BindNavigationPropertyNode(QueryNode sourceNode, IEdmNavigationProperty navigationProperty, string propertyPath)
		{
			Expression expression;
			if (sourceNode == null)
			{
				expression = this._lambdaParameters["$it"];
			}
			else
			{
				expression = this.Bind(sourceNode);
			}
			return this.CreatePropertyAccessExpression(expression, navigationProperty, propertyPath);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00021F5C File Offset: 0x0002015C
		public virtual Expression BindBinaryOperatorNode(BinaryOperatorNode binaryOperatorNode)
		{
			Expression expression = this.Bind(binaryOperatorNode.Left);
			Expression expression2 = this.Bind(binaryOperatorNode.Right);
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && (ExpressionBinderBase.IsNullable(expression.Type) || ExpressionBinderBase.IsNullable(expression2.Type)))
			{
				expression = ExpressionBinderBase.ToNullable(expression);
				expression2 = ExpressionBinderBase.ToNullable(expression2);
				bool flag = true;
				if (expression == ExpressionBinderBase.NullConstant || expression2 == ExpressionBinderBase.NullConstant)
				{
					flag = false;
				}
				return base.CreateBinaryExpression(binaryOperatorNode.OperatorKind, expression, expression2, flag);
			}
			return base.CreateBinaryExpression(binaryOperatorNode.OperatorKind, expression, expression2, false);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00021FF4 File Offset: 0x000201F4
		public virtual Expression BindInNode(InNode inNode)
		{
			Expression expression = this.Bind(inNode.Left);
			Expression expression2 = this.Bind(inNode.Right);
			if (ExpressionBinderBase.IsIQueryable(expression2.Type))
			{
				return Expression.Call(null, ExpressionHelperMethods.QueryableContainsGeneric.MakeGenericMethod(new Type[] { expression.Type }), expression2, expression);
			}
			return Expression.Call(null, ExpressionHelperMethods.EnumerableContainsGeneric.MakeGenericMethod(new Type[] { expression.Type }), expression2, expression);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002206C File Offset: 0x0002026C
		public virtual Expression BindConstantNode(ConstantNode constantNode)
		{
			if (constantNode.Value == null)
			{
				return ExpressionBinderBase.NullConstant;
			}
			object value = constantNode.Value;
			Type type = this.RetrieveClrTypeForConstant(constantNode.TypeReference, ref value);
			if (base.QuerySettings.EnableConstantParameterization)
			{
				return LinqParameterContainer.Parameterize(type, value);
			}
			return Expression.Constant(value, type);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000220BC File Offset: 0x000202BC
		public virtual Expression BindConvertNode(ConvertNode convertNode)
		{
			Expression expression = this.Bind(convertNode.Source);
			return base.CreateConvertExpression(convertNode, expression);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000220E0 File Offset: 0x000202E0
		private LambdaExpression BindExpression(SingleValueNode expression, RangeVariable rangeVariable, Type elementType)
		{
			ParameterExpression parameterExpression = Expression.Parameter(elementType, rangeVariable.Name);
			this._lambdaParameters = new Dictionary<string, ParameterExpression>();
			this._lambdaParameters.Add(rangeVariable.Name, parameterExpression);
			base.EnsureFlattenedPropertyContainer(parameterExpression);
			return Expression.Lambda(this.Bind(expression), new ParameterExpression[] { parameterExpression });
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00022134 File Offset: 0x00020334
		private Expression ApplyNullPropagationForFilterBody(Expression body)
		{
			if (ExpressionBinderBase.IsNullable(body.Type))
			{
				if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True)
				{
					body = Expression.Equal(body, Expression.Constant(true, typeof(bool?)), false, null);
				}
				else
				{
					body = Expression.Convert(body, typeof(bool));
				}
			}
			return body;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00022190 File Offset: 0x00020390
		public virtual Expression BindRangeVariable(RangeVariable rangeVariable)
		{
			ParameterExpression parameterExpression = this._lambdaParameters[rangeVariable.Name];
			return base.ConvertNonStandardPrimitives(parameterExpression);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000221B8 File Offset: 0x000203B8
		public virtual Expression BindCollectionPropertyAccessNode(CollectionPropertyAccessNode propertyAccessNode)
		{
			Expression expression = this.Bind(propertyAccessNode.Source);
			return this.CreatePropertyAccessExpression(expression, propertyAccessNode.Property, null);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000221E0 File Offset: 0x000203E0
		public virtual Expression BindCollectionComplexNode(CollectionComplexNode collectionComplexNode)
		{
			Expression expression = this.Bind(collectionComplexNode.Source);
			return this.CreatePropertyAccessExpression(expression, collectionComplexNode.Property, null);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00022208 File Offset: 0x00020408
		public virtual Expression BindPropertyAccessQueryNode(SingleValuePropertyAccessNode propertyAccessNode)
		{
			Expression expression = this.Bind(propertyAccessNode.Source);
			return this.CreatePropertyAccessExpression(expression, propertyAccessNode.Property, base.GetFullPropertyPath(propertyAccessNode));
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00022238 File Offset: 0x00020438
		public virtual Expression BindSingleComplexNode(SingleComplexNode singleComplexNode)
		{
			Expression expression = this.Bind(singleComplexNode.Source);
			return this.CreatePropertyAccessExpression(expression, singleComplexNode.Property, base.GetFullPropertyPath(singleComplexNode));
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00022268 File Offset: 0x00020468
		private Expression CreatePropertyAccessExpression(Expression source, IEdmProperty property, string propertyPath = null)
		{
			string clrPropertyName = EdmLibHelpers.GetClrPropertyName(property, base.Model);
			propertyPath = propertyPath ?? clrPropertyName;
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && ExpressionBinderBase.IsNullable(source.Type) && source != this._lambdaParameters["$it"])
			{
				Expression expression = base.RemoveInnerNullPropagation(source);
				Expression expression2 = base.GetFlattenedPropertyExpression(propertyPath) ?? Expression.Property(expression, clrPropertyName);
				Expression expression3 = ExpressionBinderBase.ToNullable(base.ConvertNonStandardPrimitives(expression2));
				return Expression.Condition(Expression.Equal(source, ExpressionBinderBase.NullConstant), Expression.Constant(null, expression3.Type), expression3);
			}
			return base.GetFlattenedPropertyExpression(propertyPath) ?? base.ConvertNonStandardPrimitives(Expression.Property(source, clrPropertyName));
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00022318 File Offset: 0x00020518
		public virtual Expression BindUnaryOperatorNode(UnaryOperatorNode unaryOperatorNode)
		{
			Expression expression = this.Bind(unaryOperatorNode.Operand);
			UnaryOperatorKind operatorKind = unaryOperatorNode.OperatorKind;
			if (operatorKind == UnaryOperatorKind.Negate)
			{
				return Expression.Negate(expression);
			}
			if (operatorKind != UnaryOperatorKind.Not)
			{
				throw Error.NotSupported(SRResources.QueryNodeBindingNotSupported, new object[]
				{
					unaryOperatorNode.Kind,
					typeof(FilterBinder).Name
				});
			}
			return Expression.Not(expression);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00022380 File Offset: 0x00020580
		public virtual Expression BindSingleValueFunctionCallNode(SingleValueFunctionCallNode node)
		{
			string name = node.Name;
			if (name != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num > 2611590440U)
				{
					if (num <= 3102149661U)
					{
						if (num <= 2885211357U)
						{
							if (num != 2659683000U)
							{
								if (num != 2854572110U)
								{
									if (num != 2885211357U)
									{
										goto IL_0447;
									}
									if (!(name == "second"))
									{
										goto IL_0447;
									}
									goto IL_03F7;
								}
								else
								{
									if (!(name == "cast"))
									{
										goto IL_0447;
									}
									return this.BindCastSingleValue(node);
								}
							}
							else
							{
								if (!(name == "isof"))
								{
									goto IL_0447;
								}
								return this.BindIsOf(node);
							}
						}
						else if (num != 2927578396U)
						{
							if (num != 3053661199U)
							{
								if (num != 3102149661U)
								{
									goto IL_0447;
								}
								if (!(name == "floor"))
								{
									goto IL_0447;
								}
								return this.BindFloor(node);
							}
							else
							{
								if (!(name == "hour"))
								{
									goto IL_0447;
								}
								goto IL_03F7;
							}
						}
						else if (!(name == "year"))
						{
							goto IL_0447;
						}
					}
					else if (num <= 3598321157U)
					{
						if (num != 3118831744U)
						{
							if (num != 3564297305U)
							{
								if (num != 3598321157U)
								{
									goto IL_0447;
								}
								if (!(name == "month"))
								{
									goto IL_0447;
								}
							}
							else
							{
								if (!(name == "date"))
								{
									goto IL_0447;
								}
								return this.BindDate(node);
							}
						}
						else
						{
							if (!(name == "ceiling"))
							{
								goto IL_0447;
							}
							return this.BindCeiling(node);
						}
					}
					else if (num <= 3830391293U)
					{
						if (num != 3691983576U)
						{
							if (num != 3830391293U)
							{
								goto IL_0447;
							}
							if (!(name == "day"))
							{
								goto IL_0447;
							}
						}
						else
						{
							if (!(name == "toupper"))
							{
								goto IL_0447;
							}
							return this.BindToUpper(node);
						}
					}
					else if (num != 4124019837U)
					{
						if (num != 4221853948U)
						{
							goto IL_0447;
						}
						if (!(name == "startswith"))
						{
							goto IL_0447;
						}
						return this.BindStartsWith(node);
					}
					else
					{
						if (!(name == "concat"))
						{
							goto IL_0447;
						}
						return this.BindConcat(node);
					}
					return this.BindDateRelatedProperty(node);
				}
				if (num <= 1326178875U)
				{
					if (num <= 790464931U)
					{
						if (num != 187241883U)
						{
							if (num != 682728183U)
							{
								if (num != 790464931U)
								{
									goto IL_0447;
								}
								if (!(name == "endswith"))
								{
									goto IL_0447;
								}
								return this.BindEndsWith(node);
							}
							else
							{
								if (!(name == "now"))
								{
									goto IL_0447;
								}
								return this.BindNow(node);
							}
						}
						else
						{
							if (!(name == "fractionalseconds"))
							{
								goto IL_0447;
							}
							return this.BindFractionalSeconds(node);
						}
					}
					else if (num != 954666857U)
					{
						if (num != 1042520049U)
						{
							if (num != 1326178875U)
							{
								goto IL_0447;
							}
							if (!(name == "round"))
							{
								goto IL_0447;
							}
							return this.BindRound(node);
						}
						else
						{
							if (!(name == "tolower"))
							{
								goto IL_0447;
							}
							return this.BindToLower(node);
						}
					}
					else if (!(name == "minute"))
					{
						goto IL_0447;
					}
				}
				else if (num <= 1972648905U)
				{
					if (num != 1564253156U)
					{
						if (num != 1825239352U)
						{
							if (num != 1972648905U)
							{
								goto IL_0447;
							}
							if (!(name == "trim"))
							{
								goto IL_0447;
							}
							return this.BindTrim(node);
						}
						else
						{
							if (!(name == "contains"))
							{
								goto IL_0447;
							}
							return this.BindContains(node);
						}
					}
					else
					{
						if (!(name == "time"))
						{
							goto IL_0447;
						}
						return this.BindTime(node);
					}
				}
				else if (num != 2211460629U)
				{
					if (num != 2479856990U)
					{
						if (num != 2611590440U)
						{
							goto IL_0447;
						}
						if (!(name == "substring"))
						{
							goto IL_0447;
						}
						return this.BindSubstring(node);
					}
					else
					{
						if (!(name == "indexof"))
						{
							goto IL_0447;
						}
						return this.BindIndexOf(node);
					}
				}
				else
				{
					if (!(name == "length"))
					{
						goto IL_0447;
					}
					return this.BindLength(node);
				}
				IL_03F7:
				return this.BindTimeRelatedProperty(node);
			}
			IL_0447:
			Expression expression = this.BindCustomMethodExpressionOrNull(node);
			if (expression != null)
			{
				return expression;
			}
			throw new NotImplementedException(Error.Format(SRResources.ODataFunctionNotSupported, new object[] { node.Name }));
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00022800 File Offset: 0x00020A00
		private Expression BindCastSingleValue(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			Expression expression = ((array.Length == 1) ? this._lambdaParameters["$it"] : array[0]);
			string text = (string)((ConstantNode)node.Parameters.Last<QueryNode>()).Value;
			IEdmType edmType = base.Model.FindType(text);
			Type type = null;
			if (edmType != null)
			{
				IEdmTypeReference edmTypeReference = edmType.ToEdmTypeReference(false);
				type = EdmLibHelpers.GetClrType(edmTypeReference, base.Model);
				if (expression != ExpressionBinderBase.NullConstant)
				{
					if (expression.Type == type)
					{
						return expression;
					}
					if ((!edmTypeReference.IsPrimitive() && !edmTypeReference.IsEnum()) || (EdmLibHelpers.GetEdmPrimitiveTypeOrNull(expression.Type) == null && !TypeHelper.IsEnum(expression.Type)))
					{
						return ExpressionBinderBase.NullConstant;
					}
				}
			}
			if (type == null || expression == ExpressionBinderBase.NullConstant)
			{
				return ExpressionBinderBase.NullConstant;
			}
			if (type == typeof(string))
			{
				return FilterBinder.BindCastToStringType(expression);
			}
			if (TypeHelper.IsEnum(type))
			{
				return this.BindCastToEnumType(expression.Type, type, node.Parameters.First<QueryNode>(), array.Length);
			}
			if (TypeHelper.IsNullable(expression.Type) && !TypeHelper.IsNullable(type))
			{
				type = typeof(Nullable<>).MakeGenericType(new Type[] { type });
			}
			Expression expression2;
			try
			{
				expression2 = Expression.Convert(expression, type);
			}
			catch (InvalidOperationException)
			{
				expression2 = ExpressionBinderBase.NullConstant;
			}
			return expression2;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002297C File Offset: 0x00020B7C
		private static Expression BindCastToStringType(Expression source)
		{
			Expression expression;
			if (source.Type.IsGenericType() && source.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				if (TypeHelper.IsEnum(source.Type))
				{
					expression = Expression.Convert(Expression.Property(source, "Value"), Enum.GetUnderlyingType(TypeHelper.GetUnderlyingTypeOrSelf(source.Type)));
				}
				else
				{
					expression = Expression.Property(source, "Value");
				}
				return Expression.Condition(Expression.Property(source, "HasValue"), Expression.Call(expression, "ToString", null, null), Expression.Constant(null, typeof(string)));
			}
			expression = (TypeHelper.IsEnum(source.Type) ? Expression.Convert(source, Enum.GetUnderlyingType(source.Type)) : source);
			return Expression.Call(expression, "ToString", null, null);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00022A50 File Offset: 0x00020C50
		private Expression BindCastToEnumType(Type sourceType, Type targetClrType, QueryNode firstParameter, int parameterLength)
		{
			Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(targetClrType);
			ConstantNode constantNode = firstParameter as ConstantNode;
			if (parameterLength == 1 || constantNode == null || sourceType != typeof(string))
			{
				return ExpressionBinderBase.NullConstant;
			}
			object[] array = new object[]
			{
				constantNode.Value,
				Enum.ToObject(underlyingTypeOrSelf, 0)
			};
			if (!(bool)ExpressionBinderBase.EnumTryParseMethod.MakeGenericMethod(new Type[] { underlyingTypeOrSelf }).Invoke(null, array))
			{
				return ExpressionBinderBase.NullConstant;
			}
			if (base.QuerySettings.EnableConstantParameterization)
			{
				return LinqParameterContainer.Parameterize(targetClrType, array[1]);
			}
			return Expression.Constant(array[1], targetClrType);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00022AF0 File Offset: 0x00020CF0
		private Expression BindIsOf(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			Expression expression = ((array.Length == 1) ? this._lambdaParameters["$it"] : array[0]);
			if (expression == ExpressionBinderBase.NullConstant)
			{
				return ExpressionBinderBase.FalseConstant;
			}
			string text = (string)((ConstantNode)node.Parameters.Last<QueryNode>()).Value;
			IEdmType edmType = base.Model.FindType(text);
			Type type = null;
			if (edmType != null)
			{
				type = EdmLibHelpers.GetClrType(edmType.ToEdmTypeReference(false), base.Model);
			}
			if (type == null)
			{
				return ExpressionBinderBase.FalseConstant;
			}
			bool flag = EdmLibHelpers.GetEdmPrimitiveTypeOrNull(expression.Type) != null || TypeHelper.IsEnum(expression.Type);
			bool flag2 = EdmLibHelpers.GetEdmPrimitiveTypeOrNull(type) != null || TypeHelper.IsEnum(type);
			if (flag && flag2 && TypeHelper.IsNullable(expression.Type))
			{
				type = TypeHelper.ToNullable(type);
			}
			return Expression.Condition(Expression.TypeIs(expression, type), ExpressionBinderBase.TrueConstant, ExpressionBinderBase.FalseConstant);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00022BE8 File Offset: 0x00020DE8
		private Expression BindCeiling(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			MethodInfo methodInfo = (ExpressionBinderBase.IsType<double>(array[0].Type) ? ClrCanonicalFunctions.CeilingOfDouble : ClrCanonicalFunctions.CeilingOfDecimal);
			return base.MakeFunctionCall(methodInfo, array);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00022C28 File Offset: 0x00020E28
		private Expression BindFloor(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			MethodInfo methodInfo = (ExpressionBinderBase.IsType<double>(array[0].Type) ? ClrCanonicalFunctions.FloorOfDouble : ClrCanonicalFunctions.FloorOfDecimal);
			return base.MakeFunctionCall(methodInfo, array);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00022C68 File Offset: 0x00020E68
		private Expression BindRound(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			MethodInfo methodInfo = (ExpressionBinderBase.IsType<double>(array[0].Type) ? ClrCanonicalFunctions.RoundOfDouble : ClrCanonicalFunctions.RoundOfDecimal);
			return base.MakeFunctionCall(methodInfo, array);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00022CA6 File Offset: 0x00020EA6
		private Expression BindDate(SingleValueFunctionCallNode node)
		{
			return this.BindArguments(node.Parameters)[0];
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00022CB6 File Offset: 0x00020EB6
		private Expression BindNow(SingleValueFunctionCallNode node)
		{
			this.BindArguments(node.Parameters);
			return Expression.Property(null, typeof(DateTimeOffset), "UtcNow");
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00022CA6 File Offset: 0x00020EA6
		private Expression BindTime(SingleValueFunctionCallNode node)
		{
			return this.BindArguments(node.Parameters)[0];
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00022CDC File Offset: 0x00020EDC
		private Expression BindFractionalSeconds(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			Expression expression = array[0];
			PropertyInfo propertyInfo;
			if (ExpressionBinderBase.IsTimeOfDay(expression.Type))
			{
				propertyInfo = ClrCanonicalFunctions.TimeOfDayProperties["millisecond"];
			}
			else if (ExpressionBinderBase.IsDateTime(expression.Type))
			{
				propertyInfo = ClrCanonicalFunctions.DateTimeProperties["millisecond"];
			}
			else if (ExpressionBinderBase.IsTimeSpan(expression.Type))
			{
				propertyInfo = ClrCanonicalFunctions.TimeSpanProperties["millisecond"];
			}
			else
			{
				propertyInfo = ClrCanonicalFunctions.DateTimeOffsetProperties["millisecond"];
			}
			Expression expression2 = Expression.Divide(Expression.Convert(base.MakePropertyAccess(propertyInfo, expression), typeof(decimal)), Expression.Constant(1000m, typeof(decimal)));
			return base.CreateFunctionCallWithNullPropagation(expression2, array);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00022DAC File Offset: 0x00020FAC
		private Expression BindDateRelatedProperty(SingleValueFunctionCallNode node)
		{
			Expression expression = this.BindArguments(node.Parameters)[0];
			PropertyInfo propertyInfo;
			if (ExpressionBinderBase.IsDate(expression.Type))
			{
				propertyInfo = ClrCanonicalFunctions.DateProperties[node.Name];
			}
			else if (ExpressionBinderBase.IsDateTime(expression.Type))
			{
				propertyInfo = ClrCanonicalFunctions.DateTimeProperties[node.Name];
			}
			else
			{
				propertyInfo = ClrCanonicalFunctions.DateTimeOffsetProperties[node.Name];
			}
			return base.MakeFunctionCall(propertyInfo, new Expression[] { expression });
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00022E2C File Offset: 0x0002102C
		private Expression BindTimeRelatedProperty(SingleValueFunctionCallNode node)
		{
			Expression expression = this.BindArguments(node.Parameters)[0];
			PropertyInfo propertyInfo;
			if (ExpressionBinderBase.IsTimeOfDay(expression.Type))
			{
				propertyInfo = ClrCanonicalFunctions.TimeOfDayProperties[node.Name];
			}
			else if (ExpressionBinderBase.IsDateTime(expression.Type))
			{
				propertyInfo = ClrCanonicalFunctions.DateTimeProperties[node.Name];
			}
			else if (ExpressionBinderBase.IsTimeSpan(expression.Type))
			{
				propertyInfo = ClrCanonicalFunctions.TimeSpanProperties[node.Name];
			}
			else
			{
				propertyInfo = ClrCanonicalFunctions.DateTimeOffsetProperties[node.Name];
			}
			return base.MakeFunctionCall(propertyInfo, new Expression[] { expression });
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00022ECC File Offset: 0x000210CC
		private Expression BindConcat(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.Concat, array);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00022F00 File Offset: 0x00021100
		private Expression BindTrim(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.Trim, array);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00022F34 File Offset: 0x00021134
		private Expression BindToUpper(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.ToUpper, array);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00022F68 File Offset: 0x00021168
		private Expression BindToLower(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.ToLower, array);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00022F9C File Offset: 0x0002119C
		private Expression BindIndexOf(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.IndexOf, array);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00022FD0 File Offset: 0x000211D0
		private Expression BindSubstring(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			if (array[0].Type != typeof(string))
			{
				throw new ODataException(Error.Format(SRResources.FunctionNotSupportedOnEnum, new object[] { node.Name }));
			}
			Expression expression;
			if (array.Length == 2)
			{
				if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True)
				{
					expression = base.MakeFunctionCall(ClrCanonicalFunctions.SubstringStartNoThrow, array);
				}
				else
				{
					expression = base.MakeFunctionCall(ClrCanonicalFunctions.SubstringStart, array);
				}
			}
			else if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True)
			{
				expression = base.MakeFunctionCall(ClrCanonicalFunctions.SubstringStartAndLengthNoThrow, array);
			}
			else
			{
				expression = base.MakeFunctionCall(ClrCanonicalFunctions.SubstringStartAndLength, array);
			}
			return expression;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00023080 File Offset: 0x00021280
		private Expression BindLength(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.Length, array);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000230B4 File Offset: 0x000212B4
		private Expression BindContains(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.Contains, new Expression[]
			{
				array[0],
				array[1]
			});
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000230F8 File Offset: 0x000212F8
		private Expression BindStartsWith(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.StartsWith, array);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002312C File Offset: 0x0002132C
		private Expression BindEndsWith(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			FilterBinder.ValidateAllStringArguments(node.Name, array);
			return base.MakeFunctionCall(ClrCanonicalFunctions.EndsWith, array);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002315E File Offset: 0x0002135E
		private Expression[] BindArguments(IEnumerable<QueryNode> nodes)
		{
			return (from n in nodes.OfType<SingleValueNode>()
				select this.Bind(n)).ToArray<Expression>();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002317C File Offset: 0x0002137C
		private static void ValidateAllStringArguments(string functionName, Expression[] arguments)
		{
			if (arguments.Any((Expression arg) => arg.Type != typeof(string)))
			{
				throw new ODataException(Error.Format(SRResources.FunctionNotSupportedOnEnum, new object[] { functionName }));
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000231CC File Offset: 0x000213CC
		public virtual Expression BindAllNode(AllNode allNode)
		{
			ParameterExpression parameterExpression = this.HandleLambdaParameters(allNode.RangeVariables);
			Expression expression = this.Bind(allNode.Source);
			Expression expression2 = this.Bind(allNode.Body);
			expression2 = this.ApplyNullPropagationForFilterBody(expression2);
			expression2 = Expression.Lambda(expression2, new ParameterExpression[] { parameterExpression });
			Expression expression3 = FilterBinder.All(expression, expression2);
			this.ExitLamdbaScope();
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && ExpressionBinderBase.IsNullable(expression.Type))
			{
				expression3 = ExpressionBinderBase.ToNullable(expression3);
				return Expression.Condition(Expression.Equal(expression, ExpressionBinderBase.NullConstant), Expression.Constant(null, expression3.Type), expression3);
			}
			return expression3;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002326C File Offset: 0x0002146C
		public virtual Expression BindAnyNode(AnyNode anyNode)
		{
			ParameterExpression parameterExpression = this.HandleLambdaParameters(anyNode.RangeVariables);
			Expression expression = this.Bind(anyNode.Source);
			Expression expression2 = null;
			if (anyNode.Body != null && anyNode.Body.Kind != QueryNodeKind.Constant)
			{
				expression2 = this.Bind(anyNode.Body);
				expression2 = this.ApplyNullPropagationForFilterBody(expression2);
				expression2 = Expression.Lambda(expression2, new ParameterExpression[] { parameterExpression });
			}
			else if (anyNode.Body != null && anyNode.Body.Kind == QueryNodeKind.Constant && !(bool)(anyNode.Body as ConstantNode).Value)
			{
				this.ExitLamdbaScope();
				return ExpressionBinderBase.FalseConstant;
			}
			Expression expression3 = FilterBinder.Any(expression, expression2);
			this.ExitLamdbaScope();
			if (base.QuerySettings.HandleNullPropagation == HandleNullPropagationOption.True && ExpressionBinderBase.IsNullable(expression.Type))
			{
				expression3 = ExpressionBinderBase.ToNullable(expression3);
				return Expression.Condition(Expression.Equal(expression, ExpressionBinderBase.NullConstant), Expression.Constant(null, expression3.Type), expression3);
			}
			return expression3;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002335C File Offset: 0x0002155C
		private Expression BindCustomMethodExpressionOrNull(SingleValueFunctionCallNode node)
		{
			Expression[] array = this.BindArguments(node.Parameters);
			IEnumerable<Type> enumerable = array.Select((Expression argument) => argument.Type);
			MethodInfo methodInfo;
			if (UriFunctionsBinder.TryGetMethodInfo(node.Name, enumerable, out methodInfo))
			{
				return base.MakeFunctionCall(methodInfo, array);
			}
			return null;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000233B8 File Offset: 0x000215B8
		private Expression BindSingleValueNode(SingleValueNode node)
		{
			switch (node.Kind)
			{
			case QueryNodeKind.Constant:
				return this.BindConstantNode(node as ConstantNode);
			case QueryNodeKind.Convert:
				return this.BindConvertNode(node as ConvertNode);
			case QueryNodeKind.NonResourceRangeVariableReference:
				return this.BindRangeVariable((node as NonResourceRangeVariableReferenceNode).RangeVariable);
			case QueryNodeKind.BinaryOperator:
				return this.BindBinaryOperatorNode(node as BinaryOperatorNode);
			case QueryNodeKind.UnaryOperator:
				return this.BindUnaryOperatorNode(node as UnaryOperatorNode);
			case QueryNodeKind.SingleValuePropertyAccess:
				return this.BindPropertyAccessQueryNode(node as SingleValuePropertyAccessNode);
			case QueryNodeKind.SingleValueFunctionCall:
				return this.BindSingleValueFunctionCallNode(node as SingleValueFunctionCallNode);
			case QueryNodeKind.Any:
				return this.BindAnyNode(node as AnyNode);
			case QueryNodeKind.SingleNavigationNode:
			{
				SingleNavigationNode singleNavigationNode = node as SingleNavigationNode;
				return this.BindNavigationPropertyNode(singleNavigationNode.Source, singleNavigationNode.NavigationProperty, base.GetFullPropertyPath(singleNavigationNode));
			}
			case QueryNodeKind.SingleValueOpenPropertyAccess:
				return this.BindDynamicPropertyAccessQueryNode(node as SingleValueOpenPropertyAccessNode);
			case QueryNodeKind.SingleResourceCast:
				return this.BindSingleResourceCastNode(node as SingleResourceCastNode);
			case QueryNodeKind.All:
				return this.BindAllNode(node as AllNode);
			case QueryNodeKind.ResourceRangeVariableReference:
				return this.BindRangeVariable((node as ResourceRangeVariableReferenceNode).RangeVariable);
			case QueryNodeKind.SingleResourceFunctionCall:
				return this.BindSingleResourceFunctionCallNode(node as SingleResourceFunctionCallNode);
			case QueryNodeKind.SingleComplexNode:
				return this.BindSingleComplexNode(node as SingleComplexNode);
			case QueryNodeKind.Count:
				return this.BindCountNode(node as CountNode);
			case QueryNodeKind.In:
				return this.BindInNode(node as InNode);
			}
			throw Error.NotSupported(SRResources.QueryNodeBindingNotSupported, new object[]
			{
				node.Kind,
				typeof(FilterBinder).Name
			});
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00023584 File Offset: 0x00021784
		private Expression BindCollectionNode(CollectionNode node)
		{
			QueryNodeKind kind = node.Kind;
			if (kind <= QueryNodeKind.CollectionResourceCast)
			{
				if (kind == QueryNodeKind.CollectionPropertyAccess)
				{
					return this.BindCollectionPropertyAccessNode(node as CollectionPropertyAccessNode);
				}
				if (kind == QueryNodeKind.CollectionNavigationNode)
				{
					CollectionNavigationNode collectionNavigationNode = node as CollectionNavigationNode;
					return this.BindNavigationPropertyNode(collectionNavigationNode.Source, collectionNavigationNode.NavigationProperty);
				}
				if (kind == QueryNodeKind.CollectionResourceCast)
				{
					return this.BindCollectionResourceCastNode(node as CollectionResourceCastNode);
				}
			}
			else if (kind <= QueryNodeKind.CollectionOpenPropertyAccess)
			{
				if (kind - QueryNodeKind.CollectionFunctionCall > 1 && kind != QueryNodeKind.CollectionOpenPropertyAccess)
				{
				}
			}
			else
			{
				if (kind == QueryNodeKind.CollectionComplexNode)
				{
					return this.BindCollectionComplexNode(node as CollectionComplexNode);
				}
				if (kind == QueryNodeKind.CollectionConstant)
				{
					return this.BindCollectionConstantNode(node as CollectionConstantNode);
				}
			}
			throw Error.NotSupported(SRResources.QueryNodeBindingNotSupported, new object[]
			{
				node.Kind,
				typeof(FilterBinder).Name
			});
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002364C File Offset: 0x0002184C
		private Type RetrieveClrTypeForConstant(IEdmTypeReference edmTypeReference, ref object value)
		{
			Type type = EdmLibHelpers.GetClrType(edmTypeReference, base.Model, base.InternalAssembliesResolver);
			if (value != null && edmTypeReference != null && edmTypeReference.IsEnum())
			{
				string value2 = ((ODataEnumValue)value).Value;
				type = Nullable.GetUnderlyingType(type) ?? type;
				value = Enum.Parse(type, value2);
			}
			if (edmTypeReference != null && edmTypeReference.IsNullable && (edmTypeReference.IsDate() || edmTypeReference.IsTimeOfDay()))
			{
				type = Nullable.GetUnderlyingType(type) ?? type;
			}
			return type;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000236C8 File Offset: 0x000218C8
		private ParameterExpression HandleLambdaParameters(IEnumerable<RangeVariable> rangeVariables)
		{
			ParameterExpression parameterExpression = null;
			this.EnterLambdaScope();
			Dictionary<string, ParameterExpression> dictionary = new Dictionary<string, ParameterExpression>();
			foreach (RangeVariable rangeVariable in rangeVariables)
			{
				ParameterExpression parameterExpression2;
				if (!this._lambdaParameters.TryGetValue(rangeVariable.Name, out parameterExpression2))
				{
					IEdmTypeReference edmTypeReference = rangeVariable.TypeReference;
					IEdmCollectionTypeReference edmCollectionTypeReference = edmTypeReference as IEdmCollectionTypeReference;
					if (edmCollectionTypeReference != null)
					{
						IEdmCollectionType edmCollectionType = edmCollectionTypeReference.Definition as IEdmCollectionType;
						if (edmCollectionType != null)
						{
							edmTypeReference = edmCollectionType.ElementType;
						}
					}
					parameterExpression2 = Expression.Parameter(EdmLibHelpers.GetClrType(edmTypeReference, base.Model, base.InternalAssembliesResolver), rangeVariable.Name);
					parameterExpression = parameterExpression2;
				}
				dictionary.Add(rangeVariable.Name, parameterExpression2);
			}
			this._lambdaParameters = dictionary;
			return parameterExpression;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00023798 File Offset: 0x00021998
		private void EnterLambdaScope()
		{
			this._parametersStack.Push(this._lambdaParameters);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000237AB File Offset: 0x000219AB
		private void ExitLamdbaScope()
		{
			if (this._parametersStack.Count != 0)
			{
				this._lambdaParameters = this._parametersStack.Pop();
				return;
			}
			this._lambdaParameters = null;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000237D4 File Offset: 0x000219D4
		private static Expression Any(Expression source, Expression filter)
		{
			Type type;
			TypeHelper.IsCollection(source.Type, out type);
			if (filter == null)
			{
				if (ExpressionBinderBase.IsIQueryable(source.Type))
				{
					return Expression.Call(null, ExpressionHelperMethods.QueryableEmptyAnyGeneric.MakeGenericMethod(new Type[] { type }), new Expression[] { source });
				}
				return Expression.Call(null, ExpressionHelperMethods.EnumerableEmptyAnyGeneric.MakeGenericMethod(new Type[] { type }), new Expression[] { source });
			}
			else
			{
				if (ExpressionBinderBase.IsIQueryable(source.Type))
				{
					return Expression.Call(null, ExpressionHelperMethods.QueryableNonEmptyAnyGeneric.MakeGenericMethod(new Type[] { type }), source, filter);
				}
				return Expression.Call(null, ExpressionHelperMethods.EnumerableNonEmptyAnyGeneric.MakeGenericMethod(new Type[] { type }), source, filter);
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00023890 File Offset: 0x00021A90
		private static Expression All(Expression source, Expression filter)
		{
			Type type;
			TypeHelper.IsCollection(source.Type, out type);
			if (ExpressionBinderBase.IsIQueryable(source.Type))
			{
				return Expression.Call(null, ExpressionHelperMethods.QueryableAllGeneric.MakeGenericMethod(new Type[] { type }), source, filter);
			}
			return Expression.Call(null, ExpressionHelperMethods.EnumerableAllGeneric.MakeGenericMethod(new Type[] { type }), source, filter);
		}

		// Token: 0x040002B8 RID: 696
		private const string ODataItParameterName = "$it";

		// Token: 0x040002B9 RID: 697
		private Stack<Dictionary<string, ParameterExpression>> _parametersStack = new Stack<Dictionary<string, ParameterExpression>>();

		// Token: 0x040002BA RID: 698
		private Dictionary<string, ParameterExpression> _lambdaParameters;

		// Token: 0x040002BB RID: 699
		private Type _filterType;
	}
}
