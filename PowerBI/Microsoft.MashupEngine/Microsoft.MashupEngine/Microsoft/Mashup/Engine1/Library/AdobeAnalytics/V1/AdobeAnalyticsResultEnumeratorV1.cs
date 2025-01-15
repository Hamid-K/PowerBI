using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1
{
	// Token: 0x02000FB6 RID: 4022
	internal abstract class AdobeAnalyticsResultEnumeratorV1 : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x060069B2 RID: 27058 RVA: 0x0016B3E1 File Offset: 0x001695E1
		public IValueReference Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x060069B3 RID: 27059 RVA: 0x0016B3E9 File Offset: 0x001695E9
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060069B4 RID: 27060
		protected abstract Value GetResult();

		// Token: 0x060069B5 RID: 27061
		protected abstract IList<string> GetGranularityLevels();

		// Token: 0x060069B6 RID: 27062
		protected abstract IList<string> GetDimensions();

		// Token: 0x060069B7 RID: 27063
		protected abstract IList<string> GetMeasures();

		// Token: 0x060069B8 RID: 27064
		protected abstract Keys GetKeys();

		// Token: 0x060069B9 RID: 27065 RVA: 0x0016B3F1 File Offset: 0x001695F1
		public void Dispose()
		{
			this.current = null;
		}

		// Token: 0x060069BA RID: 27066 RVA: 0x0016B3FC File Offset: 0x001695FC
		public bool MoveNext()
		{
			if (this.dimensionEnumerators == null)
			{
				this.result = this.GetResult();
				Value value;
				if (!this.result.AsRecord.TryGetValue("report", out value))
				{
					return false;
				}
				Value value2;
				if (!value.AsRecord.TryGetValue("data", out value2))
				{
					return false;
				}
				this.dimensions = this.GetDimensions();
				this.granularityLevels = this.GetGranularityLevels();
				this.measures = this.GetMeasures();
				this.keys = this.GetKeys();
				int num = this.dimensions.Count;
				if (this.granularityLevels.Count > 0)
				{
					num++;
				}
				if (num == 0)
				{
					num = 1;
				}
				this.dimensionEnumerators = new IEnumerator<IValueReference>[num];
				this.dimensionEnumerators[0] = value2.AsList.GetEnumerator();
			}
			if (!this.TryIncrementEnumerators(this.dimensionEnumerators.Length - 1))
			{
				return false;
			}
			IValueReference[] array = new IValueReference[this.keys.Length];
			IEnumerable<IEnumerator<IValueReference>> enumerable = this.dimensionEnumerators;
			if (this.granularityLevels.Any<string>())
			{
				Value value3 = this.dimensionEnumerators.First<IEnumerator<IValueReference>>().Current.Value;
				foreach (string text in this.granularityLevels)
				{
					int num2;
					this.keys.TryGetKeyIndex(text, out num2);
					array[num2] = value3[text];
				}
				enumerable = this.dimensionEnumerators.Skip(1);
			}
			int num3 = 0;
			if (this.dimensions.Count > 0)
			{
				foreach (IEnumerator<IValueReference> enumerator3 in enumerable)
				{
					int num4;
					this.keys.TryGetKeyIndex(this.dimensions[num3], out num4);
					array[num4] = enumerator3.Current.Value["name"];
					num3++;
				}
			}
			if (this.measures.Count > 0)
			{
				num3 = 0;
				foreach (IValueReference valueReference in this.dimensionEnumerators[this.dimensionEnumerators.Length - 1].Current.Value["counts"].AsList)
				{
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
					int num5;
					this.keys.TryGetKeyIndex(this.measures[num3], out num5);
					array[num5] = numberValue;
					num3++;
				}
			}
			this.current = RecordValue.New(this.keys, array);
			return true;
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x0016B744 File Offset: 0x00169944
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

		// Token: 0x060069BC RID: 27068 RVA: 0x0016B7D7 File Offset: 0x001699D7
		private Value CurrentAtDepth(int depth)
		{
			return this.dimensionEnumerators[depth].Current.Value;
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x0016B7EC File Offset: 0x001699EC
		public void Reset()
		{
			for (int i = 0; i < this.dimensionEnumerators.Length; i++)
			{
				this.dimensionEnumerators[i].Dispose();
				this.dimensionEnumerators[i] = null;
			}
			this.dimensionEnumerators = null;
		}

		// Token: 0x04003A76 RID: 14966
		private IList<string> granularityLevels;

		// Token: 0x04003A77 RID: 14967
		private IList<string> dimensions;

		// Token: 0x04003A78 RID: 14968
		private IList<string> measures;

		// Token: 0x04003A79 RID: 14969
		private Keys keys;

		// Token: 0x04003A7A RID: 14970
		private Value result;

		// Token: 0x04003A7B RID: 14971
		private IEnumerator<IValueReference>[] dimensionEnumerators;

		// Token: 0x04003A7C RID: 14972
		private IValueReference current;
	}
}
