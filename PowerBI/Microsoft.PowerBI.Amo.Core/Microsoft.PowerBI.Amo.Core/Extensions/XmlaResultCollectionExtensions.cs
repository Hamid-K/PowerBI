using System;
using System.Collections;
using System.Text;

namespace Microsoft.AnalysisServices.Extensions
{
	// Token: 0x0200014F RID: 335
	internal static class XmlaResultCollectionExtensions
	{
		// Token: 0x0600116A RID: 4458 RVA: 0x0003D128 File Offset: 0x0003B328
		internal static string GetAggregatedMessage(this XmlaResultCollection results)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < results.Count; i++)
			{
				foreach (object obj in ((IEnumerable)results[i].Messages))
				{
					XmlaMessage xmlaMessage = (XmlaMessage)obj;
					stringBuilder.AppendLine(xmlaMessage.Description);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
