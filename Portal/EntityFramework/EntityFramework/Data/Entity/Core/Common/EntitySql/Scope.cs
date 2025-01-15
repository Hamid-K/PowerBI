using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000665 RID: 1637
	internal sealed class Scope : IEnumerable<KeyValuePair<string, ScopeEntry>>, IEnumerable
	{
		// Token: 0x06004E15 RID: 19989 RVA: 0x00118880 File Offset: 0x00116A80
		internal Scope(IEqualityComparer<string> keyComparer)
		{
			this._scopeEntries = new Dictionary<string, ScopeEntry>(keyComparer);
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x00118894 File Offset: 0x00116A94
		internal Scope Add(string key, ScopeEntry value)
		{
			this._scopeEntries.Add(key, value);
			return this;
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x001188A4 File Offset: 0x00116AA4
		internal void Remove(string key)
		{
			this._scopeEntries.Remove(key);
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x001188B3 File Offset: 0x00116AB3
		internal void Replace(string key, ScopeEntry value)
		{
			this._scopeEntries[key] = value;
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x001188C2 File Offset: 0x00116AC2
		internal bool Contains(string key)
		{
			return this._scopeEntries.ContainsKey(key);
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x001188D0 File Offset: 0x00116AD0
		internal bool TryLookup(string key, out ScopeEntry value)
		{
			return this._scopeEntries.TryGetValue(key, out value);
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x001188DF File Offset: 0x00116ADF
		public Dictionary<string, ScopeEntry>.Enumerator GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x001188EC File Offset: 0x00116AEC
		IEnumerator<KeyValuePair<string, ScopeEntry>> IEnumerable<KeyValuePair<string, ScopeEntry>>.GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x001188FE File Offset: 0x00116AFE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._scopeEntries.GetEnumerator();
		}

		// Token: 0x04001C58 RID: 7256
		private readonly Dictionary<string, ScopeEntry> _scopeEntries;
	}
}
