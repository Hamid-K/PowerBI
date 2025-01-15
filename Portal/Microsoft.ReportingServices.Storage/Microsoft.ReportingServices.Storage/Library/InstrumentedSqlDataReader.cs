using System;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000039 RID: 57
	internal sealed class InstrumentedSqlDataReader : HandledSqlDataReader
	{
		// Token: 0x06000184 RID: 388 RVA: 0x0000959F File Offset: 0x0000779F
		internal InstrumentedSqlDataReader(SqlDataReader reader)
			: base(reader)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000095A8 File Offset: 0x000077A8
		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			long bytes;
			using (new MeasureSql())
			{
				bytes = base.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
			}
			return bytes;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000095E8 File Offset: 0x000077E8
		public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			long chars;
			using (new MeasureSql())
			{
				chars = base.GetChars(i, fieldoffset, buffer, bufferoffset, length);
			}
			return chars;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009628 File Offset: 0x00007828
		protected override void InvokeNoReturn(Action<SqlDataReader> action)
		{
			using (new MeasureSql())
			{
				base.InvokeNoReturn(action);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00009660 File Offset: 0x00007860
		protected override T Invoke<T>(Func<SqlDataReader, T> action)
		{
			T t;
			using (new MeasureSql())
			{
				t = base.Invoke<T>(action);
			}
			return t;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009698 File Offset: 0x00007898
		protected override T Invoke<T, K>(Func<SqlDataReader, K, T> action, K i)
		{
			T t;
			using (new MeasureSql())
			{
				t = base.Invoke<T, K>(action, i);
			}
			return t;
		}
	}
}
