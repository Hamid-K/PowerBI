using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Data.Serialization
{
	// Token: 0x0200014C RID: 332
	public abstract class DbDataReaderWithTableSchema : DbDataReader, IDataReaderWithTableSchema, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005D4 RID: 1492
		public abstract TableSchema Schema { get; }

		// Token: 0x060005D5 RID: 1493 RVA: 0x00009332 File Offset: 0x00007532
		[Obsolete]
		public sealed override DataTable GetSchemaTable()
		{
			return this.Schema.ToDataTable();
		}
	}
}
