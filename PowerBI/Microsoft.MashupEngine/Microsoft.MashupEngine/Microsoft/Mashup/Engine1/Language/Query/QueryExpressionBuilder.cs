using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001893 RID: 6291
	internal struct QueryExpressionBuilder
	{
		// Token: 0x06009F98 RID: 40856 RVA: 0x0020F4BB File Offset: 0x0020D6BB
		public static QueryExpression ToQueryExpression(Query query, FunctionValue function)
		{
			return QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(query), function);
		}

		// Token: 0x06009F99 RID: 40857 RVA: 0x0020F4CC File Offset: 0x0020D6CC
		public static QueryExpression ToQueryExpression(TypeValue type, FunctionValue function)
		{
			QueryExpression queryExpression;
			if (!QueryExpressionBuilder.TryToQueryExpression(type, function.Expression as IFunctionExpression, out queryExpression))
			{
				queryExpression = new InvocationQueryExpression(new ConstantQueryExpression(function), new QueryExpression[] { ArgumentAccessQueryExpression.Instance });
			}
			return queryExpression;
		}

		// Token: 0x06009F9A RID: 40858 RVA: 0x0020F509 File Offset: 0x0020D709
		public static bool TryToQueryExpression(TypeValue type, FunctionValue function, out QueryExpression queryExpression)
		{
			return QueryExpressionBuilder.TryToQueryExpression(type, function.Expression as IFunctionExpression, out queryExpression);
		}

		// Token: 0x06009F9B RID: 40859 RVA: 0x0020F520 File Offset: 0x0020D720
		public static bool TryToQueryExpression(TypeValue type, IFunctionExpression functionExpression, out QueryExpression queryExpression)
		{
			if (functionExpression != null && functionExpression.FunctionType.Parameters.Count == 1)
			{
				functionExpression = (IFunctionExpression)NormalizationVisitor.Normalize(functionExpression, new TypeValue[] { type }, true);
				if (functionExpression != null)
				{
					QueryExpressionBuilder queryExpressionBuilder = new QueryExpressionBuilder(functionExpression.FunctionType.Parameters[0].Identifier, type);
					queryExpression = queryExpressionBuilder.VisitExpression(functionExpression.Expression);
					return queryExpressionBuilder.supported;
				}
			}
			queryExpression = null;
			return false;
		}

		// Token: 0x06009F9C RID: 40860 RVA: 0x0020F598 File Offset: 0x0020D798
		private QueryExpressionBuilder(Identifier parameter, TypeValue type)
		{
			this.parameter = parameter;
			this.type = type;
			this.supported = true;
			if (type.IsRecordType)
			{
				this.keys = type.AsRecordType.FieldKeys;
				this.getColumnType = QueryExpressionBuilder.getRecordField;
				return;
			}
			if (type.IsTableType)
			{
				this.keys = type.AsTableType.ItemType.FieldKeys;
				this.getColumnType = QueryExpressionBuilder.getTableColumn;
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06009F9D RID: 40861 RVA: 0x0020F610 File Offset: 0x0020D810
		private QueryExpression VisitExpression(IExpression expression)
		{
			if (!this.supported)
			{
				return QueryExpressionBuilder.sentinel;
			}
			ExpressionKind kind = expression.Kind;
			switch (kind)
			{
			case ExpressionKind.Binary:
				return this.VisitBinary((IBinaryExpression)expression);
			case ExpressionKind.Constant:
				return this.VisitConstant((IConstantExpression)expression);
			case ExpressionKind.ElementAccess:
			case ExpressionKind.Exports:
			case ExpressionKind.Function:
				break;
			case ExpressionKind.FieldAccess:
				return this.VisitFieldAccess((IFieldAccessExpression)expression);
			case ExpressionKind.Identifier:
				return this.VisitIdentifier((IIdentifierExpression)expression);
			case ExpressionKind.If:
				return this.VisitIf((IIfExpression)expression);
			case ExpressionKind.Invocation:
				return this.VisitInvocation((IInvocationExpression)expression);
			default:
				if (kind == ExpressionKind.Unary)
				{
					return this.VisitUnary((IUnaryExpression)expression);
				}
				break;
			}
			this.supported = false;
			return QueryExpressionBuilder.sentinel;
		}

		// Token: 0x06009F9E RID: 40862 RVA: 0x0020F6CA File Offset: 0x0020D8CA
		private QueryExpression VisitBinary(IBinaryExpression binary)
		{
			return new BinaryQueryExpression(binary.Operator, this.VisitExpression(binary.Left), this.VisitExpression(binary.Right));
		}

		// Token: 0x06009F9F RID: 40863 RVA: 0x0020F6EF File Offset: 0x0020D8EF
		private QueryExpression VisitConstant(IConstantExpression constant)
		{
			return new ConstantQueryExpression(constant.Value);
		}

		// Token: 0x06009FA0 RID: 40864 RVA: 0x0020F6FC File Offset: 0x0020D8FC
		private QueryExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			if (!identifier.Name.Equals(this.parameter))
			{
				throw new NotSupportedException("Field access must be to parameter record.");
			}
			return ArgumentAccessQueryExpression.Instance;
		}

		// Token: 0x06009FA1 RID: 40865 RVA: 0x0020F724 File Offset: 0x0020D924
		private QueryExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			QueryExpression queryExpression = this.VisitExpression(fieldAccess.Expression);
			int num;
			if (queryExpression.Kind == QueryExpressionKind.ArgumentAccess && !fieldAccess.IsOptional && this.keys.TryGetKeyIndex(fieldAccess.MemberName.Name, out num))
			{
				return new ColumnAccessQueryExpression(num);
			}
			FunctionValue functionValue = null;
			int num2;
			if (queryExpression.TryGetColumnAccess(out num2))
			{
				TypeValue typeValue = this.getColumnType(this.type, num2);
				if (typeValue.IsRecordType)
				{
					functionValue = ((typeValue.IsNullable || fieldAccess.IsOptional) ? Library.Record.FieldOrDefault : Library.Record.Field);
				}
			}
			if (functionValue == null)
			{
				functionValue = (fieldAccess.IsOptional ? Library.Collection.FieldOrNull : Library.Collection.Field);
			}
			return new InvocationQueryExpression(new ConstantQueryExpression(functionValue), new QueryExpression[]
			{
				queryExpression,
				new ConstantQueryExpression(TextValue.New(fieldAccess.MemberName.Name))
			});
		}

		// Token: 0x06009FA2 RID: 40866 RVA: 0x0020F7FC File Offset: 0x0020D9FC
		private QueryExpression VisitIf(IIfExpression ifExpression)
		{
			return new IfQueryExpression(this.VisitExpression(ifExpression.Condition), this.VisitExpression(ifExpression.TrueCase), this.VisitExpression(ifExpression.FalseCase));
		}

		// Token: 0x06009FA3 RID: 40867 RVA: 0x0020F828 File Offset: 0x0020DA28
		private QueryExpression VisitInvocation(IInvocationExpression invocation)
		{
			QueryExpression[] array = new QueryExpression[invocation.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.VisitExpression(invocation.Arguments[i]);
			}
			return new InvocationQueryExpression(this.VisitExpression(invocation.Function), array);
		}

		// Token: 0x06009FA4 RID: 40868 RVA: 0x0020F87B File Offset: 0x0020DA7B
		private QueryExpression VisitUnary(IUnaryExpression unary)
		{
			return new UnaryQueryExpression(unary.Operator, this.VisitExpression(unary.Expression));
		}

		// Token: 0x040053AD RID: 21421
		private static readonly Func<TypeValue, int, TypeValue> getRecordField = delegate(TypeValue type, int column)
		{
			bool flag;
			return type.AsRecordType.GetFieldType(column, out flag);
		};

		// Token: 0x040053AE RID: 21422
		private static readonly Func<TypeValue, int, TypeValue> getTableColumn = delegate(TypeValue type, int column)
		{
			bool flag2;
			return ListTypeValue.New(type.AsTableType.ItemType.GetFieldType(column, out flag2));
		};

		// Token: 0x040053AF RID: 21423
		private static readonly QueryExpression sentinel = new ConstantQueryExpression(Value.Null);

		// Token: 0x040053B0 RID: 21424
		private readonly Identifier parameter;

		// Token: 0x040053B1 RID: 21425
		private readonly TypeValue type;

		// Token: 0x040053B2 RID: 21426
		private readonly Keys keys;

		// Token: 0x040053B3 RID: 21427
		private readonly Func<TypeValue, int, TypeValue> getColumnType;

		// Token: 0x040053B4 RID: 21428
		private bool supported;
	}
}
