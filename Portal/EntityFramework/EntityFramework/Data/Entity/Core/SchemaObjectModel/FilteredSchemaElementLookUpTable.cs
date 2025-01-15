using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F2 RID: 754
	internal sealed class FilteredSchemaElementLookUpTable<T, S> : IEnumerable<T>, IEnumerable, ISchemaElementLookUpTable<T> where T : S where S : SchemaElement
	{
		// Token: 0x060023E2 RID: 9186 RVA: 0x00065751 File Offset: 0x00063951
		public FilteredSchemaElementLookUpTable(SchemaElementLookUpTable<S> lookUpTable)
		{
			this._lookUpTable = lookUpTable;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00065760 File Offset: 0x00063960
		public IEnumerator<T> GetEnumerator()
		{
			return this._lookUpTable.GetFilteredEnumerator<T>();
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x0006576D File Offset: 0x0006396D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._lookUpTable.GetFilteredEnumerator<T>();
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0006577C File Offset: 0x0006397C
		public int Count
		{
			get
			{
				int num = 0;
				using (IEnumerator<S> enumerator = this._lookUpTable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current is T)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000657D4 File Offset: 0x000639D4
		public bool ContainsKey(string key)
		{
			return this._lookUpTable.ContainsKey(key) && this._lookUpTable[key] is T;
		}

		// Token: 0x17000788 RID: 1928
		public T this[string key]
		{
			get
			{
				S s = this._lookUpTable[key];
				if (s == null)
				{
					return default(T);
				}
				T t = s as T;
				if (t != null)
				{
					return t;
				}
				throw new InvalidOperationException(Strings.UnexpectedTypeInCollection(s.GetType(), key));
			}
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00065869 File Offset: 0x00063A69
		public T LookUpEquivalentKey(string key)
		{
			return this._lookUpTable.LookUpEquivalentKey(key) as T;
		}

		// Token: 0x04000CD0 RID: 3280
		private readonly SchemaElementLookUpTable<S> _lookUpTable;
	}
}
