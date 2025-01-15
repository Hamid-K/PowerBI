using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class ListWithDefault<T> : List<T>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004154 File Offset: 0x00002354
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000415C File Offset: 0x0000235C
		public string Default { get; set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00004165 File Offset: 0x00002365
		public ListWithDefault(ListWithDefault<T>.NameAccessor nameAccessor)
		{
			this.m_nameAccessor = nameAccessor;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004174 File Offset: 0x00002374
		public bool IsDefault(string name)
		{
			return string.Equals(name, "default", 3);
		}

		// Token: 0x17000020 RID: 32
		public T this[string name]
		{
			get
			{
				T t = default(T);
				if (!this.TryGetItem(name, out t))
				{
					if (this.IsDefault(name))
					{
						if (!string.IsNullOrEmpty(this.Default))
						{
							throw new Exception(string.Format("Unable to find the default item '{0}'.", this.Default));
						}
					}
					else if (!string.IsNullOrEmpty(name))
					{
						throw new Exception(string.Format("The item named '{0}' was not found.", name));
					}
				}
				return t;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000041EC File Offset: 0x000023EC
		public bool TryGetItem(string name, out T item)
		{
			bool flag = false;
			item = default(T);
			if (!string.IsNullOrEmpty(name))
			{
				foreach (T t in this)
				{
					if (name.Equals(this.m_nameAccessor(t)))
					{
						item = t;
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				int num;
				if (this.IsDefault(name))
				{
					if (!string.IsNullOrEmpty(this.Default))
					{
						flag = this.TryGetItem(this.Default, out item);
					}
				}
				else if (int.TryParse(name, ref num) && num >= 0 && num < base.Count)
				{
					item = base[num];
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x04000037 RID: 55
		private ListWithDefault<T>.NameAccessor m_nameAccessor;

		// Token: 0x0200011F RID: 287
		// (Invoke) Token: 0x06000BC9 RID: 3017
		public delegate string NameAccessor(T item);
	}
}
