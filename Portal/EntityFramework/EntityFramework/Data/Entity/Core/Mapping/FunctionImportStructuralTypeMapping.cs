using System;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000541 RID: 1345
	public abstract class FunctionImportStructuralTypeMapping : MappingItem
	{
		// Token: 0x060041EC RID: 16876 RVA: 0x000DF45F File Offset: 0x000DD65F
		internal FunctionImportStructuralTypeMapping(Collection<FunctionImportReturnTypePropertyMapping> columnsRenameList, LineInfo lineInfo)
		{
			this.ColumnsRenameList = columnsRenameList;
			this.LineInfo = lineInfo;
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x000DF475 File Offset: 0x000DD675
		public ReadOnlyCollection<FunctionImportReturnTypePropertyMapping> PropertyMappings
		{
			get
			{
				return new ReadOnlyCollection<FunctionImportReturnTypePropertyMapping>(this.ColumnsRenameList);
			}
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000DF482 File Offset: 0x000DD682
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this.ColumnsRenameList);
			base.SetReadOnly();
		}

		// Token: 0x040016E3 RID: 5859
		internal readonly LineInfo LineInfo;

		// Token: 0x040016E4 RID: 5860
		internal readonly Collection<FunctionImportReturnTypePropertyMapping> ColumnsRenameList;
	}
}
