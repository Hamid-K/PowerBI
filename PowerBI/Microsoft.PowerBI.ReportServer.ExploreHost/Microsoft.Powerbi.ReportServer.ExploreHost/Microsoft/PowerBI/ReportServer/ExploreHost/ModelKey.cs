using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000015 RID: 21
	internal sealed class ModelKey
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002F8C File Offset: 0x0000118C
		internal ModelKey(string modelId, string connectionKey, string modelMetadataVersion, TranslationsBehavior translationsBehavior = TranslationsBehavior.Default)
		{
			this.ModelId = modelId;
			this.ModelMetadataVersion = modelMetadataVersion;
			this.TranslationsBehavior = translationsBehavior;
			this.CacheKey = string.Join("\0", new object[] { modelId, connectionKey, modelMetadataVersion, translationsBehavior });
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002FE1 File Offset: 0x000011E1
		public string ModelId { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002FE9 File Offset: 0x000011E9
		public string CacheKey { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002FF1 File Offset: 0x000011F1
		public string ModelMetadataVersion { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002FF9 File Offset: 0x000011F9
		public TranslationsBehavior TranslationsBehavior { get; }
	}
}
