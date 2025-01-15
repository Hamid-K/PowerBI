using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001CA RID: 458
	public static class ModelExtensions
	{
		// Token: 0x06001BDB RID: 7131 RVA: 0x000C3375 File Offset: 0x000C1575
		public static IEnumerable<MetadataDocument> ToTmdl(this Model model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			return MetadataSerializationContext.Create(MetadataSerializationStyle.Tmdl, model);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000C338C File Offset: 0x000C158C
		public static IEnumerable<MetadataDocument> ToTmdl(this Model model, MetadataSerializationOptions options)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return MetadataSerializationContext.Create(MetadataSerializationStyle.Tmdl, model, options);
		}
	}
}
