using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000478 RID: 1144
	internal sealed class SapHanaQueryExpressionVisitor : OdbcQueryExpressionVisitor
	{
		// Token: 0x06002606 RID: 9734 RVA: 0x0006E026 File Offset: 0x0006C226
		public SapHanaQueryExpressionVisitor(SapHanaCubeBase cube, OdbcDataSource dataSource, IList<SelectItem> selectItems, OdbcQueryColumnInfo[] columns, bool allowAggregates, bool tolerateConcatOverflow, int[] groupKey, bool useSemanticSet)
			: base(dataSource, selectItems, columns, allowAggregates, false, tolerateConcatOverflow, groupKey)
		{
			this.dataSource = dataSource;
			this.cube = cube;
			this.useSemanticSet = useSemanticSet;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0006E04F File Offset: 0x0006C24F
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, BinaryValue value)
		{
			return new SapHanaQueryExpressionVisitor.Literal(string.Format(CultureInfo.InvariantCulture, "X'{0}'", SapHanaValueFormatter.FormatBinary(value)));
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0006E06C File Offset: 0x0006C26C
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, TextValue textValue)
		{
			Odbc32.SQL_TYPE sqlType = typeInfo.SqlType;
			if (sqlType <= Odbc32.SQL_TYPE.LONGVARCHAR)
			{
				if (sqlType - Odbc32.SQL_TYPE.WLONGVARCHAR <= 2)
				{
					return new SqlConstant(ConstantType.UnicodeString, textValue.AsString);
				}
				if (sqlType != Odbc32.SQL_TYPE.LONGVARCHAR)
				{
					goto IL_003B;
				}
			}
			else if (sqlType != Odbc32.SQL_TYPE.CHAR && sqlType != Odbc32.SQL_TYPE.VARCHAR)
			{
				goto IL_003B;
			}
			return new SqlConstant(ConstantType.AnsiString, textValue.AsString);
			IL_003B:
			throw new NotSupportedException();
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0006E0B9 File Offset: 0x0006C2B9
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, DateTimeValue value)
		{
			return new SapHanaQueryExpressionVisitor.Literal(string.Format(CultureInfo.InvariantCulture, "timestamp'{0}'", SapHanaValueFormatter.FormatDateTime(value)));
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x0006E0D5 File Offset: 0x0006C2D5
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, DateValue value)
		{
			return new SapHanaQueryExpressionVisitor.Literal(string.Format(CultureInfo.InvariantCulture, "date'{0}'", SapHanaValueFormatter.FormatDate(value)));
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0006E0F1 File Offset: 0x0006C2F1
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, TimeValue value)
		{
			return new SapHanaQueryExpressionVisitor.Literal(string.Format(CultureInfo.InvariantCulture, "time'{0}'", SapHanaValueFormatter.FormatTime(value)));
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0006E10D File Offset: 0x0006C30D
		protected override SqlExpression VisitConstant(OdbcTypeInfo typeInfo, LogicalValue value)
		{
			return new SapHanaQueryExpressionVisitor.Literal(SapHanaValueFormatter.FormatBoolean(value));
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0006E11A File Offset: 0x0006C31A
		protected override SqlExpression VisitConstantInt32(OdbcTypeInfo typeInfo, NumberValue value)
		{
			return new SapHanaQueryExpressionVisitor.Literal(SapHanaValueFormatter.FormatInteger(value));
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x0006E11A File Offset: 0x0006C31A
		protected override SqlExpression VisitConstantInt64(OdbcTypeInfo typeInfo, NumberValue value)
		{
			return new SapHanaQueryExpressionVisitor.Literal(SapHanaValueFormatter.FormatInteger(value));
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0006E127 File Offset: 0x0006C327
		protected override SqlExpression VisitConstantDouble(OdbcTypeInfo typeInfo, NumberValue value)
		{
			return new SapHanaQueryExpressionVisitor.Literal(SapHanaValueFormatter.FormatDouble(value));
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0006E134 File Offset: 0x0006C334
		protected override bool TryVisitConvert(OdbcTypeInfo fromType, OdbcTypeInfo toType, SqlExpression expression, out SqlExpression convertedExpression)
		{
			if (fromType.SqlType == Odbc32.SQL_TYPE.TYPE_TIME && toType.SqlType == Odbc32.SQL_TYPE.TYPE_TIMESTAMP)
			{
				expression = base.Concat(SapHanaQueryExpressionVisitor.literalOleDBEpochDatePrefix, expression);
			}
			convertedExpression = new CastCall
			{
				Expression = expression,
				Type = new SqlDataType(TypeValue.Any, new ConstantSqlString(toType.Name))
			};
			return true;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0006E190 File Offset: 0x0006C390
		protected override bool TryGetMatchingType(OdbcTypeMatchCriteria criteria, out OdbcTypeInfo typeInfo)
		{
			typeInfo = null;
			Odbc32.SQL_TYPE sqlType = criteria.SqlType;
			if (sqlType <= Odbc32.SQL_TYPE.WCHAR)
			{
				if (sqlType != Odbc32.SQL_TYPE.WLONGVARCHAR)
				{
					if (sqlType - Odbc32.SQL_TYPE.WVARCHAR <= 1)
					{
						typeInfo = this.dataSource.Types.GetType("NVARCHAR");
					}
				}
				else
				{
					typeInfo = this.dataSource.Types.GetType("NCLOB");
				}
			}
			else if (sqlType != Odbc32.SQL_TYPE.LONGVARCHAR)
			{
				if (sqlType == Odbc32.SQL_TYPE.CHAR || sqlType == Odbc32.SQL_TYPE.VARCHAR)
				{
					typeInfo = this.dataSource.Types.GetType("VARCHAR");
				}
			}
			else
			{
				typeInfo = this.dataSource.Types.GetType("CLOB");
			}
			if (typeInfo != null)
			{
				int? columnSize = typeInfo.ColumnSize;
				int size = criteria.Size;
				if (!((columnSize.GetValueOrDefault() < size) & (columnSize != null)))
				{
					return true;
				}
			}
			return base.TryGetMatchingType(criteria, out typeInfo);
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x0006E25C File Offset: 0x0006C45C
		protected override OdbcScalarExpression VisitAggregateInvocation(InvocationQueryExpression expression, ConstantSqlString functionName, bool convert, bool isNumeric)
		{
			OdbcScalarExpression odbcScalarExpression = base.Visit(expression.Arguments[0]).AsScalar;
			if (isNumeric)
			{
				if (odbcScalarExpression.TypeInfo.TypeValue.TypeKind != ValueKind.Number)
				{
					throw new NotSupportedException();
				}
				ColumnReference columnReference = odbcScalarExpression.Expression as ColumnReference;
				SapHanaMeasure sapHanaMeasure;
				if (this.cube != null && convert && columnReference != null && this.cube.Measures.TryGetMeasureByColumn(columnReference.Name.Name, out sapHanaMeasure))
				{
					convert = !this.useSemanticSet && sapHanaMeasure.IsAggregatable;
				}
				if (convert)
				{
					odbcScalarExpression = base.Convert(base.DoubleType, odbcScalarExpression);
				}
			}
			return new OdbcScalarExpression(odbcScalarExpression.TypeInfo, OdbcQueryExpressionVisitor.Call(new BuiltInFunctionReference(functionName), new SqlExpression[] { odbcScalarExpression.Expression }));
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x0006E324 File Offset: 0x0006C524
		protected override bool TryLocate(InvocationQueryExpression expression, int substringArgumentIndex, OdbcScalarExpression substring, int valueArgumentIndex, OdbcScalarExpression value, out OdbcScalarExpression result)
		{
			return this.TryLocate(expression, substringArgumentIndex, substring, valueArgumentIndex, value, base.VisitConstant(NumberValue.One), out result);
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0006E340 File Offset: 0x0006C540
		protected override bool TryLocate(InvocationQueryExpression expression, int substringArgumentIndex, OdbcScalarExpression substring, int valueArgumentIndex, OdbcScalarExpression value, OdbcScalarExpression start, out OdbcScalarExpression result)
		{
			if (this.trace.ArgumentTypeKindIs(expression, substringArgumentIndex, substring, ValueKind.Text) && this.trace.ArgumentTypeKindIs(expression, valueArgumentIndex, value, ValueKind.Text) && this.trace.ArgumentNotNullable(expression, substringArgumentIndex, substring))
			{
				OdbcQueryExpressionVisitor.CompatibilityAdjustmentResult compatibilityAdjustmentResult = base.AdjustTextValuesForCompatibility(substring, value);
				OdbcDerivedColumnTypeInfo odbcDerivedColumnTypeInfo;
				if (base.TryGetColumnType(base.IntegerType.SqlType, value.TypeInfo.IsNullable, out odbcDerivedColumnTypeInfo))
				{
					result = new OdbcScalarExpression(odbcDerivedColumnTypeInfo, OdbcQueryExpressionVisitor.Call(new BuiltInFunctionReference(SqlLanguageStrings.LocateSqlString), new SqlExpression[]
					{
						compatibilityAdjustmentResult.Right.Expression,
						compatibilityAdjustmentResult.Left.Expression,
						start.Expression
					}));
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x0006E400 File Offset: 0x0006C600
		protected override Condition IsNull(SqlExpression expression)
		{
			SqlExpression sqlExpression;
			if (this.TryEliminateCastOrConvertCall(expression, out sqlExpression))
			{
				expression = sqlExpression;
			}
			return base.IsNull(expression);
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0006E424 File Offset: 0x0006C624
		protected override Condition IsNotNull(SqlExpression expression)
		{
			SqlExpression sqlExpression;
			if (this.TryEliminateCastOrConvertCall(expression, out sqlExpression))
			{
				expression = sqlExpression;
			}
			return base.IsNotNull(expression);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x0006E448 File Offset: 0x0006C648
		private bool TryEliminateCastOrConvertCall(SqlExpression expression, out SqlExpression newExpression)
		{
			CastCall castCall = expression as CastCall;
			if (castCall != null)
			{
				newExpression = castCall.Expression;
				return true;
			}
			BuiltInFunctionReference builtInFunctionReference = expression as BuiltInFunctionReference;
			if (builtInFunctionReference != null && builtInFunctionReference.Name.String == SqlLanguageStrings.ConvertSqlString.String && builtInFunctionReference.Parameters.Count == 2)
			{
				newExpression = builtInFunctionReference.Parameters[0];
				return true;
			}
			newExpression = null;
			return false;
		}

		// Token: 0x04000FE8 RID: 4072
		private static readonly SapHanaQueryExpressionVisitor.Literal literalOleDBEpochDatePrefix = new SapHanaQueryExpressionVisitor.Literal("'1899-12-30 '");

		// Token: 0x04000FE9 RID: 4073
		private readonly SapHanaCubeBase cube;

		// Token: 0x04000FEA RID: 4074
		private readonly OdbcDataSource dataSource;

		// Token: 0x04000FEB RID: 4075
		private readonly bool useSemanticSet;

		// Token: 0x02000479 RID: 1145
		private class Literal : ScalarExpression
		{
			// Token: 0x06002619 RID: 9753 RVA: 0x0006E4C8 File Offset: 0x0006C6C8
			public Literal(string literal)
			{
				this.literal = new ConstantSqlString(literal);
			}

			// Token: 0x17000F3F RID: 3903
			// (get) Token: 0x0600261A RID: 9754 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600261B RID: 9755 RVA: 0x0006E4DC File Offset: 0x0006C6DC
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.Write(this.literal);
			}

			// Token: 0x04000FEC RID: 4076
			private readonly ConstantSqlString literal;
		}
	}
}
