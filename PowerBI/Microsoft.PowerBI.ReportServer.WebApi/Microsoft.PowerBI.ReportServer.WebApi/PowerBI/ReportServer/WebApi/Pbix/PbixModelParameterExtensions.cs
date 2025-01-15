using System;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x02000012 RID: 18
	public static class PbixModelParameterExtensions
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002F05 File Offset: 0x00001105
		public static DataModelParameter ToDataModelParameter(this PbixModelParameter pbixModelParameter)
		{
			return new DataModelParameter
			{
				Name = pbixModelParameter.Name,
				Value = pbixModelParameter.Value
			};
		}
	}
}
