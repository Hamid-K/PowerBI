using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200004E RID: 78
	[NullableContext(1)]
	[Nullable(0)]
	internal struct KeyValueAccumulator
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00007464 File Offset: 0x00005664
		public void Append(string key, string value)
		{
			if (this._accumulator == null)
			{
				this._accumulator = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			}
			string[] array;
			if (this._accumulator.TryGetValue(key, out array))
			{
				if (array != null && array.Length == 0)
				{
					this._expandingAccumulator[key].Add(value);
				}
				else if (array != null && array.Length == 1)
				{
					this._accumulator[key] = new string[]
					{
						array[0],
						value
					};
				}
				else if (array != null)
				{
					this._accumulator[key] = null;
					if (this._expandingAccumulator == null)
					{
						this._expandingAccumulator = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
					}
					List<string> list = new List<string>(8);
					list.Add(array[0]);
					list.Add(array[1]);
					list.Add(value);
					this._expandingAccumulator[key] = list;
				}
			}
			else
			{
				this._accumulator[key] = new string[] { value };
			}
			int valueCount = this.ValueCount;
			this.ValueCount = valueCount + 1;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000755E File Offset: 0x0000575E
		public bool HasValues
		{
			get
			{
				return this.ValueCount > 0;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00007569 File Offset: 0x00005769
		public int KeyCount
		{
			get
			{
				Dictionary<string, string[]> accumulator = this._accumulator;
				if (accumulator == null)
				{
					return 0;
				}
				return accumulator.Count;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000757C File Offset: 0x0000577C
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00007584 File Offset: 0x00005784
		public int ValueCount { readonly get; private set; }

		// Token: 0x06000259 RID: 601 RVA: 0x00007590 File Offset: 0x00005790
		[return: Nullable(new byte[] { 1, 1, 2, 1 })]
		public Dictionary<string, string[]> GetResults()
		{
			if (this._expandingAccumulator != null)
			{
				foreach (KeyValuePair<string, List<string>> keyValuePair in this._expandingAccumulator)
				{
					this._accumulator[keyValuePair.Key] = keyValuePair.Value.ToArray();
				}
			}
			return this._accumulator ?? new Dictionary<string, string[]>(0, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x04000103 RID: 259
		[Nullable(new byte[] { 1, 1, 2, 1 })]
		private Dictionary<string, string[]> _accumulator;

		// Token: 0x04000104 RID: 260
		private Dictionary<string, List<string>> _expandingAccumulator;
	}
}
