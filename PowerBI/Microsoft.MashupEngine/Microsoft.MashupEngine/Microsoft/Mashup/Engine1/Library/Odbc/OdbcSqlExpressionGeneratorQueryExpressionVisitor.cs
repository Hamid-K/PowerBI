using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000658 RID: 1624
	internal sealed class OdbcSqlExpressionGeneratorQueryExpressionVisitor : OdbcQueryExpressionVisitor
	{
		// Token: 0x06003368 RID: 13160 RVA: 0x000A43C2 File Offset: 0x000A25C2
		public OdbcSqlExpressionGeneratorQueryExpressionVisitor(OdbcSqlExpressionGenerator generator, OdbcDataSource dataSource, IList<SelectItem> selectItems, OdbcQueryColumnInfo[] columns, bool allowAggregates, bool softNumbers, bool tolerateConcatOverflow, int[] groupKey = null)
			: base(dataSource, selectItems, columns, allowAggregates, softNumbers, tolerateConcatOverflow, groupKey)
		{
			this.generator = generator;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000A43DD File Offset: 0x000A25DD
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, BinaryValue value)
		{
			return this.VisitConstant<BinaryValue>(typeInfo, value, new Func<OdbcTypeInfo, BinaryValue, SqlExpression>(base.VisitConstant));
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000A43F3 File Offset: 0x000A25F3
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, DateTimeValue value)
		{
			return this.VisitConstant<DateTimeValue>(typeInfo, value, new Func<OdbcTypeInfo, DateTimeValue, SqlExpression>(base.VisitConstant));
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000A4409 File Offset: 0x000A2609
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, DateValue value)
		{
			return this.VisitConstant<DateValue>(typeInfo, value, new Func<OdbcTypeInfo, DateValue, SqlExpression>(base.VisitConstant));
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000A441F File Offset: 0x000A261F
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, DurationValue value)
		{
			return this.VisitConstant<DurationValue>(typeInfo, value, new Func<OdbcTypeInfo, DurationValue, SqlExpression>(base.VisitConstant));
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000A4435 File Offset: 0x000A2635
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, LogicalValue value)
		{
			return this.VisitConstant<LogicalValue>(typeInfo, value, new Func<OdbcTypeInfo, LogicalValue, SqlExpression>(base.VisitConstant));
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000A444B File Offset: 0x000A264B
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, TextValue value)
		{
			return this.VisitConstant<TextValue>(typeInfo, value, new Func<OdbcTypeInfo, TextValue, SqlExpression>(base.VisitConstant));
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000A4461 File Offset: 0x000A2661
		protected override SqlExpression VisitConstantDecimal(OdbcTypeInfo typeInfo, NumberValue value)
		{
			return this.VisitConstant<NumberValue>(typeInfo, value, new Func<OdbcTypeInfo, NumberValue, SqlExpression>(base.VisitConstantDecimal));
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000A4477 File Offset: 0x000A2677
		protected override SqlExpression VisitConstantDouble(OdbcTypeInfo typeInfo, NumberValue value)
		{
			return this.VisitConstant<NumberValue>(typeInfo, value, new Func<OdbcTypeInfo, NumberValue, SqlExpression>(base.VisitConstantDouble));
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000A448D File Offset: 0x000A268D
		protected override SqlExpression VisitConstantInt32(OdbcTypeInfo typeInfo, NumberValue value)
		{
			return this.VisitConstant<NumberValue>(typeInfo, value, new Func<OdbcTypeInfo, NumberValue, SqlExpression>(base.VisitConstantInt32));
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000A44A3 File Offset: 0x000A26A3
		protected override SqlExpression VisitConstantInt64(OdbcTypeInfo typeInfo, NumberValue value)
		{
			return this.VisitConstant<NumberValue>(typeInfo, value, new Func<OdbcTypeInfo, NumberValue, SqlExpression>(base.VisitConstantInt64));
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000A44B9 File Offset: 0x000A26B9
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, TimeValue value)
		{
			return this.VisitConstant<TimeValue>(typeInfo, value, new Func<OdbcTypeInfo, TimeValue, SqlExpression>(base.VisitConstant));
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000A44D0 File Offset: 0x000A26D0
		protected override Dictionary<FunctionValue, Func<InvocationQueryExpression, OdbcSqlExpression>> GetFunctionVisitors()
		{
			Dictionary<FunctionValue, Func<InvocationQueryExpression, OdbcSqlExpression>> functionVisitors = base.GetFunctionVisitors();
			ListValue listValue;
			if (this.generator.TryGetAdditionalFunctions(out listValue))
			{
				foreach (IValueReference valueReference in listValue)
				{
					functionVisitors[valueReference.Value.AsFunction] = new Func<InvocationQueryExpression, OdbcSqlExpression>(this.VisitInvocation);
				}
			}
			return functionVisitors;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000A4548 File Offset: 0x000A2748
		private OdbcSqlExpression VisitInvocation(InvocationQueryExpression invocation)
		{
			using (this.trace.NewScope("VisitInvocation"))
			{
				try
				{
					RecordTypeValue rowType = this.GetRowType();
					Value value;
					if (this.generator.TryGenerateInvocation(new VisitorFunctionValue(rowType.FieldKeys, this), rowType, this.GetGroupKeys(), new QueryExpressionToMAstVisitor(rowType.FieldKeys).Visit(invocation), out value))
					{
						IScriptable scriptable = MAstToSqlExpressionVisitor.VisitAst(value.AsRecord);
						if (scriptable is Condition)
						{
							return new OdbcConditionExpression((Condition)scriptable);
						}
						if (!(scriptable is SqlExpression))
						{
							throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.InvalidType(scriptable.GetType().Name));
						}
						TypeValue asType = value["Type"].AsType;
						OdbcTypeInfo odbcTypeInfo;
						if (!base.TryGetType(asType, out odbcTypeInfo))
						{
							throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.InvalidType(FoldingWarnings.TypeValueToString(asType)));
						}
						return new OdbcScalarExpression(new OdbcDerivedColumnTypeInfo(odbcTypeInfo, asType.IsNullable, null, null), (SqlExpression)scriptable);
					}
				}
				catch (ValueException ex)
				{
					throw this.trace.NewFoldingFailureException(ex);
				}
				catch (Exception ex2) when (SafeExceptions.IsSafeException(ex2) && !(ex2 is FoldingFailureException))
				{
				}
				throw this.trace.NewFoldingFailureException("OdbcSqlExpressionGeneratorQueryExpressionVisitor.VisitInvocation");
			}
			OdbcSqlExpression odbcSqlExpression;
			return odbcSqlExpression;
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000A4700 File Offset: 0x000A2900
		private SqlExpression VisitConstant<TValue>(OdbcTypeInfo typeInfo, TValue value, Func<OdbcTypeInfo, TValue, SqlExpression> visit) where TValue : Value
		{
			SqlExpression sqlExpression;
			if (this.generator.TryGenerateConstant(typeInfo, value, out sqlExpression))
			{
				return sqlExpression;
			}
			return visit(typeInfo, value);
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000A4730 File Offset: 0x000A2930
		private RecordTypeValue GetRowType()
		{
			RecordBuilder recordBuilder = new RecordBuilder(base.Columns.Length);
			foreach (OdbcQueryColumnInfo odbcQueryColumnInfo in base.Columns)
			{
				recordBuilder.Add(odbcQueryColumnInfo.LocalName, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					odbcQueryColumnInfo.AscribedTypeValue,
					LogicalValue.False
				}), TypeValue.Record);
			}
			return RecordTypeValue.New(recordBuilder.ToRecord());
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000A47A4 File Offset: 0x000A29A4
		private Value GetGroupKeys()
		{
			if (base.GroupKeys == null)
			{
				return Value.Null;
			}
			Value[] array = base.GroupKeys.Select((int i) => TextValue.New(base.Columns[i].LocalName)).ToArray<TextValue>();
			return ListValue.New(array);
		}

		// Token: 0x040016E2 RID: 5858
		private readonly OdbcSqlExpressionGenerator generator;
	}
}
