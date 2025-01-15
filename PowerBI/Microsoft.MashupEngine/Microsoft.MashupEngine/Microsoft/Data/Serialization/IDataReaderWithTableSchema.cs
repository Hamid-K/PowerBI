using System;
using System.Data;

namespace Microsoft.Data.Serialization
{
	// Token: 0x0200014F RID: 335
	public interface IDataReaderWithTableSchema : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005E6 RID: 1510
		TableSchema Schema { get; }

		// Token: 0x060005E7 RID: 1511
		[Obsolete]
		DataTable GetSchemaTable();
	}
}
