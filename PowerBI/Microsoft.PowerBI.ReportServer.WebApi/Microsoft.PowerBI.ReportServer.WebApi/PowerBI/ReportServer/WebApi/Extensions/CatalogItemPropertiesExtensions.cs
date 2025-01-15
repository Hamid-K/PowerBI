using System;
using System.Collections.Generic;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.ReportingServices.CatalogAccess;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ReportServer.WebApi.Extensions
{
	// Token: 0x02000035 RID: 53
	public static class CatalogItemPropertiesExtensions
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00006D7A File Offset: 0x00004F7A
		public static List<PbixModelParameter> ConvertPbixModelParameters(this CatalogItemPropertiesEntity catalogItem)
		{
			if (!string.IsNullOrEmpty((catalogItem != null) ? catalogItem.Parameter : null))
			{
				return new List<PbixModelParameter>(JsonConvert.DeserializeObject<PbixModelParameter[]>(catalogItem.Parameter));
			}
			return new List<PbixModelParameter>();
		}
	}
}
