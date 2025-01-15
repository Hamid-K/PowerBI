using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C6 RID: 454
	public static class DatabaseExtensions
	{
		// Token: 0x06001BCE RID: 7118 RVA: 0x000C304D File Offset: 0x000C124D
		public static IEnumerable<MetadataDocument> ToTmdl(this Database db)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			return MetadataSerializationContext.Create(MetadataSerializationStyle.Tmdl, db);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000C3064 File Offset: 0x000C1264
		public static IEnumerable<MetadataDocument> ToTmdl(this Database db, MetadataSerializationOptions options)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return MetadataSerializationContext.Create(MetadataSerializationStyle.Tmdl, db, options);
		}
	}
}
