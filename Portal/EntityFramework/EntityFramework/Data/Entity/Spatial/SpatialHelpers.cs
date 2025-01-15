using System;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000098 RID: 152
	internal static class SpatialHelpers
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00012D68 File Offset: 0x00010F68
		internal static object GetSpatialValue(MetadataWorkspace workspace, DbDataReader reader, TypeUsage columnType, int columnOrdinal)
		{
			DbSpatialDataReader dbSpatialDataReader = SpatialHelpers.CreateSpatialDataReader(workspace, reader);
			if (Helper.IsGeographicType((PrimitiveType)columnType.EdmType))
			{
				return dbSpatialDataReader.GetGeography(columnOrdinal);
			}
			return dbSpatialDataReader.GetGeometry(columnOrdinal);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00012DA0 File Offset: 0x00010FA0
		internal static async Task<object> GetSpatialValueAsync(MetadataWorkspace workspace, DbDataReader reader, TypeUsage columnType, int columnOrdinal, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			DbSpatialDataReader dbSpatialDataReader = SpatialHelpers.CreateSpatialDataReader(workspace, reader);
			object obj;
			if (Helper.IsGeographicType((PrimitiveType)columnType.EdmType))
			{
				obj = await dbSpatialDataReader.GetGeographyAsync(columnOrdinal, cancellationToken).WithCurrentCulture<DbGeography>();
			}
			else
			{
				obj = await dbSpatialDataReader.GetGeometryAsync(columnOrdinal, cancellationToken).WithCurrentCulture<DbGeometry>();
			}
			return obj;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00012E08 File Offset: 0x00011008
		internal static DbSpatialDataReader CreateSpatialDataReader(MetadataWorkspace workspace, DbDataReader reader)
		{
			StoreItemCollection storeItemCollection = (StoreItemCollection)workspace.GetItemCollection(DataSpace.SSpace);
			DbSpatialDataReader spatialDataReader = storeItemCollection.ProviderFactory.GetProviderServices().GetSpatialDataReader(reader, storeItemCollection.ProviderManifestToken);
			if (spatialDataReader == null)
			{
				throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnSpatialServices);
			}
			return spatialDataReader;
		}
	}
}
