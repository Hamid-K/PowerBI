using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000147 RID: 327
	public static class PowerBIDedicatedUtility
	{
		// Token: 0x06001174 RID: 4468 RVA: 0x00047020 File Offset: 0x00045220
		public static void ValidateDatabaseCapacity(DatabaseMoniker dbMoniker, DatabaseEntity databaseEntity, string capacityObjectId)
		{
			if (string.Compare(databaseEntity.PBIDedicatedCapacity, capacityObjectId, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new InconsistentPowerBIDedicatedCapacityException("Database {0}'s expected capacity is '{1}', but found as '{2}' in ANOperational store.".FormatWithInvariantCulture(new object[] { databaseEntity.DatabaseMoniker, capacityObjectId, databaseEntity.PBIDedicatedCapacity }));
			}
		}
	}
}
