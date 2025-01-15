using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200016E RID: 366
	public sealed class GetNonIndexableItemStatisticsResponse : ServiceResponse
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x000314B2 File Offset: 0x000304B2
		internal GetNonIndexableItemStatisticsResponse()
		{
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000314BA File Offset: 0x000304BA
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.NonIndexableStatistics = NonIndexableItemStatistic.LoadFromXml(reader);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000314CF File Offset: 0x000304CF
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			throw new NotImplementedException("GetNonIndexableItemStatistics doesn't support JSON.");
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x000314DB File Offset: 0x000304DB
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x000314E3 File Offset: 0x000304E3
		public List<NonIndexableItemStatistic> NonIndexableStatistics { get; internal set; }
	}
}
