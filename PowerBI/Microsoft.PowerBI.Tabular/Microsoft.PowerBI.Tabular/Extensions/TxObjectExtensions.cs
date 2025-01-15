using System;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001DB RID: 475
	internal static class TxObjectExtensions
	{
		// Token: 0x06001C29 RID: 7209 RVA: 0x000C41F0 File Offset: 0x000C23F0
		internal static ITxObjectBody CloneBody(this ITxObject obj, TxSavepoint targetSavepoint = null)
		{
			ITxObjectBody txObjectBody = obj.CreateBody();
			txObjectBody.CopyFrom(obj.Body, ObjectChangeTracker.BodyCloneContext);
			txObjectBody.CreatedFrom = obj.Body;
			obj.Body = txObjectBody;
			if (targetSavepoint != null)
			{
				targetSavepoint.RegisterBody(txObjectBody);
			}
			return txObjectBody;
		}
	}
}
