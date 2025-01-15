using System;
using System.Globalization;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200013A RID: 314
	internal static class DaxLiteral
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00030314 File Offset: 0x0002E514
		internal static DaxExpression StringStartIndex
		{
			get
			{
				return DaxLiteral.FromInt32(1);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0003031C File Offset: 0x0002E51C
		internal static DaxExpression StringInvalidIndex
		{
			get
			{
				return DaxLiteral.FromInt32(0);
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00030324 File Offset: 0x0002E524
		internal static DaxExpression FromInt32(int value)
		{
			return DaxExpression.Scalar(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00030337 File Offset: 0x0002E537
		internal static DaxExpression FromInt64(long value)
		{
			return DaxExpression.Scalar(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0003034A File Offset: 0x0002E54A
		internal static DaxExpression FromSingle(float value)
		{
			return DaxExpression.Scalar(value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00030364 File Offset: 0x0002E564
		internal static DaxExpression FromDouble(double value)
		{
			if (double.IsPositiveInfinity(value))
			{
				return DaxExpression.Scalar("1/0");
			}
			if (double.IsNegativeInfinity(value))
			{
				return DaxExpression.Scalar("-1/0");
			}
			if (double.IsNaN(value))
			{
				return DaxExpression.Scalar("0/0");
			}
			return DaxExpression.Scalar(value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000303C0 File Offset: 0x0002E5C0
		internal static DaxExpression FromDecimal(decimal value, CultureInfo cultureInfo)
		{
			return DaxFunctions.Currency(DaxExpression.Scalar("\"" + value.ToString(cultureInfo) + "\""));
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000303E3 File Offset: 0x0002E5E3
		internal static DaxExpression FromString(string value)
		{
			return DaxExpression.Scalar("\"" + value.Replace("\"", "\"\"") + "\"");
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00030409 File Offset: 0x0002E609
		internal static DaxExpression FromBoolean(bool value)
		{
			return DaxExpression.Scalar(value.ToString(CultureInfo.InvariantCulture).ToUpperInvariant());
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00030424 File Offset: 0x0002E624
		internal static DaxExpression FromDateTime(DateTime value)
		{
			ArgumentValidation.CheckCondition(value >= QueryConstants.EarliestSupportedDateTime, "value", SR.DateTimeCannotBeConvertedToDax);
			DaxExpression daxExpression = DaxFunctions.Date(DaxLiteral.FromInt32(value.Year), DaxLiteral.FromInt32(value.Month), DaxLiteral.FromInt32(value.Day));
			TimeSpan timeOfDay = value.TimeOfDay;
			if (timeOfDay != TimeSpan.Zero)
			{
				daxExpression = DaxOperators.Plus(daxExpression, DaxLiteral.FromTimeSpan(timeOfDay));
			}
			return daxExpression;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00030498 File Offset: 0x0002E698
		internal static DaxExpression FromTimeSpan(TimeSpan value)
		{
			DaxExpression daxExpression = DaxFunctions.Time(DaxLiteral.FromInt32(value.Hours), DaxLiteral.FromInt32(value.Minutes), DaxLiteral.FromInt32(value.Seconds));
			double subsecondComponent = value.GetSubsecondComponent();
			if (subsecondComponent != 0.0)
			{
				DaxExpression daxExpression2 = DaxOperators.DivideRaw(DaxLiteral.FromDouble(subsecondComponent), DaxLiteral.FromInt32(86400));
				daxExpression = DaxOperators.Plus(daxExpression, daxExpression2);
			}
			return daxExpression;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00030501 File Offset: 0x0002E701
		internal static DaxExpression FromGuid(Guid value)
		{
			return DaxLiteral.FromString(value.ToString("B", CultureInfo.InvariantCulture).ToUpperInvariant());
		}

		// Token: 0x04000AC5 RID: 2757
		private const string NumericRoundTripFormat = "R";
	}
}
