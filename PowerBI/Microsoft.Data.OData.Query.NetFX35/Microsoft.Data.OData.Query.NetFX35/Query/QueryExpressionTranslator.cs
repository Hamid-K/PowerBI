using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Metadata;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000037 RID: 55
	public abstract class QueryExpressionTranslator
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00007013 File Offset: 0x00005213
		protected QueryExpressionTranslator(bool nullPropagationRequired, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			this.nullPropagationRequired = nullPropagationRequired;
			this.model = model;
			this.parameterNodeDefinitions = new Stack<KeyValuePair<ParameterQueryNode, Expression>>();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007040 File Offset: 0x00005240
		public Expression Translate(QueryNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(node, "node");
			Type type = null;
			CollectionQueryNode collectionQueryNode = node as CollectionQueryNode;
			SingleValueQueryNode singleValueQueryNode = node as SingleValueQueryNode;
			Expression expression;
			if (collectionQueryNode != null)
			{
				if (collectionQueryNode.ItemType == null)
				{
					throw new ODataException(Strings.QueryExpressionTranslator_CollectionQueryNodeWithoutItemType(node.Kind));
				}
				bool flag = false;
				QueryNodeKind kind = node.Kind;
				switch (kind)
				{
				case QueryNodeKind.Extension:
					expression = this.TranslateExtension(node);
					goto IL_011B;
				case QueryNodeKind.QueryDescriptor:
					break;
				case QueryNodeKind.EntitySet:
					flag = true;
					expression = this.TranslateEntitySet((EntitySetQueryNode)node);
					goto IL_011B;
				default:
					switch (kind)
					{
					case QueryNodeKind.CollectionServiceOperation:
						flag = true;
						expression = this.TranslateCollectionServiceOperation((CollectionServiceOperationQueryNode)node);
						goto IL_011B;
					case QueryNodeKind.SingleValueServiceOperation:
					case QueryNodeKind.UncomposableServiceOperation:
					case QueryNodeKind.Parameter:
						break;
					case QueryNodeKind.Filter:
						flag = true;
						expression = this.TranslateFilter((FilterQueryNode)node);
						goto IL_011B;
					case QueryNodeKind.Skip:
						flag = true;
						expression = this.TranslateSkip((SkipQueryNode)node);
						goto IL_011B;
					case QueryNodeKind.Top:
						flag = true;
						expression = this.TranslateTop((TopQueryNode)node);
						goto IL_011B;
					default:
						if (kind == QueryNodeKind.OrderBy)
						{
							flag = true;
							expression = this.TranslateOrderBy((OrderByQueryNode)node);
							goto IL_011B;
						}
						break;
					}
					break;
				}
				throw new ODataException(Strings.QueryExpressionTranslator_UnsupportedQueryNodeKind(node.Kind));
				IL_011B:
				if (flag)
				{
					type = typeof(IQueryable).MakeGenericType(new Type[] { collectionQueryNode.ItemType.GetInstanceType(this.model) });
				}
				else
				{
					type = typeof(IEnumerable).MakeGenericType(new Type[] { collectionQueryNode.ItemType.GetInstanceType(this.model) });
				}
			}
			else if (singleValueQueryNode != null)
			{
				IEdmTypeReference typeReference = singleValueQueryNode.TypeReference;
				if (typeReference == null && singleValueQueryNode.Kind != QueryNodeKind.Constant && singleValueQueryNode.Kind != QueryNodeKind.UnaryOperator && singleValueQueryNode.Kind != QueryNodeKind.BinaryOperator)
				{
					throw new ODataException(Strings.QueryExpressionTranslator_SingleValueQueryNodeWithoutTypeReference(node.Kind));
				}
				switch (node.Kind)
				{
				case QueryNodeKind.Extension:
					expression = this.TranslateExtension(node);
					goto IL_038F;
				case QueryNodeKind.KeyLookup:
					expression = this.TranslateKeyLookup((KeyLookupQueryNode)node);
					goto IL_038F;
				case QueryNodeKind.Constant:
					expression = this.TranslateConstant((ConstantQueryNode)node);
					type = ((typeReference == null) ? null : typeReference.GetInstanceType(this.model));
					goto IL_038F;
				case QueryNodeKind.Convert:
					expression = this.TranslateConvert((ConvertQueryNode)node);
					type = typeReference.GetInstanceType(this.model);
					goto IL_038F;
				case QueryNodeKind.SingleValueServiceOperation:
					expression = this.TranslateSingleValueServiceOperation((SingleValueServiceOperationQueryNode)node);
					goto IL_038F;
				case QueryNodeKind.Parameter:
					expression = this.TranslateParameter((ParameterQueryNode)node);
					type = typeReference.GetInstanceType(this.model);
					goto IL_038F;
				case QueryNodeKind.BinaryOperator:
					expression = this.TranslateBinaryOperator((BinaryOperatorQueryNode)node);
					type = ((typeReference == null) ? null : typeReference.GetInstanceType(this.model));
					goto IL_038F;
				case QueryNodeKind.UnaryOperator:
					expression = this.TranslateUnaryOperator((UnaryOperatorQueryNode)node);
					type = ((typeReference == null) ? null : typeReference.GetInstanceType(this.model));
					goto IL_038F;
				case QueryNodeKind.PropertyAccess:
					expression = this.TranslatePropertyAccess((PropertyAccessQueryNode)node);
					type = typeReference.GetInstanceType(this.model);
					goto IL_038F;
				case QueryNodeKind.SingleValueFunctionCall:
					expression = this.TranslateSingleValueFunctionCall((SingleValueFunctionCallQueryNode)node);
					type = singleValueQueryNode.TypeReference.GetInstanceType(this.model);
					goto IL_038F;
				}
				throw new ODataException(Strings.QueryExpressionTranslator_UnsupportedQueryNodeKind(node.Kind));
			}
			else
			{
				QueryNodeKind kind2 = node.Kind;
				if (kind2 != QueryNodeKind.Extension)
				{
					throw new ODataException(Strings.QueryExpressionTranslator_UnsupportedQueryNodeKind(node.Kind));
				}
				expression = this.TranslateExtension(node);
			}
			IL_038F:
			if (expression == null)
			{
				throw new ODataException(Strings.QueryExpressionTranslator_NodeTranslatedToNull(node.Kind));
			}
			if (type != null && !type.IsAssignableFrom(expression.Type) && (!this.nullPropagationRequired || !TypeUtils.AreTypesEquivalent(TypeUtils.GetNonNullableType(expression.Type), type)))
			{
				throw new ODataException(Strings.QueryExpressionTranslator_NodeTranslatedToWrongType(node.Kind, expression.Type, type));
			}
			return expression;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000743F File Offset: 0x0000563F
		protected virtual Expression TranslateExtension(QueryNode extensionNode)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(extensionNode, "extensionNode");
			throw new ODataException(Strings.QueryExpressionTranslator_UnsupportedExtensionNode);
		}

		// Token: 0x06000131 RID: 305
		protected abstract Expression TranslateEntitySet(EntitySetQueryNode entitySetNode);

		// Token: 0x06000132 RID: 306
		protected abstract Expression TranslateCollectionServiceOperation(CollectionServiceOperationQueryNode serviceOperationNode);

		// Token: 0x06000133 RID: 307
		protected abstract Expression TranslateSingleValueServiceOperation(SingleValueServiceOperationQueryNode serviceOperationNode);

		// Token: 0x06000134 RID: 308 RVA: 0x00007458 File Offset: 0x00005658
		protected virtual Expression TranslateConstant(ConstantQueryNode constantNode)
		{
			ExceptionUtils.CheckArgumentNotNull<ConstantQueryNode>(constantNode, "constantNode");
			if (constantNode.TypeReference == null)
			{
				return QueryExpressionTranslator.nullLiteralExpression;
			}
			if (!constantNode.TypeReference.IsODataPrimitiveTypeKind())
			{
				throw new ODataException(Strings.QueryExpressionTranslator_ConstantNonPrimitive(constantNode.TypeReference.ODataFullName()));
			}
			return Expression.Constant(constantNode.Value, constantNode.TypeReference.GetInstanceType(this.model));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000074C0 File Offset: 0x000056C0
		protected virtual Expression TranslateConvert(ConvertQueryNode convertNode)
		{
			ExceptionUtils.CheckArgumentNotNull<ConvertQueryNode>(convertNode, "convertNode");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(convertNode.TypeReference, "convertNode.Type");
			if (!convertNode.TypeReference.GetCanReflectOnInstanceType(this.model))
			{
				throw new NotImplementedException();
			}
			Expression expression = this.Translate(convertNode.Source);
			if (expression != QueryExpressionTranslator.nullLiteralExpression)
			{
				return Expression.Convert(expression, convertNode.TypeReference.GetInstanceType(this.model));
			}
			return Expression.Constant(null, convertNode.TypeReference.GetInstanceType(this.model));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007578 File Offset: 0x00005778
		protected virtual Expression TranslateKeyLookup(KeyLookupQueryNode keyLookupNode)
		{
			ExceptionUtils.CheckArgumentNotNull<KeyLookupQueryNode>(keyLookupNode, "keyLookupNode");
			ExceptionUtils.CheckArgumentNotNull<CollectionQueryNode>(keyLookupNode.Collection, "keyLookupNode.Collection");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(keyLookupNode.Collection.ItemType, "keyLookupNode.Collection.ItemType");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<KeyPropertyValue>>(keyLookupNode.KeyPropertyValues, "keyLookupNode.KeyPropertyValues");
			IEdmTypeReference itemType = keyLookupNode.Collection.ItemType;
			if (keyLookupNode.Collection.ItemType.TypeKind() != EdmTypeKind.Entity)
			{
				throw new ODataException(Strings.QueryExpressionTranslator_KeyLookupOnlyOnEntities(keyLookupNode.Collection.ItemType.ODataFullName(), keyLookupNode.Collection.ItemType.TypeKind()));
			}
			IEdmEntityTypeReference edmEntityTypeReference = itemType.AsEntityOrNull();
			Expression expression = this.Translate(keyLookupNode.Collection);
			Type type = typeof(IQueryable).MakeGenericType(new Type[] { itemType.GetInstanceType(this.model) });
			if (!type.IsAssignableFrom(expression.Type))
			{
				throw new ODataException(Strings.QueryExpressionTranslator_KeyLookupOnlyOnQueryable(expression.Type, type));
			}
			ParameterExpression parameterExpression = Expression.Parameter(itemType.GetInstanceType(this.model), "it");
			Expression expression2 = null;
			List<KeyPropertyValue> list = new List<KeyPropertyValue>(keyLookupNode.KeyPropertyValues);
			using (IEnumerator<IEdmStructuralProperty> enumerator = edmEntityTypeReference.Key().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					QueryExpressionTranslator.<>c__DisplayClass3 CS$<>8__locals1 = new QueryExpressionTranslator.<>c__DisplayClass3();
					CS$<>8__locals1.keyProperty = enumerator.Current;
					KeyPropertyValue keyPropertyValue = null;
					foreach (KeyPropertyValue keyPropertyValue2 in Enumerable.Where<KeyPropertyValue>(list, (KeyPropertyValue kpv) => kpv.KeyProperty == CS$<>8__locals1.keyProperty))
					{
						if (keyPropertyValue != null)
						{
							throw new ODataException(Strings.QueryExpressionTranslator_KeyLookupWithoutKeyProperty(CS$<>8__locals1.keyProperty.Name, itemType.ODataFullName()));
						}
						keyPropertyValue = keyPropertyValue2;
					}
					if (keyPropertyValue == null)
					{
						throw new ODataException(Strings.QueryExpressionTranslator_KeyLookupWithoutKeyProperty(CS$<>8__locals1.keyProperty.Name, itemType.ODataFullName()));
					}
					if (keyPropertyValue.KeyProperty == null || !Enumerable.Any<IEdmStructuralProperty>(edmEntityTypeReference.Key(), (IEdmStructuralProperty k) => k == keyPropertyValue.KeyProperty))
					{
						throw new ODataException(Strings.QueryExpressionTranslator_KeyPropertyValueWithoutProperty);
					}
					if (keyPropertyValue.KeyValue == null || keyPropertyValue.KeyValue.TypeReference == null || !keyPropertyValue.KeyValue.TypeReference.IsEquivalentTo(keyPropertyValue.KeyProperty.Type))
					{
						throw new ODataException(Strings.QueryExpressionTranslator_KeyPropertyValueWithWrongValue(keyPropertyValue.KeyProperty.Name));
					}
					Expression expression3 = this.CreatePropertyAccessExpression(parameterExpression, edmEntityTypeReference, keyPropertyValue.KeyProperty);
					Expression expression4 = this.Translate(keyPropertyValue.KeyValue);
					Expression expression5 = Expression.Equal(expression3, expression4);
					if (expression2 == null)
					{
						expression2 = expression5;
					}
					else
					{
						expression2 = Expression.AndAlso(expression2, expression5);
					}
				}
			}
			if (expression2 == null)
			{
				throw new ODataException(Strings.QueryExpressionTranslator_KeyLookupWithNoKeyValues);
			}
			return Expression.Call(typeof(Queryable), "Where", new Type[] { itemType.GetInstanceType(this.model) }, new Expression[]
			{
				expression,
				Expression.Quote(Expression.Lambda(expression2, new ParameterExpression[] { parameterExpression }))
			});
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007914 File Offset: 0x00005B14
		protected virtual Expression TranslateSkip(SkipQueryNode skipNode)
		{
			ExceptionUtils.CheckArgumentNotNull<SkipQueryNode>(skipNode, "skipNode");
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(skipNode.Amount, "skipNode.Amount");
			Expression expression = this.Translate(skipNode.Collection);
			Expression expression2 = this.Translate(skipNode.Amount);
			return Expression.Call(typeof(Queryable), "Skip", new Type[] { skipNode.Collection.ItemType.GetInstanceType(this.model) }, new Expression[] { expression, expression2 });
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000799C File Offset: 0x00005B9C
		protected virtual Expression TranslateTop(TopQueryNode topNode)
		{
			ExceptionUtils.CheckArgumentNotNull<TopQueryNode>(topNode, "topNode");
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(topNode.Amount, "topNode.Amount");
			Expression expression = this.Translate(topNode.Collection);
			Expression expression2 = this.Translate(topNode.Amount);
			return Expression.Call(typeof(Queryable), "Take", new Type[] { topNode.Collection.ItemType.GetInstanceType(this.model) }, new Expression[] { expression, expression2 });
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007A24 File Offset: 0x00005C24
		protected virtual Expression TranslateFilter(FilterQueryNode filterNode)
		{
			ExceptionUtils.CheckArgumentNotNull<FilterQueryNode>(filterNode, "filterNode");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(filterNode.ItemType, "filterNode.ItemType");
			ExceptionUtils.CheckArgumentNotNull<CollectionQueryNode>(filterNode.Collection, "filterNode.Collection");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(filterNode.Collection.ItemType, "filterNode.Collection.ItemType");
			ExceptionUtils.CheckArgumentNotNull<SingleValueQueryNode>(filterNode.Expression, "filterNode.Expression");
			ExceptionUtils.CheckArgumentNotNull<ParameterQueryNode>(filterNode.Parameter, "filterNode.Parameter");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(filterNode.Parameter.TypeReference, "filterNode.Parameter.Type");
			ParameterExpression parameterExpression = Expression.Parameter(filterNode.Parameter.TypeReference.GetInstanceType(this.model), "it");
			Expression expression = this.Translate(filterNode.Collection);
			Type type = typeof(IQueryable).MakeGenericType(new Type[] { parameterExpression.Type });
			if (!type.IsAssignableFrom(expression.Type))
			{
				throw new ODataException(Strings.QueryExpressionTranslator_FilterCollectionOfWrongType(expression.Type, type));
			}
			this.parameterNodeDefinitions.Push(new KeyValuePair<ParameterQueryNode, Expression>(filterNode.Parameter, parameterExpression));
			Expression expression2 = this.Translate(filterNode.Expression);
			this.parameterNodeDefinitions.Pop();
			if (expression2 == QueryExpressionTranslator.nullLiteralExpression)
			{
				expression2 = QueryExpressionTranslator.falseLiteralExpression;
			}
			else if (expression2.Type == typeof(bool?))
			{
				Expression expression3 = Expression.Equal(expression2, Expression.Constant(null, typeof(bool?)));
				expression2 = Expression.Condition(expression3, QueryExpressionTranslator.falseLiteralExpression, Expression.Property(expression2, "Value"));
			}
			if (expression2.Type != typeof(bool))
			{
				throw new ODataException(Strings.QueryExpressionTranslator_FilterExpressionOfWrongType(expression2.Type));
			}
			return Expression.Call(typeof(Queryable), "Where", new Type[] { parameterExpression.Type }, new Expression[]
			{
				expression,
				Expression.Quote(Expression.Lambda(expression2, new ParameterExpression[] { parameterExpression }))
			});
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007C14 File Offset: 0x00005E14
		protected virtual Expression TranslateBinaryOperator(BinaryOperatorQueryNode binaryOperatorNode)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorQueryNode>(binaryOperatorNode, "binaryOperatorNode");
			ExceptionUtils.CheckArgumentNotNull<SingleValueQueryNode>(binaryOperatorNode.Left, "binaryOperatorNode.Left");
			ExceptionUtils.CheckArgumentNotNull<SingleValueQueryNode>(binaryOperatorNode.Right, "binaryOperatorNode.Right");
			Expression expression = this.Translate(binaryOperatorNode.Left);
			Expression expression2 = this.Translate(binaryOperatorNode.Right);
			if (expression == QueryExpressionTranslator.nullLiteralExpression && expression2 == QueryExpressionTranslator.nullLiteralExpression)
			{
				if (binaryOperatorNode.OperatorKind == BinaryOperatorKind.Equal)
				{
					return QueryExpressionTranslator.trueLiteralExpression;
				}
				if (binaryOperatorNode.OperatorKind == BinaryOperatorKind.NotEqual)
				{
					return QueryExpressionTranslator.falseLiteralExpression;
				}
				return QueryExpressionTranslator.nullLiteralExpression;
			}
			else
			{
				if (binaryOperatorNode.TypeReference == null)
				{
					throw new ODataException(Strings.QueryExpressionTranslator_SingleValueQueryNodeWithoutTypeReference(binaryOperatorNode.Kind));
				}
				if (this.nullPropagationRequired)
				{
					QueryExpressionTranslator.HandleBinaryOperatorNullPropagation(ref expression, ref expression2);
				}
				switch (binaryOperatorNode.OperatorKind)
				{
				case BinaryOperatorKind.Or:
					return Expression.OrElse(expression, expression2);
				case BinaryOperatorKind.And:
					return Expression.AndAlso(expression, expression2);
				case BinaryOperatorKind.Equal:
					if (expression.Type == typeof(byte[]))
					{
						return Expression.Equal(expression, expression2, false, DataServiceProviderMethods.AreByteArraysEqualMethodInfo);
					}
					return Expression.Equal(expression, expression2);
				case BinaryOperatorKind.NotEqual:
					if (expression.Type == typeof(byte[]))
					{
						return Expression.NotEqual(expression, expression2, false, DataServiceProviderMethods.AreByteArraysNotEqualMethodInfo);
					}
					return Expression.NotEqual(expression, expression2);
				case BinaryOperatorKind.GreaterThan:
					return QueryExpressionTranslator.CreateGreaterThanExpression(expression, expression2);
				case BinaryOperatorKind.GreaterThanOrEqual:
					return QueryExpressionTranslator.CreateGreaterThanOrEqualExpression(expression, expression2);
				case BinaryOperatorKind.LessThan:
					return QueryExpressionTranslator.CreateLessThanExpression(expression, expression2);
				case BinaryOperatorKind.LessThanOrEqual:
					return QueryExpressionTranslator.CreateLessThanOrEqualExpression(expression, expression2);
				case BinaryOperatorKind.Add:
					return Expression.Add(expression, expression2);
				case BinaryOperatorKind.Subtract:
					return Expression.Subtract(expression, expression2);
				case BinaryOperatorKind.Multiply:
					return Expression.Multiply(expression, expression2);
				case BinaryOperatorKind.Divide:
					return Expression.Divide(expression, expression2);
				case BinaryOperatorKind.Modulo:
					return Expression.Modulo(expression, expression2);
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.QueryExpressionTranslator_TranslateBinaryOperator_UnreachableCodepath));
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007DC8 File Offset: 0x00005FC8
		protected virtual Expression TranslateUnaryOperator(UnaryOperatorQueryNode unaryOperatorNode)
		{
			ExceptionUtils.CheckArgumentNotNull<UnaryOperatorQueryNode>(unaryOperatorNode, "unaryOperatorNode");
			ExceptionUtils.CheckArgumentNotNull<SingleValueQueryNode>(unaryOperatorNode.Operand, "unaryOperatorNode.Operand");
			Expression expression = this.Translate(unaryOperatorNode.Operand);
			if (expression == QueryExpressionTranslator.nullLiteralExpression)
			{
				return QueryExpressionTranslator.nullLiteralExpression;
			}
			if (unaryOperatorNode.TypeReference == null)
			{
				throw new ODataException(Strings.QueryExpressionTranslator_SingleValueQueryNodeWithoutTypeReference(unaryOperatorNode.Kind));
			}
			switch (unaryOperatorNode.OperatorKind)
			{
			case UnaryOperatorKind.Negate:
				return Expression.Negate(expression);
			case UnaryOperatorKind.Not:
				if (expression.Type == typeof(bool) || expression.Type == typeof(bool?))
				{
					return Expression.Not(expression);
				}
				throw new ODataException(Strings.QueryExpressionTranslator_UnaryNotOperandNotBoolean(expression.Type));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.QueryExpressionTranslator_TranslateUnaryOperator_UnreachableCodepath));
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007E94 File Offset: 0x00006094
		protected virtual Expression TranslatePropertyAccess(PropertyAccessQueryNode propertyAccessNode)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertyAccessQueryNode>(propertyAccessNode, "propertyAccessNode");
			ExceptionUtils.CheckArgumentNotNull<SingleValueQueryNode>(propertyAccessNode.Source, "propertyAccessNode.Source");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(propertyAccessNode.Source.TypeReference, "propertyAccessNode.Source.TypeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(propertyAccessNode.Property, "propertyAccessNode.Property");
			Expression expression = this.Translate(propertyAccessNode.Source);
			if (!propertyAccessNode.Source.TypeReference.GetInstanceType(this.model).IsAssignableFrom(expression.Type))
			{
				throw new ODataException(Strings.QueryExpressionTranslator_PropertyAccessSourceWrongType(expression.Type, propertyAccessNode.Source.TypeReference.GetInstanceType(this.model)));
			}
			if (!propertyAccessNode.Property.GetCanReflectOnInstanceTypeProperty(this.model))
			{
				throw new NotImplementedException();
			}
			IEdmTypeReference typeReference = propertyAccessNode.Source.TypeReference;
			IEdmStructuredTypeReference edmStructuredTypeReference = typeReference.AsStructuredOrNull();
			if (edmStructuredTypeReference == null)
			{
				throw new ODataException(Strings.QueryExpressionTranslator_PropertyAccessSourceNotStructured(typeReference.TypeKind()));
			}
			Expression expression2 = Expression.Property(expression, edmStructuredTypeReference.GetPropertyInfo(propertyAccessNode.Property, this.model));
			if (this.nullPropagationRequired && expression.NodeType != 38 && TypeUtils.TypeAllowsNull(expression.Type))
			{
				Expression expression3 = Expression.Equal(expression, Expression.Constant(null, expression.Type));
				Type nullableType = TypeUtils.GetNullableType(expression2.Type);
				if (nullableType != expression2.Type)
				{
					expression2 = Expression.Convert(expression2, nullableType);
				}
				expression2 = Expression.Condition(expression3, Expression.Constant(null, nullableType), expression2);
			}
			return expression2;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00008000 File Offset: 0x00006200
		protected virtual Expression TranslateParameter(ParameterQueryNode parameterNode)
		{
			ExceptionUtils.CheckArgumentNotNull<ParameterQueryNode>(parameterNode, "parameterNode");
			if (this.parameterNodeDefinitions.Count == 0)
			{
				throw new ODataException(Strings.QueryExpressionTranslator_ParameterNotDefinedInScope);
			}
			KeyValuePair<ParameterQueryNode, Expression> keyValuePair = this.parameterNodeDefinitions.Peek();
			if (!object.ReferenceEquals(keyValuePair.Key, parameterNode))
			{
				throw new ODataException(Strings.QueryExpressionTranslator_ParameterNotDefinedInScope);
			}
			return keyValuePair.Value;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00008060 File Offset: 0x00006260
		protected virtual Expression TranslateOrderBy(OrderByQueryNode orderByNode)
		{
			ExceptionUtils.CheckArgumentNotNull<OrderByQueryNode>(orderByNode, "orderByNode");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(orderByNode.ItemType, "orderByNode.ItemType");
			ExceptionUtils.CheckArgumentNotNull<CollectionQueryNode>(orderByNode.Collection, "orderByNode.Collection");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(orderByNode.Collection.ItemType, "orderByNode.Collection.ItemType");
			ExceptionUtils.CheckArgumentNotNull<SingleValueQueryNode>(orderByNode.Expression, "orderByNode.Expression");
			ExceptionUtils.CheckArgumentNotNull<ParameterQueryNode>(orderByNode.Parameter, "orderByNode.Parameter");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(orderByNode.Parameter.TypeReference, "orderByNode.Parameter.Type");
			ParameterExpression parameterExpression = Expression.Parameter(orderByNode.Parameter.TypeReference.GetInstanceType(this.model), "element");
			Expression expression = this.Translate(orderByNode.Collection);
			Type type = typeof(IQueryable).MakeGenericType(new Type[] { parameterExpression.Type });
			if (!type.IsAssignableFrom(expression.Type))
			{
				throw new ODataException(Strings.QueryExpressionTranslator_OrderByCollectionOfWrongType(expression.Type, type));
			}
			this.parameterNodeDefinitions.Push(new KeyValuePair<ParameterQueryNode, Expression>(orderByNode.Parameter, parameterExpression));
			Expression expression2 = this.Translate(orderByNode.Expression);
			this.parameterNodeDefinitions.Pop();
			string text = ((orderByNode.Direction == OrderByDirection.Ascending) ? "OrderBy" : "OrderByDescending");
			if (expression.NodeType == 6)
			{
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
				if ((methodCallExpression.Method.DeclaringType == typeof(Queryable) && methodCallExpression.Method.Name == "OrderBy") || methodCallExpression.Method.Name == "OrderByDescending" || methodCallExpression.Method.Name == "ThenBy" || methodCallExpression.Method.Name == "ThenByDescending")
				{
					text = ((orderByNode.Direction == OrderByDirection.Ascending) ? "ThenBy" : "ThenByDescending");
				}
			}
			return Expression.Call(typeof(Queryable), text, new Type[] { parameterExpression.Type, expression2.Type }, new Expression[]
			{
				expression,
				Expression.Quote(Expression.Lambda(expression2, new ParameterExpression[] { parameterExpression }))
			});
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000082A0 File Offset: 0x000064A0
		protected virtual Expression TranslateSingleValueFunctionCall(SingleValueFunctionCallQueryNode functionCallNode)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueFunctionCallQueryNode>(functionCallNode, "functionCallNode");
			ExceptionUtils.CheckArgumentNotNull<string>(functionCallNode.Name, "functionCallNode.Name");
			List<SingleValueQueryNode> list = new List<SingleValueQueryNode>();
			if (functionCallNode.Arguments != null)
			{
				foreach (QueryNode queryNode in functionCallNode.Arguments)
				{
					SingleValueQueryNode singleValueQueryNode = queryNode as SingleValueQueryNode;
					if (singleValueQueryNode == null)
					{
						throw new ODataException(Strings.QueryExpressionTranslator_FunctionArgumentNotSingleValue(functionCallNode.Name));
					}
					list.Add(singleValueQueryNode);
				}
			}
			BuiltInFunctionSignature[] array;
			if (!BuiltInFunctions.TryGetBuiltInFunction(functionCallNode.Name, out array))
			{
				throw new ODataException(Strings.QueryExpressionTranslator_UnknownFunction(functionCallNode.Name));
			}
			BuiltInFunctionSignature builtInFunctionSignature = (BuiltInFunctionSignature)TypePromotionUtils.FindExactFunctionSignature(array, Enumerable.ToArray<IEdmTypeReference>(Enumerable.Select<SingleValueQueryNode, IEdmTypeReference>(list, (SingleValueQueryNode argumentNode) => argumentNode.TypeReference)));
			if (builtInFunctionSignature == null)
			{
				throw new ODataException(Strings.QueryExpressionTranslator_NoApplicableFunctionFound(functionCallNode.Name, BuiltInFunctions.BuildFunctionSignatureListDescription(functionCallNode.Name, array)));
			}
			Expression[] array2 = new Expression[list.Count];
			Expression[] array3 = new Expression[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				SingleValueQueryNode singleValueQueryNode2 = list[i];
				Expression expression = this.Translate(singleValueQueryNode2);
				array2[i] = expression;
				if (TypeUtils.IsNullableType(expression.Type))
				{
					array3[i] = Expression.Convert(expression, TypeUtils.GetNonNullableType(expression.Type));
				}
				else
				{
					array3[i] = expression;
				}
			}
			Expression expression2 = builtInFunctionSignature.BuildExpression(array3);
			if (this.nullPropagationRequired)
			{
				foreach (Expression expression3 in array2)
				{
					if (expression3 != this.parameterNodeDefinitions.Peek().Value && TypeUtils.TypeAllowsNull(expression3.Type))
					{
						ConstantExpression constantExpression = expression3 as ConstantExpression;
						if (constantExpression == null || constantExpression.Value == null)
						{
							Expression expression4 = Expression.Equal(expression3, Expression.Constant(null, expression3.Type));
							Expression expression5 = expression2;
							if (!TypeUtils.TypeAllowsNull(expression5.Type))
							{
								expression5 = Expression.Convert(expression5, typeof(Nullable).MakeGenericType(new Type[] { expression5.Type }));
							}
							Expression expression6 = Expression.Constant(null, expression5.Type);
							expression2 = Expression.Condition(expression4, expression6, expression5);
						}
					}
				}
			}
			return expression2;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00008510 File Offset: 0x00006710
		private static Expression CreateGreaterThanExpression(Expression left, Expression right)
		{
			MethodInfo comparisonMethodInfo = QueryExpressionTranslator.GetComparisonMethodInfo(left.Type);
			if (comparisonMethodInfo != null)
			{
				left = Expression.Call(null, comparisonMethodInfo, new Expression[] { left, right });
				right = Expression.Constant(0, typeof(int));
			}
			return Expression.GreaterThan(left, right);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008564 File Offset: 0x00006764
		private static Expression CreateGreaterThanOrEqualExpression(Expression left, Expression right)
		{
			MethodInfo comparisonMethodInfo = QueryExpressionTranslator.GetComparisonMethodInfo(left.Type);
			if (comparisonMethodInfo != null)
			{
				left = Expression.Call(null, comparisonMethodInfo, new Expression[] { left, right });
				right = Expression.Constant(0, typeof(int));
			}
			return Expression.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000085B8 File Offset: 0x000067B8
		private static Expression CreateLessThanExpression(Expression left, Expression right)
		{
			MethodInfo comparisonMethodInfo = QueryExpressionTranslator.GetComparisonMethodInfo(left.Type);
			if (comparisonMethodInfo != null)
			{
				left = Expression.Call(null, comparisonMethodInfo, new Expression[] { left, right });
				right = Expression.Constant(0, typeof(int));
			}
			return Expression.LessThan(left, right);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000860C File Offset: 0x0000680C
		private static Expression CreateLessThanOrEqualExpression(Expression left, Expression right)
		{
			MethodInfo comparisonMethodInfo = QueryExpressionTranslator.GetComparisonMethodInfo(left.Type);
			if (comparisonMethodInfo != null)
			{
				left = Expression.Call(null, comparisonMethodInfo, new Expression[] { left, right });
				right = Expression.Constant(0, typeof(int));
			}
			return Expression.LessThanOrEqual(left, right);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00008660 File Offset: 0x00006860
		private static MethodInfo GetComparisonMethodInfo(Type type)
		{
			if (type == typeof(string))
			{
				return DataServiceProviderMethods.StringCompareMethodInfo;
			}
			if (type == typeof(bool))
			{
				return DataServiceProviderMethods.BoolCompareMethodInfo;
			}
			if (type == typeof(bool?))
			{
				return DataServiceProviderMethods.BoolCompareMethodInfoNullable;
			}
			if (type == typeof(Guid))
			{
				return DataServiceProviderMethods.GuidCompareMethodInfo;
			}
			if (type == typeof(Guid?))
			{
				return DataServiceProviderMethods.GuidCompareMethodInfoNullable;
			}
			return null;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000086D0 File Offset: 0x000068D0
		private static void HandleBinaryOperatorNullPropagation(ref Expression left, ref Expression right)
		{
			if (left.Type != right.Type)
			{
				if (TypeUtils.IsNullableType(left.Type))
				{
					right = Expression.Convert(right, TypeUtils.GetNullableType(right.Type));
					return;
				}
				if (TypeUtils.IsNullableType(right.Type))
				{
					left = Expression.Convert(left, TypeUtils.GetNullableType(left.Type));
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008734 File Offset: 0x00006934
		private Expression CreatePropertyAccessExpression(Expression source, IEdmStructuredTypeReference sourceStructuredType, IEdmProperty property)
		{
			if (property.GetCanReflectOnInstanceTypeProperty(this.model))
			{
				return Expression.Property(source, sourceStructuredType.GetPropertyInfo(property, this.model));
			}
			throw new NotImplementedException();
		}

		// Token: 0x04000169 RID: 361
		private const string WhereMethodName = "Where";

		// Token: 0x0400016A RID: 362
		private const string SkipMethodName = "Skip";

		// Token: 0x0400016B RID: 363
		private const string TakeMethodName = "Take";

		// Token: 0x0400016C RID: 364
		private const string OrderByMethodName = "OrderBy";

		// Token: 0x0400016D RID: 365
		private const string OrderByDescendingMethodName = "OrderByDescending";

		// Token: 0x0400016E RID: 366
		private const string ThenByMethodName = "ThenBy";

		// Token: 0x0400016F RID: 367
		private const string ThenByDescendingMethodName = "ThenByDescending";

		// Token: 0x04000170 RID: 368
		private static readonly Expression nullLiteralExpression = Expression.Constant(null);

		// Token: 0x04000171 RID: 369
		private static readonly Expression trueLiteralExpression = Expression.Constant(true, typeof(bool));

		// Token: 0x04000172 RID: 370
		private static readonly ConstantExpression falseLiteralExpression = Expression.Constant(false);

		// Token: 0x04000173 RID: 371
		private readonly bool nullPropagationRequired;

		// Token: 0x04000174 RID: 372
		private readonly IEdmModel model;

		// Token: 0x04000175 RID: 373
		private Stack<KeyValuePair<ParameterQueryNode, Expression>> parameterNodeDefinitions;
	}
}
