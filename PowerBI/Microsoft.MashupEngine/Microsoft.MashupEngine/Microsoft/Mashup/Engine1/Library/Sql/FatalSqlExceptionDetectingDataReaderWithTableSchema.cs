using System;
using System.Data.Common;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003A6 RID: 934
	internal class FatalSqlExceptionDetectingDataReaderWithTableSchema : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x06002076 RID: 8310 RVA: 0x00055680 File Offset: 0x00053880
		public FatalSqlExceptionDetectingDataReaderWithTableSchema(DbDataReaderWithTableSchema reader, Func<DbException, bool> isFatalSqlException)
			: base(reader)
		{
			this.isFatalSqlException = isFatalSqlException;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x00055690 File Offset: 0x00053890
		public override object GetValue(int ordinal)
		{
			object value;
			try
			{
				value = base.GetValue(ordinal);
			}
			catch (Exception ex)
			{
				if (this.fatalSqlException != null)
				{
					throw this.fatalSqlException;
				}
				DbException ex2 = ex as DbException;
				if (ex2 != null && this.isFatalSqlException(ex2))
				{
					this.fatalSqlException = ex2;
				}
				throw;
			}
			return value;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000556E8 File Offset: 0x000538E8
		public override bool Read()
		{
			bool flag;
			try
			{
				flag = base.Read();
			}
			catch (Exception ex)
			{
				if (this.fatalSqlException != null)
				{
					throw this.fatalSqlException;
				}
				DbException ex2 = ex as DbException;
				if (ex2 != null && this.isFatalSqlException(ex2))
				{
					this.fatalSqlException = ex2;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x04000C6A RID: 3178
		private readonly Func<DbException, bool> isFatalSqlException;

		// Token: 0x04000C6B RID: 3179
		private DbException fatalSqlException;
	}
}
