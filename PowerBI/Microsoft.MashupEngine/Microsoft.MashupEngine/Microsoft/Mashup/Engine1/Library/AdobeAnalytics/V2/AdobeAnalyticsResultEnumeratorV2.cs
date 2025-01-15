using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000FA2 RID: 4002
	internal abstract class AdobeAnalyticsResultEnumeratorV2 : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x06006943 RID: 26947 RVA: 0x0016985D File Offset: 0x00167A5D
		public IValueReference Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x06006944 RID: 26948 RVA: 0x00169865 File Offset: 0x00167A65
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06006945 RID: 26949
		protected abstract Value GetResult();

		// Token: 0x06006946 RID: 26950
		protected abstract IList<string> GetDimensions();

		// Token: 0x06006947 RID: 26951
		protected abstract IList<string> GetMeasures();

		// Token: 0x06006948 RID: 26952
		protected abstract Keys GetKeys();

		// Token: 0x06006949 RID: 26953 RVA: 0x0016986D File Offset: 0x00167A6D
		public void Dispose()
		{
			this.current = null;
		}

		// Token: 0x0600694A RID: 26954 RVA: 0x00169878 File Offset: 0x00167A78
		public bool MoveNext()
		{
			if (this.dimensionEnumerators == null)
			{
				this.result = this.GetResult();
				this.dimensions = this.GetDimensions();
				this.measures = this.GetMeasures();
				this.keys = this.GetKeys();
				int num = this.dimensions.Count;
				if (num == 0)
				{
					num = 1;
				}
				this.dimensionEnumerators = new IEnumerator<IValueReference>[num];
				this.dimensionEnumerators[0] = this.result.AsList.GetEnumerator();
			}
			if (!this.TryIncrementEnumerators(this.dimensionEnumerators.Length - 1))
			{
				return false;
			}
			IValueReference[] array = new IValueReference[this.keys.Length];
			IEnumerable<IEnumerator<IValueReference>> enumerable = this.dimensionEnumerators;
			int num2 = 0;
			if (this.dimensions.Count > 0)
			{
				foreach (IEnumerator<IValueReference> enumerator2 in enumerable)
				{
					int num3;
					this.keys.TryGetKeyIndex(this.dimensions[num2], out num3);
					array[num3] = enumerator2.Current.Value["value"];
					num2++;
				}
			}
			if (this.measures.Count > 0)
			{
				ListValue asList = this.dimensionEnumerators[this.dimensionEnumerators.Length - 1].Current.Value["data"].AsList;
				for (int i = 0; i < this.measures.Count; i++)
				{
					IValueReference valueReference2;
					try
					{
						IValueReference valueReference = asList[i];
						NumberValue numberValue;
						if (valueReference.Value.IsNumber)
						{
							numberValue = valueReference.Value.AsNumber;
						}
						else
						{
							if (!valueReference.Value.IsText)
							{
								throw ValueException.CastTypeMismatch(valueReference.Value, TypeValue.Number);
							}
							if (valueReference.Value.AsString.Equals("INF", StringComparison.OrdinalIgnoreCase))
							{
								numberValue = NumberValue.Infinity;
							}
							else if (!NumberValue.TryParse(valueReference.Value.AsString, NumberStyles.Any, CultureInfo.InvariantCulture, out numberValue))
							{
								throw ValueException.NewDataFormatError<Message0>(Strings.Number_FromFunction_NotConvertibleToNumber, valueReference.Value, null);
							}
						}
						valueReference2 = numberValue;
					}
					catch (ValueException ex)
					{
						valueReference2 = new ExceptionValueReference(ex);
					}
					int num4;
					this.keys.TryGetKeyIndex(this.measures[i], out num4);
					array[num4] = valueReference2;
				}
			}
			this.current = RecordValue.New(this.keys, array);
			return true;
		}

		// Token: 0x0600694B RID: 26955 RVA: 0x00169AEC File Offset: 0x00167CEC
		private bool TryIncrementEnumerators(int depth)
		{
			if (depth < 0)
			{
				return false;
			}
			if (this.dimensionEnumerators[depth] != null && this.dimensionEnumerators[depth].MoveNext())
			{
				return true;
			}
			if (this.dimensionEnumerators[depth] != null)
			{
				this.dimensionEnumerators[depth].Dispose();
				this.dimensionEnumerators[depth] = null;
			}
			while (this.TryIncrementEnumerators(depth - 1))
			{
				Value value;
				if (this.CurrentAtDepth(depth - 1).AsRecord.TryGetValue("breakdown", out value))
				{
					this.dimensionEnumerators[depth] = value.AsList.GetEnumerator();
					return this.TryIncrementEnumerators(depth);
				}
			}
			return false;
		}

		// Token: 0x0600694C RID: 26956 RVA: 0x00169B7F File Offset: 0x00167D7F
		private Value CurrentAtDepth(int depth)
		{
			return this.dimensionEnumerators[depth].Current.Value;
		}

		// Token: 0x0600694D RID: 26957 RVA: 0x00169B94 File Offset: 0x00167D94
		public void Reset()
		{
			for (int i = 0; i < this.dimensionEnumerators.Length; i++)
			{
				this.dimensionEnumerators[i].Dispose();
				this.dimensionEnumerators[i] = null;
			}
			this.dimensionEnumerators = null;
		}

		// Token: 0x04003A26 RID: 14886
		public const string DimensionValueKey = "value";

		// Token: 0x04003A27 RID: 14887
		public const string MetricsListKey = "data";

		// Token: 0x04003A28 RID: 14888
		public const string SubReportKey = "breakdown";

		// Token: 0x04003A29 RID: 14889
		private IList<string> dimensions;

		// Token: 0x04003A2A RID: 14890
		private IList<string> measures;

		// Token: 0x04003A2B RID: 14891
		private Keys keys;

		// Token: 0x04003A2C RID: 14892
		private Value result;

		// Token: 0x04003A2D RID: 14893
		private IEnumerator<IValueReference>[] dimensionEnumerators;

		// Token: 0x04003A2E RID: 14894
		private IValueReference current;
	}
}
