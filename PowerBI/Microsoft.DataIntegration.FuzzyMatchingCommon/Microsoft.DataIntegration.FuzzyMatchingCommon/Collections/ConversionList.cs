using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	internal class ConversionList<T, TOutput> : IList<TOutput>, ICollection<TOutput>, IEnumerable<TOutput>, IEnumerable where T : TOutput
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x0001C04A File Offset: 0x0001A24A
		public ConversionList(IList<T> list, Converter<T, TOutput> converter)
		{
			this.m_items = list;
			this.m_converter = converter;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001C060 File Offset: 0x0001A260
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0001C068 File Offset: 0x0001A268
		protected IList<T> Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				this.m_items = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		public virtual TOutput this[int index]
		{
			get
			{
				return this.m_converter.Invoke(this.Items[index]);
			}
			set
			{
				this.Insert(index, value);
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001C094 File Offset: 0x0001A294
		public int IndexOf(TOutput item)
		{
			if (item is T)
			{
				return this.Items.IndexOf((T)((object)item));
			}
			for (int i = 0; i < this.Count; i++)
			{
				TOutput toutput = this.m_converter.Invoke(this.Items[i]);
				if (toutput.Equals(item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001C108 File Offset: 0x0001A308
		public virtual void Insert(int index, TOutput item)
		{
			if (item is T)
			{
				this.Items.Insert(index, (T)((object)item));
				return;
			}
			throw new ArgumentException(string.Concat(new string[]
			{
				"Unable to convert ",
				typeof(TOutput).ToString(),
				" to ",
				typeof(T).ToString(),
				"."
			}));
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001C186 File Offset: 0x0001A386
		public virtual void RemoveAt(int index)
		{
			this.Items.RemoveAt(index);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001C194 File Offset: 0x0001A394
		public IEnumerator<TOutput> GetEnumerator()
		{
			return new ConversionEnumerator<T, TOutput>(this.Items.GetEnumerator(), this.m_converter);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ConversionEnumerator<T, TOutput>(this.Items.GetEnumerator(), this.m_converter);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001C1D1 File Offset: 0x0001A3D1
		public bool IsReadOnly
		{
			get
			{
				return this.Items.IsReadOnly;
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001C1E0 File Offset: 0x0001A3E0
		public virtual void Add(TOutput item)
		{
			if (item is T)
			{
				this.Items.Add((T)((object)item));
				return;
			}
			throw new ArgumentException(string.Concat(new string[]
			{
				"Unable to convert ",
				typeof(TOutput).ToString(),
				" to ",
				typeof(T).ToString(),
				"."
			}));
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001C25D File Offset: 0x0001A45D
		public void Clear()
		{
			this.Items.Clear();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001C26A File Offset: 0x0001A46A
		public bool Contains(TOutput item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001C27C File Offset: 0x0001A47C
		public void CopyTo(TOutput[] array, int arrayIndex)
		{
			int num = 0;
			while (arrayIndex < array.GetLength(0) && num < this.Count)
			{
				array[arrayIndex++] = this.m_converter.Invoke(this.Items[num++]);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		public virtual bool Remove(TOutput item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.Items.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		public virtual object Clone()
		{
			return new ConversionList<T, TOutput>(this.Items, this.m_converter);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001C304 File Offset: 0x0001A504
		public virtual void AddRange(IEnumerable<TOutput> items)
		{
			foreach (TOutput toutput in items)
			{
				if (!(toutput is T))
				{
					throw new ArgumentException(string.Concat(new string[]
					{
						"Unable to convert ",
						typeof(TOutput).ToString(),
						" to ",
						typeof(T).ToString(),
						"."
					}));
				}
				this.Items.Add((T)((object)toutput));
			}
		}

		// Token: 0x040000C8 RID: 200
		private IList<T> m_items;

		// Token: 0x040000C9 RID: 201
		private Converter<T, TOutput> m_converter;
	}
}
