using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001368 RID: 4968
	public struct KeysBuilder
	{
		// Token: 0x060082C6 RID: 33478 RVA: 0x001BB1D9 File Offset: 0x001B93D9
		public KeysBuilder(int initialCapacity)
		{
			this.s0 = null;
			this.s1 = null;
			this.s2 = null;
			this.s3 = null;
			if (initialCapacity >= 8)
			{
				this.indices = new Dictionary<string, int>(initialCapacity);
				return;
			}
			this.indices = null;
		}

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x060082C7 RID: 33479 RVA: 0x001BB20F File Offset: 0x001B940F
		public int Count
		{
			get
			{
				if (this.indices != null)
				{
					return this.indices.Count;
				}
				if (this.s0 == null)
				{
					return 0;
				}
				if (this.s1 == null)
				{
					return 1;
				}
				if (this.s2 == null)
				{
					return 2;
				}
				if (this.s3 == null)
				{
					return 3;
				}
				return 4;
			}
		}

		// Token: 0x060082C8 RID: 33480 RVA: 0x001BB250 File Offset: 0x001B9450
		public void Union(Keys keys)
		{
			foreach (string text in keys)
			{
				this.Union(text);
			}
		}

		// Token: 0x060082C9 RID: 33481 RVA: 0x001BB2A0 File Offset: 0x001B94A0
		public void Add(string key)
		{
			if (!this.Union(key))
			{
				throw ValueException.DuplicateField(key);
			}
		}

		// Token: 0x060082CA RID: 33482 RVA: 0x001BB2B4 File Offset: 0x001B94B4
		public bool Union(string key)
		{
			if (this.indices != null)
			{
				if (this.indices.ContainsKey(key))
				{
					return false;
				}
				this.indices.Add(key, this.indices.Count);
				return true;
			}
			else
			{
				if (this.s0 == null)
				{
					this.s0 = key;
					return true;
				}
				if (this.s0 == key)
				{
					return false;
				}
				if (this.s1 == null)
				{
					this.s1 = key;
					return true;
				}
				if (this.s1 == key)
				{
					return false;
				}
				if (this.s2 == null)
				{
					this.s2 = key;
					return true;
				}
				if (this.s2 == key)
				{
					return false;
				}
				if (this.s3 == null)
				{
					this.s3 = key;
					return true;
				}
				if (this.s3 == key)
				{
					return false;
				}
				this.indices = new Dictionary<string, int>(8);
				this.indices.Add(this.s0, 0);
				this.indices.Add(this.s1, 1);
				this.indices.Add(this.s2, 2);
				this.indices.Add(this.s3, 3);
				this.indices.Add(key, 4);
				this.s0 = null;
				this.s1 = null;
				this.s2 = null;
				this.s3 = null;
				return true;
			}
		}

		// Token: 0x060082CB RID: 33483 RVA: 0x001BB3F4 File Offset: 0x001B95F4
		public int IndexOf(string key)
		{
			if (this.indices != null)
			{
				int num;
				if (this.indices.TryGetValue(key, out num))
				{
					return num;
				}
				return -1;
			}
			else
			{
				if (this.s0 == null)
				{
					return -1;
				}
				if (this.s0 == key)
				{
					return 0;
				}
				if (this.s1 == null)
				{
					return -1;
				}
				if (this.s1 == key)
				{
					return 1;
				}
				if (this.s2 == null)
				{
					return -1;
				}
				if (this.s2 == key)
				{
					return 2;
				}
				if (this.s3 == null)
				{
					return -1;
				}
				if (this.s3 == key)
				{
					return 3;
				}
				return -1;
			}
		}

		// Token: 0x060082CC RID: 33484 RVA: 0x001BB488 File Offset: 0x001B9688
		public Keys ToKeys()
		{
			if (this.indices != null)
			{
				return Keys.New(this.indices);
			}
			if (this.s0 == null)
			{
				return Keys.Empty;
			}
			if (this.s1 == null)
			{
				return Keys.New(this.s0);
			}
			if (this.s2 == null)
			{
				return Keys.New(this.s0, this.s1);
			}
			if (this.s3 == null)
			{
				return Keys.New(this.s0, this.s1, this.s2);
			}
			return Keys.New(this.s0, this.s1, this.s2, this.s3);
		}

		// Token: 0x040046FB RID: 18171
		private const int defaultInitialCapacity = 8;

		// Token: 0x040046FC RID: 18172
		private string s0;

		// Token: 0x040046FD RID: 18173
		private string s1;

		// Token: 0x040046FE RID: 18174
		private string s2;

		// Token: 0x040046FF RID: 18175
		private string s3;

		// Token: 0x04004700 RID: 18176
		private Dictionary<string, int> indices;
	}
}
