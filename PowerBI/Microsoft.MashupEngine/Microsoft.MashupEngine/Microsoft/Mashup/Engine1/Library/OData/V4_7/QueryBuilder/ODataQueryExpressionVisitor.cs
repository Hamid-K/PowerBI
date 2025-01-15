using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007EA RID: 2026
	internal sealed class ODataQueryExpressionVisitor
	{
		// Token: 0x06003AB1 RID: 15025 RVA: 0x000BE248 File Offset: 0x000BC448
		public ODataQueryExpressionVisitor(ODataEnvironment environment, SingleResourceNode source, RecordTypeValue sourceType)
		{
			this.environment = environment;
			this.source = source;
			this.sourceType = sourceType;
			if (source != null)
			{
				this.entityType = source.StructuredTypeReference.StructuredDefinition();
				this.capability = this.environment.Annotations.GetElementCapability(source.NavigationSource);
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000BE2A0 File Offset: 0x000BC4A0
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000BE2A8 File Offset: 0x000BC4A8
		public ODataExpression Compile(QueryExpression queryExpression)
		{
			return new ODataExpression((SingleValueNode)this.Visit(queryExpression), this.type);
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000BE2C4 File Offset: 0x000BC4C4
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

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000BE340 File Offset: 0x000BC540
		private QueryNode VisitBinary(BinaryQueryExpression binary)
		{
			SingleValueNode singleValueNode = (SingleValueNode)this.Visit(binary.Left);
			TypeValue typeValue = this.type;
			SingleValueNode singleValueNode2 = (SingleValueNode)this.Visit(binary.Right);
			TypeValue typeValue2 = this.type;
			ValueKind typeKind = typeValue.TypeKind;
			ValueKind typeKind2 = typeValue2.TypeKind;
			switch (binary.Operator)
			{
			case BinaryOperator2.Add:
				if ((typeKind != ValueKind.Duration || typeKind2 != ValueKind.Duration) && (typeKind != ValueKind.Number || typeKind2 != ValueKind.Number))
				{
					throw new NotSupportedException();
				}
				break;
			case BinaryOperator2.Subtract:
				if ((typeKind != ValueKind.DateTimeZone || (typeKind2 != ValueKind.DateTimeZone && typeKind2 != ValueKind.Duration)) && (typeKind != ValueKind.Date || (typeKind2 != ValueKind.Date && typeKind2 != ValueKind.Duration)) && (typeKind != ValueKind.Duration || typeKind2 != ValueKind.Duration) && (typeKind != ValueKind.Number || typeKind2 != ValueKind.Number))
				{
					throw new NotSupportedException();
				}
				break;
			case BinaryOperator2.Multiply:
			case BinaryOperator2.Divide:
				if (typeKind != ValueKind.Number || typeKind2 != ValueKind.Number)
				{
					throw new NotSupportedException();
				}
				break;
			case BinaryOperator2.GreaterThan:
			case BinaryOperator2.LessThan:
			case BinaryOperator2.GreaterThanOrEquals:
			case BinaryOperator2.LessThanOrEquals:
				if (singleValueNode.TypeReference.IsGuid() || singleValueNode2.TypeReference.IsGuid())
				{
					throw new NotSupportedException();
				}
				break;
			}
			this.type = this.CheckType(OperatorTypeflowModels.Binary(binary.Operator, typeValue, typeValue2));
			BinaryOperatorKind binaryOperatorKind;
			switch (binary.Operator)
			{
			case BinaryOperator2.Add:
				binaryOperatorKind = BinaryOperatorKind.Add;
				goto IL_0227;
			case BinaryOperator2.Subtract:
				binaryOperatorKind = BinaryOperatorKind.Subtract;
				goto IL_0227;
			case BinaryOperator2.Multiply:
				binaryOperatorKind = BinaryOperatorKind.Multiply;
				goto IL_0227;
			case BinaryOperator2.Divide:
				binaryOperatorKind = BinaryOperatorKind.Divide;
				goto IL_0227;
			case BinaryOperator2.GreaterThan:
				binaryOperatorKind = BinaryOperatorKind.GreaterThan;
				goto IL_0227;
			case BinaryOperator2.LessThan:
				binaryOperatorKind = BinaryOperatorKind.LessThan;
				goto IL_0227;
			case BinaryOperator2.GreaterThanOrEquals:
				binaryOperatorKind = BinaryOperatorKind.GreaterThanOrEqual;
				goto IL_0227;
			case BinaryOperator2.LessThanOrEquals:
				binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
				goto IL_0227;
			case BinaryOperator2.Equals:
			{
				binaryOperatorKind = BinaryOperatorKind.Equal;
				SingleValueNode singleValueNode3;
				if (this.TryHandleIncompatibleTypes(binaryOperatorKind, singleValueNode, singleValueNode2, out singleValueNode3))
				{
					return singleValueNode3;
				}
				goto IL_0227;
			}
			case BinaryOperator2.NotEquals:
			{
				binaryOperatorKind = BinaryOperatorKind.NotEqual;
				SingleValueNode singleValueNode3;
				if (this.TryHandleIncompatibleTypes(binaryOperatorKind, singleValueNode, singleValueNode2, out singleValueNode3))
				{
					return singleValueNode3;
				}
				goto IL_0227;
			}
			case BinaryOperator2.And:
				if (singleValueNode.IsFalseConstant() || singleValueNode2.IsFalseConstant())
				{
					return ODataQueryExpressionVisitor.FalseConstant;
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
				goto IL_0227;
			case BinaryOperator2.Or:
				if (singleValueNode.IsTrueConstant() || singleValueNode2.IsTrueConstant())
				{
					return ODataQueryExpressionVisitor.TrueConstant;
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
				goto IL_0227;
			case BinaryOperator2.Concatenate:
				return ODataBuiltInFunctions.ConvertCombineToConcat(new QueryNode[] { singleValueNode, singleValueNode2 });
			}
			throw new NotSupportedException();
			IL_0227:
			return new BinaryOperatorNode(binaryOperatorKind, singleValueNode, singleValueNode2);
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000BE57C File Offset: 0x000BC77C
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
						incompatibleTypesComparison = new ConstantNode(false, ODataUriUtils.ConvertToUriLiteral(false, ODataQueryExpressionVisitor.literalVersion));
						return true;
					}
					ConstantNode constantNode = new ConstantNode(guid, ODataUriUtils.ConvertToUriLiteral(guid, ODataQueryExpressionVisitor.literalVersion));
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
						if (left.Kind == QueryNodeKind.Constant)
						{
							if (left.IsNullConstant())
							{
								incompatibleTypesComparison = new BinaryOperatorNode(BinaryOperatorKind.Equal, right, ODataQueryExpressionVisitor.NullConstant);
							}
							else
							{
								incompatibleTypesComparison = ODataQueryExpressionVisitor.FalseConstant;
							}
						}
						else if (right.Kind == QueryNodeKind.Constant)
						{
							if (right.IsNullConstant())
							{
								incompatibleTypesComparison = new BinaryOperatorNode(BinaryOperatorKind.Equal, left, ODataQueryExpressionVisitor.NullConstant);
							}
							else
							{
								incompatibleTypesComparison = ODataQueryExpressionVisitor.FalseConstant;
							}
						}
						else
						{
							incompatibleTypesComparison = new BinaryOperatorNode(BinaryOperatorKind.And, new BinaryOperatorNode(BinaryOperatorKind.Equal, left, ODataQueryExpressionVisitor.NullConstant), new BinaryOperatorNode(BinaryOperatorKind.Equal, right, ODataQueryExpressionVisitor.NullConstant));
						}
						return true;
					}
					if (kind == BinaryOperatorKind.NotEqual)
					{
						if (left.Kind == QueryNodeKind.Constant)
						{
							if (left.IsNullConstant())
							{
								incompatibleTypesComparison = new BinaryOperatorNode(BinaryOperatorKind.NotEqual, right, ODataQueryExpressionVisitor.NullConstant);
							}
							else
							{
								incompatibleTypesComparison = ODataQueryExpressionVisitor.TrueConstant;
							}
						}
						else if (right.Kind == QueryNodeKind.Constant)
						{
							if (right.IsNullConstant())
							{
								incompatibleTypesComparison = new BinaryOperatorNode(BinaryOperatorKind.NotEqual, left, ODataQueryExpressionVisitor.NullConstant);
							}
							else
							{
								incompatibleTypesComparison = ODataQueryExpressionVisitor.TrueConstant;
							}
						}
						else
						{
							incompatibleTypesComparison = new BinaryOperatorNode(BinaryOperatorKind.Or, new BinaryOperatorNode(BinaryOperatorKind.NotEqual, left, ODataQueryExpressionVisitor.NullConstant), new BinaryOperatorNode(BinaryOperatorKind.NotEqual, right, ODataQueryExpressionVisitor.NullConstant));
						}
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

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000BE880 File Offset: 0x000BCA80
		private NonResourceRangeVariableReferenceNode GetAnyNodeRangeVariableNode(AnyNode anyNode)
		{
			return new NonResourceRangeVariableReferenceNode("e", (NonResourceRangeVariable)anyNode.CurrentRangeVariable);
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000BE898 File Offset: 0x000BCA98
		private BinaryOperatorNode GetEnumStringBinaryNode(BinaryOperatorKind kind, SingleValueNode enumNode, SingleValueNode stringNode)
		{
			string text = enumNode.TypeReference.AsEnum().FullName();
			string literalText = ((ConstantNode)stringNode).LiteralText;
			ConstantNode constantNode = new ConstantNode(new ODataEnumValue(literalText, text), text + literalText, enumNode.TypeReference);
			return new BinaryOperatorNode(kind, enumNode, constantNode);
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000BE8E4 File Offset: 0x000BCAE4
		private bool TryVisitColumnAccess(ColumnAccessQueryExpression expression, string columnName, out QueryNode queryNode)
		{
			Microsoft.OData.Edm.IEdmProperty edmProperty = this.entityType.FindProperty(columnName);
			if (edmProperty == null)
			{
				edmProperty = this.source.NavigationSource.EntityType().DeclaredProperties.Where((Microsoft.OData.Edm.IEdmProperty e) => e.Name.Equals(columnName)).SingleOrDefault<Microsoft.OData.Edm.IEdmProperty>();
			}
			if (edmProperty != null)
			{
				this.type = this.sourceType.Fields[expression.Column]["Type"].AsType;
				if (edmProperty.Type.IsCollection())
				{
					Microsoft.OData.Edm.EdmPropertyKind edmPropertyKind = edmProperty.PropertyKind;
					if (edmPropertyKind == Microsoft.OData.Edm.EdmPropertyKind.Structural)
					{
						queryNode = new CollectionPropertyAccessNode(this.source, edmProperty);
						return true;
					}
					if (edmPropertyKind == Microsoft.OData.Edm.EdmPropertyKind.Navigation)
					{
						queryNode = new CollectionNavigationNode(this.source, (Microsoft.OData.Edm.IEdmNavigationProperty)edmProperty, null);
						return true;
					}
				}
				else
				{
					Microsoft.OData.Edm.EdmPropertyKind edmPropertyKind = edmProperty.PropertyKind;
					if (edmPropertyKind == Microsoft.OData.Edm.EdmPropertyKind.Structural)
					{
						queryNode = new SingleValuePropertyAccessNode(this.source, edmProperty);
						return true;
					}
					if (edmPropertyKind == Microsoft.OData.Edm.EdmPropertyKind.Navigation)
					{
						queryNode = new SingleNavigationNode(this.source, (Microsoft.OData.Edm.IEdmNavigationProperty)edmProperty, null);
						return true;
					}
				}
			}
			queryNode = null;
			return false;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000BE9F0 File Offset: 0x000BCBF0
		private SingleValueNode VisitColumnAccess(ColumnAccessQueryExpression expression)
		{
			string text = this.sourceType.Fields.Keys[expression.Column];
			QueryNode queryNode;
			if (this.TryVisitColumnAccess(expression, EdmNameEncoder.Encode(text), out queryNode))
			{
				SingleValueNode singleValueNode = queryNode as SingleValueNode;
				if (singleValueNode != null)
				{
					return singleValueNode;
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000BEA3C File Offset: 0x000BCC3C
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
			throw new NotSupportedException();
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000BEAE8 File Offset: 0x000BCCE8
		private QueryNode[] VisitInvocationExpressionArguments(IList<QueryExpression> args)
		{
			QueryNode[] array = new QueryNode[args.Count];
			for (int i = 0; i < args.Count; i++)
			{
				array[i] = this.Visit(args[i]);
			}
			return array;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000BEB24 File Offset: 0x000BCD24
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
					return ODataQueryExpressionVisitor.FalseConstant;
				}
				if (singleValueNode.IsFalseConstant())
				{
					return ODataQueryExpressionVisitor.TrueConstant;
				}
				unaryOperatorKind = UnaryOperatorKind.Not;
			}
			this.type = this.CheckType(OperatorTypeflowModels.Unary(unary.Operator, typeValue));
			return new UnaryOperatorNode(unaryOperatorKind, singleValueNode);
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000BEBA4 File Offset: 0x000BCDA4
		private QueryNode VisitConstant(ConstantQueryExpression constantExpression)
		{
			Value value = constantExpression.Value;
			ConstantNode constantNode = null;
			switch (value.Kind)
			{
			case ValueKind.Null:
				constantNode = ODataQueryExpressionVisitor.NullConstant;
				goto IL_0254;
			case ValueKind.Time:
			{
				TimeOfDay timeOfDay = new TimeOfDay(value.AsTime.AsClrTimeSpan.Ticks);
				constantNode = new ConstantNode(timeOfDay, ODataUriUtils.ConvertToUriLiteral(timeOfDay, ODataQueryExpressionVisitor.literalVersion));
				goto IL_0254;
			}
			case ValueKind.Date:
			{
				DateValue asDate = value.AsDate;
				Date date = new Date(asDate.Year, asDate.Month, asDate.Day);
				constantNode = new ConstantNode(date, ODataUriUtils.ConvertToUriLiteral(date, ODataQueryExpressionVisitor.literalVersion));
				goto IL_0254;
			}
			case ValueKind.DateTimeZone:
			{
				DateTimeOffset asClrDateTimeOffset = value.AsDateTimeZone.AsClrDateTimeOffset;
				constantNode = new ConstantNode(asClrDateTimeOffset, ODataUriUtils.ConvertToUriLiteral(asClrDateTimeOffset, ODataQueryExpressionVisitor.literalVersion));
				goto IL_0254;
			}
			case ValueKind.Duration:
			{
				TimeSpan asClrTimeSpan = value.AsDuration.AsClrTimeSpan;
				constantNode = new ConstantNode(asClrTimeSpan, ODataUriUtils.ConvertToUriLiteral(asClrTimeSpan, ODataQueryExpressionVisitor.literalVersion));
				goto IL_0254;
			}
			case ValueKind.Number:
				switch (value.AsNumber.NumberKind)
				{
				case NumberKind.Int32:
				{
					int asInteger = value.AsNumber.AsInteger32;
					constantNode = new ConstantNode(asInteger, ODataUriUtils.ConvertToUriLiteral(asInteger, ODataQueryExpressionVisitor.literalVersion));
					goto IL_0254;
				}
				case NumberKind.Double:
				{
					double asDouble = value.AsNumber.AsDouble;
					constantNode = new ConstantNode(asDouble, ODataUriUtils.ConvertToUriLiteral(asDouble, ODataQueryExpressionVisitor.literalVersion));
					goto IL_0254;
				}
				case NumberKind.Decimal:
				{
					decimal asDecimal = value.AsNumber.AsDecimal;
					constantNode = new ConstantNode(asDecimal, ODataUriUtils.ConvertToUriLiteral(asDecimal, ODataQueryExpressionVisitor.literalVersion));
					goto IL_0254;
				}
				default:
					goto IL_0254;
				}
				break;
			case ValueKind.Logical:
			{
				bool asBoolean = value.AsLogical.AsBoolean;
				constantNode = new ConstantNode(asBoolean, ODataUriUtils.ConvertToUriLiteral(asBoolean, ODataQueryExpressionVisitor.literalVersion));
				goto IL_0254;
			}
			case ValueKind.Text:
			{
				string asString = value.AsString;
				constantNode = new ConstantNode(asString, ODataUriUtils.ConvertToUriLiteral(asString, ODataQueryExpressionVisitor.literalVersion));
				goto IL_0254;
			}
			case ValueKind.Binary:
			{
				byte[] asBytes = value.AsBinary.AsBytes;
				constantNode = new ConstantNode(asBytes, ODataUriUtils.ConvertToUriLiteral(asBytes, ODataQueryExpressionVisitor.literalVersion));
				goto IL_0254;
			}
			}
			throw new NotSupportedException();
			IL_0254:
			this.type = value.Type;
			return constantNode;
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000BEE12 File Offset: 0x000BD012
		private TypeValue CheckType(TypeValue type)
		{
			if (type.TypeKind == ValueKind.None)
			{
				throw new NotSupportedException();
			}
			return type;
		}

		// Token: 0x04001E72 RID: 7794
		private static readonly ConstantNode FalseConstant = new ConstantNode(false, "false", EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean, false));

		// Token: 0x04001E73 RID: 7795
		private static readonly ConstantNode TrueConstant = new ConstantNode(true, "true", EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean, false));

		// Token: 0x04001E74 RID: 7796
		private static readonly ConstantNode NullConstant = new ConstantNode(null, ODataUriUtils.ConvertToUriLiteral(null, ODataVersion.V4));

		// Token: 0x04001E75 RID: 7797
		private static readonly ODataVersion literalVersion = ODataVersion.V4;

		// Token: 0x04001E76 RID: 7798
		private readonly ODataEnvironment environment;

		// Token: 0x04001E77 RID: 7799
		private readonly SingleResourceNode source;

		// Token: 0x04001E78 RID: 7800
		private readonly RecordTypeValue sourceType;

		// Token: 0x04001E79 RID: 7801
		private readonly Capabilities capability;

		// Token: 0x04001E7A RID: 7802
		private readonly Microsoft.OData.Edm.IEdmStructuredType entityType;

		// Token: 0x04001E7B RID: 7803
		private TypeValue type;
	}
}
