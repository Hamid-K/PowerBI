using System;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001DA RID: 474
	internal static class TxObjectBodyExtensions
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x000C41AC File Offset: 0x000C23AC
		internal static ITxObjectBody FindNextBody(this ITxObjectBody body)
		{
			ITxObjectBody body2 = body.Owner.Body;
			if (body2 == body)
			{
				return null;
			}
			ITxObjectBody txObjectBody = null;
			for (ITxObjectBody txObjectBody2 = body2; txObjectBody2 != null; txObjectBody2 = txObjectBody2.CreatedFrom)
			{
				if (txObjectBody2.CreatedFrom == body)
				{
					txObjectBody = txObjectBody2;
					break;
				}
			}
			if (txObjectBody == null)
			{
				return null;
			}
			return txObjectBody;
		}
	}
}
