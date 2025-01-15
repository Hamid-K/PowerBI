using System;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000028 RID: 40
	public sealed class CatalogEventAccessor : ICatalogEventAccessor
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00006A98 File Offset: 0x00004C98
		public async Task<EventEntity> TakeEventFromQueue(EventEntity.ReportScheduleActions eventType)
		{
			var <>f__AnonymousType = new
			{
				EventType = eventType.ToString()
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<EventEntity>("TakeEventFromQueue", <>f__AnonymousType);
		}
	}
}
