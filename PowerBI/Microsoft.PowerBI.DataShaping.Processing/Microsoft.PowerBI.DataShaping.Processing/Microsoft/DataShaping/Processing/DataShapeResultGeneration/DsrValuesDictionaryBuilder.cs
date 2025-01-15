using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000075 RID: 117
	internal sealed class DsrValuesDictionaryBuilder
	{
		// Token: 0x060002FD RID: 765 RVA: 0x00009E50 File Offset: 0x00008050
		internal DsrValuesDictionaryBuilder(bool isVariant, string id, int capacity)
		{
			this._values = new List<object>();
			this._positionsByValue = new Dictionary<object, int>();
			this._isVariant = isVariant;
			this._id = id;
			this._capacity = capacity;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00009E83 File Offset: 0x00008083
		public IReadOnlyList<object> Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00009E8B File Offset: 0x0000808B
		public bool IsVariant
		{
			get
			{
				return this._isVariant;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009E93 File Offset: 0x00008093
		public bool HasValues
		{
			get
			{
				return this.Values.Count > 0;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00009EA3 File Offset: 0x000080A3
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009EAB File Offset: 0x000080AB
		public int HitCount
		{
			get
			{
				return this._hitCount;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00009EB3 File Offset: 0x000080B3
		public int MissCount
		{
			get
			{
				return this._missCount;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009EBC File Offset: 0x000080BC
		public bool TryGetOrAdd(object value, out int idx)
		{
			if (this._positionsByValue.TryGetValue(value, out idx))
			{
				this._hitCount++;
				return true;
			}
			if (this._capacity <= this._values.Count)
			{
				this._missCount++;
				return false;
			}
			idx = this.Values.Count;
			this._values.Add(value);
			this._positionsByValue.Add(value, idx);
			return true;
		}

		// Token: 0x040001BD RID: 445
		private readonly Dictionary<object, int> _positionsByValue;

		// Token: 0x040001BE RID: 446
		private readonly List<object> _values;

		// Token: 0x040001BF RID: 447
		private readonly bool _isVariant;

		// Token: 0x040001C0 RID: 448
		private readonly string _id;

		// Token: 0x040001C1 RID: 449
		private readonly int _capacity;

		// Token: 0x040001C2 RID: 450
		private int _hitCount;

		// Token: 0x040001C3 RID: 451
		private int _missCount;
	}
}
