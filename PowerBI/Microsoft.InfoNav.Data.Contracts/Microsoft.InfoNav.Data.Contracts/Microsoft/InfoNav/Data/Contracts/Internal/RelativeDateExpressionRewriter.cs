using System;
using System.ComponentModel;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000209 RID: 521
	[ImmutableObject(true)]
	public sealed class RelativeDateExpressionRewriter : ResolvedQueryExpressionRewriter
	{
		// Token: 0x06000F2F RID: 3887 RVA: 0x0001D4E8 File Offset: 0x0001B6E8
		public RelativeDateExpressionRewriter(IDateTimeProvider dateTimeProvider)
		{
			this._dateTimeProvider = dateTimeProvider;
			this._now = dateTimeProvider.BaseTime;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0001D503 File Offset: 0x0001B703
		public override ResolvedQueryExpression Visit(ResolvedQueryNowExpression expression)
		{
			return this._now.Literal();
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0001D518 File Offset: 0x0001B718
		public override ResolvedQueryExpression Visit(ResolvedQueryDateAddExpression expression)
		{
			DateTime dateTime;
			if (!base.VisitExpression(expression.Expression).TryGetDateTime(out dateTime))
			{
				return expression;
			}
			int amount = expression.Amount;
			DateTime dateTime2;
			switch (expression.TimeUnit)
			{
			case TimeUnit.Day:
				if (!this._dateTimeProvider.TryAddDays(dateTime, amount, out dateTime2))
				{
					return expression;
				}
				break;
			case TimeUnit.Week:
				if (!this._dateTimeProvider.TryAddDays(dateTime, amount * 7, out dateTime2))
				{
					return expression;
				}
				break;
			case TimeUnit.Month:
				if (!this._dateTimeProvider.TryAddMonths(dateTime, amount, out dateTime2))
				{
					return expression;
				}
				break;
			case TimeUnit.Year:
				if (!this._dateTimeProvider.TryAddYears(dateTime, amount, out dateTime2))
				{
					return expression;
				}
				break;
			case TimeUnit.Decade:
				if (!this._dateTimeProvider.TryAddYears(dateTime, amount * 10, out dateTime2))
				{
					return expression;
				}
				break;
			case TimeUnit.Second:
				if (!this._dateTimeProvider.TryAddSeconds(dateTime, amount, out dateTime2))
				{
					return expression;
				}
				break;
			case TimeUnit.Minute:
				if (!this._dateTimeProvider.TryAddMinutes(dateTime, amount, out dateTime2))
				{
					return expression;
				}
				break;
			case TimeUnit.Hour:
				if (!this._dateTimeProvider.TryAddHours(dateTime, amount, out dateTime2))
				{
					return expression;
				}
				break;
			default:
				throw Contract.Except("Unsupported time unit " + expression.TimeUnit.ToString());
			}
			return dateTime2.Literal();
		}

		// Token: 0x04000715 RID: 1813
		private readonly IDateTimeProvider _dateTimeProvider;

		// Token: 0x04000716 RID: 1814
		private readonly DateTime _now;
	}
}
