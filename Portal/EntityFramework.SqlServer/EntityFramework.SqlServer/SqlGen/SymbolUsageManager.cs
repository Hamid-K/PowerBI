using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003F RID: 63
	internal class SymbolUsageManager
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0001A07C File Offset: 0x0001827C
		internal bool ContainsKey(Symbol key)
		{
			return this.optionalColumnUsage.ContainsKey(key);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001A08C File Offset: 0x0001828C
		internal bool TryGetValue(Symbol key, out bool value)
		{
			BoolWrapper boolWrapper;
			if (this.optionalColumnUsage.TryGetValue(key, out boolWrapper))
			{
				value = boolWrapper.Value;
				return true;
			}
			value = false;
			return false;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001A0B8 File Offset: 0x000182B8
		internal void Add(Symbol sourceSymbol, Symbol symbolToAdd)
		{
			BoolWrapper boolWrapper;
			if (sourceSymbol == null || !this.optionalColumnUsage.TryGetValue(sourceSymbol, out boolWrapper))
			{
				boolWrapper = new BoolWrapper();
			}
			this.optionalColumnUsage.Add(symbolToAdd, boolWrapper);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001A0EB File Offset: 0x000182EB
		internal void MarkAsUsed(Symbol key)
		{
			if (this.optionalColumnUsage.ContainsKey(key))
			{
				this.optionalColumnUsage[key].Value = true;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001A10D File Offset: 0x0001830D
		internal bool IsUsed(Symbol key)
		{
			return this.optionalColumnUsage[key].Value;
		}

		// Token: 0x04000127 RID: 295
		private readonly Dictionary<Symbol, BoolWrapper> optionalColumnUsage = new Dictionary<Symbol, BoolWrapper>();
	}
}
