using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A20 RID: 2592
	internal class DrdaSchemaMetaDataCollectionsInformation
	{
		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06005163 RID: 20835 RVA: 0x001481DA File Offset: 0x001463DA
		public static string SchemaName
		{
			get
			{
				return "MetaDataCollections";
			}
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x001481E4 File Offset: 0x001463E4
		public static DataTable Execute(DrdaSchemaInformation[] dynamicSchemas)
		{
			DataTable dataTable = new DataTable(DrdaSchemaMetaDataCollectionsInformation.SchemaName);
			dataTable.Columns.Add(DbMetaDataColumnNames.CollectionName, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.NumberOfRestrictions, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.NumberOfIdentifierParts, typeof(int));
			DataRow dataRow = dataTable.NewRow();
			dataRow[0] = DrdaSchemaMetaDataCollectionsInformation.SchemaName;
			dataRow[1] = 0;
			dataRow[2] = 0;
			dataTable.Rows.Add(dataRow);
			dataRow.AcceptChanges();
			dataRow = dataTable.NewRow();
			dataRow[0] = DrdaSchemaDataSourceInformation.SchemaName;
			dataRow[1] = 0;
			dataRow[2] = 0;
			dataTable.Rows.Add(dataRow);
			dataRow.AcceptChanges();
			dataRow = dataTable.NewRow();
			dataRow[0] = DrdaReservedWordsInformation.SchemaName;
			dataRow[1] = 0;
			dataRow[2] = 0;
			dataTable.Rows.Add(dataRow);
			dataRow.AcceptChanges();
			for (int i = 0; i < dynamicSchemas.Length; i++)
			{
				dataRow = dataTable.NewRow();
				dataRow[0] = dynamicSchemas[i].SchemaName;
				dataRow[1] = dynamicSchemas[i].Restrictions.Length;
				dataRow[2] = dynamicSchemas[i].IdentifierParts;
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			return dataTable;
		}
	}
}
