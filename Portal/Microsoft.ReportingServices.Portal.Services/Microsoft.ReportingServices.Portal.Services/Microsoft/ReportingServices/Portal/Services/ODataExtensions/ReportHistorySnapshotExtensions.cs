using System;
using Microsoft.ReportingServices.Library.Soap;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000042 RID: 66
	internal static class ReportHistorySnapshotExtensions
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00010845 File Offset: 0x0000EA45
		public static global::Model.ReportHistorySnapshot ToWebApi(this Microsoft.ReportingServices.Library.Soap.ReportHistorySnapshot reportHistorySnapshot)
		{
			if (reportHistorySnapshot == null)
			{
				throw new ArgumentNullException("reportHistorySnapshot");
			}
			return new global::Model.ReportHistorySnapshot
			{
				HistoryId = reportHistorySnapshot.HistoryID,
				CreationDate = reportHistorySnapshot.CreationDate,
				Size = reportHistorySnapshot.Size
			};
		}
	}
}
