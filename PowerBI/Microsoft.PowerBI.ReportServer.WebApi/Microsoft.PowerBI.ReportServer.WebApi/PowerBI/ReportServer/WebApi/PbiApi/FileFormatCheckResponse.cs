using System;
using Microsoft.PowerBI.ReportServer.WebApi.Pbix;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x02000026 RID: 38
	internal sealed class FileFormatCheckResponse
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000350C File Offset: 0x0000170C
		internal static JObject Create(StatusCode errorCode, PbixComponents pbixComponents = null)
		{
			object obj;
			if (errorCode != StatusCode.Ok)
			{
				if (errorCode == StatusCode.PowerBIReportNotSupportedVersion)
				{
					obj = SR.Error_UnsupportedPowerBIReportVersion;
				}
				else
				{
					obj = string.Empty;
				}
			}
			else
			{
				obj = ((pbixComponents == null) ? string.Empty : JsonConvert.SerializeObject(new PbixPortalWireContract
				{
					Properties = new PbixPortalWireContract.PbixProperties
					{
						IsMobileOptimized = pbixComponents.IsMobileOptimized,
						HasEmbeddedModels = pbixComponents.HasEmbeddedModels,
						ModelRefreshAllowed = pbixComponents.ModelRefreshAllowed,
						HasDirectQuery = pbixComponents.HasDirectQuery,
						DataModel = pbixComponents.DataModel,
						ModelVersion = pbixComponents.ModelVersion,
						PbixShredderVersion = new ServiceState().PbixShredderVersion
					},
					DataSources = pbixComponents.EmbeddedDataSources,
					DataModelRoles = pbixComponents.DataModelRoles,
					DataModelParameters = pbixComponents.DataModelParameters
				}));
			}
			return JObject.FromObject(new
			{
				StatusCode = errorCode,
				Body = obj
			});
		}
	}
}
