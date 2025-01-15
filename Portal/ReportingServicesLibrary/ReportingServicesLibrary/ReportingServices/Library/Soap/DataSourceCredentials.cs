using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000325 RID: 805
	public class DataSourceCredentials
	{
		// Token: 0x06001B59 RID: 7001 RVA: 0x0006F998 File Offset: 0x0006DB98
		internal static DatasourceCredentialsCollection ThisArrayToDatasourcesCredentials(DataSourceCredentials[] credentials)
		{
			DatasourceCredentialsCollection datasourceCredentialsCollection = new DatasourceCredentialsCollection();
			if (credentials == null)
			{
				return datasourceCredentialsCollection;
			}
			foreach (DataSourceCredentials dataSourceCredentials in credentials)
			{
				if (dataSourceCredentials == null)
				{
					throw new MissingElementException("DataSourceCredentials");
				}
				if (dataSourceCredentials.DataSourceName == null)
				{
					throw new MissingElementException("DataSourceName");
				}
				if (dataSourceCredentials.UserName == null)
				{
					throw new MissingElementException("UserName");
				}
				DatasourceCredentials datasourceCredentials = new DatasourceCredentials(dataSourceCredentials.DataSourceName, dataSourceCredentials.UserName, dataSourceCredentials.Password);
				datasourceCredentialsCollection.Add(datasourceCredentials);
			}
			return datasourceCredentialsCollection;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0006FA18 File Offset: 0x0006DC18
		internal static DataSourceCredentials[] DatasourcesCredentialsToThisArray(DatasourceCredentialsCollection collection)
		{
			if (collection == null)
			{
				return null;
			}
			DataSourceCredentials[] array = new DataSourceCredentials[collection.Count];
			for (int i = 0; i < collection.Count; i++)
			{
				array[i] = new DataSourceCredentials
				{
					DataSourceName = collection[i].PromptID,
					UserName = collection[i].UserName,
					Password = collection[i].Password
				};
			}
			return array;
		}

		// Token: 0x04000AE9 RID: 2793
		public string DataSourceName;

		// Token: 0x04000AEA RID: 2794
		public string UserName;

		// Token: 0x04000AEB RID: 2795
		public string Password;
	}
}
