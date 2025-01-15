using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200041F RID: 1055
	internal sealed class ObjectStateEntryDbDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x060032C9 RID: 13001 RVA: 0x000A2160 File Offset: 0x000A0360
		internal ObjectStateEntryDbDataRecord(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
		{
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state == EntityState.Deleted || state == EntityState.Modified)
			{
				this._cacheEntry = cacheEntry;
				this._userObject = userObject;
				this._metadata = metadata;
			}
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000A219C File Offset: 0x000A039C
		internal ObjectStateEntryDbDataRecord(RelationshipEntry cacheEntry)
		{
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state == EntityState.Deleted || state == EntityState.Modified)
			{
				this._cacheEntry = cacheEntry;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x000A21CA File Offset: 0x000A03CA
		public override int FieldCount
		{
			get
			{
				return this._cacheEntry.GetFieldCount(this._metadata);
			}
		}

		// Token: 0x170009D2 RID: 2514
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x170009D3 RID: 2515
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000A21F5 File Offset: 0x000A03F5
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.GetValue(ordinal);
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000A2203 File Offset: 0x000A0403
		public override byte GetByte(int ordinal)
		{
			return (byte)this.GetValue(ordinal);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000A2214 File Offset: 0x000A0414
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			byte[] array = (byte[])this.GetValue(ordinal);
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("dataIndex", Strings.ADP_InvalidSourceBufferIndex(array.Length.ToString(CultureInfo.InvariantCulture), ((long)num).ToString(CultureInfo.InvariantCulture)));
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw new ArgumentOutOfRangeException("bufferIndex", Strings.ADP_InvalidDestinationBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000A22EF File Offset: 0x000A04EF
		public override char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000A2300 File Offset: 0x000A0500
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = (char[])this.GetValue(ordinal);
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("bufferIndex", Strings.ADP_InvalidSourceBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), ((long)bufferIndex).ToString(CultureInfo.InvariantCulture)));
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw new ArgumentOutOfRangeException("bufferIndex", Strings.ADP_InvalidDestinationBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000A23DC File Offset: 0x000A05DC
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000A23E3 File Offset: 0x000A05E3
		public override string GetDataTypeName(int ordinal)
		{
			return this.GetFieldType(ordinal).Name;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000A23F1 File Offset: 0x000A05F1
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000A23FF File Offset: 0x000A05FF
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000A240D File Offset: 0x000A060D
		public override double GetDouble(int ordinal)
		{
			return (double)this.GetValue(ordinal);
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000A241B File Offset: 0x000A061B
		public override Type GetFieldType(int ordinal)
		{
			return this._cacheEntry.GetFieldType(ordinal, this._metadata);
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000A242F File Offset: 0x000A062F
		public override float GetFloat(int ordinal)
		{
			return (float)this.GetValue(ordinal);
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000A243D File Offset: 0x000A063D
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.GetValue(ordinal);
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000A244B File Offset: 0x000A064B
		public override short GetInt16(int ordinal)
		{
			return (short)this.GetValue(ordinal);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000A2459 File Offset: 0x000A0659
		public override int GetInt32(int ordinal)
		{
			return (int)this.GetValue(ordinal);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000A2467 File Offset: 0x000A0667
		public override long GetInt64(int ordinal)
		{
			return (long)this.GetValue(ordinal);
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000A2475 File Offset: 0x000A0675
		public override string GetName(int ordinal)
		{
			return this._cacheEntry.GetCLayerName(ordinal, this._metadata);
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000A2489 File Offset: 0x000A0689
		public override int GetOrdinal(string name)
		{
			int ordinalforCLayerName = this._cacheEntry.GetOrdinalforCLayerName(name, this._metadata);
			if (ordinalforCLayerName == -1)
			{
				throw new ArgumentOutOfRangeException("name");
			}
			return ordinalforCLayerName;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000A24AC File Offset: 0x000A06AC
		public override string GetString(int ordinal)
		{
			return (string)this.GetValue(ordinal);
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000A24BA File Offset: 0x000A06BA
		public override object GetValue(int ordinal)
		{
			if (this._cacheEntry.IsRelationship)
			{
				return (this._cacheEntry as RelationshipEntry).GetOriginalRelationValue(ordinal);
			}
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalReadonly);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000A24FC File Offset: 0x000A06FC
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000A253B File Offset: 0x000A073B
		public override bool IsDBNull(int ordinal)
		{
			return this.GetValue(ordinal) == DBNull.Value;
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000A254B File Offset: 0x000A074B
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				if (this._recordInfo == null)
				{
					this._recordInfo = this._cacheEntry.GetDataRecordInfo(this._metadata, this._userObject);
				}
				return this._recordInfo;
			}
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000A2578 File Offset: 0x000A0778
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this.GetValue(ordinal);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000A2586 File Offset: 0x000A0786
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x04001089 RID: 4233
		private readonly StateManagerTypeMetadata _metadata;

		// Token: 0x0400108A RID: 4234
		private readonly ObjectStateEntry _cacheEntry;

		// Token: 0x0400108B RID: 4235
		private readonly object _userObject;

		// Token: 0x0400108C RID: 4236
		private DataRecordInfo _recordInfo;
	}
}
