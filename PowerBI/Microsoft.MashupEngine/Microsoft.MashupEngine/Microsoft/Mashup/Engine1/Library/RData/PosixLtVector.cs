using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.MetaAnalytics.RDataSupport;

namespace Microsoft.Mashup.Engine1.Library.RData
{
	// Token: 0x02000525 RID: 1317
	internal sealed class PosixLtVector
	{
		// Token: 0x06002A52 RID: 10834 RVA: 0x0007EBFE File Offset: 0x0007CDFE
		private PosixLtVector(RObject<int?> years, RObject<int?> months, RObject<int?> days, RObject<int?> hours, RObject<int?> minutes, RObject<double?> seconds)
		{
			this.years = years;
			this.months = months;
			this.days = days;
			this.hours = hours;
			this.minutes = minutes;
			this.seconds = seconds;
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x0007EC33 File Offset: 0x0007CE33
		public IEnumerable<IValueReference> Values()
		{
			int num;
			for (int i = 0; i < this.years.Values.Length; i = num + 1)
			{
				if (this.years.TypedValues[i] != null && this.months.TypedValues[i] != null && this.days.TypedValues[i] != null && this.hours.TypedValues[i] != null && this.minutes.TypedValues[i] != null && this.seconds.TypedValues[i] != null)
				{
					IValueReference valueReference;
					try
					{
						valueReference = DateTimeValue.New(this.years.TypedValues[i].Value + 1900, this.months.TypedValues[i].Value + 1, this.days.TypedValues[i].Value, this.hours.TypedValues[i].Value, this.minutes.TypedValues[i].Value, this.seconds.TypedValues[i].Value);
					}
					catch (ValueException ex)
					{
						valueReference = new ExceptionValueReference(ValueException.NewDataFormatError<Message0>(Strings.R_DataTypeNotSupported, Value.Null, ex));
					}
					yield return valueReference;
				}
				else
				{
					yield return Value.Null;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x0007EC44 File Offset: 0x0007CE44
		public static bool TryNew(RObject<RObject> posixLtDate, out PosixLtVector posixLtVector)
		{
			posixLtVector = null;
			RObject<string> attribute = posixLtDate.GetAttribute<string>("names");
			if (attribute == null || attribute.TypedValues == null)
			{
				return false;
			}
			int num = Array.FindIndex<string>(attribute.TypedValues, (string item) => item == "sec");
			int num2 = Array.FindIndex<string>(attribute.TypedValues, (string item) => item == "min");
			int num3 = Array.FindIndex<string>(attribute.TypedValues, (string item) => item == "hour");
			int num4 = Array.FindIndex<string>(attribute.TypedValues, (string item) => item == "mday");
			int num5 = Array.FindIndex<string>(attribute.TypedValues, (string item) => item == "mon");
			int num6 = Array.FindIndex<string>(attribute.TypedValues, (string item) => item == "year");
			if (num == -1 || num2 == -1 || num3 == -1 || num5 == -1 || num6 == -1 || num4 == -1)
			{
				return false;
			}
			RObject[] typedValues = posixLtDate.TypedValues;
			RObject<int?> robject = typedValues[num6] as RObject<int?>;
			RObject<int?> robject2 = typedValues[num5] as RObject<int?>;
			RObject<int?> robject3 = typedValues[num4] as RObject<int?>;
			RObject<int?> robject4 = typedValues[num3] as RObject<int?>;
			RObject<int?> robject5 = typedValues[num2] as RObject<int?>;
			RObject<double?> robject6 = typedValues[num] as RObject<double?>;
			if (robject == null || robject2 == null || robject3 == null || robject4 == null || robject5 == null || robject6 == null)
			{
				return false;
			}
			int length = robject.Values.Length;
			if (robject2.Values.Length != length || robject3.Values.Length != length || robject4.Values.Length != length || robject5.Values.Length != length || robject6.Values.Length != length)
			{
				return false;
			}
			posixLtVector = new PosixLtVector(robject, robject2, robject3, robject4, robject5, robject6);
			return true;
		}

		// Token: 0x0400125F RID: 4703
		private const string SecName = "sec";

		// Token: 0x04001260 RID: 4704
		private const string MinName = "min";

		// Token: 0x04001261 RID: 4705
		private const string HourName = "hour";

		// Token: 0x04001262 RID: 4706
		private const string DayName = "mday";

		// Token: 0x04001263 RID: 4707
		private const string MonthName = "mon";

		// Token: 0x04001264 RID: 4708
		private const string YearName = "year";

		// Token: 0x04001265 RID: 4709
		private readonly RObject<int?> years;

		// Token: 0x04001266 RID: 4710
		private readonly RObject<int?> months;

		// Token: 0x04001267 RID: 4711
		private readonly RObject<int?> days;

		// Token: 0x04001268 RID: 4712
		private readonly RObject<int?> hours;

		// Token: 0x04001269 RID: 4713
		private readonly RObject<int?> minutes;

		// Token: 0x0400126A RID: 4714
		private readonly RObject<double?> seconds;
	}
}
