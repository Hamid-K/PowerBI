using System;
using System.Threading.Tasks;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200000E RID: 14
	public interface ICatalogEventAccessor
	{
		// Token: 0x060000D0 RID: 208
		Task<EventEntity> TakeEventFromQueue(EventEntity.ReportScheduleActions eventType);
	}
}
