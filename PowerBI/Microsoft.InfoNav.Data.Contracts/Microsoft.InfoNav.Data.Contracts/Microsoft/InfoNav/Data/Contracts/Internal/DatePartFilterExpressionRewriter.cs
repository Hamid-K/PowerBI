using System;
using System.ComponentModel;
using System.Globalization;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000208 RID: 520
	[ImmutableObject(true)]
	public sealed class DatePartFilterExpressionRewriter : ResolvedQueryExpressionRewriter
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x0001D27E File Offset: 0x0001B47E
		public DatePartFilterExpressionRewriter(IDateTimeProvider dateTimeProvider)
		{
			this._dateTimeProvider = dateTimeProvider;
			this._now = dateTimeProvider.BaseTime;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0001D29C File Offset: 0x0001B49C
		public override ResolvedQueryExpression Visit(ResolvedQueryBetweenExpression expression)
		{
			DataType dataType = this.GetDataType(expression.Expression);
			ResolvedQueryExpression resolvedQueryExpression = expression.LowerBound;
			ResolvedQueryExpression resolvedQueryExpression2 = expression.UpperBound;
			bool flag = false;
			DatePartFilterExpressionRewriter.ConstantConverterResult constantConverterResult;
			if (this.TryConvertToDateTimeConstant(expression.LowerBound, dataType, out constantConverterResult, false))
			{
				resolvedQueryExpression = constantConverterResult.Value;
			}
			DatePartFilterExpressionRewriter.ConstantConverterResult constantConverterResult2;
			if (this.TryConvertToDateTimeConstant(expression.UpperBound, dataType, out constantConverterResult2, true))
			{
				resolvedQueryExpression2 = constantConverterResult2.Value;
				flag = constantConverterResult2.IsExclusiveUpper;
			}
			if (expression.LowerBound == resolvedQueryExpression && expression.UpperBound == resolvedQueryExpression2)
			{
				return expression;
			}
			return DatePartFilterExpressionRewriter.CreateRangeExpression(expression.Expression, resolvedQueryExpression, resolvedQueryExpression2, flag);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0001D324 File Offset: 0x0001B524
		public override ResolvedQueryExpression Visit(ResolvedQueryComparisonExpression expression)
		{
			ResolvedQueryLiteralExpression resolvedQueryLiteralExpression = expression.Right as ResolvedQueryLiteralExpression;
			if (resolvedQueryLiteralExpression != null && resolvedQueryLiteralExpression.Value.Type == ConceptualPrimitiveType.Null)
			{
				return expression;
			}
			DataType dataType = this.GetDataType(expression.Left);
			if (expression.ComparisonKind == QueryComparisonKind.Equal)
			{
				DatePartFilterExpressionRewriter.ConstantConverterResult constantConverterResult;
				DatePartFilterExpressionRewriter.ConstantConverterResult constantConverterResult2;
				if (!this.TryConvertToDateTimeConstant(expression.Right, dataType, out constantConverterResult, false) || !this.TryConvertToDateTimeConstant(expression.Right, dataType, out constantConverterResult2, true))
				{
					return expression;
				}
				return DatePartFilterExpressionRewriter.CreateRangeExpression(expression.Left, constantConverterResult.Value, constantConverterResult2.Value, constantConverterResult2.IsExclusiveUpper);
			}
			else
			{
				bool flag = expression.ComparisonKind == QueryComparisonKind.GreaterThan || expression.ComparisonKind == QueryComparisonKind.LessThanOrEqual;
				DatePartFilterExpressionRewriter.ConstantConverterResult constantConverterResult3;
				if (!this.TryConvertToDateTimeConstant(expression.Right, dataType, out constantConverterResult3, flag))
				{
					return expression;
				}
				QueryComparisonKind queryComparisonKind = expression.ComparisonKind;
				if (constantConverterResult3.IsExclusiveUpper)
				{
					if (queryComparisonKind == QueryComparisonKind.GreaterThan)
					{
						queryComparisonKind = QueryComparisonKind.GreaterThanOrEqual;
					}
					else
					{
						if (queryComparisonKind != QueryComparisonKind.LessThanOrEqual)
						{
							throw Contract.Except("Unexpected comparison kind with exclusive upper");
						}
						queryComparisonKind = QueryComparisonKind.LessThan;
					}
				}
				return expression.Left.Comparison(constantConverterResult3.Value, queryComparisonKind);
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0001D420 File Offset: 0x0001B620
		private DataType GetDataType(ResolvedQueryExpression expression)
		{
			ResolvedQueryPropertyExpression resolvedQueryPropertyExpression = expression as ResolvedQueryPropertyExpression;
			if (resolvedQueryPropertyExpression != null)
			{
				return resolvedQueryPropertyExpression.Property.Type;
			}
			ResolvedQueryHierarchyLevelExpression resolvedQueryHierarchyLevelExpression = expression as ResolvedQueryHierarchyLevelExpression;
			if (resolvedQueryHierarchyLevelExpression != null)
			{
				return resolvedQueryHierarchyLevelExpression.Level.Source.Type;
			}
			return DataType.DateTime;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0001D470 File Offset: 0x0001B670
		private bool TryConvertToDateTimeConstant(ResolvedQueryExpression expression, DataType targetDataType, out DatePartFilterExpressionRewriter.ConstantConverterResult constant, bool upperBound)
		{
			DatePartFilterExpressionRewriter.ConstantConverterVisitor constantConverterVisitor = new DatePartFilterExpressionRewriter.ConstantConverterVisitor(this._dateTimeProvider, this._now, targetDataType, upperBound);
			constant = expression.Accept<DatePartFilterExpressionRewriter.ConstantConverterResult>(constantConverterVisitor);
			return constant != null;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		private static ResolvedQueryExpression CreateRangeExpression(ResolvedQueryExpression expression, ResolvedQueryExpression lowerBound, ResolvedQueryExpression upperBound, bool exclusiveUpper)
		{
			if (exclusiveUpper)
			{
				return expression.GreaterThanOrEqual(lowerBound).And(expression.LessThan(upperBound));
			}
			if (lowerBound.Equals(upperBound))
			{
				return expression.Equal(lowerBound);
			}
			return expression.Between(lowerBound, upperBound);
		}

		// Token: 0x04000712 RID: 1810
		private static readonly DateTimeFormatInfo _dateTimeFormat = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;

		// Token: 0x04000713 RID: 1811
		private readonly IDateTimeProvider _dateTimeProvider;

		// Token: 0x04000714 RID: 1812
		private readonly DateTime _now;

		// Token: 0x0200032A RID: 810
		private sealed class ConstantConverterResult
		{
			// Token: 0x060019E8 RID: 6632 RVA: 0x0002E9CE File Offset: 0x0002CBCE
			internal ConstantConverterResult(ResolvedQueryLiteralExpression value, bool isExclusiveUpper)
			{
				this.Value = value;
				this.IsExclusiveUpper = isExclusiveUpper;
			}

			// Token: 0x040009AE RID: 2478
			internal readonly ResolvedQueryLiteralExpression Value;

			// Token: 0x040009AF RID: 2479
			internal readonly bool IsExclusiveUpper;
		}

		// Token: 0x0200032B RID: 811
		[ImmutableObject(true)]
		private sealed class ConstantConverterVisitor : DefaultResolvedQueryExpressionVisitor<DatePartFilterExpressionRewriter.ConstantConverterResult>
		{
			// Token: 0x060019E9 RID: 6633 RVA: 0x0002E9E4 File Offset: 0x0002CBE4
			internal ConstantConverterVisitor(IDateTimeProvider dateTimeProvider, DateTime now, DataType targetDataType, bool upperBound)
			{
				this._dateTimeProvider = dateTimeProvider;
				this._now = now;
				this._targetDataType = targetDataType;
				this._upperBound = upperBound;
			}

			// Token: 0x060019EA RID: 6634 RVA: 0x0002EA0C File Offset: 0x0002CC0C
			public override DatePartFilterExpressionRewriter.ConstantConverterResult Visit(ResolvedQueryDateSpanExpression expression)
			{
				DateTime dateTime;
				if (!expression.Expression.TryGetDateTime(out dateTime))
				{
					return null;
				}
				switch (expression.TimeUnit)
				{
				case TimeUnit.Day:
					return this.CreateResultForDayUnit(dateTime);
				case TimeUnit.Week:
					return this.CreateResultForWeekUnit(dateTime);
				case TimeUnit.Month:
					return this.CreateResultForMonthUnit(dateTime);
				case TimeUnit.Year:
					return this.CreateResultForYearUnit(dateTime);
				case TimeUnit.Decade:
					return this.CreateResultForDecadeUnit(dateTime);
				case TimeUnit.Second:
					return this.CreateResultForSecondUnit(dateTime);
				case TimeUnit.Minute:
					return this.CreateResultForMinuteUnit(dateTime);
				case TimeUnit.Hour:
					return this.CreateResultForHourUnit(dateTime);
				default:
					throw Contract.Except("Unsupported date span unit " + expression.TimeUnit.ToString());
				}
			}

			// Token: 0x060019EB RID: 6635 RVA: 0x0002EABC File Offset: 0x0002CCBC
			protected override DatePartFilterExpressionRewriter.ConstantConverterResult VisitUnhandledExpression(ResolvedQueryExpression expression)
			{
				return null;
			}

			// Token: 0x060019EC RID: 6636 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateResultForDecadeUnit(DateTime dateValue)
			{
				int decade = dateValue.GetDecade();
				if (this._targetDataType.IsDateTimeOrDate())
				{
					if (!this._dateTimeProvider.TryCreateDateTime(decade, 1, 1, out dateValue))
					{
						return null;
					}
					if (this._upperBound && !this._dateTimeProvider.TryAddYears(dateValue, 10, out dateValue))
					{
						return null;
					}
					return this.CreateDateTimeConstant(dateValue, this._upperBound);
				}
				else
				{
					if (this._targetDataType.IsInteger() || this._targetDataType.IsDecadeYearOrMonth())
					{
						return this.CreateIntegerConstant(this._upperBound ? ((long)decade + 9L) : ((long)decade), false);
					}
					return null;
				}
			}

			// Token: 0x060019ED RID: 6637 RVA: 0x0002EB54 File Offset: 0x0002CD54
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateResultForYearUnit(DateTime dateValue)
			{
				if (this._targetDataType.IsDateTimeOrDate())
				{
					if (!this._dateTimeProvider.TryCreateDateTime(dateValue.Year, 1, 1, out dateValue))
					{
						return null;
					}
					if (this._upperBound && !this._dateTimeProvider.TryAddYears(dateValue, 1, out dateValue))
					{
						return null;
					}
					return this.CreateDateTimeConstant(dateValue, this._upperBound);
				}
				else
				{
					if (this._targetDataType.IsInteger() || this._targetDataType.IsDecadeYearOrMonth())
					{
						return this.CreateIntegerConstant((long)dateValue.Year, false);
					}
					return null;
				}
			}

			// Token: 0x060019EE RID: 6638 RVA: 0x0002EBDC File Offset: 0x0002CDDC
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateResultForMonthUnit(DateTime value)
			{
				if (!this._targetDataType.IsDateTimeOrDate())
				{
					return null;
				}
				DateTime dateTime;
				if (!this._dateTimeProvider.TryCreateDateTime(value.Year, value.Month, 1, out dateTime))
				{
					return null;
				}
				if (this._upperBound && !this._dateTimeProvider.TryAddMonths(dateTime, 1, out dateTime))
				{
					return null;
				}
				return this.CreateDateTimeConstant(dateTime, this._upperBound);
			}

			// Token: 0x060019EF RID: 6639 RVA: 0x0002EC40 File Offset: 0x0002CE40
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateResultForWeekUnit(DateTime value)
			{
				if (!this._targetDataType.IsDateTimeOrDate())
				{
					return null;
				}
				DateTime dateTime;
				if (!this._dateTimeProvider.TryCreateDateTime(value.Year, value.Month, value.Day, out dateTime))
				{
					return null;
				}
				DayOfWeek firstDayOfWeek = DatePartFilterExpressionRewriter._dateTimeFormat.FirstDayOfWeek;
				int num = -((dateTime.DayOfWeek + 7 - firstDayOfWeek) % 7);
				int num2 = (this._upperBound ? (num + 7) : num);
				if (!this._dateTimeProvider.TryAddDays(dateTime, num2, out dateTime))
				{
					return null;
				}
				return this.CreateDateTimeConstant(dateTime, this._upperBound);
			}

			// Token: 0x060019F0 RID: 6640 RVA: 0x0002ECCC File Offset: 0x0002CECC
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateResultForDayUnit(DateTime dateValue)
			{
				if (!this._targetDataType.IsDateTimeOrDate())
				{
					return null;
				}
				if (!this._dateTimeProvider.TryCreateDateTime(dateValue.Year, dateValue.Month, dateValue.Day, out dateValue))
				{
					return null;
				}
				if (this._upperBound && !this._dateTimeProvider.TryAddDays(dateValue, 1, out dateValue))
				{
					return null;
				}
				return this.CreateDateTimeConstant(dateValue, this._upperBound);
			}

			// Token: 0x060019F1 RID: 6641 RVA: 0x0002ED38 File Offset: 0x0002CF38
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateResultForHourUnit(DateTime dateValue)
			{
				if (!this._targetDataType.IsDateTimeOrDate())
				{
					return null;
				}
				if (!this._dateTimeProvider.TryCreateDateTime(dateValue.Year, dateValue.Month, dateValue.Day, dateValue.Hour, 0, 0, out dateValue))
				{
					return null;
				}
				if (this._upperBound && !this._dateTimeProvider.TryAddHours(dateValue, 1, out dateValue))
				{
					return null;
				}
				return this.CreateDateTimeConstant(dateValue, this._upperBound);
			}

			// Token: 0x060019F2 RID: 6642 RVA: 0x0002EDAC File Offset: 0x0002CFAC
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateResultForMinuteUnit(DateTime dateValue)
			{
				if (!this._targetDataType.IsDateTimeOrDate())
				{
					return null;
				}
				if (!this._dateTimeProvider.TryCreateDateTime(dateValue.Year, dateValue.Month, dateValue.Day, dateValue.Hour, dateValue.Minute, 0, out dateValue))
				{
					return null;
				}
				if (this._upperBound && !this._dateTimeProvider.TryAddMinutes(dateValue, 1, out dateValue))
				{
					return null;
				}
				return this.CreateDateTimeConstant(dateValue, this._upperBound);
			}

			// Token: 0x060019F3 RID: 6643 RVA: 0x0002EE28 File Offset: 0x0002D028
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateResultForSecondUnit(DateTime dateValue)
			{
				if (!this._targetDataType.IsDateTimeOrDate())
				{
					return null;
				}
				if (!this._dateTimeProvider.TryCreateDateTime(dateValue.Year, dateValue.Month, dateValue.Day, dateValue.Hour, dateValue.Minute, dateValue.Second, out dateValue))
				{
					return null;
				}
				if (this._upperBound && !this._dateTimeProvider.TryAddSeconds(dateValue, 1, out dateValue))
				{
					return null;
				}
				return this.CreateDateTimeConstant(dateValue, this._upperBound);
			}

			// Token: 0x060019F4 RID: 6644 RVA: 0x0002EEA7 File Offset: 0x0002D0A7
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateDateTimeConstant(DateTime value, bool isExclusiveUpper)
			{
				return new DatePartFilterExpressionRewriter.ConstantConverterResult(value.Literal(), isExclusiveUpper);
			}

			// Token: 0x060019F5 RID: 6645 RVA: 0x0002EEBA File Offset: 0x0002D0BA
			private DatePartFilterExpressionRewriter.ConstantConverterResult CreateIntegerConstant(long value, bool isExclusiveUpper)
			{
				return new DatePartFilterExpressionRewriter.ConstantConverterResult(value.Literal(), isExclusiveUpper);
			}

			// Token: 0x040009B0 RID: 2480
			private readonly IDateTimeProvider _dateTimeProvider;

			// Token: 0x040009B1 RID: 2481
			private readonly DateTime _now;

			// Token: 0x040009B2 RID: 2482
			private readonly DataType _targetDataType;

			// Token: 0x040009B3 RID: 2483
			private readonly bool _upperBound;
		}
	}
}
