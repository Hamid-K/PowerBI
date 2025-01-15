using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class CreateDataReaderRowset : RowsetDefinition
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002E32 File Offset: 0x00001032
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002E3A File Offset: 0x0000103A
		public CreateDataReaderDelegate CreateDataReaderDelegate { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002E43 File Offset: 0x00001043
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002E4B File Offset: 0x0000104B
		public string RidColumnType { get; set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002E54 File Offset: 0x00001054
		public override IDataReader CreateDataReader(ConnectionManager connectionManager)
		{
			return this.CreateDataReaderDelegate();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E64 File Offset: 0x00001064
		public override DataTable GetSchemaTable(ConnectionManager connectionManager)
		{
			DataTable schemaTable;
			using (IDataReader dataReader = this.CreateDataReaderDelegate())
			{
				schemaTable = dataReader.GetSchemaTable();
			}
			return schemaTable;
		}
	}
}
