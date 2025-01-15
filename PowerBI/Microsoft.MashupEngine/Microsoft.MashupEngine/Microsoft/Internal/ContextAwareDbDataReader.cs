using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Internal
{
	// Token: 0x02000186 RID: 390
	internal abstract class ContextAwareDbDataReader<T, U> : DbDataReaderWithTableSchema where T : struct, IContext<U> where U : struct, IDisposable
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		public ContextAwareDbDataReader(T context, DbDataReaderWithTableSchema reader)
		{
			this.context = context;
			this.reader = reader;
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0000CBD6 File Offset: 0x0000ADD6
		protected T Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0000CBDE File Offset: 0x0000ADDE
		private ContextAwareDbDataReader<T, U>.MarshalType[] MarshalTypes
		{
			get
			{
				if (this.marshalTypes == null)
				{
					this.marshalTypes = this.CreateMarshalMap();
				}
				return this.marshalTypes;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0000CBFA File Offset: 0x0000ADFA
		private ContextAwareDbDataReader<T, U>.MarshalType[] ProviderSpecificMarshalTypes
		{
			get
			{
				if (this.providerSpecificMarshalTypes == null)
				{
					this.providerSpecificMarshalTypes = this.CreateProviderSpecificMarshalMap();
				}
				return this.providerSpecificMarshalTypes;
			}
		}

		// Token: 0x17000248 RID: 584
		public override object this[string name]
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				object obj;
				try
				{
					int ordinal = this.GetOrdinal(name);
					obj = this[ordinal];
				}
				finally
				{
					u.Dispose();
				}
				return obj;
			}
		}

		// Token: 0x17000249 RID: 585
		public override object this[int i]
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				object obj;
				try
				{
					obj = this.Marshal(i, this.reader[i]);
				}
				finally
				{
					u.Dispose();
				}
				return obj;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		public override int Depth
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int depth;
				try
				{
					depth = this.reader.Depth;
				}
				finally
				{
					u.Dispose();
				}
				return depth;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0000CD14 File Offset: 0x0000AF14
		public override int FieldCount
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int fieldCount;
				try
				{
					fieldCount = this.reader.FieldCount;
				}
				finally
				{
					u.Dispose();
				}
				return fieldCount;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0000CD64 File Offset: 0x0000AF64
		public override bool HasRows
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				bool hasRows;
				try
				{
					hasRows = this.reader.HasRows;
				}
				finally
				{
					u.Dispose();
				}
				return hasRows;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		public override bool IsClosed
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				bool isClosed;
				try
				{
					isClosed = this.reader.IsClosed;
				}
				finally
				{
					u.Dispose();
				}
				return isClosed;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0000CE04 File Offset: 0x0000B004
		public override int RecordsAffected
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int recordsAffected;
				try
				{
					recordsAffected = this.reader.RecordsAffected;
				}
				finally
				{
					u.Dispose();
				}
				return recordsAffected;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0000CE54 File Offset: 0x0000B054
		public override int VisibleFieldCount
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int visibleFieldCount;
				try
				{
					visibleFieldCount = this.reader.VisibleFieldCount;
				}
				finally
				{
					u.Dispose();
				}
				return visibleFieldCount;
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
		public override void Close()
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.reader.Close();
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				T t = this.context;
				U u = t.Enter();
				try
				{
					this.reader.Dispose();
				}
				finally
				{
					u.Dispose();
				}
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000CF44 File Offset: 0x0000B144
		public override bool GetBoolean(int i)
		{
			T t = this.context;
			U u = t.Enter();
			bool boolean;
			try
			{
				boolean = this.reader.GetBoolean(i);
			}
			finally
			{
				u.Dispose();
			}
			return boolean;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000CF94 File Offset: 0x0000B194
		public override byte GetByte(int i)
		{
			T t = this.context;
			U u = t.Enter();
			byte @byte;
			try
			{
				@byte = this.reader.GetByte(i);
			}
			finally
			{
				u.Dispose();
			}
			return @byte;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			T t = this.context;
			U u = t.Enter();
			long bytes;
			try
			{
				bytes = this.reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
			}
			finally
			{
				u.Dispose();
			}
			return bytes;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000D03C File Offset: 0x0000B23C
		public override char GetChar(int i)
		{
			T t = this.context;
			U u = t.Enter();
			char @char;
			try
			{
				@char = this.reader.GetChar(i);
			}
			finally
			{
				u.Dispose();
			}
			return @char;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000D08C File Offset: 0x0000B28C
		public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			T t = this.context;
			U u = t.Enter();
			long chars;
			try
			{
				chars = this.reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
			}
			finally
			{
				u.Dispose();
			}
			return chars;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		public override string GetDataTypeName(int i)
		{
			T t = this.context;
			U u = t.Enter();
			string dataTypeName;
			try
			{
				dataTypeName = this.reader.GetDataTypeName(i);
			}
			finally
			{
				u.Dispose();
			}
			return dataTypeName;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000D134 File Offset: 0x0000B334
		public override DateTime GetDateTime(int i)
		{
			T t = this.context;
			U u = t.Enter();
			DateTime dateTime;
			try
			{
				dateTime = this.reader.GetDateTime(i);
			}
			finally
			{
				u.Dispose();
			}
			return dateTime;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000D184 File Offset: 0x0000B384
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			T t = this.context;
			U u = t.Enter();
			DbDataReader dbDataReader;
			try
			{
				dbDataReader = (DbDataReader)this.MarshalIntoContext(new DataReaderDbDataReader(((IDataRecord)this.reader).GetData(ordinal).WithTableSchema()));
			}
			finally
			{
				u.Dispose();
			}
			return dbDataReader;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		public override decimal GetDecimal(int i)
		{
			T t = this.context;
			U u = t.Enter();
			decimal @decimal;
			try
			{
				@decimal = this.reader.GetDecimal(i);
			}
			finally
			{
				u.Dispose();
			}
			return @decimal;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000D23C File Offset: 0x0000B43C
		public override double GetDouble(int i)
		{
			T t = this.context;
			U u = t.Enter();
			double @double;
			try
			{
				@double = this.reader.GetDouble(i);
			}
			finally
			{
				u.Dispose();
			}
			return @double;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000091AE File Offset: 0x000073AE
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000D28C File Offset: 0x0000B48C
		public override Type GetFieldType(int i)
		{
			T t = this.context;
			U u = t.Enter();
			Type fieldType;
			try
			{
				fieldType = this.reader.GetFieldType(i);
			}
			finally
			{
				u.Dispose();
			}
			return fieldType;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public override float GetFloat(int i)
		{
			T t = this.context;
			U u = t.Enter();
			float @float;
			try
			{
				@float = this.reader.GetFloat(i);
			}
			finally
			{
				u.Dispose();
			}
			return @float;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000D32C File Offset: 0x0000B52C
		public override Guid GetGuid(int i)
		{
			T t = this.context;
			U u = t.Enter();
			Guid guid;
			try
			{
				guid = this.reader.GetGuid(i);
			}
			finally
			{
				u.Dispose();
			}
			return guid;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000D37C File Offset: 0x0000B57C
		public override short GetInt16(int i)
		{
			T t = this.context;
			U u = t.Enter();
			short @int;
			try
			{
				@int = this.reader.GetInt16(i);
			}
			finally
			{
				u.Dispose();
			}
			return @int;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		public override int GetInt32(int i)
		{
			T t = this.context;
			U u = t.Enter();
			int @int;
			try
			{
				@int = this.reader.GetInt32(i);
			}
			finally
			{
				u.Dispose();
			}
			return @int;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000D41C File Offset: 0x0000B61C
		public override long GetInt64(int i)
		{
			T t = this.context;
			U u = t.Enter();
			long @int;
			try
			{
				@int = this.reader.GetInt64(i);
			}
			finally
			{
				u.Dispose();
			}
			return @int;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000D46C File Offset: 0x0000B66C
		public override string GetName(int i)
		{
			T t = this.context;
			U u = t.Enter();
			string name;
			try
			{
				name = this.reader.GetName(i);
			}
			finally
			{
				u.Dispose();
			}
			return name;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		public override int GetOrdinal(string name)
		{
			T t = this.context;
			U u = t.Enter();
			int ordinal;
			try
			{
				ordinal = this.reader.GetOrdinal(name);
			}
			finally
			{
				u.Dispose();
			}
			return ordinal;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000D50C File Offset: 0x0000B70C
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			T t = this.context;
			U u = t.Enter();
			Type providerSpecificFieldType;
			try
			{
				providerSpecificFieldType = this.reader.GetProviderSpecificFieldType(ordinal);
			}
			finally
			{
				u.Dispose();
			}
			return providerSpecificFieldType;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000D55C File Offset: 0x0000B75C
		public override object GetProviderSpecificValue(int ordinal)
		{
			T t = this.context;
			U u = t.Enter();
			object obj;
			try
			{
				obj = this.MarshalProviderSpecific(ordinal, this.reader.GetProviderSpecificValue(ordinal));
			}
			finally
			{
				u.Dispose();
			}
			return obj;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		public override int GetProviderSpecificValues(object[] values)
		{
			T t = this.context;
			U u = t.Enter();
			int num;
			try
			{
				int providerSpecificValues = this.reader.GetProviderSpecificValues(values);
				for (int i = 0; i < providerSpecificValues; i++)
				{
					values[i] = this.MarshalProviderSpecific(i, values[i]);
				}
				num = providerSpecificValues;
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000D624 File Offset: 0x0000B824
		public override string GetString(int i)
		{
			T t = this.context;
			U u = t.Enter();
			string @string;
			try
			{
				@string = this.reader.GetString(i);
			}
			finally
			{
				u.Dispose();
			}
			return @string;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000D674 File Offset: 0x0000B874
		public override object GetValue(int i)
		{
			T t = this.context;
			U u = t.Enter();
			object obj;
			try
			{
				obj = this.Marshal(i, this.reader.GetValue(i));
			}
			finally
			{
				u.Dispose();
			}
			return obj;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		public override int GetValues(object[] values)
		{
			T t = this.context;
			U u = t.Enter();
			int num;
			try
			{
				int values2 = this.reader.GetValues(values);
				for (int i = 0; i < values2; i++)
				{
					values[i] = this.Marshal(i, values[i]);
				}
				num = values2;
			}
			finally
			{
				u.Dispose();
			}
			return num;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000D73C File Offset: 0x0000B93C
		public override bool IsDBNull(int i)
		{
			T t = this.context;
			U u = t.Enter();
			bool flag;
			try
			{
				flag = this.reader.IsDBNull(i);
			}
			finally
			{
				u.Dispose();
			}
			return flag;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000D78C File Offset: 0x0000B98C
		public override bool NextResult()
		{
			T t = this.context;
			U u = t.Enter();
			bool flag;
			try
			{
				flag = this.reader.NextResult();
			}
			finally
			{
				u.Dispose();
			}
			return flag;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public override bool Read()
		{
			T t = this.context;
			U u = t.Enter();
			bool flag;
			try
			{
				flag = this.reader.Read();
			}
			finally
			{
				u.Dispose();
			}
			return flag;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public override TableSchema Schema
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				TableSchema schema;
				try
				{
					schema = this.reader.Schema;
				}
				finally
				{
					u.Dispose();
				}
				return schema;
			}
		}

		// Token: 0x06000783 RID: 1923
		protected abstract object MarshalIntoContext(object obj);

		// Token: 0x06000784 RID: 1924 RVA: 0x0000D87C File Offset: 0x0000BA7C
		private ContextAwareDbDataReader<T, U>.MarshalType[] CreateMarshalMap()
		{
			ContextAwareDbDataReader<T, U>.MarshalType[] array = new ContextAwareDbDataReader<T, U>.MarshalType[this.reader.FieldCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetMarshalType(this.reader.GetFieldType(i));
			}
			return array;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
		private ContextAwareDbDataReader<T, U>.MarshalType[] CreateProviderSpecificMarshalMap()
		{
			ContextAwareDbDataReader<T, U>.MarshalType[] array = new ContextAwareDbDataReader<T, U>.MarshalType[this.reader.FieldCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetMarshalType(this.reader.GetProviderSpecificFieldType(i));
			}
			return array;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000D902 File Offset: 0x0000BB02
		private ContextAwareDbDataReader<T, U>.MarshalType GetMarshalType(Type type)
		{
			if (type == typeof(object))
			{
				return ContextAwareDbDataReader<T, U>.MarshalType.Sometimes;
			}
			if (ContextFreeTypes.Types.Contains(type))
			{
				return ContextAwareDbDataReader<T, U>.MarshalType.Never;
			}
			return ContextAwareDbDataReader<T, U>.MarshalType.Always;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000D928 File Offset: 0x0000BB28
		private object Marshal(int position, object obj)
		{
			return this.Marshal(this.MarshalTypes[position], obj);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000D939 File Offset: 0x0000BB39
		private object MarshalProviderSpecific(int position, object obj)
		{
			return this.Marshal(this.ProviderSpecificMarshalTypes[position], obj);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000D94A File Offset: 0x0000BB4A
		private object Marshal(ContextAwareDbDataReader<T, U>.MarshalType marshalType, object obj)
		{
			if (marshalType == ContextAwareDbDataReader<T, U>.MarshalType.Always || (obj != null && marshalType == ContextAwareDbDataReader<T, U>.MarshalType.Sometimes && this.GetMarshalType(obj.GetType()) != ContextAwareDbDataReader<T, U>.MarshalType.Never))
			{
				return this.MarshalIntoContext(obj);
			}
			return obj;
		}

		// Token: 0x04000489 RID: 1161
		private readonly T context;

		// Token: 0x0400048A RID: 1162
		private readonly DbDataReaderWithTableSchema reader;

		// Token: 0x0400048B RID: 1163
		private ContextAwareDbDataReader<T, U>.MarshalType[] marshalTypes;

		// Token: 0x0400048C RID: 1164
		private ContextAwareDbDataReader<T, U>.MarshalType[] providerSpecificMarshalTypes;

		// Token: 0x02000187 RID: 391
		private enum MarshalType
		{
			// Token: 0x0400048E RID: 1166
			Never,
			// Token: 0x0400048F RID: 1167
			Sometimes,
			// Token: 0x04000490 RID: 1168
			Always
		}
	}
}
