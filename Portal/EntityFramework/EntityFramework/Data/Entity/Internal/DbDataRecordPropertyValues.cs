using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000101 RID: 257
	internal class DbDataRecordPropertyValues : InternalPropertyValues
	{
		// Token: 0x06001287 RID: 4743 RVA: 0x000308B8 File Offset: 0x0002EAB8
		internal DbDataRecordPropertyValues(InternalContext internalContext, Type type, DbUpdatableDataRecord dataRecord, bool isEntity)
			: base(internalContext, type, isEntity)
		{
			this._dataRecord = dataRecord;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000308CC File Offset: 0x0002EACC
		protected override IPropertyValuesItem GetItemImpl(string propertyName)
		{
			int ordinal = this._dataRecord.GetOrdinal(propertyName);
			object obj = this._dataRecord[ordinal];
			DbUpdatableDataRecord dbUpdatableDataRecord = obj as DbUpdatableDataRecord;
			if (dbUpdatableDataRecord != null)
			{
				obj = new DbDataRecordPropertyValues(base.InternalContext, this._dataRecord.GetFieldType(ordinal), dbUpdatableDataRecord, false);
			}
			else if (obj == DBNull.Value)
			{
				obj = null;
			}
			return new DbDataRecordPropertyValuesItem(this._dataRecord, ordinal, obj);
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00030930 File Offset: 0x0002EB30
		public override ISet<string> PropertyNames
		{
			get
			{
				if (this._names == null)
				{
					HashSet<string> hashSet = new HashSet<string>();
					for (int i = 0; i < this._dataRecord.FieldCount; i++)
					{
						hashSet.Add(this._dataRecord.GetName(i));
					}
					this._names = new ReadOnlySet<string>(hashSet);
				}
				return this._names;
			}
		}

		// Token: 0x04000926 RID: 2342
		private readonly DbUpdatableDataRecord _dataRecord;

		// Token: 0x04000927 RID: 2343
		private ISet<string> _names;
	}
}
