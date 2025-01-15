using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000437 RID: 1079
	internal abstract class BufferedDataRecord
	{
		// Token: 0x06003495 RID: 13461 RVA: 0x000A92EC File Offset: 0x000A74EC
		protected virtual void ReadMetadata(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			string[] array = new string[fieldCount];
			Type[] array2 = new Type[fieldCount];
			string[] columnNames = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = reader.GetDataTypeName(i);
				array2[i] = reader.GetFieldType(i);
				columnNames[i] = reader.GetName(i);
			}
			this._dataTypeNames = array;
			this._fieldTypes = array2;
			this._columnNames = columnNames;
			this._fieldNameLookup = new Lazy<FieldNameLookup>(() => new FieldNameLookup(new ReadOnlyCollection<string>(columnNames)), false);
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000A938B File Offset: 0x000A758B
		// (set) Token: 0x06003497 RID: 13463 RVA: 0x000A9393 File Offset: 0x000A7593
		public bool IsDataReady { get; protected set; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000A939C File Offset: 0x000A759C
		public bool HasRows
		{
			get
			{
				return this._rowCount > 0;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000A93A7 File Offset: 0x000A75A7
		public int FieldCount
		{
			get
			{
				return this._dataTypeNames.Length;
			}
		}

		// Token: 0x0600349A RID: 13466
		public abstract bool GetBoolean(int ordinal);

		// Token: 0x0600349B RID: 13467
		public abstract byte GetByte(int ordinal);

		// Token: 0x0600349C RID: 13468
		public abstract char GetChar(int ordinal);

		// Token: 0x0600349D RID: 13469
		public abstract DateTime GetDateTime(int ordinal);

		// Token: 0x0600349E RID: 13470
		public abstract decimal GetDecimal(int ordinal);

		// Token: 0x0600349F RID: 13471
		public abstract double GetDouble(int ordinal);

		// Token: 0x060034A0 RID: 13472
		public abstract float GetFloat(int ordinal);

		// Token: 0x060034A1 RID: 13473
		public abstract Guid GetGuid(int ordinal);

		// Token: 0x060034A2 RID: 13474
		public abstract short GetInt16(int ordinal);

		// Token: 0x060034A3 RID: 13475
		public abstract int GetInt32(int ordinal);

		// Token: 0x060034A4 RID: 13476
		public abstract long GetInt64(int ordinal);

		// Token: 0x060034A5 RID: 13477
		public abstract string GetString(int ordinal);

		// Token: 0x060034A6 RID: 13478
		public abstract T GetFieldValue<T>(int ordinal);

		// Token: 0x060034A7 RID: 13479
		public abstract Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken);

		// Token: 0x060034A8 RID: 13480
		public abstract object GetValue(int ordinal);

		// Token: 0x060034A9 RID: 13481
		public abstract int GetValues(object[] values);

		// Token: 0x060034AA RID: 13482
		public abstract bool IsDBNull(int ordinal);

		// Token: 0x060034AB RID: 13483
		public abstract Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken);

		// Token: 0x060034AC RID: 13484 RVA: 0x000A93B1 File Offset: 0x000A75B1
		public string GetDataTypeName(int ordinal)
		{
			return this._dataTypeNames[ordinal];
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000A93BB File Offset: 0x000A75BB
		public Type GetFieldType(int ordinal)
		{
			return this._fieldTypes[ordinal];
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000A93C5 File Offset: 0x000A75C5
		public string GetName(int ordinal)
		{
			return this._columnNames[ordinal];
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000A93CF File Offset: 0x000A75CF
		public int GetOrdinal(string name)
		{
			return this._fieldNameLookup.Value.GetOrdinal(name);
		}

		// Token: 0x060034B0 RID: 13488
		public abstract bool Read();

		// Token: 0x060034B1 RID: 13489
		public abstract Task<bool> ReadAsync(CancellationToken cancellationToken);

		// Token: 0x040010F1 RID: 4337
		protected int _currentRowNumber = -1;

		// Token: 0x040010F2 RID: 4338
		protected int _rowCount;

		// Token: 0x040010F3 RID: 4339
		private string[] _dataTypeNames;

		// Token: 0x040010F4 RID: 4340
		private Type[] _fieldTypes;

		// Token: 0x040010F5 RID: 4341
		private string[] _columnNames;

		// Token: 0x040010F6 RID: 4342
		private Lazy<FieldNameLookup> _fieldNameLookup;
	}
}
