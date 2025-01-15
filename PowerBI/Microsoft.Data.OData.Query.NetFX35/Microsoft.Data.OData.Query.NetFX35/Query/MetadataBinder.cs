using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Metadata;
using Microsoft.Data.Experimental.OData.Query.Metadata;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000047 RID: 71
	public class MetadataBinder
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000974D File Offset: 0x0000794D
		public MetadataBinder(IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			this.model = model;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009767 File Offset: 0x00007967
		protected IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000976F File Offset: 0x0000796F
		public QueryDescriptorQueryNode BindQuery(QueryDescriptorQueryToken queryDescriptorQueryToken)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryDescriptorQueryToken>(queryDescriptorQueryToken, "queryDescriptorQueryToken");
			return (QueryDescriptorQueryNode)this.Bind(queryDescriptorQueryToken);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009788 File Offset: 0x00007988
		protected QueryNode Bind(QueryToken token)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(token, "token");
			QueryNode queryNode;
			switch (token.Kind)
			{
			case QueryTokenKind.Extension:
				queryNode = this.BindExtension(token);
				goto IL_00E0;
			case QueryTokenKind.QueryDescriptor:
				queryNode = this.BindQueryDescriptor((QueryDescriptorQueryToken)token);
				goto IL_00E0;
			case QueryTokenKind.Segment:
				queryNode = this.BindSegment((SegmentQueryToken)token);
				goto IL_00E0;
			case QueryTokenKind.BinaryOperator:
				queryNode = this.BindBinaryOperator((BinaryOperatorQueryToken)token);
				goto IL_00E0;
			case QueryTokenKind.UnaryOperator:
				queryNode = this.BindUnaryOperator((UnaryOperatorQueryToken)token);
				goto IL_00E0;
			case QueryTokenKind.Literal:
				queryNode = this.BindLiteral((LiteralQueryToken)token);
				goto IL_00E0;
			case QueryTokenKind.FunctionCall:
				queryNode = this.BindFunctionCall((FunctionCallQueryToken)token);
				goto IL_00E0;
			case QueryTokenKind.PropertyAccess:
				queryNode = this.BindPropertyAccess((PropertyAccessQueryToken)token);
				goto IL_00E0;
			case QueryTokenKind.QueryOption:
				queryNode = this.BindQueryOption((QueryOptionQueryToken)token);
				goto IL_00E0;
			}
			throw new ODataException(Strings.MetadataBinder_UnsupportedQueryTokenKind(token.Kind));
			IL_00E0:
			if (queryNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BoundNodeCannotBeNull(token.Kind));
			}
			return queryNode;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000988F File Offset: 0x00007A8F
		protected virtual QueryNode BindExtension(QueryToken extensionToken)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(extensionToken, "extensionToken");
			throw new ODataException(Strings.MetadataBinder_UnsupportedExtensionToken);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000098A8 File Offset: 0x00007AA8
		protected virtual QueryDescriptorQueryNode BindQueryDescriptor(QueryDescriptorQueryToken queryDescriptorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryDescriptorQueryToken>(queryDescriptorToken, "queryDescriptorToken");
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(queryDescriptorToken.Path, "queryDescriptorToken.Path");
			this.queryOptions = new List<QueryOptionQueryToken>(queryDescriptorToken.QueryOptions);
			QueryNode queryNode = this.Bind(queryDescriptorToken.Path);
			queryNode = this.ProcessFilter(queryNode, queryDescriptorToken.Filter);
			queryNode = this.ProcessOrderBy(queryNode, queryDescriptorToken.OrderByTokens);
			queryNode = MetadataBinder.ProcessSkip(queryNode, queryDescriptorToken.Skip);
			queryNode = MetadataBinder.ProcessTop(queryNode, queryDescriptorToken.Top);
			QueryDescriptorQueryNode queryDescriptorQueryNode = new QueryDescriptorQueryNode();
			queryDescriptorQueryNode.Query = queryNode;
			List<QueryNode> list = this.ProcessQueryOptions();
			queryDescriptorQueryNode.CustomQueryOptions = new ReadOnlyCollection<QueryNode>(list);
			return queryDescriptorQueryNode;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009945 File Offset: 0x00007B45
		protected virtual QueryNode BindSegment(SegmentQueryToken segmentToken)
		{
			ExceptionUtils.CheckArgumentNotNull<SegmentQueryToken>(segmentToken, "segmentToken");
			ExceptionUtils.CheckArgumentNotNull<string>(segmentToken.Name, "segmentToken.Name");
			if (segmentToken.Parent == null)
			{
				return this.BindRootSegment(segmentToken);
			}
			throw new NotImplementedException();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009978 File Offset: 0x00007B78
		protected virtual QueryNode BindLiteral(LiteralQueryToken literalToken)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralQueryToken>(literalToken, "literalToken");
			return new ConstantQueryNode
			{
				Value = literalToken.Value
			};
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000099A4 File Offset: 0x00007BA4
		protected virtual QueryNode BindBinaryOperator(BinaryOperatorQueryToken binaryOperatorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorQueryToken>(binaryOperatorToken, "binaryOperatorToken");
			SingleValueQueryNode singleValueQueryNode = this.Bind(binaryOperatorToken.Left) as SingleValueQueryNode;
			if (singleValueQueryNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BinaryOperatorOperandNotSingleValue(binaryOperatorToken.OperatorKind.ToString()));
			}
			SingleValueQueryNode singleValueQueryNode2 = this.Bind(binaryOperatorToken.Right) as SingleValueQueryNode;
			if (singleValueQueryNode2 == null)
			{
				throw new ODataException(Strings.MetadataBinder_BinaryOperatorOperandNotSingleValue(binaryOperatorToken.OperatorKind.ToString()));
			}
			IEdmTypeReference typeReference = singleValueQueryNode.TypeReference;
			IEdmTypeReference typeReference2 = singleValueQueryNode2.TypeReference;
			if (!TypePromotionUtils.PromoteOperandTypes(binaryOperatorToken.OperatorKind, ref typeReference, ref typeReference2))
			{
				string text = ((singleValueQueryNode.TypeReference == null) ? "<null>" : singleValueQueryNode.TypeReference.ODataFullName());
				string text2 = ((singleValueQueryNode2.TypeReference == null) ? "<null>" : singleValueQueryNode2.TypeReference.ODataFullName());
				throw new ODataException(Strings.MetadataBinder_IncompatibleOperandsError(text, text2, binaryOperatorToken.OperatorKind));
			}
			if (typeReference != null)
			{
				singleValueQueryNode = MetadataBinder.ConvertToType(singleValueQueryNode, typeReference);
			}
			if (typeReference2 != null)
			{
				singleValueQueryNode2 = MetadataBinder.ConvertToType(singleValueQueryNode2, typeReference2);
			}
			return new BinaryOperatorQueryNode
			{
				OperatorKind = binaryOperatorToken.OperatorKind,
				Left = singleValueQueryNode,
				Right = singleValueQueryNode2
			};
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009ACC File Offset: 0x00007CCC
		protected virtual QueryNode BindUnaryOperator(UnaryOperatorQueryToken unaryOperatorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<UnaryOperatorQueryToken>(unaryOperatorToken, "unaryOperatorToken");
			SingleValueQueryNode singleValueQueryNode = this.Bind(unaryOperatorToken.Operand) as SingleValueQueryNode;
			if (singleValueQueryNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_UnaryOperatorOperandNotSingleValue(unaryOperatorToken.OperatorKind.ToString()));
			}
			IEdmTypeReference typeReference = singleValueQueryNode.TypeReference;
			if (!TypePromotionUtils.PromoteOperandType(unaryOperatorToken.OperatorKind, ref typeReference))
			{
				string text = ((singleValueQueryNode.TypeReference == null) ? "<null>" : singleValueQueryNode.TypeReference.ODataFullName());
				throw new ODataException(Strings.MetadataBinder_IncompatibleOperandError(text, unaryOperatorToken.OperatorKind));
			}
			if (typeReference != null)
			{
				singleValueQueryNode = MetadataBinder.ConvertToType(singleValueQueryNode, typeReference);
			}
			return new UnaryOperatorQueryNode
			{
				OperatorKind = unaryOperatorToken.OperatorKind,
				Operand = singleValueQueryNode
			};
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009B84 File Offset: 0x00007D84
		protected virtual QueryNode BindPropertyAccess(PropertyAccessQueryToken propertyAccessToken)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertyAccessQueryToken>(propertyAccessToken, "propertyAccessToken");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(propertyAccessToken.Name, "propertyAccessToken.Name");
			SingleValueQueryNode singleValueQueryNode;
			if (propertyAccessToken.Parent == null)
			{
				if (this.parameter == null)
				{
					throw new ODataException(Strings.MetadataBinder_PropertyAccessWithoutParentParameter);
				}
				singleValueQueryNode = this.parameter;
			}
			else
			{
				singleValueQueryNode = this.Bind(propertyAccessToken.Parent) as SingleValueQueryNode;
				if (singleValueQueryNode == null)
				{
					throw new ODataException(Strings.MetadataBinder_PropertyAccessSourceNotSingleValue(propertyAccessToken.Name));
				}
			}
			IEdmStructuredTypeReference edmStructuredTypeReference = ((singleValueQueryNode.TypeReference == null) ? null : singleValueQueryNode.TypeReference.AsStructuredOrNull());
			IEdmProperty edmProperty = ((edmStructuredTypeReference == null) ? null : edmStructuredTypeReference.FindProperty(propertyAccessToken.Name));
			if (edmProperty != null)
			{
				if (edmProperty.Type.IsNonEntityODataCollectionTypeKind())
				{
					throw new ODataException(Strings.MetadataBinder_MultiValuePropertyNotSupportedInExpression(edmProperty.Name));
				}
				if (edmProperty.PropertyKind == EdmPropertyKind.Navigation)
				{
					throw new NotImplementedException();
				}
				return new PropertyAccessQueryNode
				{
					Source = singleValueQueryNode,
					Property = edmProperty
				};
			}
			else
			{
				if (singleValueQueryNode.TypeReference != null && !singleValueQueryNode.TypeReference.Definition.IsOpenType())
				{
					throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(singleValueQueryNode.TypeReference.ODataFullName(), propertyAccessToken.Name));
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009CAC File Offset: 0x00007EAC
		protected virtual QueryNode BindFunctionCall(FunctionCallQueryToken functionCallToken)
		{
			ExceptionUtils.CheckArgumentNotNull<FunctionCallQueryToken>(functionCallToken, "functionCallToken");
			ExceptionUtils.CheckArgumentNotNull<string>(functionCallToken.Name, "functionCallToken.Name");
			List<QueryNode> list = new List<QueryNode>(Enumerable.Select<QueryToken, QueryNode>(functionCallToken.Arguments, (QueryToken ar) => this.Bind(ar)));
			if (functionCallToken.Name == "cast")
			{
				throw new NotImplementedException();
			}
			if (functionCallToken.Name == "isof")
			{
				throw new NotImplementedException();
			}
			BuiltInFunctionSignature[] array;
			if (!BuiltInFunctions.TryGetBuiltInFunction(functionCallToken.Name, out array))
			{
				throw new ODataException(Strings.MetadataBinder_UnknownFunction(functionCallToken.Name));
			}
			IEdmTypeReference[] array2 = new IEdmTypeReference[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				SingleValueQueryNode singleValueQueryNode = list[i] as SingleValueQueryNode;
				if (singleValueQueryNode == null)
				{
					throw new ODataException(Strings.MetadataBinder_FunctionArgumentNotSingleValue(functionCallToken.Name));
				}
				array2[i] = singleValueQueryNode.TypeReference;
			}
			BuiltInFunctionSignature builtInFunctionSignature = (BuiltInFunctionSignature)TypePromotionUtils.FindBestFunctionSignature(array, array2);
			if (builtInFunctionSignature == null)
			{
				throw new ODataException(Strings.MetadataBinder_NoApplicableFunctionFound(functionCallToken.Name, BuiltInFunctions.BuildFunctionSignatureListDescription(functionCallToken.Name, array)));
			}
			for (int j = 0; j < list.Count; j++)
			{
				SingleValueQueryNode singleValueQueryNode2 = (SingleValueQueryNode)list[j];
				IEdmTypeReference edmTypeReference = builtInFunctionSignature.ArgumentTypes[j];
				if (edmTypeReference != singleValueQueryNode2.TypeReference)
				{
					list[j] = MetadataBinder.ConvertToType(singleValueQueryNode2, edmTypeReference);
				}
			}
			return new SingleValueFunctionCallQueryNode
			{
				Name = functionCallToken.Name,
				Arguments = new ReadOnlyCollection<QueryNode>(list),
				ReturnType = builtInFunctionSignature.ReturnType
			};
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009E38 File Offset: 0x00008038
		protected virtual QueryNode BindQueryOption(QueryOptionQueryToken queryOptionToken)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryOptionQueryToken>(queryOptionToken, "queryOptionToken");
			string name = queryOptionToken.Name;
			if (!string.IsNullOrEmpty(name) && name.get_Chars(0) == '$')
			{
				throw new ODataException(Strings.MetadataBinder_UnsupportedSystemQueryOption(name));
			}
			return new CustomQueryOptionQueryNode
			{
				Name = name,
				Value = queryOptionToken.Value
			};
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009E90 File Offset: 0x00008090
		private static QueryNode ProcessSkip(QueryNode query, int? skip)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(query, "query");
			if (skip != null)
			{
				CollectionQueryNode collectionQueryNode = query.AsEntityCollectionNode();
				if (collectionQueryNode == null)
				{
					throw new ODataException(Strings.MetadataBinder_SkipNotApplicable);
				}
				int value = skip.Value;
				if (value < 0)
				{
					throw new ODataException(Strings.MetadataBinder_SkipRequiresNonNegativeInteger(value.ToString(CultureInfo.CurrentCulture)));
				}
				query = new SkipQueryNode
				{
					Collection = collectionQueryNode,
					Amount = new ConstantQueryNode
					{
						Value = value
					}
				};
			}
			return query;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009F14 File Offset: 0x00008114
		private static QueryNode ProcessTop(QueryNode query, int? top)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(query, "query");
			if (top != null)
			{
				CollectionQueryNode collectionQueryNode = query.AsEntityCollectionNode();
				if (collectionQueryNode == null)
				{
					throw new ODataException(Strings.MetadataBinder_TopNotApplicable);
				}
				int value = top.Value;
				if (value < 0)
				{
					throw new ODataException(Strings.MetadataBinder_TopRequiresNonNegativeInteger(value.ToString(CultureInfo.CurrentCulture)));
				}
				query = new TopQueryNode
				{
					Collection = collectionQueryNode,
					Amount = new ConstantQueryNode
					{
						Value = value
					}
				};
			}
			return query;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009F98 File Offset: 0x00008198
		private static SingleValueQueryNode ConvertToType(SingleValueQueryNode source, IEdmTypeReference targetTypeReference)
		{
			if (source.TypeReference != null)
			{
				if (source.TypeReference.IsEquivalentTo(targetTypeReference))
				{
					return source;
				}
				if (!TypePromotionUtils.CanConvertTo(source.TypeReference, targetTypeReference))
				{
					throw new ODataException(Strings.MetadataBinder_CannotConvertToType(source.TypeReference.ODataFullName(), targetTypeReference.ODataFullName()));
				}
			}
			return new ConvertQueryNode
			{
				Source = source,
				TargetType = targetTypeReference
			};
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009FFC File Offset: 0x000081FC
		private QueryNode ProcessFilter(QueryNode query, QueryToken filter)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(query, "query");
			if (filter != null)
			{
				CollectionQueryNode collectionQueryNode = query.AsEntityCollectionNode();
				if (collectionQueryNode == null)
				{
					throw new ODataException(Strings.MetadataBinder_FilterNotApplicable);
				}
				this.parameter = new ParameterQueryNode
				{
					ParameterType = collectionQueryNode.ItemType
				};
				QueryNode queryNode = this.Bind(filter);
				SingleValueQueryNode singleValueQueryNode = queryNode as SingleValueQueryNode;
				if (singleValueQueryNode == null || (singleValueQueryNode.TypeReference != null && !singleValueQueryNode.TypeReference.IsODataPrimitiveTypeKind()))
				{
					throw new ODataException(Strings.MetadataBinder_FilterExpressionNotSingleValue);
				}
				IEdmTypeReference typeReference = singleValueQueryNode.TypeReference;
				if (typeReference != null)
				{
					IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
					if (edmPrimitiveTypeReference == null || edmPrimitiveTypeReference.PrimitiveKind() != EdmPrimitiveTypeKind.Boolean)
					{
						throw new ODataException(Strings.MetadataBinder_FilterExpressionNotSingleValue);
					}
				}
				query = new FilterQueryNode
				{
					Collection = collectionQueryNode,
					Parameter = this.parameter,
					Expression = singleValueQueryNode
				};
				this.parameter = null;
			}
			return query;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A0D8 File Offset: 0x000082D8
		private QueryNode ProcessOrderBy(QueryNode query, IEnumerable<OrderByQueryToken> orderByTokens)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(query, "query");
			foreach (OrderByQueryToken orderByQueryToken in orderByTokens)
			{
				query = this.ProcessSingleOrderBy(query, orderByQueryToken);
			}
			return query;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000A130 File Offset: 0x00008330
		private List<QueryNode> ProcessQueryOptions()
		{
			List<QueryNode> list = new List<QueryNode>();
			foreach (QueryOptionQueryToken queryOptionQueryToken in this.queryOptions)
			{
				QueryNode queryNode = this.Bind(queryOptionQueryToken);
				if (queryNode != null)
				{
					list.Add(queryNode);
				}
			}
			this.queryOptions = null;
			return list;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000A19C File Offset: 0x0000839C
		private QueryNode ProcessSingleOrderBy(QueryNode query, OrderByQueryToken orderByToken)
		{
			ExceptionUtils.CheckArgumentNotNull<OrderByQueryToken>(orderByToken, "orderByToken");
			CollectionQueryNode collectionQueryNode = query.AsEntityCollectionNode();
			if (collectionQueryNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_OrderByNotApplicable);
			}
			this.parameter = new ParameterQueryNode
			{
				ParameterType = collectionQueryNode.ItemType
			};
			QueryNode queryNode = this.Bind(orderByToken.Expression);
			SingleValueQueryNode singleValueQueryNode = queryNode as SingleValueQueryNode;
			if (singleValueQueryNode == null || (singleValueQueryNode.TypeReference != null && !singleValueQueryNode.TypeReference.IsODataPrimitiveTypeKind()))
			{
				throw new ODataException(Strings.MetadataBinder_OrderByExpressionNotSingleValue);
			}
			query = new OrderByQueryNode
			{
				Collection = collectionQueryNode,
				Direction = orderByToken.Direction,
				Parameter = this.parameter,
				Expression = singleValueQueryNode
			};
			this.parameter = null;
			return query;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A258 File Offset: 0x00008458
		private QueryNode BindRootSegment(SegmentQueryToken segmentToken)
		{
			if (segmentToken.Name == "$metadata")
			{
				throw new NotImplementedException();
			}
			if (segmentToken.Name == "$batch")
			{
				throw new NotImplementedException();
			}
			IEdmFunctionImport edmFunctionImport = this.model.TryResolveServiceOperation(segmentToken.Name);
			if (edmFunctionImport != null)
			{
				return this.BindServiceOperation(segmentToken, edmFunctionImport);
			}
			IEdmEntitySet edmEntitySet = this.model.TryResolveEntitySet(segmentToken.Name);
			if (edmEntitySet == null)
			{
				throw new ODataException(Strings.MetadataBinder_RootSegmentResourceNotFound(segmentToken.Name));
			}
			EntitySetQueryNode entitySetQueryNode = new EntitySetQueryNode
			{
				EntitySet = edmEntitySet
			};
			if (segmentToken.NamedValues != null)
			{
				return this.BindKeyValues(entitySetQueryNode, segmentToken.NamedValues);
			}
			return entitySetQueryNode;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A300 File Offset: 0x00008500
		private QueryNode BindServiceOperation(SegmentQueryToken segmentToken, IEdmFunctionImport serviceOperation)
		{
			ODataServiceOperationResultKind? serviceOperationResultKind = serviceOperation.GetServiceOperationResultKind(this.model);
			if (serviceOperationResultKind != ODataServiceOperationResultKind.QueryWithMultipleResults && segmentToken.NamedValues != null)
			{
				throw new ODataException(Strings.MetadataBinder_NonQueryableServiceOperationWithKeyLookup(segmentToken.Name));
			}
			if (serviceOperationResultKind == null)
			{
				throw new ODataException(Strings.MetadataBinder_ServiceOperationWithoutResultKind(serviceOperation.Name));
			}
			IEnumerable<QueryNode> enumerable = this.BindServiceOperationParameters(serviceOperation);
			switch (serviceOperationResultKind.Value)
			{
			case ODataServiceOperationResultKind.DirectValue:
				if (serviceOperation.ReturnType.IsODataPrimitiveTypeKind())
				{
					return new SingleValueServiceOperationQueryNode
					{
						ServiceOperation = serviceOperation,
						Parameters = enumerable
					};
				}
				return new UncomposableServiceOperationQueryNode
				{
					ServiceOperation = serviceOperation,
					Parameters = enumerable
				};
			case ODataServiceOperationResultKind.Enumeration:
			case ODataServiceOperationResultKind.Void:
				return new UncomposableServiceOperationQueryNode
				{
					ServiceOperation = serviceOperation,
					Parameters = enumerable
				};
			case ODataServiceOperationResultKind.QueryWithMultipleResults:
			{
				if (serviceOperation.ReturnType.TypeKind() != EdmTypeKind.Entity)
				{
					throw new ODataException(Strings.MetadataBinder_QueryServiceOperationOfNonEntityType(serviceOperation.Name, serviceOperationResultKind.Value.ToString(), serviceOperation.ReturnType.ODataFullName()));
				}
				CollectionServiceOperationQueryNode collectionServiceOperationQueryNode = new CollectionServiceOperationQueryNode
				{
					ServiceOperation = serviceOperation,
					Parameters = enumerable
				};
				if (segmentToken.NamedValues != null)
				{
					return this.BindKeyValues(collectionServiceOperationQueryNode, segmentToken.NamedValues);
				}
				return collectionServiceOperationQueryNode;
			}
			case ODataServiceOperationResultKind.QueryWithSingleResult:
				if (!serviceOperation.ReturnType.IsODataEntityTypeKind())
				{
					throw new ODataException(Strings.MetadataBinder_QueryServiceOperationOfNonEntityType(serviceOperation.Name, serviceOperationResultKind.Value.ToString(), serviceOperation.ReturnType.ODataFullName()));
				}
				return new SingleValueServiceOperationQueryNode
				{
					ServiceOperation = serviceOperation,
					Parameters = enumerable
				};
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.MetadataBinder_BindServiceOperation));
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A4C4 File Offset: 0x000086C4
		private IEnumerable<QueryNode> BindServiceOperationParameters(IEdmFunctionImport serviceOperation)
		{
			List<QueryNode> list = new List<QueryNode>();
			foreach (IEdmFunctionParameter edmFunctionParameter in serviceOperation.Parameters)
			{
				IEdmTypeReference type = edmFunctionParameter.Type;
				string text = this.ConsumeQueryOption(edmFunctionParameter.Name);
				object obj;
				if (string.IsNullOrEmpty(text))
				{
					if (!type.IsNullable)
					{
						throw new ODataException(Strings.MetadataBinder_ServiceOperationParameterMissing(serviceOperation.Name, edmFunctionParameter.Name));
					}
					obj = null;
				}
				else
				{
					text = text.Trim();
					if (!UriPrimitiveTypeParser.TryUriStringToPrimitive(text, type, out obj))
					{
						throw new ODataException(Strings.MetadataBinder_ServiceOperationParameterInvalidType(edmFunctionParameter.Name, text, serviceOperation.Name, edmFunctionParameter.Type.ODataFullName()));
					}
				}
				list.Add(new ConstantQueryNode
				{
					Value = obj
				});
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new ReadOnlyCollection<QueryNode>(list);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A5B8 File Offset: 0x000087B8
		private QueryNode BindKeyValues(CollectionQueryNode collectionQueryNode, IEnumerable<NamedValue> namedValues)
		{
			IEdmTypeReference itemType = collectionQueryNode.ItemType;
			List<KeyPropertyValue> list = new List<KeyPropertyValue>();
			if (!itemType.IsODataEntityTypeKind())
			{
				throw new ODataException(Strings.MetadataBinder_KeyValueApplicableOnlyToEntityType(itemType.ODataFullName()));
			}
			IEdmEntityTypeReference edmEntityTypeReference = itemType.AsEntityOrNull();
			IEdmEntityType edmEntityType = edmEntityTypeReference.EntityDefinition();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (NamedValue namedValue in namedValues)
			{
				KeyPropertyValue keyPropertyValue = this.BindKeyPropertyValue(namedValue, edmEntityType);
				if (!hashSet.Add(keyPropertyValue.KeyProperty.Name))
				{
					throw new ODataException(Strings.MetadataBinder_DuplicitKeyPropertyInKeyValues(keyPropertyValue.KeyProperty.Name));
				}
				list.Add(keyPropertyValue);
			}
			if (list.Count == 0)
			{
				return collectionQueryNode;
			}
			if (list.Count != Enumerable.Count<IEdmStructuralProperty>(edmEntityType.Key()))
			{
				throw new ODataException(Strings.MetadataBinder_NotAllKeyPropertiesSpecifiedInKeyValues(collectionQueryNode.ItemType.ODataFullName()));
			}
			return new KeyLookupQueryNode
			{
				Collection = collectionQueryNode,
				KeyPropertyValues = new ReadOnlyCollection<KeyPropertyValue>(list)
			};
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A6F4 File Offset: 0x000088F4
		private KeyPropertyValue BindKeyPropertyValue(NamedValue namedValue, IEdmEntityType collectionItemEntityType)
		{
			ExceptionUtils.CheckArgumentNotNull<NamedValue>(namedValue, "namedValue");
			ExceptionUtils.CheckArgumentNotNull<LiteralQueryToken>(namedValue.Value, "namedValue.Value");
			IEdmProperty edmProperty = null;
			if (namedValue.Name == null)
			{
				using (IEnumerator<IEdmStructuralProperty> enumerator = collectionItemEntityType.Key().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmProperty edmProperty2 = enumerator.Current;
						if (edmProperty != null)
						{
							throw new ODataException(Strings.MetadataBinder_UnnamedKeyValueOnTypeWithMultipleKeyProperties(collectionItemEntityType.ODataFullName()));
						}
						edmProperty = edmProperty2;
					}
					goto IL_00D5;
				}
			}
			edmProperty = Enumerable.SingleOrDefault<IEdmStructuralProperty>(Enumerable.Where<IEdmStructuralProperty>(collectionItemEntityType.Key(), (IEdmStructuralProperty k) => string.CompareOrdinal(k.Name, namedValue.Name) == 0));
			if (edmProperty == null)
			{
				throw new ODataException(Strings.MetadataBinder_PropertyNotDeclaredOrNotKeyInKeyValue(namedValue.Name, collectionItemEntityType.ODataFullName()));
			}
			IL_00D5:
			IEdmTypeReference type = edmProperty.Type;
			SingleValueQueryNode singleValueQueryNode = (SingleValueQueryNode)this.Bind(namedValue.Value);
			singleValueQueryNode = MetadataBinder.ConvertToType(singleValueQueryNode, type);
			return new KeyPropertyValue
			{
				KeyProperty = edmProperty,
				KeyValue = singleValueQueryNode
			};
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A828 File Offset: 0x00008A28
		private string ConsumeQueryOption(string queryOptionName)
		{
			if (this.queryOptions != null)
			{
				return this.queryOptions.GetQueryOptionValueAndRemove(queryOptionName);
			}
			return null;
		}

		// Token: 0x040001BA RID: 442
		private readonly IEdmModel model;

		// Token: 0x040001BB RID: 443
		private List<QueryOptionQueryToken> queryOptions;

		// Token: 0x040001BC RID: 444
		private ParameterQueryNode parameter;
	}
}
