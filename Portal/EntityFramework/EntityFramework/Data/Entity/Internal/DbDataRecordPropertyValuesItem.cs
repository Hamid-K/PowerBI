using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000102 RID: 258
	internal class DbDataRecordPropertyValuesItem : IPropertyValuesItem
	{
		// Token: 0x0600128A RID: 4746 RVA: 0x00030986 File Offset: 0x0002EB86
		public DbDataRecordPropertyValuesItem(DbUpdatableDataRecord dataRecord, int ordinal, object value)
		{
			this._dataRecord = dataRecord;
			this._ordinal = ordinal;
			this._value = value;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x000309A3 File Offset: 0x0002EBA3
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x000309AB File Offset: 0x0002EBAB
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._dataRecord.SetValue(this._ordinal, value);
				this._value = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x000309C6 File Offset: 0x0002EBC6
		public string Name
		{
			get
			{
				return this._dataRecord.GetName(this._ordinal);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x000309DC File Offset: 0x0002EBDC
		public bool IsComplex
		{
			get
			{
				return this._dataRecord.DataRecordInfo.FieldMetadata[this._ordinal].FieldType.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00030A1E File Offset: 0x0002EC1E
		public Type Type
		{
			get
			{
				return this._dataRecord.GetFieldType(this._ordinal);
			}
		}

		// Token: 0x04000928 RID: 2344
		private readonly DbUpdatableDataRecord _dataRecord;

		// Token: 0x04000929 RID: 2345
		private readonly int _ordinal;

		// Token: 0x0400092A RID: 2346
		private object _value;
	}
}
