using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D0 RID: 464
	internal static class TmdlPropertyInfoExtensions
	{
		// Token: 0x06001BF8 RID: 7160 RVA: 0x000C391C File Offset: 0x000C1B1C
		public static TmdlPropertyInfo WithProperty(this TmdlPropertyInfo propertyInfo, TmdlPropertyInfo property)
		{
			propertyInfo.AddChildProperty(property);
			return propertyInfo;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000C3928 File Offset: 0x000C1B28
		public static TmdlPropertyInfo WithProperties(this TmdlPropertyInfo propertyInfo, params TmdlPropertyInfo[] properties)
		{
			if (properties != null && properties.Length != 0)
			{
				foreach (TmdlPropertyInfo tmdlPropertyInfo in properties)
				{
					propertyInfo.AddChildProperty(tmdlPropertyInfo);
				}
			}
			return propertyInfo;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000C3958 File Offset: 0x000C1B58
		public static TmdlPropertyInfo WithProperties(this TmdlPropertyInfo propertyInfo, IEnumerable<TmdlPropertyInfo> properties)
		{
			foreach (TmdlPropertyInfo tmdlPropertyInfo in properties)
			{
				propertyInfo.AddChildProperty(tmdlPropertyInfo);
			}
			return propertyInfo;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000C39A4 File Offset: 0x000C1BA4
		public static TmdlPropertyInfo ApplyDeprecationStatus(this TmdlPropertyInfo propertyInfo, string deprecationMessage)
		{
			if (deprecationMessage != null)
			{
				propertyInfo.MarkAsDeprecated(deprecationMessage);
			}
			return propertyInfo;
		}
	}
}
