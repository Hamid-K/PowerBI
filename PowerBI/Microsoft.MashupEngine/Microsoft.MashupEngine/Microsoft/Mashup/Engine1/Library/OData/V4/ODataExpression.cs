using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000862 RID: 2146
	internal sealed class ODataExpression
	{
		// Token: 0x06003DCB RID: 15819 RVA: 0x000C9978 File Offset: 0x000C7B78
		public ODataExpression(Capabilities capabilities, RecordTypeValue recordType, TypeValue argumentType, QueryExpression expression, Microsoft.OData.Edm.IEdmEntityType entityType, ODataEnvironment environment, EntityRangeVariableReferenceNode currentSource, SingleNavigationNode singleNavigationNode = null)
		{
			this.capability = capabilities;
			this.recordType = recordType;
			this.argumentType = argumentType;
			this.expression = expression;
			this.entityType = entityType;
			this.environment = environment;
			this.currentSource = currentSource;
			this.columns = recordType.Fields.Keys;
			this.singleNavigationNode = singleNavigationNode;
			this.queryNode = this.Visit(expression);
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000C99E7 File Offset: 0x000C7BE7
		private ODataExpression(Capabilities capability, EntityRangeVariableReferenceNode currentSource, QueryExpression expression, Keys columns)
		{
			this.capability = capability;
			this.expression = expression;
			this.currentSource = currentSource;
			this.columns = columns;
			if (expression != null)
			{
				this.queryNode = this.Visit(expression);
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06003DCD RID: 15821 RVA: 0x000C9A1C File Offset: 0x000C7C1C
		public QueryExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000C9A24 File Offset: 0x000C7C24
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000C9A2C File Offset: 0x000C7C2C
		public static ConstantNode BuildConstantNode(Value value)
		{
			return (ConstantNode)new ODataExpression(null, null, null, null).VisitConstant(new ConstantQueryExpression(value));
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000C9A47 File Offset: 0x000C7C47
		public static FilterClause GetExpression(Capabilities sourceCapabilities, EntityRangeVariableReferenceNode currentSource, QueryExpression expression, Keys columnNames)
		{
			return new ODataExpression(sourceCapabilities, currentSource, expression, columnNames).GetFilterClause();
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000C9A58 File Offset: 0x000C7C58
		public AggregateExpression BuildAggregateExpression(string alias, Keys groupingKeys)
		{
			InvocationQueryExpression invocationQueryExpression = this.expression as InvocationQueryExpression;
			Value value;
			AggregationMethod aggregationMethod;
			if (invocationQueryExpression != null && invocationQueryExpression.Function.TryGetConstant(out value) && ODataBuiltInFunctions.TryFindAggregateFunction(value, out aggregationMethod))
			{
				SingleValueNode singleValueNode = this.queryNode as SingleValuePropertyAccessNode;
				if (singleValueNode != null && (this.capability.AggregateTransformations.Count == 0 || this.capability.AggregateTransformations.Contains(aggregationMethod.ToString())))
				{
					return new AggregateExpression(singleValueNode, aggregationMethod, alias, singleValueNode.TypeReference);
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000C9AE4 File Offset: 0x000C7CE4
		public FilterClause GetFilterClause()
		{
			return new FilterClause((SingleValueNode)this.queryNode, this.currentSource.RangeVariable);
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000C9B04 File Offset: 0x000C7D04
		private QueryNode Visit(QueryExpression queryExpression)
		{
			switch (queryExpression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.VisitBinary((BinaryQueryExpression)queryExpression);
			case QueryExpressionKind.Constant:
				return this.VisitConstant((ConstantQueryExpression)queryExpression);
			case QueryExpressionKind.ColumnAccess:
				return this.VisitColumnAccess((ColumnAccessQueryExpression)queryExpression);
			case QueryExpressionKind.Invocation:
				return this.VisitInvocation((InvocationQueryExpression)queryExpression);
			case QueryExpressionKind.Unary:
				return this.VisitUnary((UnaryQueryExpression)queryExpression);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000C9B80 File Offset: 0x000C7D80
		private QueryNode VisitBinary(BinaryQueryExpression binary)
		{
			SingleValueNode singleValueNode = (SingleValueNode)this.Visit(binary.Left);
			TypeValue typeValue = this.type;
			SingleValueNode singleValueNode2 = (SingleValueNode)this.Visit(binary.Right);
			TypeValue typeValue2 = this.type;
			ValueKind typeKind = typeValue.TypeKind;
			ValueKind typeKind2 = typeValue2.TypeKind;
			if (binary.Operator == BinaryOperator2.Add)
			{
				if ((typeKind != ValueKind.Duration || typeKind2 != ValueKind.Duration) && (typeKind != ValueKind.Number || typeKind2 != ValueKind.Number))
				{
					throw new NotSupportedException();
				}
			}
			else if (binary.Operator == BinaryOperator2.Subtract)
			{
				if ((typeKind != ValueKind.DateTimeZone || (typeKind2 != ValueKind.DateTimeZone && typeKind2 != ValueKind.Duration)) && (typeKind != ValueKind.Date || (typeKind2 != ValueKind.Date && typeKind2 != ValueKind.Duration)) && (typeKind != ValueKind.Duration || typeKind2 != ValueKind.Duration) && (typeKind != ValueKind.Number || typeKind2 != ValueKind.Number))
				{
					throw new NotSupportedException();
				}
			}
			else if ((binary.Operator == BinaryOperator2.Multiply || binary.Operator == BinaryOperator2.Divide) && (typeKind != ValueKind.Number || typeKind2 != ValueKind.Number))
			{
				throw new NotSupportedException();
			}
			this.type = this.CheckType(OperatorTypeflowModels.Binary(binary.Operator, typeValue, typeValue2));
			BinaryOperatorKind binaryOperatorKind;
			switch (binary.Operator)
			{
			case BinaryOperator2.Add:
				binaryOperatorKind = BinaryOperatorKind.Add;
				goto IL_01F6;
			case BinaryOperator2.Subtract:
				binaryOperatorKind = BinaryOperatorKind.Subtract;
				goto IL_01F6;
			case BinaryOperator2.Multiply:
				binaryOperatorKind = BinaryOperatorKind.Multiply;
				goto IL_01F6;
			case BinaryOperator2.Divide:
				binaryOperatorKind = BinaryOperatorKind.Divide;
				goto IL_01F6;
			case BinaryOperator2.GreaterThan:
				binaryOperatorKind = BinaryOperatorKind.GreaterThan;
				goto IL_01F6;
			case BinaryOperator2.LessThan:
				binaryOperatorKind = BinaryOperatorKind.LessThan;
				goto IL_01F6;
			case BinaryOperator2.GreaterThanOrEquals:
				binaryOperatorKind = BinaryOperatorKind.GreaterThanOrEqual;
				goto IL_01F6;
			case BinaryOperator2.LessThanOrEquals:
				binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
				goto IL_01F6;
			case BinaryOperator2.Equals:
			{
				binaryOperatorKind = BinaryOperatorKind.Equal;
				SingleValueNode singleValueNode3;
				if (this.TryHandleIncompatibleTypes(binaryOperatorKind, singleValueNode, singleValueNode2, out singleValueNode3))
				{
					return singleValueNode3;
				}
				goto IL_01F6;
			}
			case BinaryOperator2.NotEquals:
			{
				binaryOperatorKind = BinaryOperatorKind.NotEqual;
				SingleValueNode singleValueNode3;
				if (this.TryHandleIncompatibleTypes(binaryOperatorKind, singleValueNode, singleValueNode2, out singleValueNode3))
				{
					return singleValueNode3;
				}
				goto IL_01F6;
			}
			case BinaryOperator2.And:
				if (singleValueNode.IsFalseConstant() || singleValueNode2.IsFalseConstant())
				{
					return ODataExpression.FalseConstant;
				}
				if (singleValueNode.IsTrueConstant())
				{
					return singleValueNode2;
				}
				if (singleValueNode2.IsTrueConstant())
				{
					return singleValueNode;
				}
				binaryOperatorKind = BinaryOperatorKind.And;
				goto IL_01F6;
			case BinaryOperator2.Or:
				if (singleValueNode.IsTrueConstant() || singleValueNode2.IsTrueConstant())
				{
					return ODataExpression.TrueConstant;
				}
				if (singleValueNode.IsFalseConstant())
				{
					return singleValueNode2;
				}
				if (singleValueNode2.IsFalseConstant())
				{
					return singleValueNode;
				}
				binaryOperatorKind = BinaryOperatorKind.Or;
				goto IL_01F6;
			case BinaryOperator2.Concatenate:
				return ODataBuiltInFunctions.ConvertCombineToConcat(new QueryNode[] { singleValueNode, singleValueNode2 });
			}
			throw new NotSupportedException();
			IL_01F6:
			return new BinaryOperatorNode(binaryOperatorKind, singleValueNode, singleValueNode2);
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000C9D8C File Offset: 0x000C7F8C
		private bool TryHandleIncompatibleTypes(BinaryOperatorKind kind, SingleValueNode left, SingleValueNode right, out SingleValueNode incompatibleTypesComparison)
		{
			incompatibleTypesComparison = null;
			AnyNode anyNode = left as AnyNode;
			AnyNode anyNode2 = right as AnyNode;
			if (!right.TypeReference.IsCompatible(left.TypeReference) && !left.IsNullConstant() && !right.IsNullConstant())
			{
				if (left.TypeReference.IsEnum() && right.TypeReference.IsString())
				{
					incompatibleTypesComparison = this.GetEnumStringBinaryNode(kind, left, right);
					return true;
				}
				if (left.TypeReference.IsString() && right.TypeReference.IsEnum())
				{
					incompatibleTypesComparison = this.GetEnumStringBinaryNode(kind, right, left);
					return true;
				}
				if (left.TypeReference.IsGuid() || right.TypeReference.IsGuid())
				{
					bool flag;
					if (right.TypeReference.IsString() && right is ConstantNode)
					{
						flag = true;
					}
					else
					{
						if (!left.TypeReference.IsString() || !(left is ConstantNode))
						{
							return false;
						}
						flag = false;
					}
					string text = (flag ? ((string)((ConstantNode)right).Value) : ((string)((ConstantNode)left).Value));
					Guid guid;
					try
					{
						guid = new Guid(text);
					}
					catch (FormatException)
					{
						incompatibleTypesComparison = new ConstantNode(false, ODataUriUtils.ConvertToUriLiteral(false, ODataExpression.literalVersion));
						return true;
					}
					ConstantNode constantNode = new ConstantNode(guid, ODataUriUtils.ConvertToUriLiteral(guid, ODataExpression.literalVersion));
					incompatibleTypesComparison = (flag ? new BinaryOperatorNode(kind, left, constantNode) : new BinaryOperatorNode(kind, constantNode, right));
					return true;
				}
				if (anyNode != null || anyNode2 != null)
				{
					throw new NotSupportedException();
				}
				if (left.TypeReference.IsNullable && right.TypeReference.IsNullable)
				{
					if (kind == BinaryOperatorKind.Equal)
					{
						incompatibleTypesComparison = new BinaryOperatorNode(BinaryOperatorKind.And, new BinaryOperatorNode(BinaryOperatorKind.Equal, left, ODataExpression.NullConstant), new BinaryOperatorNode(BinaryOperatorKind.Equal, right, ODataExpression.NullConstant));
						return true;
					}
					if (kind == BinaryOperatorKind.NotEqual)
					{
						incompatibleTypesComparison = new BinaryOperatorNode(BinaryOperatorKind.Or, new BinaryOperatorNode(BinaryOperatorKind.NotEqual, left, ODataExpression.NullConstant), new BinaryOperatorNode(BinaryOperatorKind.NotEqual, right, ODataExpression.NullConstant));
						return true;
					}
				}
				throw new NotSupportedException();
			}
			else
			{
				if (anyNode != null)
				{
					anyNode.Body = new BinaryOperatorNode(kind, this.GetAnyNodeRangeVariableNode(anyNode), right);
					incompatibleTypesComparison = anyNode;
					return true;
				}
				if (anyNode2 != null)
				{
					anyNode2.Body = new BinaryOperatorNode(kind, right, this.GetAnyNodeRangeVariableNode(anyNode2));
					incompatibleTypesComparison = anyNode2;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000C9FD4 File Offset: 0x000C81D4
		private NonentityRangeVariableReferenceNode GetAnyNodeRangeVariableNode(AnyNode anyNode)
		{
			return new NonentityRangeVariableReferenceNode("e", (NonentityRangeVariable)anyNode.CurrentRangeVariable);
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000C9FEC File Offset: 0x000C81EC
		private BinaryOperatorNode GetEnumStringBinaryNode(BinaryOperatorKind kind, SingleValueNode enumNode, SingleValueNode stringNode)
		{
			string text = enumNode.TypeReference.AsEnum().FullName();
			string literalText = ((ConstantNode)stringNode).LiteralText;
			ConstantNode constantNode = new ConstantNode(new ODataEnumValue(literalText, text), text + literalText, enumNode.TypeReference);
			return new BinaryOperatorNode(kind, enumNode, constantNode);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000CA038 File Offset: 0x000C8238
		private bool TryVisitColumnAccess(ColumnAccessQueryExpression expression, string columnName, out SingleValueNode singleValueNode)
		{
			if (this.entityType.FindProperty(columnName) == null && !(from e in this.environment.EdmModel.FindBoundOperations(this.entityType)
				where e.Name == columnName
				select e).Any<Microsoft.OData.Edm.IEdmOperation>() && (!this.environment.UserSettings.MoreColumns || !this.recordType.MetaValue.Keys.Contains("MoreColumns")) && !this.recordType.MetaValue.Keys.Contains("aggregate"))
			{
				singleValueNode = null;
				return false;
			}
			this.type = this.recordType.Fields[expression.Column]["Type"].AsType;
			if (this.argumentType.TypeKind == ValueKind.Table)
			{
				this.type = ListTypeValue.New(this.type);
			}
			if (this.singleNavigationNode != null)
			{
				Microsoft.OData.Edm.IEdmProperty edmProperty = this.currentSource.TypeReference.AsStructured().FindProperty(columnName);
				singleValueNode = new SingleValuePropertyAccessNode(this.singleNavigationNode, edmProperty);
				return true;
			}
			if (this.currentSource != null)
			{
				singleValueNode = this.currentSource.PropertyAccessNode(columnName);
				return true;
			}
			singleValueNode = null;
			return true;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000CA188 File Offset: 0x000C8388
		private SingleValueNode VisitColumnAccess(ColumnAccessQueryExpression expression)
		{
			string text = this.columns[expression.Column];
			SingleValueNode singleValueNode;
			if (this.TryVisitColumnAccess(expression, text, out singleValueNode))
			{
				return singleValueNode;
			}
			if (this.TryVisitColumnAccess(expression, EdmNameEncoder.Encode(text), out singleValueNode))
			{
				return singleValueNode;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000CA1D0 File Offset: 0x000C83D0
		private QueryNode VisitInvocation(InvocationQueryExpression invocationExpression)
		{
			Value value;
			Func<IEnumerable<QueryNode>, QueryNode> func;
			ODataBuiltInFunctions.PrimitiveFunctionValue[] array;
			if (invocationExpression.Function.TryGetConstant(out value) && ODataBuiltInFunctions.TryFindBuiltInFunctionCallNode(value, out func) && ODataBuiltInFunctions.TryFindODataFunctionValue(value, out array))
			{
				ODataBuiltInFunctions.PrimitiveFunctionValue primitiveFunctionValue = array.SingleOrDefault((ODataBuiltInFunctions.PrimitiveFunctionValue f) => f.Type.AsFunctionType.ParameterCount == invocationExpression.Arguments.Count);
				if (primitiveFunctionValue != null && this.capability.FilterFunctions.Contains(primitiveFunctionValue.ODataFunctionName))
				{
					QueryNode queryNode = func(this.VisitInvocationExpressionArguments(invocationExpression.Arguments));
					this.type = value.AsFunction.Type.AsFunctionType.ReturnType;
					return queryNode;
				}
			}
			AggregationMethod aggregationMethod;
			if (ODataBuiltInFunctions.TryFindAggregateFunction(value, out aggregationMethod))
			{
				QueryNode[] array2 = this.VisitInvocationExpressionArguments(invocationExpression.Arguments);
				if (array2.Length != 0)
				{
					SingleValuePropertyAccessNode singleValuePropertyAccessNode = array2[0] as SingleValuePropertyAccessNode;
					if (singleValuePropertyAccessNode != null)
					{
						IEnumerable<IEdmVocabularyAnnotation> enumerable = singleValuePropertyAccessNode.Property.VocabularyAnnotations(this.environment.EdmModel);
						bool flag;
						if (this.capability.ApplySupported)
						{
							if (this.capability.PropertyRestrictions)
							{
								flag = enumerable.Any((IEdmVocabularyAnnotation annotation) => annotation.Term.FullName().Contains("Org.OData.Aggregation.V1.Aggregatable"));
							}
							else
							{
								flag = true;
							}
						}
						else
						{
							flag = false;
						}
						if (flag)
						{
							return singleValuePropertyAccessNode;
						}
					}
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000CA314 File Offset: 0x000C8514
		private QueryNode[] VisitInvocationExpressionArguments(IList<QueryExpression> args)
		{
			QueryNode[] array = new QueryNode[args.Count];
			for (int i = 0; i < args.Count; i++)
			{
				array[i] = this.Visit(args[i]);
			}
			return array;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000CA350 File Offset: 0x000C8550
		private SingleValueNode VisitUnary(UnaryQueryExpression unary)
		{
			SingleValueNode singleValueNode = this.Visit(unary.Expression) as SingleValueNode;
			TypeValue typeValue = this.type;
			UnaryOperator2 @operator = unary.Operator;
			UnaryOperatorKind unaryOperatorKind;
			if (@operator != UnaryOperator2.Not)
			{
				if (@operator != UnaryOperator2.Negative)
				{
					throw new NotSupportedException();
				}
				unaryOperatorKind = UnaryOperatorKind.Negate;
			}
			else
			{
				if (singleValueNode.IsTrueConstant())
				{
					return ODataExpression.FalseConstant;
				}
				if (singleValueNode.IsFalseConstant())
				{
					return ODataExpression.TrueConstant;
				}
				unaryOperatorKind = UnaryOperatorKind.Not;
			}
			this.type = this.CheckType(OperatorTypeflowModels.Unary(unary.Operator, typeValue));
			return new UnaryOperatorNode(unaryOperatorKind, singleValueNode);
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000CA3D0 File Offset: 0x000C85D0
		private QueryNode VisitConstant(ConstantQueryExpression constantExpression)
		{
			Value value = constantExpression.Value;
			ConstantNode constantNode = null;
			switch (value.Kind)
			{
			case ValueKind.Null:
				constantNode = ODataExpression.NullConstant;
				goto IL_0216;
			case ValueKind.Date:
			{
				DateValue asDate = value.AsDate;
				Microsoft.OData.Edm.Library.Date date = new Microsoft.OData.Edm.Library.Date(asDate.Year, asDate.Month, asDate.Day);
				constantNode = new ConstantNode(date, ODataUriUtils.ConvertToUriLiteral(date, ODataExpression.literalVersion));
				goto IL_0216;
			}
			case ValueKind.DateTimeZone:
			{
				DateTimeOffset asClrDateTimeOffset = value.AsDateTimeZone.AsClrDateTimeOffset;
				constantNode = new ConstantNode(asClrDateTimeOffset, ODataUriUtils.ConvertToUriLiteral(asClrDateTimeOffset, ODataExpression.literalVersion));
				goto IL_0216;
			}
			case ValueKind.Duration:
			{
				TimeSpan asClrTimeSpan = value.AsDuration.AsClrTimeSpan;
				constantNode = new ConstantNode(asClrTimeSpan, ODataUriUtils.ConvertToUriLiteral(asClrTimeSpan, ODataExpression.literalVersion));
				goto IL_0216;
			}
			case ValueKind.Number:
				switch (value.AsNumber.NumberKind)
				{
				case NumberKind.Int32:
				{
					int asInteger = value.AsNumber.AsInteger32;
					constantNode = new ConstantNode(asInteger, ODataUriUtils.ConvertToUriLiteral(asInteger, ODataExpression.literalVersion));
					goto IL_0216;
				}
				case NumberKind.Double:
				{
					double asDouble = value.AsNumber.AsDouble;
					constantNode = new ConstantNode(asDouble, ODataUriUtils.ConvertToUriLiteral(asDouble, ODataExpression.literalVersion));
					goto IL_0216;
				}
				case NumberKind.Decimal:
				{
					decimal asDecimal = value.AsNumber.AsDecimal;
					constantNode = new ConstantNode(asDecimal, ODataUriUtils.ConvertToUriLiteral(asDecimal, ODataExpression.literalVersion));
					goto IL_0216;
				}
				default:
					goto IL_0216;
				}
				break;
			case ValueKind.Logical:
			{
				bool asBoolean = value.AsLogical.AsBoolean;
				constantNode = new ConstantNode(asBoolean, ODataUriUtils.ConvertToUriLiteral(asBoolean, ODataExpression.literalVersion));
				goto IL_0216;
			}
			case ValueKind.Text:
			{
				string asString = value.AsString;
				constantNode = new ConstantNode(asString, ODataUriUtils.ConvertToUriLiteral(asString, ODataExpression.literalVersion));
				goto IL_0216;
			}
			case ValueKind.Binary:
			{
				byte[] asBytes = value.AsBinary.AsBytes;
				constantNode = new ConstantNode(asBytes, ODataUriUtils.ConvertToUriLiteral(asBytes, ODataExpression.literalVersion));
				goto IL_0216;
			}
			}
			throw new NotSupportedException();
			IL_0216:
			this.type = value.Type;
			return constantNode;
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000BEE12 File Offset: 0x000BD012
		private TypeValue CheckType(TypeValue type)
		{
			if (type.TypeKind == ValueKind.None)
			{
				throw new NotSupportedException();
			}
			return type;
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000CA600 File Offset: 0x000C8800
		public static SingleValueNode AppendNullRowsExprToExpandedRecordFilter(SingleValueNode recordExpr, SingleNavigationNode singleNavSource)
		{
			if (recordExpr != null)
			{
				SingleValueNode singleValueNode = new BinaryOperatorNode(BinaryOperatorKind.Equal, singleNavSource, new ConstantNode(null));
				return new BinaryOperatorNode(BinaryOperatorKind.Or, recordExpr, singleValueNode);
			}
			return recordExpr;
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000CA628 File Offset: 0x000C8828
		public static SingleValueNode AppendNullRowsExprToExpandedTableFilter(SingleValueNode collectionExpr, CollectionNavigationNode collectionSource)
		{
			if (collectionExpr != null)
			{
				AnyNode anyNode = new AnyNode(new Collection<RangeVariable> { null })
				{
					Body = new ConstantNode(null),
					Source = collectionSource
				};
				SingleValueNode singleValueNode = new UnaryOperatorNode(UnaryOperatorKind.Not, anyNode);
				return new BinaryOperatorNode(BinaryOperatorKind.Or, collectionExpr, singleValueNode);
			}
			return collectionExpr;
		}

		// Token: 0x04002080 RID: 8320
		public static readonly ConstantNode EmptyStringConstant = new ConstantNode(string.Empty, "''", Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.String, true));

		// Token: 0x04002081 RID: 8321
		private static readonly ConstantNode FalseConstant = new ConstantNode(false, "false", Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean, false));

		// Token: 0x04002082 RID: 8322
		private static readonly ConstantNode TrueConstant = new ConstantNode(true, "true", Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean, false));

		// Token: 0x04002083 RID: 8323
		private static readonly ConstantNode NullConstant = new ConstantNode(null, ODataUriUtils.ConvertToUriLiteral(null, ODataVersion.V4));

		// Token: 0x04002084 RID: 8324
		private static readonly ODataVersion literalVersion = ODataVersion.V4;

		// Token: 0x04002085 RID: 8325
		private readonly Capabilities capability;

		// Token: 0x04002086 RID: 8326
		private readonly RecordTypeValue recordType;

		// Token: 0x04002087 RID: 8327
		private readonly TypeValue argumentType;

		// Token: 0x04002088 RID: 8328
		private readonly QueryExpression expression;

		// Token: 0x04002089 RID: 8329
		private readonly Microsoft.OData.Edm.IEdmStructuredType entityType;

		// Token: 0x0400208A RID: 8330
		private readonly EntityRangeVariableReferenceNode currentSource;

		// Token: 0x0400208B RID: 8331
		private readonly SingleNavigationNode singleNavigationNode;

		// Token: 0x0400208C RID: 8332
		private readonly Keys columns;

		// Token: 0x0400208D RID: 8333
		private readonly QueryNode queryNode;

		// Token: 0x0400208E RID: 8334
		private readonly ODataEnvironment environment;

		// Token: 0x0400208F RID: 8335
		private TypeValue type;
	}
}
