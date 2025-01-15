using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B6 RID: 694
	internal sealed class CreateDataSetJsonCacheEntry : CancelableLibraryStep
	{
		// Token: 0x06001925 RID: 6437 RVA: 0x00064FA1 File Offset: 0x000631A1
		internal CreateDataSetJsonCacheEntry(DataSetCatalogItem item, string itemPath, JobType jobType)
			: base(UrlFriendlyUIDGenerator.Create(), item.ItemContext.OriginalItemPath, JobActionEnum.RefreshCache, jobType, item.Service.UserContext)
		{
			this.m_item = item;
			this.m_itemPath = itemPath;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00064FD4 File Offset: 0x000631D4
		protected override void Execute()
		{
			ParameterInfoCollection processedDataSetParameters = CreateDataSetJsonCacheEntry.GetProcessedDataSetParameters(this.m_itemPath, this.m_item.ItemContext.RSRequestParameters.ReportParameters);
			ContentCacheManagerFactory.CreateJsonContentCache(this.m_item.ItemID, this.m_itemPath, processedDataSetParameters, this.m_item.ItemContext.RSRequestParameters, this.m_item.Service).CreateOrUpdateCache();
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0006503C File Offset: 0x0006323C
		private static ParameterInfoCollection GetProcessedDataSetParameters(string itemPath, NameValueCollection requestParameters)
		{
			RSService rsservice = new RSService(false);
			ParameterInfoCollection parameters;
			using (rsservice.SetStreamFactory(new MemoryThenFileStreamFactory()))
			{
				GetDataSetParametersAction getDataSetParametersAction = rsservice.GetDataSetParametersAction;
				getDataSetParametersAction.ActionParameters.ItemPath = itemPath;
				getDataSetParametersAction.ActionParameters.ForRendering = true;
				if (requestParameters.Count > 0)
				{
					getDataSetParametersAction.ActionParameters.ParameterValidationValues = requestParameters;
				}
				getDataSetParametersAction.Execute();
				parameters = getDataSetParametersAction.ActionParameters.Parameters;
			}
			return parameters;
		}

		// Token: 0x04000917 RID: 2327
		private readonly DataSetCatalogItem m_item;

		// Token: 0x04000918 RID: 2328
		private readonly string m_itemPath;
	}
}
