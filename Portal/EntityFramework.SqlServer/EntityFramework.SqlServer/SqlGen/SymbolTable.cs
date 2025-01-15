using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003E RID: 62
	internal sealed class SymbolTable
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x00019FC7 File Offset: 0x000181C7
		internal void EnterScope()
		{
			this.symbols.Add(new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase));
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00019FDE File Offset: 0x000181DE
		internal void ExitScope()
		{
			this.symbols.RemoveAt(this.symbols.Count - 1);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00019FF8 File Offset: 0x000181F8
		internal void Add(string name, Symbol value)
		{
			this.symbols[this.symbols.Count - 1][name] = value;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001A01C File Offset: 0x0001821C
		internal Symbol Lookup(string name)
		{
			for (int i = this.symbols.Count - 1; i >= 0; i--)
			{
				if (this.symbols[i].ContainsKey(name))
				{
					return this.symbols[i][name];
				}
			}
			return null;
		}

		// Token: 0x04000126 RID: 294
		private readonly List<Dictionary<string, Symbol>> symbols = new List<Dictionary<string, Symbol>>();
	}
}
