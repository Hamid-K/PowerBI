using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200053F RID: 1343
	internal sealed class FunctionImportReturnTypeStructuralTypeColumn
	{
		// Token: 0x060041E5 RID: 16869 RVA: 0x000DF28D File Offset: 0x000DD48D
		internal FunctionImportReturnTypeStructuralTypeColumn(string columnName, StructuralType type, bool isTypeOf, LineInfo lineInfo)
		{
			this.ColumnName = columnName;
			this.IsTypeOf = isTypeOf;
			this.Type = type;
			this.LineInfo = lineInfo;
		}

		// Token: 0x040016DB RID: 5851
		internal readonly StructuralType Type;

		// Token: 0x040016DC RID: 5852
		internal readonly bool IsTypeOf;

		// Token: 0x040016DD RID: 5853
		internal readonly string ColumnName;

		// Token: 0x040016DE RID: 5854
		internal readonly LineInfo LineInfo;
	}
}
