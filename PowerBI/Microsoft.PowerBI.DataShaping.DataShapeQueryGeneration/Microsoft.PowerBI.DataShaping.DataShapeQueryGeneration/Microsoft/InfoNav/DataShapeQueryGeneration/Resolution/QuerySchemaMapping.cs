using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Resolution
{
	// Token: 0x020000F6 RID: 246
	internal sealed class QuerySchemaMapping
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00021380 File Offset: 0x0001F580
		internal QuerySchemaMapping(string schemaName, IReadOnlyList<ExtensionEntityMapping> entities)
		{
			this.SchemaName = schemaName;
			this.Entities = entities;
			Dictionary<string, ExtensionEntityMapping> dictionary = new Dictionary<string, ExtensionEntityMapping>(ConceptualNameComparer.Instance);
			foreach (ExtensionEntityMapping extensionEntityMapping in this.Entities)
			{
				dictionary.Add(extensionEntityMapping.OriginalName, extensionEntityMapping);
			}
			this.EntitiesByName = dictionary;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000213FC File Offset: 0x0001F5FC
		internal string SchemaName { get; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00021404 File Offset: 0x0001F604
		internal IReadOnlyList<ExtensionEntityMapping> Entities { get; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0002140C File Offset: 0x0001F60C
		internal IReadOnlyDictionary<string, ExtensionEntityMapping> EntitiesByName { get; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00021414 File Offset: 0x0001F614
		internal bool IsEmpty
		{
			get
			{
				return this.Entities.Count == 0;
			}
		}

		// Token: 0x04000440 RID: 1088
		internal static readonly QuerySchemaMapping Empty = new QuerySchemaMapping(null, Util.EmptyReadOnlyCollection<ExtensionEntityMapping>());
	}
}
