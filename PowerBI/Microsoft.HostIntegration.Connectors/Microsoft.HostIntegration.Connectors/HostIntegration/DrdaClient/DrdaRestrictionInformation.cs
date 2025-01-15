using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A22 RID: 2594
	internal class DrdaRestrictionInformation
	{
		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06005169 RID: 20841 RVA: 0x001486AC File Offset: 0x001468AC
		public static string SchemaName
		{
			get
			{
				return "Restrictions";
			}
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x001486B4 File Offset: 0x001468B4
		public static DataTable Execute(DrdaSchemaInformation[] dynamicSchemas)
		{
			DataTable dataTable = new DataTable(DrdaRestrictionInformation.SchemaName);
			dataTable.Columns.Add(DbMetaDataColumnNames.CollectionName, typeof(string));
			dataTable.Columns.Add("RestrictionName", typeof(string));
			dataTable.Columns.Add("RestrictionDefault", typeof(string));
			dataTable.Columns.Add("RestrictionNumber", typeof(int));
			for (int i = 0; i < dynamicSchemas.Length; i++)
			{
				for (int j = 0; j < dynamicSchemas[i].Restrictions.Length; j++)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow[DbMetaDataColumnNames.CollectionName] = dynamicSchemas[i].SchemaName;
					dataRow["RestrictionName"] = dynamicSchemas[i].Restrictions[j].Name;
					dataRow["RestrictionDefault"] = DBNull.Value;
					dataRow["RestrictionNumber"] = j + 1;
					dataTable.Rows.Add(dataRow);
					dataRow.AcceptChanges();
				}
			}
			return dataTable;
		}
	}
}
