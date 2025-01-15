using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000525 RID: 1317
	public class ConditionPropertyMapping : PropertyMapping
	{
		// Token: 0x060040DD RID: 16605 RVA: 0x000DB430 File Offset: 0x000D9630
		internal ConditionPropertyMapping(EdmProperty propertyOrColumn, object value, bool? isNull)
		{
			DataSpace dataSpace = propertyOrColumn.TypeUsage.EdmType.DataSpace;
			if (dataSpace != DataSpace.CSpace)
			{
				if (dataSpace != DataSpace.SSpace)
				{
					throw new ArgumentException(Strings.MetadataItem_InvalidDataSpace(dataSpace, typeof(EdmProperty).Name), "propertyOrColumn");
				}
				this._column = propertyOrColumn;
			}
			else
			{
				base.Property = propertyOrColumn;
			}
			this._value = value;
			this._isNull = isNull;
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x000DB4A3 File Offset: 0x000D96A3
		internal ConditionPropertyMapping(EdmProperty property, EdmProperty column, object value, bool? isNull)
			: base(property)
		{
			this._column = column;
			this._value = value;
			this._isNull = isNull;
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x000DB4C2 File Offset: 0x000D96C2
		internal object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x000DB4CA File Offset: 0x000D96CA
		internal bool? IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x000DB4D2 File Offset: 0x000D96D2
		// (set) Token: 0x060040E2 RID: 16610 RVA: 0x000DB4DA File Offset: 0x000D96DA
		public override EdmProperty Property
		{
			get
			{
				return base.Property;
			}
			internal set
			{
				base.Property = value;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x000DB4E3 File Offset: 0x000D96E3
		// (set) Token: 0x060040E4 RID: 16612 RVA: 0x000DB4EB File Offset: 0x000D96EB
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

		// Token: 0x04001684 RID: 5764
		private EdmProperty _column;

		// Token: 0x04001685 RID: 5765
		private readonly object _value;

		// Token: 0x04001686 RID: 5766
		private readonly bool? _isNull;
	}
}
