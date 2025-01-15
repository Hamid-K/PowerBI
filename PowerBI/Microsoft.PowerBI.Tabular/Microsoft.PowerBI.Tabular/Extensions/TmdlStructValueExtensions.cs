using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D3 RID: 467
	internal static class TmdlStructValueExtensions
	{
		// Token: 0x06001C00 RID: 7168 RVA: 0x000C3A58 File Offset: 0x000C1C58
		public static TmdlStructValue WithProperties(this TmdlStructValue structValue, params TmdlProperty[] properties)
		{
			if (properties != null && properties.Length != 0)
			{
				foreach (TmdlProperty tmdlProperty in properties)
				{
					structValue.Properties.Add(tmdlProperty);
				}
			}
			return structValue;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000C3A90 File Offset: 0x000C1C90
		public static TmdlStructValue WithProperties(this TmdlStructValue structValue, IEnumerable<TmdlProperty> properties)
		{
			foreach (TmdlProperty tmdlProperty in properties)
			{
				structValue.Properties.Add(tmdlProperty);
			}
			return structValue;
		}
	}
}
