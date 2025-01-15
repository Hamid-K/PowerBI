using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000127 RID: 295
	internal sealed class DefinitionDbInterface : DBInterface
	{
		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002CFD8 File Offset: 0x0002B1D8
		public bool LoadForDefinitionCheck(CatalogItemPath itemPath, bool acquireUpdateLocks, out ReportSnapshot compiledDefinition, out byte[] secDesc)
		{
			compiledDefinition = null;
			secDesc = null;
			bool flag;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("LoadForDefinitionCheck"))
			{
				cancelableSqlCommand.Parameters.Add("@Path", SqlDbType.NVarChar, 425).Value = itemPath.Value;
				cancelableSqlCommand.Parameters.Add("@AcquireUpdateLocks", SqlDbType.Bit).Value = acquireUpdateLocks;
				cancelableSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = WebConfigUtil.AuthenticationType;
				using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						flag = false;
					}
					else
					{
						compiledDefinition = this.LoadSnapshotFromDataRecord(dataReader, 0);
						if (!dataReader.IsDBNull(2))
						{
							secDesc = DataReaderHelper.ReadAllBytes(dataReader, 2);
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002D0BC File Offset: 0x0002B2BC
		public bool LoadForRepublishing(CatalogItemPath itemPath, out byte[] contents, out ReportSnapshot compiledDefinition)
		{
			contents = null;
			compiledDefinition = null;
			bool flag;
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("LoadForRepublishing"))
			{
				cancelableSqlCommand.Parameters.Add("@Path", SqlDbType.NVarChar, 425).Value = itemPath.Value;
				using (IDataReader dataReader = cancelableSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						flag = false;
					}
					else
					{
						contents = DataReaderHelper.ReadAllBytes(dataReader, 0);
						compiledDefinition = this.LoadSnapshotFromDataRecord(dataReader, 1);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002D158 File Offset: 0x0002B358
		public void RebindDataSource(Guid itemId, string dataSourceName, Guid newDataSourceId)
		{
			RSTrace.CatalogTrace.Assert(dataSourceName != null);
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("RebindDataSource"))
			{
				cancelableSqlCommand.Parameters.Add("@ItemId", SqlDbType.UniqueIdentifier).Value = itemId;
				cancelableSqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 260).Value = dataSourceName;
				cancelableSqlCommand.Parameters.Add("@NewDSID", SqlDbType.UniqueIdentifier).Value = newDataSourceId;
				cancelableSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002D1FC File Offset: 0x0002B3FC
		public void RebindDataSet(Guid itemId, string dataSetName, Guid newDataSetId)
		{
			RSTrace.CatalogTrace.Assert(dataSetName != null);
			using (CancelableSqlCommand cancelableSqlCommand = base.NewCancelableStandardSqlCommand("RebindDataSet"))
			{
				cancelableSqlCommand.Parameters.Add("@ItemId", SqlDbType.UniqueIdentifier).Value = itemId;
				cancelableSqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 260).Value = dataSetName;
				cancelableSqlCommand.Parameters.Add("@NewID", SqlDbType.UniqueIdentifier).Value = newDataSetId;
				cancelableSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002D2A0 File Offset: 0x0002B4A0
		private ReportSnapshot LoadSnapshotFromDataRecord(IDataRecord reader, int offset)
		{
			int num = offset + 1;
			ReportProcessingFlags reportProcessingFlags = ReportProcessingFlags.NotSet;
			Guid guid = reader.GetGuid(offset);
			if (!reader.IsDBNull(num))
			{
				reportProcessingFlags = (ReportProcessingFlags)reader.GetInt32(num);
			}
			return ReportSnapshot.Create(guid, true, false, reportProcessingFlags);
		}
	}
}
