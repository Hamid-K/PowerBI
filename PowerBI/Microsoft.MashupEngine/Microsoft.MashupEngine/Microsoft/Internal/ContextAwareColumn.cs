using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Internal
{
	// Token: 0x0200018B RID: 395
	internal class ContextAwareColumn<T, U> : IColumn where T : struct, IContext<U> where U : struct, IDisposable
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		public ContextAwareColumn(T context, IColumn column)
		{
			this.context = context;
			this.column = column;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public object GetObject(int row)
		{
			T t = this.context;
			U u = t.Enter();
			object obj;
			try
			{
				object @object = this.column.GetObject(row);
				obj = this.context.Marshal(@object);
			}
			finally
			{
				u.Dispose();
			}
			return obj;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000DF34 File Offset: 0x0000C134
		public bool IsNull(int row)
		{
			T t = this.context;
			U u = t.Enter();
			bool flag;
			try
			{
				flag = this.column.IsNull(row);
			}
			finally
			{
				u.Dispose();
			}
			return flag;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000DF84 File Offset: 0x0000C184
		public bool GetBoolean(int row)
		{
			T t = this.context;
			U u = t.Enter();
			bool boolean;
			try
			{
				boolean = this.column.GetBoolean(row);
			}
			finally
			{
				u.Dispose();
			}
			return boolean;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		public byte GetByte(int row)
		{
			T t = this.context;
			U u = t.Enter();
			byte @byte;
			try
			{
				@byte = this.column.GetByte(row);
			}
			finally
			{
				u.Dispose();
			}
			return @byte;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000E024 File Offset: 0x0000C224
		public short GetInt16(int row)
		{
			T t = this.context;
			U u = t.Enter();
			short @int;
			try
			{
				@int = this.column.GetInt16(row);
			}
			finally
			{
				u.Dispose();
			}
			return @int;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000E074 File Offset: 0x0000C274
		public int GetInt32(int row)
		{
			T t = this.context;
			U u = t.Enter();
			int @int;
			try
			{
				@int = this.column.GetInt32(row);
			}
			finally
			{
				u.Dispose();
			}
			return @int;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		public long GetInt64(int row)
		{
			T t = this.context;
			U u = t.Enter();
			long @int;
			try
			{
				@int = this.column.GetInt64(row);
			}
			finally
			{
				u.Dispose();
			}
			return @int;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000E114 File Offset: 0x0000C314
		public float GetFloat(int row)
		{
			T t = this.context;
			U u = t.Enter();
			float @float;
			try
			{
				@float = this.column.GetFloat(row);
			}
			finally
			{
				u.Dispose();
			}
			return @float;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000E164 File Offset: 0x0000C364
		public Guid GetGuid(int row)
		{
			T t = this.context;
			U u = t.Enter();
			Guid guid;
			try
			{
				guid = this.column.GetGuid(row);
			}
			finally
			{
				u.Dispose();
			}
			return guid;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		public double GetDouble(int row)
		{
			T t = this.context;
			U u = t.Enter();
			double @double;
			try
			{
				@double = this.column.GetDouble(row);
			}
			finally
			{
				u.Dispose();
			}
			return @double;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000E204 File Offset: 0x0000C404
		public decimal GetDecimal(int row)
		{
			T t = this.context;
			U u = t.Enter();
			decimal @decimal;
			try
			{
				@decimal = this.column.GetDecimal(row);
			}
			finally
			{
				u.Dispose();
			}
			return @decimal;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000E254 File Offset: 0x0000C454
		public DateTime GetDateTime(int row)
		{
			T t = this.context;
			U u = t.Enter();
			DateTime dateTime;
			try
			{
				dateTime = this.column.GetDateTime(row);
			}
			finally
			{
				u.Dispose();
			}
			return dateTime;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		public string GetString(int row)
		{
			T t = this.context;
			U u = t.Enter();
			string @string;
			try
			{
				@string = this.column.GetString(row);
			}
			finally
			{
				u.Dispose();
			}
			return @string;
		}

		// Token: 0x04000498 RID: 1176
		protected readonly T context;

		// Token: 0x04000499 RID: 1177
		protected readonly IColumn column;
	}
}
