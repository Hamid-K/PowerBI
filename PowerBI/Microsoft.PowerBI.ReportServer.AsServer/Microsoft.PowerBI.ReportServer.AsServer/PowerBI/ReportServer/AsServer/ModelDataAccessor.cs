using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.PowerBI.ReportServer.AsServer.DataAccessObject;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000022 RID: 34
	public sealed class ModelDataAccessor : IModelDataAccessor
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000043A4 File Offset: 0x000025A4
		public ModelDataAccessor(AnalysisServicesSettings settings)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", settings.ServerAddress, settings.Port);
			this._connectionStringBuilder = new DbConnectionStringBuilder { { "DataSource", text } };
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000043F0 File Offset: 0x000025F0
		public void RefreshLastQueried(string databaseName)
		{
			this._connectionStringBuilder.Add("Catalog", databaseName);
			using (AdomdConnection adomdConnection = new AdomdConnection(this._connectionStringBuilder.ConnectionString))
			{
				adomdConnection.Open();
				AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection { { "CATALOG_NAME", databaseName } };
				adomdConnection.GetSchemaDataSet("MDSCHEMA_CUBES", adomdRestrictionCollection);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004464 File Offset: 0x00002664
		public IList<ModelInfoEntity> GetModelInfo()
		{
			string text = "SELECT Database_Id,Date_Queried,[Date_Modified] FROM $System.DBSchema_Catalogs";
			List<ModelInfoEntity> list = new List<ModelInfoEntity>();
			using (AdomdConnection adomdConnection = new AdomdConnection(this._connectionStringBuilder.ConnectionString))
			{
				adomdConnection.Open();
				using (AdomdCommand adomdCommand = adomdConnection.CreateCommand())
				{
					adomdCommand.CommandType = CommandType.Text;
					adomdCommand.CommandText = text;
					AdomdDataReader adomdDataReader = adomdCommand.ExecuteReader();
					while (adomdDataReader.Read())
					{
						list.Add(new ModelInfoEntity
						{
							LastModified = (DateTime?)adomdDataReader["Date_Modified"],
							LastQueried = (DateTime?)adomdDataReader["Date_Queried"],
							ModelId = long.Parse(adomdDataReader["Database_Id"].ToString())
						});
					}
				}
			}
			return list;
		}

		// Token: 0x04000062 RID: 98
		private readonly DbConnectionStringBuilder _connectionStringBuilder;
	}
}
