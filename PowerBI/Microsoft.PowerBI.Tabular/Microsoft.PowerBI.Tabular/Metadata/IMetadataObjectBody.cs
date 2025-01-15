using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E9 RID: 489
	internal interface IMetadataObjectBody : ITxObjectBody
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001C70 RID: 7280
		// (set) Token: 0x06001C71 RID: 7281
		ObjectId Id { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001C72 RID: 7282
		// (set) Token: 0x06001C73 RID: 7283
		bool IsRemoved { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001C74 RID: 7284
		// (set) Token: 0x06001C75 RID: 7285
		MetadataObject LastParent { get; set; }

		// Token: 0x06001C76 RID: 7286
		bool IsEqualTo(IMetadataObjectBody other);

		// Token: 0x06001C77 RID: 7287
		void CompareWith(IMetadataObjectBody other, CompareContext context);

		// Token: 0x06001C78 RID: 7288
		void ClearOperationFlags();

		// Token: 0x06001C79 RID: 7289
		bool HasCompatibilityInfo(CompatibilityMode mode);

		// Token: 0x06001C7A RID: 7290
		bool GetCompatibilityRequirement(CompatibilityMode mode, out int requiredLevel, out string requestingPath);

		// Token: 0x06001C7B RID: 7291
		void UpdateCompatibilityRequirement(CompatibilityMode mode, int requiredLevel, string requestingPath);
	}
}
