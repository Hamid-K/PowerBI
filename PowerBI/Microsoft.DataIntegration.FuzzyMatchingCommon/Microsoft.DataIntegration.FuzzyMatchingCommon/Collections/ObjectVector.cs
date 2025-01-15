using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000092 RID: 146
	[Serializable]
	public sealed class ObjectVector<T> : Vector<T> where T : new()
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x000231FF File Offset: 0x000213FF
		public ObjectVector()
			: this(1)
		{
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00023208 File Offset: 0x00021408
		public ObjectVector(int capacity)
		{
			base.Items = new T[capacity];
			for (int i = 0; i < capacity; i++)
			{
				base.Items[i] = new T();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00023244 File Offset: 0x00021444
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00023250 File Offset: 0x00021450
		public override int Capacity
		{
			get
			{
				return base.Items.Length;
			}
			set
			{
				int num = base.Items.Length;
				if (value > num)
				{
					T[] items = base.Items;
					Array.Resize<T>(ref items, value);
					base.Items = items;
					for (int i = num; i < value; i++)
					{
						base.Items[i] = new T();
					}
				}
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000232A0 File Offset: 0x000214A0
		public T Add()
		{
			if (base.Count == this.Capacity)
			{
				this.Capacity = Math.Max(1, this.Capacity * 2);
			}
			T[] items = base.Items;
			int count = base.Count;
			base.Count = count + 1;
			return items[count];
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x000232EB File Offset: 0x000214EB
		public new long MemoryUsage
		{
			get
			{
				return (long)(20 + base.Items.Length * 2 * 8);
			}
		}
	}
}
