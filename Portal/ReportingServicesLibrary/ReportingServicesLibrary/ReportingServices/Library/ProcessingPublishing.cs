using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000AB RID: 171
	internal class ProcessingPublishing
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x000206FC File Offset: 0x0001E8FC
		internal static DataSourceInfo CheckDataSourcePublishingCallback(RSService Service, string catalogPath, out Guid catalogItemId, out byte[] secDesc, out ItemType type)
		{
			ExternalItemPath externalItemPath = Service.CatalogToExternal(catalogPath, true);
			catalogItemId = Guid.Empty;
			secDesc = null;
			type = ItemType.Unknown;
			try
			{
				Service.ServiceHelper.SyncToRSCatalog(externalItemPath);
			}
			catch (ItemNotFoundException)
			{
				return null;
			}
			catch (AccessDeniedException)
			{
				return null;
			}
			CatalogItemContext catalogItemContext = new CatalogItemContext(Service);
			if (!catalogItemContext.SetPath(externalItemPath))
			{
				return null;
			}
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!dataSourceContext.ItemPath.IsEditSession");
			RSService newService = Service.GetNewService();
			newService.WillDisconnectStorage();
			DataSourceInfo dataSourceInfo;
			try
			{
				if (!newService.Storage.ObjectExists(catalogItemContext.ItemPath, out type, out catalogItemId, out secDesc))
				{
					dataSourceInfo = null;
				}
				else if (type != ItemType.DataSource && type != ItemType.Model)
				{
					dataSourceInfo = null;
				}
				else
				{
					dataSourceInfo = newService.Storage.GetDataSources(catalogItemId).GetTheOnlyDataSource();
				}
			}
			catch (Exception ex)
			{
				newService.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				newService.DisconnectStorage();
			}
			return dataSourceInfo;
		}
	}
}
