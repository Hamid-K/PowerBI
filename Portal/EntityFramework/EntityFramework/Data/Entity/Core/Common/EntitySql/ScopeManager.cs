using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200066E RID: 1646
	internal sealed class ScopeManager
	{
		// Token: 0x06004ECE RID: 20174 RVA: 0x0011EE6B File Offset: 0x0011D06B
		internal ScopeManager(IEqualityComparer<string> keyComparer)
		{
			this._keyComparer = keyComparer;
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x0011EE85 File Offset: 0x0011D085
		internal void EnterScope()
		{
			this._scopes.Add(new Scope(this._keyComparer));
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x0011EE9D File Offset: 0x0011D09D
		internal void LeaveScope()
		{
			this._scopes.RemoveAt(this.CurrentScopeIndex);
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x0011EEB0 File Offset: 0x0011D0B0
		internal int CurrentScopeIndex
		{
			get
			{
				return this._scopes.Count - 1;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06004ED2 RID: 20178 RVA: 0x0011EEBF File Offset: 0x0011D0BF
		internal Scope CurrentScope
		{
			get
			{
				return this._scopes[this.CurrentScopeIndex];
			}
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x0011EED2 File Offset: 0x0011D0D2
		internal Scope GetScopeByIndex(int scopeIndex)
		{
			if (0 > scopeIndex || scopeIndex > this.CurrentScopeIndex)
			{
				throw new EntitySqlException(Strings.InvalidScopeIndex);
			}
			return this._scopes[scopeIndex];
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x0011EEF8 File Offset: 0x0011D0F8
		internal void RollbackToScope(int scopeIndex)
		{
			if (scopeIndex > this.CurrentScopeIndex || scopeIndex < 0 || this.CurrentScopeIndex < 0)
			{
				throw new EntitySqlException(Strings.InvalidSavePoint);
			}
			if (this.CurrentScopeIndex - scopeIndex > 0)
			{
				this._scopes.RemoveRange(scopeIndex + 1, this.CurrentScopeIndex - scopeIndex);
			}
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x0011EF47 File Offset: 0x0011D147
		internal bool IsInCurrentScope(string key)
		{
			return this.CurrentScope.Contains(key);
		}

		// Token: 0x04001C7D RID: 7293
		private readonly IEqualityComparer<string> _keyComparer;

		// Token: 0x04001C7E RID: 7294
		private readonly List<Scope> _scopes = new List<Scope>();
	}
}
