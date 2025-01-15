using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000069 RID: 105
	internal class DictionarySerializationWriter : ISerializationWriter
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000EA64 File Offset: 0x0000CC64
		internal DictionarySerializationWriter()
		{
			this.AccumulatedDictionary = new Dictionary<string, string>();
			this.AccumulatedMeasurements = new Dictionary<string, double>();
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000EAB6 File Offset: 0x0000CCB6
		internal Dictionary<string, string> AccumulatedDictionary { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000EABE File Offset: 0x0000CCBE
		internal Dictionary<string, double> AccumulatedMeasurements { get; }

		// Token: 0x06000331 RID: 817 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
		public void WriteProperty(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			string key = this.GetKey(name);
			if (!this.AccumulatedDictionary.ContainsKey(key))
			{
				this.AccumulatedDictionary[key] = value;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000EB10 File Offset: 0x0000CD10
		public void WriteProperty(string name, double? value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			string key = this.GetKey(name);
			if (!this.AccumulatedDictionary.ContainsKey(key))
			{
				if (value != null)
				{
					this.AccumulatedDictionary[key] = value.Value.ToString(CultureInfo.InvariantCulture);
					return;
				}
				this.AccumulatedDictionary[key] = null;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public void WriteProperty(string name, int? value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			string key = this.GetKey(name);
			if (!this.AccumulatedDictionary.ContainsKey(key))
			{
				if (value != null)
				{
					this.AccumulatedDictionary[key] = value.Value.ToString(CultureInfo.InvariantCulture);
					return;
				}
				this.AccumulatedDictionary[key] = null;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public void WriteProperty(string name, bool? value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			string key = this.GetKey(name);
			if (!this.AccumulatedDictionary.ContainsKey(key))
			{
				if (value != null)
				{
					this.AccumulatedDictionary[key] = value.Value.ToString(CultureInfo.InvariantCulture);
					return;
				}
				this.AccumulatedDictionary[key] = null;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public void WriteProperty(string name, TimeSpan? value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			string key = this.GetKey(name);
			if (!this.AccumulatedDictionary.ContainsKey(key))
			{
				if (value != null)
				{
					this.AccumulatedDictionary[key] = value.Value.ToString();
					return;
				}
				this.AccumulatedDictionary[key] = null;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		public void WriteProperty(string name, DateTimeOffset? value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			string key = this.GetKey(name);
			if (!this.AccumulatedDictionary.ContainsKey(key))
			{
				if (value != null)
				{
					this.AccumulatedDictionary[key] = value.Value.ToString(CultureInfo.InvariantCulture);
					return;
				}
				this.AccumulatedDictionary[key] = null;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000ED54 File Offset: 0x0000CF54
		public void WriteProperty(string name, ISerializableWithWriter value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			if (value != null)
			{
				this.lastPrefix.Push(this.currentPrefix);
				this.currentPrefix = this.GetKey(name);
				value.Serialize(this);
				this.currentPrefix = this.lastPrefix.Pop();
				return;
			}
			this.WriteProperty(this.GetKey(name), null);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000EDC1 File Offset: 0x0000CFC1
		public void WriteProperty(ISerializableWithWriter value)
		{
			if (value != null)
			{
				this.lastPrefix.Push(this.currentPrefix);
				this.currentPrefix = this.GenerateSequencialPrefix();
				value.Serialize(this);
				this.currentPrefix = this.lastPrefix.Pop();
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		public void WriteProperty(string name, IList<string> items)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					string key = this.GetKey(name + i);
					if (!this.AccumulatedDictionary.ContainsKey(key))
					{
						this.AccumulatedDictionary[key] = items[i];
					}
				}
				return;
			}
			this.WriteProperty(this.GetKey(name), null);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000EE78 File Offset: 0x0000D078
		public void WriteProperty(string name, IList<ISerializableWithWriter> items)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					if (items[i] != null)
					{
						this.lastPrefix.Push(this.currentPrefix);
						this.currentPrefix = this.GetKey(name + i);
						items[i].Serialize(this);
						this.currentPrefix = this.lastPrefix.Pop();
					}
					else
					{
						this.AccumulatedDictionary[this.GetKey(name + i)] = null;
					}
				}
				return;
			}
			this.WriteProperty(this.GetKey(name), null);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000EF34 File Offset: 0x0000D134
		public void WriteProperty(string name, IDictionary<string, string> items)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			if (items != null)
			{
				using (IEnumerator<KeyValuePair<string, string>> enumerator = items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, string> keyValuePair = enumerator.Current;
						string key = this.GetKey(name + "." + keyValuePair.Key);
						if (!this.AccumulatedDictionary.ContainsKey(key))
						{
							this.AccumulatedDictionary[key] = keyValuePair.Value;
						}
					}
					return;
				}
			}
			this.WriteProperty(this.GetKey(name), null);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		public void WriteProperty(string name, IDictionary<string, double> items)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			if (items != null)
			{
				foreach (KeyValuePair<string, double> keyValuePair in items)
				{
					string key = this.GetKey(name + "." + keyValuePair.Key);
					if (!this.AccumulatedMeasurements.ContainsKey(key))
					{
						this.AccumulatedMeasurements[key] = keyValuePair.Value;
					}
				}
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F070 File Offset: 0x0000D270
		public void WriteStartObject(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("This argument requires a non-empty value", "name");
			}
			this.lastPrefix.Push(this.currentPrefix);
			this.currentPrefix = this.GetKey(name);
			this.lastIndex.Push(this.currentIndex);
			this.currentIndex = 1L;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public void WriteStartObject()
		{
			this.lastPrefix.Push(this.currentPrefix);
			this.currentPrefix = this.GenerateSequencialPrefixForObject();
			this.lastIndex.Push(this.currentIndex);
			this.currentIndex = 1L;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F104 File Offset: 0x0000D304
		public void WriteEndObject()
		{
			this.currentPrefix = this.lastPrefix.Pop();
			this.currentIndex = this.lastIndex.Pop();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F128 File Offset: 0x0000D328
		private string GetKey(string fieldName)
		{
			if (!string.IsNullOrEmpty(this.currentPrefix))
			{
				return this.currentPrefix + "." + fieldName;
			}
			return fieldName;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F14C File Offset: 0x0000D34C
		private string GenerateSequencialPrefix()
		{
			object obj = "Key";
			long num = this.currentIndex;
			this.currentIndex = num + 1L;
			return this.GetKey(obj + num);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F180 File Offset: 0x0000D380
		private string GenerateSequencialPrefixForObject()
		{
			object obj = "Obj";
			long num = this.currentIndex;
			this.currentIndex = num + 1L;
			return this.GetKey(obj + num);
		}

		// Token: 0x0400015B RID: 347
		internal const string DefaultKey = "Key";

		// Token: 0x0400015C RID: 348
		internal const string DefaultObjectKey = "Obj";

		// Token: 0x0400015D RID: 349
		private readonly Stack<string> lastPrefix = new Stack<string>();

		// Token: 0x0400015E RID: 350
		private readonly Stack<long> lastIndex = new Stack<long>();

		// Token: 0x0400015F RID: 351
		private string currentPrefix = string.Empty;

		// Token: 0x04000160 RID: 352
		private long currentIndex = 1L;
	}
}
