using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003A RID: 58
	internal class HandledSqlDataReader : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000096D4 File Offset: 0x000078D4
		internal HandledSqlDataReader(SqlDataReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000096E3 File Offset: 0x000078E3
		public void Close()
		{
			this.InvokeNoReturn(HandledSqlDataReader.CloseMethod);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000096F0 File Offset: 0x000078F0
		public int Depth
		{
			get
			{
				return this.Invoke<int>(HandledSqlDataReader.DepthMethod);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000096FD File Offset: 0x000078FD
		public DataTable GetSchemaTable()
		{
			return this.Invoke<DataTable>(HandledSqlDataReader.GetSchemaTableMethod);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000970A File Offset: 0x0000790A
		public bool IsClosed
		{
			get
			{
				return this.Invoke<bool>(HandledSqlDataReader.IsClosedMethod);
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009717 File Offset: 0x00007917
		public bool NextResult()
		{
			return this.Invoke<bool>(HandledSqlDataReader.NextResultMethod);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009724 File Offset: 0x00007924
		public bool Read()
		{
			return this.Invoke<bool>(HandledSqlDataReader.ReadMethod);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009731 File Offset: 0x00007931
		public int RecordsAffected
		{
			get
			{
				return this.Invoke<int>(HandledSqlDataReader.RecordsAffectedMethod);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000973E File Offset: 0x0000793E
		public void Dispose()
		{
			this.InvokeNoReturn(HandledSqlDataReader.DisposeMethod);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000974B File Offset: 0x0000794B
		public int FieldCount
		{
			get
			{
				return this.Invoke<int>(HandledSqlDataReader.FieldCountMethod);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009758 File Offset: 0x00007958
		public bool GetBoolean(int i)
		{
			return this.Invoke<bool, int>(HandledSqlDataReader.GetBooleanMethod, i);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009766 File Offset: 0x00007966
		public byte GetByte(int i)
		{
			return this.Invoke<byte, int>(HandledSqlDataReader.GetByteMethod, i);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009774 File Offset: 0x00007974
		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			long bytes;
			try
			{
				bytes = this.reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
			}
			catch (Exception ex)
			{
				Storage.WrapAndThrowKnownExceptionTypes(ex);
				throw;
			}
			return bytes;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000097B0 File Offset: 0x000079B0
		public char GetChar(int i)
		{
			return this.Invoke<char, int>(HandledSqlDataReader.GetCharMethod, i);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000097C0 File Offset: 0x000079C0
		public virtual long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			long chars;
			try
			{
				chars = this.reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
			}
			catch (Exception ex)
			{
				Storage.WrapAndThrowKnownExceptionTypes(ex);
				throw;
			}
			return chars;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000097FC File Offset: 0x000079FC
		public IDataReader GetData(int i)
		{
			throw new InternalCatalogException("GetData() not implemented.");
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009808 File Offset: 0x00007A08
		public string GetDataTypeName(int i)
		{
			return this.Invoke<string, int>(HandledSqlDataReader.GetDataTypeNameMethod, i);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009816 File Offset: 0x00007A16
		public DateTime GetDateTime(int i)
		{
			return this.Invoke<DateTime, int>(HandledSqlDataReader.GetDateTimeMethod, i);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009824 File Offset: 0x00007A24
		public decimal GetDecimal(int i)
		{
			return this.Invoke<decimal, int>(HandledSqlDataReader.GetDecimalMethod, i);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009832 File Offset: 0x00007A32
		public double GetDouble(int i)
		{
			return this.Invoke<double, int>(HandledSqlDataReader.GetDoubleMethod, i);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009840 File Offset: 0x00007A40
		public Type GetFieldType(int i)
		{
			return this.Invoke<Type, int>(HandledSqlDataReader.GetFieldTypeMethod, i);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000984E File Offset: 0x00007A4E
		public float GetFloat(int i)
		{
			return this.Invoke<float, int>(HandledSqlDataReader.GetFloatMethod, i);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000985C File Offset: 0x00007A5C
		public Guid GetGuid(int i)
		{
			return this.Invoke<Guid, int>(HandledSqlDataReader.GetGuidMethod, i);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000986A File Offset: 0x00007A6A
		public short GetInt16(int i)
		{
			return this.Invoke<short, int>(HandledSqlDataReader.GetInt16Method, i);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009878 File Offset: 0x00007A78
		public int GetInt32(int i)
		{
			return this.Invoke<int, int>(HandledSqlDataReader.GetInt32Method, i);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009886 File Offset: 0x00007A86
		public long GetInt64(int i)
		{
			return this.Invoke<long, int>(HandledSqlDataReader.GetInt64Method, i);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009894 File Offset: 0x00007A94
		public string GetName(int i)
		{
			return this.Invoke<string, int>(HandledSqlDataReader.GetNameMethod, i);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000098A2 File Offset: 0x00007AA2
		public int GetOrdinal(string name)
		{
			return this.Invoke<int, string>(HandledSqlDataReader.GetOrdinalMethod, name);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000098B0 File Offset: 0x00007AB0
		public string GetString(int i)
		{
			return this.Invoke<string, int>(HandledSqlDataReader.GetStringMethod, i);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000098BE File Offset: 0x00007ABE
		public object GetValue(int i)
		{
			return this.Invoke<object, int>(HandledSqlDataReader.GetValueMethod, i);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000098CC File Offset: 0x00007ACC
		public int GetValues(object[] values)
		{
			return this.Invoke<int, object[]>(HandledSqlDataReader.GetValuesMethod, values);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000098DA File Offset: 0x00007ADA
		public bool IsDBNull(int i)
		{
			return this.Invoke<bool, int>(HandledSqlDataReader.IsDBNullMethod, i);
		}

		// Token: 0x1700005A RID: 90
		public object this[string name]
		{
			get
			{
				return this.Invoke<object, string>(HandledSqlDataReader.This2Method, name);
			}
		}

		// Token: 0x1700005B RID: 91
		public object this[int i]
		{
			get
			{
				return this.Invoke<object, int>(HandledSqlDataReader.ThisMethod, i);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009904 File Offset: 0x00007B04
		protected virtual void InvokeNoReturn(Action<SqlDataReader> action)
		{
			try
			{
				action(this.reader);
			}
			catch (Exception ex)
			{
				Storage.WrapAndThrowKnownExceptionTypes(ex);
				throw;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00009938 File Offset: 0x00007B38
		protected virtual T Invoke<T>(Func<SqlDataReader, T> action)
		{
			T t;
			try
			{
				t = action(this.reader);
			}
			catch (Exception ex)
			{
				Storage.WrapAndThrowKnownExceptionTypes(ex);
				throw;
			}
			return t;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000996C File Offset: 0x00007B6C
		protected virtual T Invoke<T, K>(Func<SqlDataReader, K, T> action, K i)
		{
			T t;
			try
			{
				t = action(this.reader, i);
			}
			catch (Exception ex)
			{
				Storage.WrapAndThrowKnownExceptionTypes(ex);
				throw;
			}
			return t;
		}

		// Token: 0x04000164 RID: 356
		private static readonly Action<SqlDataReader> CloseMethod = delegate(SqlDataReader r)
		{
			r.Close();
		};

		// Token: 0x04000165 RID: 357
		private static readonly Action<SqlDataReader> DisposeMethod = delegate(SqlDataReader r)
		{
			r.Dispose();
		};

		// Token: 0x04000166 RID: 358
		private static readonly Func<SqlDataReader, int> RecordsAffectedMethod = (SqlDataReader r) => r.RecordsAffected;

		// Token: 0x04000167 RID: 359
		private static readonly Func<SqlDataReader, bool> ReadMethod = (SqlDataReader r) => r.Read();

		// Token: 0x04000168 RID: 360
		private static readonly Func<SqlDataReader, bool> NextResultMethod = (SqlDataReader r) => r.NextResult();

		// Token: 0x04000169 RID: 361
		private static readonly Func<SqlDataReader, bool> IsClosedMethod = (SqlDataReader r) => r.IsClosed;

		// Token: 0x0400016A RID: 362
		private static readonly Func<SqlDataReader, DataTable> GetSchemaTableMethod = (SqlDataReader r) => r.GetSchemaTable();

		// Token: 0x0400016B RID: 363
		private static readonly Func<SqlDataReader, int> DepthMethod = (SqlDataReader r) => r.Depth;

		// Token: 0x0400016C RID: 364
		private static readonly Func<SqlDataReader, int> FieldCountMethod = (SqlDataReader r) => r.FieldCount;

		// Token: 0x0400016D RID: 365
		private static readonly Func<SqlDataReader, int, bool> GetBooleanMethod = (SqlDataReader r, int i) => r.GetBoolean(i);

		// Token: 0x0400016E RID: 366
		private static readonly Func<SqlDataReader, int, byte> GetByteMethod = (SqlDataReader r, int i) => r.GetByte(i);

		// Token: 0x0400016F RID: 367
		private static readonly Func<SqlDataReader, int, char> GetCharMethod = (SqlDataReader r, int i) => r.GetChar(i);

		// Token: 0x04000170 RID: 368
		private static readonly Func<SqlDataReader, int, string> GetDataTypeNameMethod = (SqlDataReader r, int i) => r.GetDataTypeName(i);

		// Token: 0x04000171 RID: 369
		private static readonly Func<SqlDataReader, int, DateTime> GetDateTimeMethod = (SqlDataReader r, int i) => r.GetDateTime(i);

		// Token: 0x04000172 RID: 370
		private static readonly Func<SqlDataReader, int, decimal> GetDecimalMethod = (SqlDataReader r, int i) => r.GetDecimal(i);

		// Token: 0x04000173 RID: 371
		private static readonly Func<SqlDataReader, int, double> GetDoubleMethod = (SqlDataReader r, int i) => r.GetDouble(i);

		// Token: 0x04000174 RID: 372
		private static readonly Func<SqlDataReader, int, Type> GetFieldTypeMethod = (SqlDataReader r, int i) => r.GetFieldType(i);

		// Token: 0x04000175 RID: 373
		private static readonly Func<SqlDataReader, int, float> GetFloatMethod = (SqlDataReader r, int i) => r.GetFloat(i);

		// Token: 0x04000176 RID: 374
		private static readonly Func<SqlDataReader, int, Guid> GetGuidMethod = (SqlDataReader r, int i) => r.GetGuid(i);

		// Token: 0x04000177 RID: 375
		private static readonly Func<SqlDataReader, int, short> GetInt16Method = (SqlDataReader r, int i) => r.GetInt16(i);

		// Token: 0x04000178 RID: 376
		private static readonly Func<SqlDataReader, int, int> GetInt32Method = (SqlDataReader r, int i) => r.GetInt32(i);

		// Token: 0x04000179 RID: 377
		private static readonly Func<SqlDataReader, int, long> GetInt64Method = (SqlDataReader r, int i) => r.GetInt64(i);

		// Token: 0x0400017A RID: 378
		private static readonly Func<SqlDataReader, int, string> GetNameMethod = (SqlDataReader r, int i) => r.GetName(i);

		// Token: 0x0400017B RID: 379
		private static readonly Func<SqlDataReader, int, string> GetStringMethod = (SqlDataReader r, int i) => r.GetString(i);

		// Token: 0x0400017C RID: 380
		private static readonly Func<SqlDataReader, int, object> GetValueMethod = (SqlDataReader r, int i) => r.GetValue(i);

		// Token: 0x0400017D RID: 381
		private static readonly Func<SqlDataReader, int, bool> IsDBNullMethod = (SqlDataReader r, int i) => r.IsDBNull(i);

		// Token: 0x0400017E RID: 382
		private static readonly Func<SqlDataReader, int, object> ThisMethod = (SqlDataReader r, int i) => r[i];

		// Token: 0x0400017F RID: 383
		private static readonly Func<SqlDataReader, string, object> This2Method = (SqlDataReader r, string s) => r[s];

		// Token: 0x04000180 RID: 384
		private static readonly Func<SqlDataReader, string, int> GetOrdinalMethod = (SqlDataReader r, string n) => r.GetOrdinal(n);

		// Token: 0x04000181 RID: 385
		private static readonly Func<SqlDataReader, object[], int> GetValuesMethod = (SqlDataReader r, object[] vs) => r.GetValues(vs);

		// Token: 0x04000182 RID: 386
		protected readonly SqlDataReader reader;
	}
}
