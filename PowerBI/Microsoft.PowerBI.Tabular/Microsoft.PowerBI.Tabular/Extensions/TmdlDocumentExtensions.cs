using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001CB RID: 459
	internal static class TmdlDocumentExtensions
	{
		// Token: 0x06001BDD RID: 7133 RVA: 0x000C33B4 File Offset: 0x000C15B4
		public static TmdlDocument WithObjects(this TmdlDocument document, params TmdlObject[] objects)
		{
			if (objects != null && objects.Length != 0)
			{
				foreach (TmdlObject tmdlObject in objects)
				{
					document.Objects.Add(tmdlObject);
				}
			}
			return document;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000C33EC File Offset: 0x000C15EC
		public static TmdlDocument WithObjects(this TmdlDocument document, IEnumerable<TmdlObject> objects)
		{
			foreach (TmdlObject tmdlObject in objects)
			{
				document.Objects.Add(tmdlObject);
			}
			return document;
		}
	}
}
