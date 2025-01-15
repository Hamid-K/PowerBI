using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Query;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3.Compiler
{
	// Token: 0x020008F7 RID: 2295
	internal sealed class ODataQueryCreator : EnvironmentAstVisitor<object>
	{
		// Token: 0x06004171 RID: 16753 RVA: 0x000DBE1C File Offset: 0x000DA01C
		private ODataQueryCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, ODataEnvironment externalEnvironment, int maxUriLength)
			: base(expression, cursor, externalEnvironment)
		{
			this.queryPlan = new ODataQueryPlan(expression, base.GetType(expression), externalEnvironment.QueryOptions, maxUriLength);
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000DBE42 File Offset: 0x000DA042
		internal static ODataQueryPlan Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, ODataEnvironment externalEnvironment)
		{
			ODataQueryCreator odataQueryCreator = new ODataQueryCreator(expression, cursor, externalEnvironment, externalEnvironment.UserSettings.MaxUriLength);
			odataQueryCreator.Visit(expression);
			return odataQueryCreator.queryPlan;
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000DBE64 File Offset: 0x000DA064
		private void CreateListFunction(IInvocationExpression invocation, Action<IInvocationExpression> create)
		{
			IExpression expression = invocation.Arguments[0];
			base.Visit(expression);
			create(invocation);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x000DBE8D File Offset: 0x000DA08D
		private void CreateInlineInvocation(IInvocationExpression inline)
		{
			this.VisitArgumentAsLambda(inline, 1, new Func<IExpression, IExpression>(this.CreateInlineExpression));
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x000DBEA4 File Offset: 0x000DA0A4
		private IExpression CreateInlineExpression(IExpression expression)
		{
			IListExpression listExpression = expression as IListExpression;
			if (listExpression == null)
			{
				return expression;
			}
			foreach (IExpression expression2 in listExpression.Members.OfType<IExpression>())
			{
				IFieldAccessExpression fieldAccessExpression = expression2 as IFieldAccessExpression;
				if (fieldAccessExpression != null)
				{
					TypeValue type = base.GetType(fieldAccessExpression.Expression);
					if (type.TypeKind == ValueKind.Record)
					{
						TypeValue asType = type.AsRecordType.Fields[fieldAccessExpression.MemberName.Name]["Type"].AsType;
						string text;
						if (asType.TypeKind == ValueKind.Table || PreviewServices.IsDelayed(asType, out text))
						{
							this.queryPlan.ExpandTokens.Add(this.CreatePropertyAccess(expression2));
						}
					}
				}
			}
			return expression;
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000DBF80 File Offset: 0x000DA180
		private void CreateListSelectInvocation(IInvocationExpression select)
		{
			this.VisitArgumentAsLambda(select, 1, new Func<IExpression, IExpression>(this.CreateListSelectExpression));
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000DBF98 File Offset: 0x000DA198
		private void CreateListSortInvocation(IInvocationExpression sort)
		{
			IListExpression listExpression = EnvironmentAstVisitor<object>.Reduce(sort.Arguments[1]) as IListExpression;
			List<OrderByQueryToken> list = new List<OrderByQueryToken>(this.queryPlan.OrderByTokens);
			HashSet<string> hashSet = new HashSet<string>();
			this.queryPlan.OrderByTokens.Clear();
			for (int i = 0; i < listExpression.Members.Count; i++)
			{
				IRecordExpression recordExpression = EnvironmentAstVisitor<object>.Reduce(listExpression.Members[i]) as IRecordExpression;
				base.Cursor.Push(listExpression, i);
				IExpression value = recordExpression.Members.FirstOrDefault((VariableInitializer m) => m.Name.Equals("Ascending")).Value;
				IExpression value2 = recordExpression.Members.First((VariableInitializer m) => m.Name.Equals("KeySelector")).Value;
				base.Cursor.Push(recordExpression, "KeySelector");
				OrderByQueryToken orderByQueryToken = this.CreateOrderBy(value2, value);
				PropertyAccessQueryToken propertyAccessQueryToken = (PropertyAccessQueryToken)orderByQueryToken.Expression;
				if (hashSet.Add(propertyAccessQueryToken.Name))
				{
					this.queryPlan.OrderByTokens.Add(orderByQueryToken);
				}
				base.Cursor.Pop();
				base.Cursor.Pop();
			}
			foreach (OrderByQueryToken orderByQueryToken2 in list)
			{
				if (hashSet.Add(((PropertyAccessQueryToken)orderByQueryToken2.Expression).Name))
				{
					this.queryPlan.OrderByTokens.Add(orderByQueryToken2);
				}
			}
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000DC160 File Offset: 0x000DA360
		private IExpression CreateListSelectExpression(IExpression expression)
		{
			QueryToken queryToken = this.CreateSelectExpression(expression, base.GetType(expression));
			if (queryToken != LiteralConverter.LiteralTokenTrue)
			{
				if (this.queryPlan.Filter != null)
				{
					queryToken = new BinaryOperatorQueryToken(Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.And, this.queryPlan.Filter, queryToken);
				}
				this.queryPlan.Filter = queryToken;
			}
			return expression;
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000DC1B1 File Offset: 0x000DA3B1
		private void CreateListTransformInvocation(IInvocationExpression transform)
		{
			this.VisitArgumentAsLambda(transform, 1, new Func<IExpression, IExpression>(this.CreateTransformExpression));
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000DC1C8 File Offset: 0x000DA3C8
		private PropertyAccessQueryToken CreatePropertyAccess(IExpression expression)
		{
			expression = EnvironmentAstVisitor<object>.Reduce(expression);
			ExpressionKind kind = expression.Kind;
			if (kind != ExpressionKind.Constant)
			{
				switch (kind)
				{
				case ExpressionKind.FieldAccess:
				{
					IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)expression;
					PropertyAccessQueryToken propertyAccessQueryToken = this.CreatePropertyAccess(fieldAccessExpression.Expression);
					return new PropertyAccessQueryToken(EdmNameEncoder.Encode(fieldAccessExpression.MemberName), propertyAccessQueryToken);
				}
				case ExpressionKind.Identifier:
					return null;
				case ExpressionKind.Invocation:
					goto IL_0031;
				}
				throw new InvalidOperationException();
			}
			IL_0031:
			base.Visit(expression);
			return null;
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000DC244 File Offset: 0x000DA444
		private OrderByQueryToken CreateOrderBy(IExpression keySelector, IExpression ordering)
		{
			keySelector = EnvironmentAstVisitor<object>.Reduce(keySelector);
			Microsoft.Data.Experimental.OData.Query.OrderByDirection orderByDirection = Microsoft.Data.Experimental.OData.Query.OrderByDirection.Ascending;
			if (ordering != null)
			{
				ordering = EnvironmentAstVisitor<object>.Reduce(ordering);
				if (((IConstantExpression)ordering).Value.Equals(LogicalValue.False))
				{
					orderByDirection = Microsoft.Data.Experimental.OData.Query.OrderByDirection.Descending;
				}
			}
			return new OrderByQueryToken(this.CreatePropertyAccess(((IFunctionExpression)keySelector).Expression), orderByDirection);
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000DC298 File Offset: 0x000DA498
		private QueryToken CreateSelectBinaryExpression(IBinaryExpression binary)
		{
			TypeValue type = base.GetType(binary.Left);
			TypeValue type2 = base.GetType(binary.Right);
			this.NormalizeTypes(ref type, ref type2);
			QueryToken queryToken = this.CreateSelectExpression(binary.Left, type);
			QueryToken queryToken2 = this.CreateSelectExpression(binary.Right, type2);
			Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind binaryOperatorKind;
			switch (binary.Operator)
			{
			case BinaryOperator2.Add:
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Add;
				goto IL_0247;
			case BinaryOperator2.Subtract:
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Subtract;
				goto IL_0247;
			case BinaryOperator2.Multiply:
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Multiply;
				goto IL_0247;
			case BinaryOperator2.Divide:
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Divide;
				goto IL_0247;
			case BinaryOperator2.GreaterThan:
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.GreaterThan;
				goto IL_0247;
			case BinaryOperator2.LessThan:
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.LessThan;
				goto IL_0247;
			case BinaryOperator2.GreaterThanOrEquals:
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.GreaterThanOrEqual;
				goto IL_0247;
			case BinaryOperator2.LessThanOrEquals:
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.LessThanOrEqual;
				goto IL_0247;
			case BinaryOperator2.Equals:
				if (!TypeServices.IsCompatibleType(type, type2) && queryToken != LiteralConverter.LiteralTokenNull && queryToken2 != LiteralConverter.LiteralTokenNull)
				{
					if (type.IsNullable && type2.IsNullable)
					{
						return new BinaryOperatorQueryToken(Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.And, new BinaryOperatorQueryToken(Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Equal, queryToken, LiteralConverter.LiteralTokenNull), new BinaryOperatorQueryToken(Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Equal, queryToken2, LiteralConverter.LiteralTokenNull));
					}
					return LiteralConverter.LiteralTokenFalse;
				}
				else
				{
					if (queryToken == queryToken2)
					{
						return LiteralConverter.LiteralTokenTrue;
					}
					if (LiteralConverter.AreTrueFalseLiterals(new QueryToken[] { queryToken, queryToken2 }))
					{
						return LiteralConverter.LiteralTokenFalse;
					}
					binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Equal;
					goto IL_0247;
				}
				break;
			case BinaryOperator2.NotEquals:
				if (!TypeServices.IsCompatibleType(type, type2) && LiteralConverter.LiteralTokenNull != queryToken && LiteralConverter.LiteralTokenNull != queryToken2)
				{
					if (type.IsNullable && type2.IsNullable)
					{
						return new BinaryOperatorQueryToken(Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Or, new BinaryOperatorQueryToken(Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.NotEqual, queryToken, LiteralConverter.LiteralTokenNull), new BinaryOperatorQueryToken(Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.NotEqual, queryToken2, LiteralConverter.LiteralTokenNull));
					}
					return LiteralConverter.LiteralTokenTrue;
				}
				else
				{
					if (queryToken == queryToken2)
					{
						return LiteralConverter.LiteralTokenFalse;
					}
					if (LiteralConverter.AreTrueFalseLiterals(new QueryToken[] { queryToken, queryToken2 }))
					{
						return LiteralConverter.LiteralTokenTrue;
					}
					binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.NotEqual;
					goto IL_0247;
				}
				break;
			case BinaryOperator2.And:
				if (queryToken == LiteralConverter.LiteralTokenFalse || queryToken2 == LiteralConverter.LiteralTokenFalse)
				{
					return LiteralConverter.LiteralTokenFalse;
				}
				if (queryToken == LiteralConverter.LiteralTokenTrue)
				{
					return queryToken2;
				}
				if (queryToken2 == LiteralConverter.LiteralTokenTrue)
				{
					return queryToken;
				}
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.And;
				goto IL_0247;
			case BinaryOperator2.Or:
				if (queryToken == LiteralConverter.LiteralTokenTrue || queryToken2 == LiteralConverter.LiteralTokenTrue)
				{
					return LiteralConverter.LiteralTokenTrue;
				}
				if (queryToken == LiteralConverter.LiteralTokenFalse)
				{
					return queryToken2;
				}
				if (queryToken2 == LiteralConverter.LiteralTokenFalse)
				{
					return queryToken;
				}
				binaryOperatorKind = Microsoft.Data.Experimental.OData.Query.BinaryOperatorKind.Or;
				goto IL_0247;
			case BinaryOperator2.Concatenate:
				return FunctionConversions.Convert(Library.Text.Combine, new QueryToken[] { queryToken, queryToken2 });
			}
			throw new InvalidOperationException();
			IL_0247:
			return new BinaryOperatorQueryToken(binaryOperatorKind, queryToken, queryToken2);
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000DC4F8 File Offset: 0x000DA6F8
		private void NormalizeTypes(ref TypeValue leftType, ref TypeValue rightType)
		{
			if (leftType.NonNullable.Equals(TypeValue.Guid) && rightType.TypeKind == ValueKind.Text)
			{
				rightType = TypeValue.Guid;
				return;
			}
			if (rightType.NonNullable.Equals(TypeValue.Guid) && leftType.TypeKind == ValueKind.Text)
			{
				leftType = TypeValue.Guid;
			}
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000DC550 File Offset: 0x000DA750
		private QueryToken CreateSelectExpression(IExpression expression, TypeValue type)
		{
			expression = EnvironmentAstVisitor<object>.Reduce(expression);
			ExpressionKind kind = expression.Kind;
			switch (kind)
			{
			case ExpressionKind.Binary:
				return this.CreateSelectBinaryExpression((IBinaryExpression)expression);
			case ExpressionKind.Constant:
				return LiteralConverter.Convert(((IConstantExpression)expression).Value, type);
			case ExpressionKind.ElementAccess:
			case ExpressionKind.Exports:
			case ExpressionKind.Function:
			case ExpressionKind.If:
			case ExpressionKind.Let:
				break;
			case ExpressionKind.FieldAccess:
			case ExpressionKind.Identifier:
				return this.CreatePropertyAccess(expression);
			case ExpressionKind.Invocation:
				return this.CreateSelectInvocationExpression((IInvocationExpression)expression);
			case ExpressionKind.List:
			{
				IListExpression listExpression = (IListExpression)expression;
				byte[] array = new byte[listExpression.Members.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ((IConstantExpression)listExpression.Members[i]).Value.AsNumber.ToByte();
				}
				return LiteralConverter.Convert(BinaryValue.New(array), type);
			}
			default:
				if (kind == ExpressionKind.Unary)
				{
					return this.CreateSelectUnaryExpression((IUnaryExpression)expression);
				}
				break;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000DC644 File Offset: 0x000DA844
		private QueryToken CreateSelectInvocationExpression(IInvocationExpression invocation)
		{
			IConstantExpression constantExpression = (IConstantExpression)invocation.Function;
			if (constantExpression.Value.Equals(Library.Text.Combine))
			{
				TypeValue[] parameterResults = base.Cursor.GetParameterResults(invocation);
				IListExpression listExpression = (IListExpression)invocation.Arguments[0];
				ListTypeValue asListType = parameterResults[0].AsListType;
				QueryToken[] array = new QueryToken[listExpression.Members.Count];
				for (int i = 0; i < listExpression.Members.Count; i++)
				{
					array[i] = this.CreateSelectExpression(listExpression.Members[i], asListType.ItemType);
				}
				return FunctionConversions.Convert(Library.Text.Combine, array);
			}
			if (constantExpression.Value.Equals(Library._Value.As) && invocation.Arguments.Count == 2 && invocation.Arguments[1].Kind == ExpressionKind.Constant)
			{
				TypeValue asType = ((IConstantExpression)invocation.Arguments[1]).Value.AsType;
				return this.CreateSelectExpression(invocation.Arguments[0], asType);
			}
			return FunctionConversions.Convert(constantExpression.Value.AsFunction, this.CreateSelectInvocationExpressionArguments(invocation.Arguments, base.Cursor.GetParameterResults(invocation)));
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x000DC778 File Offset: 0x000DA978
		private QueryToken[] CreateSelectInvocationExpressionArguments(IList<IExpression> args, TypeValue[] paramTypes)
		{
			QueryToken[] array = new QueryToken[args.Count];
			for (int i = 0; i < args.Count; i++)
			{
				array[i] = this.CreateSelectExpression(args[i], paramTypes[i]);
			}
			return array;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x000DC7B8 File Offset: 0x000DA9B8
		private QueryToken CreateSelectUnaryExpression(IUnaryExpression unary)
		{
			QueryToken queryToken = this.CreateSelectExpression(unary.Expression, base.GetType(unary.Expression));
			UnaryOperator2 @operator = unary.Operator;
			Microsoft.Data.Experimental.OData.Query.UnaryOperatorKind unaryOperatorKind;
			if (@operator != UnaryOperator2.Not)
			{
				if (@operator != UnaryOperator2.Negative)
				{
					throw new InvalidOperationException();
				}
				unaryOperatorKind = Microsoft.Data.Experimental.OData.Query.UnaryOperatorKind.Negate;
			}
			else
			{
				if (queryToken == LiteralConverter.LiteralTokenTrue)
				{
					return LiteralConverter.LiteralTokenFalse;
				}
				if (queryToken == LiteralConverter.LiteralTokenFalse)
				{
					return LiteralConverter.LiteralTokenTrue;
				}
				unaryOperatorKind = Microsoft.Data.Experimental.OData.Query.UnaryOperatorKind.Not;
			}
			return new UnaryOperatorQueryToken(unaryOperatorKind, queryToken);
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x000DC820 File Offset: 0x000DAA20
		private IExpression CreateTransformExpression(IExpression expression)
		{
			List<PropertyAccessQueryToken> list = new List<PropertyAccessQueryToken>();
			ExpressionKind kind = expression.Kind;
			if (kind <= ExpressionKind.Identifier)
			{
				if (kind == ExpressionKind.FieldAccess || kind == ExpressionKind.Identifier)
				{
					PropertyAccessQueryToken propertyAccessQueryToken = this.CreatePropertyAccess(expression);
					if (propertyAccessQueryToken != null)
					{
						list.Add(propertyAccessQueryToken);
						goto IL_005F;
					}
					goto IL_005F;
				}
			}
			else
			{
				if (kind == ExpressionKind.MultiFieldRecordProjection)
				{
					this.CreateTransformProjection((IMultiFieldRecordProjectionExpression)expression, list);
					goto IL_005F;
				}
				if (kind == ExpressionKind.Record)
				{
					this.CreateTransformProjection((IRecordExpression)expression, list);
					goto IL_005F;
				}
			}
			throw new InvalidOperationException();
			IL_005F:
			if (list.Count > 0)
			{
				this.queryPlan.SelectTokens.Clear();
				foreach (PropertyAccessQueryToken propertyAccessQueryToken2 in list)
				{
					this.queryPlan.SelectTokens.Add(propertyAccessQueryToken2);
				}
			}
			return expression;
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x000DC8F4 File Offset: 0x000DAAF4
		private void CreateTransformProjection(IRecordExpression record, List<PropertyAccessQueryToken> newReferences)
		{
			for (int i = 0; i < record.Members.Count; i++)
			{
				Identifier name = record.Members[i].Name;
				base.Cursor.Push(record, name);
				string text = EdmNameEncoder.Encode(name);
				newReferences.Add(new PropertyAccessQueryToken(text, null));
				base.Cursor.Pop();
			}
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x000DC960 File Offset: 0x000DAB60
		private void CreateTransformProjection(IMultiFieldRecordProjectionExpression projection, List<PropertyAccessQueryToken> newReferences)
		{
			if (base.MultifieldProjectionIsNoop(projection))
			{
				return;
			}
			for (int i = 0; i < projection.MemberNames.Count; i++)
			{
				PropertyAccessQueryToken propertyAccessQueryToken = new PropertyAccessQueryToken(EdmNameEncoder.Encode(projection.MemberNames[i].Name), null);
				newReferences.Add(propertyAccessQueryToken);
			}
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x000DC9B4 File Offset: 0x000DABB4
		private TypeValue VisitArgumentAsLambda(IInvocationExpression invocation, int argumentIndex, Func<IExpression, IExpression> visit)
		{
			IFunctionExpression functionExpression = (IFunctionExpression)EnvironmentAstVisitor<object>.Reduce(invocation.Arguments[argumentIndex]);
			FunctionTypeValue asFunctionType = base.Cursor.GetParameterResults(invocation)[argumentIndex].AsFunctionType;
			object[] array = new object[asFunctionType.ParameterCount];
			return base.VisitLambda(functionExpression, asFunctionType, EnvironmentAstVisitor<object>.GetParameterTypes(asFunctionType), visit, array);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x000DCA08 File Offset: 0x000DAC08
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			throw new NotSupportedException(binary.Kind.ToString());
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x000DCA30 File Offset: 0x000DAC30
		protected override IExpression VisitConstant(IConstantExpression constantExpression)
		{
			constantExpression = (IConstantExpression)base.VisitConstant(constantExpression);
			QueryResultTableValue queryResultTableValue = constantExpression.Value as QueryResultTableValue;
			if (queryResultTableValue != null && queryResultTableValue.Identifier != EnvironmentAstVisitor<object>.PlaceholderIdentifier)
			{
				this.queryPlan.Segment = new SegmentQueryToken(queryResultTableValue.Identifier, null, null);
			}
			return constantExpression;
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x000DCA8C File Offset: 0x000DAC8C
		protected override IExpression VisitExports(IExportsExpression exports)
		{
			throw new NotSupportedException(exports.Kind.ToString());
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x000DCAB4 File Offset: 0x000DACB4
		protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			if (base.GetType(fieldAccess.Expression).TypeKind == ValueKind.List)
			{
				this.CreateTransformExpression(fieldAccess);
			}
			else
			{
				fieldAccess = (IFieldAccessExpression)base.VisitFieldAccess(fieldAccess);
				string text = EdmNameEncoder.Encode(fieldAccess.MemberName);
				this.queryPlan.Segment = new SegmentQueryToken(text, this.queryPlan.Segment, null);
			}
			return fieldAccess;
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x000DCB20 File Offset: 0x000DAD20
		protected override IExpression VisitFunction(IFunctionExpression function)
		{
			throw new NotSupportedException(function.Kind.ToString());
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000DCB48 File Offset: 0x000DAD48
		protected override IExpression VisitFunctionType(IFunctionTypeExpression functionType)
		{
			throw new NotSupportedException(functionType.Kind.ToString());
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000DCB70 File Offset: 0x000DAD70
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			throw new NotSupportedException(identifier.Kind.ToString());
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000DCB98 File Offset: 0x000DAD98
		protected override IExpression VisitIf(IIfExpression @if)
		{
			throw new NotSupportedException(@if.Kind.ToString());
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000DCBC0 File Offset: 0x000DADC0
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Function.Simplify();
			if (expression.Kind == ExpressionKind.Constant)
			{
				IConstantExpression constantExpression = (IConstantExpression)expression;
				if (constantExpression.Value.Equals(TableModule.Table.SelectRows))
				{
					this.CreateListFunction(invocation, new Action<IInvocationExpression>(this.CreateListSelectInvocation));
				}
				else if (constantExpression.Value.Equals(Library.ListRuntime.Transform))
				{
					this.CreateListFunction(invocation, new Action<IInvocationExpression>(this.CreateListTransformInvocation));
				}
				else if (constantExpression.Value.Equals(TableModule.Table.Sort))
				{
					this.CreateListFunction(invocation, new Action<IInvocationExpression>(this.CreateListSortInvocation));
				}
				else if (constantExpression.Value.Equals(TableModule.Table.ForceColumns))
				{
					this.CreateListFunction(invocation, new Action<IInvocationExpression>(this.CreateInlineInvocation));
				}
				else
				{
					if (!(constantExpression.Value is QueryResultFunctionValue))
					{
						throw new InvalidOperationException(constantExpression.Value.GetUnderlyingClrType().FullName);
					}
					string text = EdmNameEncoder.Encode(((QueryResultFunctionValue)constantExpression.Value).FunctionName);
					this.queryPlan.Segment = new SegmentQueryToken(text, null, null);
					FunctionTypeValue asFunctionType = base.GetType(constantExpression).AsFunctionType;
					for (int i = 0; i < invocation.Arguments.Count; i++)
					{
						IExpression expression2 = EnvironmentAstVisitor<object>.Reduce(invocation.Arguments[i]);
						if (expression2.Kind != ExpressionKind.Constant)
						{
							throw new InvalidOperationException(expression2.Kind.ToString());
						}
						ODataEnvironment odataEnvironment = (ODataEnvironment)base.ExternalEnvironment;
						IResource resource = Resource.New(odataEnvironment.Resource.Kind, odataEnvironment.ServiceUri.AbsoluteUri);
						string text2;
						if (ODataQueryCreator.TryCreateConstantQueryStringValue(odataEnvironment.Host, asFunctionType.Parameters[i], ((IConstantExpression)expression2).Value, odataEnvironment.EdmModel, resource, out text2))
						{
							this.queryPlan.AddOrUpdateQueryOption(asFunctionType.Parameters.Keys[i], text2);
						}
					}
				}
			}
			return invocation;
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x000DCDCC File Offset: 0x000DAFCC
		internal static bool TryCreateConstantQueryStringValue(IEngineHost engineHost, Value functionParameter, Value constantValue, IEdmModel model, IResource resource, out string queryStringValue)
		{
			ValueKind kind = constantValue.Kind;
			if (kind == ValueKind.List)
			{
				Value value = functionParameter.MetaValue["EdmParameterType"];
				IEdmCollectionType edmCollectionType = ODataTypeServices.FindEdmCollectionType(model, value.AsString);
				ODataCollectionValue odataCollectionValue = ODataObjectConverter.GetODataCollectionValue(engineHost, constantValue.AsList, edmCollectionType, resource);
				queryStringValue = Uri.EscapeDataString(ODataUriUtils.ConvertToUriLiteral(odataCollectionValue, ODataVersion.V3, model, ODataFormat.Json));
				return true;
			}
			if (kind == ValueKind.Record)
			{
				Value value2 = functionParameter.MetaValue["EdmParameterType"];
				IEdmSchemaType edmSchemaType = model.FindDeclaredType(value2.AsString);
				ODataComplexValue odataComplexValue = ODataObjectConverter.GetODataComplexValue(engineHost, constantValue.AsRecord, (IEdmComplexType)edmSchemaType, resource);
				queryStringValue = Uri.EscapeDataString(ODataUriUtils.ConvertToUriLiteral(odataComplexValue, ODataVersion.V3, model, ODataFormat.Json));
				return true;
			}
			LiteralQueryToken literalQueryToken = LiteralConverter.Convert(constantValue, functionParameter.AsType);
			if (literalQueryToken != LiteralConverter.LiteralTokenNull)
			{
				queryStringValue = Uri.UnescapeDataString(ODataUriBuilder.GetUriRepresentation(literalQueryToken.Value));
				return true;
			}
			queryStringValue = null;
			return false;
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x000DCEB8 File Offset: 0x000DB0B8
		protected override IExpression VisitLet(ILetExpression let)
		{
			throw new NotSupportedException(let.Kind.ToString());
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x000DCEE0 File Offset: 0x000DB0E0
		protected override IExpression VisitList(IListExpression list)
		{
			throw new NotSupportedException(list.Kind.ToString());
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x000DCF08 File Offset: 0x000DB108
		protected override IExpression VisitListType(IListTypeExpression listType)
		{
			throw new NotSupportedException(listType.Kind.ToString());
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000DCF30 File Offset: 0x000DB130
		protected override IExpression VisitTableType(ITableTypeExpression tableType)
		{
			throw new NotSupportedException(tableType.Kind.ToString());
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000DCF56 File Offset: 0x000DB156
		protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			multiFieldRecordProjection = (IMultiFieldRecordProjectionExpression)base.VisitMultiFieldRecordProjection(multiFieldRecordProjection);
			return this.CreateTransformExpression(multiFieldRecordProjection);
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000DCF70 File Offset: 0x000DB170
		protected override IExpression VisitNullableType(INullableTypeExpression nullableType)
		{
			throw new NotSupportedException(nullableType.Kind.ToString());
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000DCF98 File Offset: 0x000DB198
		protected override IExpression VisitRecordType(IRecordTypeExpression recordType)
		{
			throw new NotSupportedException(recordType.Kind.ToString());
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000DCFC0 File Offset: 0x000DB1C0
		protected override IExpression VisitRecord(IRecordExpression record)
		{
			throw new NotSupportedException(record.Kind.ToString());
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000DCFE8 File Offset: 0x000DB1E8
		protected override IExpression VisitThrow(IThrowExpression @throw)
		{
			throw new NotSupportedException(@throw.Kind.ToString());
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000DD010 File Offset: 0x000DB210
		protected override IExpression VisitTryCatch(ITryCatchExpression tryCatch)
		{
			throw new NotSupportedException(tryCatch.Kind.ToString());
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000DD038 File Offset: 0x000DB238
		protected override IExpression VisitUnary(IUnaryExpression unary)
		{
			throw new NotSupportedException(unary.Kind.ToString());
		}

		// Token: 0x0400225E RID: 8798
		private ODataQueryPlan queryPlan;
	}
}
