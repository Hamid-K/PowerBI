using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015B8 RID: 5560
	internal sealed class OptionsRecord
	{
		// Token: 0x06008B4D RID: 35661 RVA: 0x001D47A4 File Offset: 0x001D29A4
		public OptionsRecord(Keys keys, Value[] values, object[] objects)
		{
			this.keys = keys;
			this.values = values;
			this.objects = objects;
		}

		// Token: 0x170024AE RID: 9390
		// (get) Token: 0x06008B4E RID: 35662 RVA: 0x001D47C1 File Offset: 0x001D29C1
		public RecordValue AsRecord
		{
			get
			{
				return RecordValue.New(this.keys, this.values);
			}
		}

		// Token: 0x06008B4F RID: 35663 RVA: 0x001D47D4 File Offset: 0x001D29D4
		public T GetAs<T>(string optionName)
		{
			int num = this.keys.IndexOfKey(optionName);
			object obj = this.objects[num];
			if (!(obj is T))
			{
				throw ValueException.NewExpressionError<Message2>(Strings.InvalidValueForOption(obj.ToString(), optionName), this.values[num], null);
			}
			return (T)((object)obj);
		}

		// Token: 0x06008B50 RID: 35664 RVA: 0x001D4820 File Offset: 0x001D2A20
		public bool TryGetValue(string optionName, out object value)
		{
			int num;
			if (!this.keys.TryGetKeyIndex(optionName, out num))
			{
				value = null;
				return false;
			}
			value = this.objects[num];
			return true;
		}

		// Token: 0x06008B51 RID: 35665 RVA: 0x001D4850 File Offset: 0x001D2A50
		public bool TryGetString(string optionName, out string value)
		{
			object obj;
			this.TryGetValue(optionName, out obj);
			value = obj as string;
			return value != null;
		}

		// Token: 0x06008B52 RID: 35666 RVA: 0x001D4874 File Offset: 0x001D2A74
		public bool TryGetValue(string optionName, out Value value)
		{
			int num;
			if (!this.keys.TryGetKeyIndex(optionName, out num))
			{
				value = null;
				return false;
			}
			value = this.values[num];
			return true;
		}

		// Token: 0x06008B53 RID: 35667 RVA: 0x001D48A4 File Offset: 0x001D2AA4
		public bool GetBool(string optionName, bool defaultValue)
		{
			object obj;
			this.TryGetValue(optionName, out obj);
			return (obj as bool?).GetValueOrDefault(defaultValue);
		}

		// Token: 0x06008B54 RID: 35668 RVA: 0x001D48D0 File Offset: 0x001D2AD0
		public int GetInt32(string optionName)
		{
			Value value;
			this.TryGetValue(optionName, out value);
			return value.AsNumber.AsInteger32;
		}

		// Token: 0x06008B55 RID: 35669 RVA: 0x001D48F4 File Offset: 0x001D2AF4
		public int GetInt32(string optionName, int defaultValue)
		{
			object obj;
			this.TryGetValue(optionName, out obj);
			return (obj as int?).GetValueOrDefault(defaultValue);
		}

		// Token: 0x06008B56 RID: 35670 RVA: 0x001D4920 File Offset: 0x001D2B20
		public bool TryGetDurationAsSeconds(string optionName, out int duration)
		{
			object obj;
			if (!this.TryGetValue(optionName, out obj) || !(obj is TimeSpan))
			{
				duration = 0;
				return false;
			}
			double num = Math.Round(((TimeSpan)obj).TotalSeconds);
			if (num > 2147483647.0)
			{
				throw ValueException.NewExpressionError<Message2>(Strings.DurationIsTooLarge(optionName, int.MaxValue), DurationValue.New((TimeSpan)obj), null);
			}
			if (num < 1.0)
			{
				throw ValueException.NewExpressionError<Message2>(Strings.DurationIsTooSmall(optionName, 1), DurationValue.New((TimeSpan)obj), null);
			}
			duration = (int)num;
			return true;
		}

		// Token: 0x04004C4B RID: 19531
		private readonly Keys keys;

		// Token: 0x04004C4C RID: 19532
		private readonly Value[] values;

		// Token: 0x04004C4D RID: 19533
		private readonly object[] objects;
	}
}
