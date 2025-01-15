using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200053D RID: 1341
	public abstract class FunctionImportReturnTypePropertyMapping : MappingItem
	{
		// Token: 0x060041DC RID: 16860 RVA: 0x000DF224 File Offset: 0x000DD424
		internal FunctionImportReturnTypePropertyMapping(LineInfo lineInfo)
		{
			this.LineInfo = lineInfo;
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060041DD RID: 16861
		internal abstract string CMember { get; }

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060041DE RID: 16862
		internal abstract string SColumn { get; }

		// Token: 0x040016D8 RID: 5848
		internal readonly LineInfo LineInfo;
	}
}
