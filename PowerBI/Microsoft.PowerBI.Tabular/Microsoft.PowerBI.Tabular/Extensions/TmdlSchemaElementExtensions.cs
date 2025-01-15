using System;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D1 RID: 465
	internal static class TmdlSchemaElementExtensions
	{
		// Token: 0x06001BFC RID: 7164 RVA: 0x000C39B1 File Offset: 0x000C1BB1
		public static TSchemaElement WithReadOnlyStatus<TSchemaElement>(this TSchemaElement element) where TSchemaElement : TmdlSchemaElement
		{
			if (!element.IsReadOnly)
			{
				element.MakeReadOnly();
			}
			return element;
		}
	}
}
