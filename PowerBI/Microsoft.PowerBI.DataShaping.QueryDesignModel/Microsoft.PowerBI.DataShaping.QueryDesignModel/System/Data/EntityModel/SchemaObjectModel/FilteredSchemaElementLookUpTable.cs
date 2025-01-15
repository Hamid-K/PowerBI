using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000028 RID: 40
	internal sealed class FilteredSchemaElementLookUpTable<T, S> : IEnumerable<T>, IEnumerable, ISchemaElementLookUpTable<T> where T : S where S : SchemaElement
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x0000B9AD File Offset: 0x00009BAD
		public FilteredSchemaElementLookUpTable(SchemaElementLookUpTable<S> lookUpTable)
		{
			this._lookUpTable = lookUpTable;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000B9BC File Offset: 0x00009BBC
		public IEnumerator<T> GetEnumerator()
		{
			return this._lookUpTable.GetFilteredEnumerator<T>();
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000B9C9 File Offset: 0x00009BC9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._lookUpTable.GetFilteredEnumerator<T>();
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0000B9D8 File Offset: 0x00009BD8
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

		// Token: 0x0600067F RID: 1663 RVA: 0x0000BA30 File Offset: 0x00009C30
		public bool ContainsKey(string key)
		{
			return this._lookUpTable.ContainsKey(key) && this._lookUpTable[key] is T;
		}

		// Token: 0x170002AA RID: 682
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
				throw EntityUtil.InvalidOperation(Strings.UnexpectedTypeInCollection(s.GetType(), key));
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000BAC5 File Offset: 0x00009CC5
		public T LookUpEquivalentKey(string key)
		{
			return this._lookUpTable.LookUpEquivalentKey(key) as T;
		}

		// Token: 0x0400064E RID: 1614
		private SchemaElementLookUpTable<S> _lookUpTable;
	}
}
