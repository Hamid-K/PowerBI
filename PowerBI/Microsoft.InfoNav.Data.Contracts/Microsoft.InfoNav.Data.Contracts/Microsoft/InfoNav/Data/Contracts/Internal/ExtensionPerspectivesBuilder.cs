using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F7 RID: 503
	public sealed class ExtensionPerspectivesBuilder
	{
		// Token: 0x06000DB2 RID: 3506 RVA: 0x0001AB2C File Offset: 0x00018D2C
		public static PerspectivesInfo BuildPerspectivesList(DataTable dataTable, string connectionCubeName)
		{
			List<string> list = new List<string>();
			if (dataTable == null || dataTable.Rows == null || dataTable.Rows.Count == 0)
			{
				return new PerspectivesInfo
				{
					PerspectiveIds = list
				};
			}
			string text = string.Empty;
			DataRowCollection rows = dataTable.Rows;
			if (!string.IsNullOrEmpty(connectionCubeName))
			{
				foreach (object obj in rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text2 = dataRow["CUBE_NAME"].ToString();
					string text3 = dataRow["BASE_CUBE_NAME"].ToString();
					if (text2.Equals(connectionCubeName, StringComparison.Ordinal) || text3.Equals(connectionCubeName, StringComparison.Ordinal))
					{
						text = (string.IsNullOrEmpty(text3) ? text2 : text3);
						break;
					}
				}
			}
			foreach (object obj2 in rows)
			{
				DataRow dataRow2 = (DataRow)obj2;
				string text4 = dataRow2["CUBE_NAME"].ToString();
				string text5 = dataRow2["BASE_CUBE_NAME"].ToString();
				if (!string.IsNullOrEmpty(text))
				{
					if (text.Equals(text5, StringComparison.Ordinal) || text.Equals(text4, StringComparison.Ordinal))
					{
						list.Add(text4);
					}
				}
				else
				{
					list.Add(text4);
				}
			}
			return new PerspectivesInfo
			{
				PerspectiveIds = list
			};
		}
	}
}
