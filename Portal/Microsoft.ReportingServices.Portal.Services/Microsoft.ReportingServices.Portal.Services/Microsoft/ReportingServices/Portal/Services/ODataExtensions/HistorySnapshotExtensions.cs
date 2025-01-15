using System;
using Microsoft.ReportingServices.Library.Soap;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000043 RID: 67
	internal static class HistorySnapshotExtensions
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00010880 File Offset: 0x0000EA80
		public static global::Model.HistorySnapshot ToWebApi(this Microsoft.ReportingServices.Library.Soap.HistorySnapshot historySnapshot)
		{
			if (historySnapshot == null)
			{
				throw new ArgumentNullException("historySnapshot");
			}
			return new global::Model.HistorySnapshot
			{
				Id = historySnapshot.Id,
				HistoryId = historySnapshot.Snapshot.HistoryID,
				CreationDate = historySnapshot.Snapshot.CreationDate,
				Size = historySnapshot.Snapshot.Size
			};
		}
	}
}
