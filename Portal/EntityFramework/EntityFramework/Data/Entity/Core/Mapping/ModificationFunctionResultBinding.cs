using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000550 RID: 1360
	public sealed class ModificationFunctionResultBinding : MappingItem
	{
		// Token: 0x060042BA RID: 17082 RVA: 0x000E59C2 File Offset: 0x000E3BC2
		public ModificationFunctionResultBinding(string columnName, EdmProperty property)
		{
			Check.NotNull<string>(columnName, "columnName");
			Check.NotNull<EdmProperty>(property, "property");
			this._columnName = columnName;
			this._property = property;
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x000E59F0 File Offset: 0x000E3BF0
		// (set) Token: 0x060042BC RID: 17084 RVA: 0x000E59F8 File Offset: 0x000E3BF8
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
			internal set
			{
				this._columnName = value;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x000E5A01 File Offset: 0x000E3C01
		public EdmProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x000E5A09 File Offset: 0x000E3C09
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}->{1}", new object[] { this.ColumnName, this.Property });
		}

		// Token: 0x04001781 RID: 6017
		private string _columnName;

		// Token: 0x04001782 RID: 6018
		private readonly EdmProperty _property;
	}
}
