using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200055A RID: 1370
	public class ScalarPropertyMapping : PropertyMapping
	{
		// Token: 0x060042DD RID: 17117 RVA: 0x000E5CB0 File Offset: 0x000E3EB0
		public ScalarPropertyMapping(EdmProperty property, EdmProperty column)
			: base(property)
		{
			Check.NotNull<EdmProperty>(property, "property");
			Check.NotNull<EdmProperty>(column, "column");
			if (!Helper.IsScalarType(property.TypeUsage.EdmType) || !Helper.IsPrimitiveType(column.TypeUsage.EdmType))
			{
				throw new ArgumentException(Strings.StorageScalarPropertyMapping_OnlyScalarPropertiesAllowed);
			}
			this._column = column;
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x000E5D12 File Offset: 0x000E3F12
		// (set) Token: 0x060042DF RID: 17119 RVA: 0x000E5D1A File Offset: 0x000E3F1A
		public EdmProperty Column
		{
			get
			{
				return this._column;
			}
			internal set
			{
				this._column = value;
			}
		}

		// Token: 0x040017E5 RID: 6117
		private EdmProperty _column;
	}
}
