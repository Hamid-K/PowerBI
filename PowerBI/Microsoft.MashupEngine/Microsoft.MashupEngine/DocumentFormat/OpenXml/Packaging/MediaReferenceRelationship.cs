using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020020F7 RID: 8439
	internal class MediaReferenceRelationship : DataPartReferenceRelationship
	{
		// Token: 0x170031E1 RID: 12769
		// (get) Token: 0x0600CFA1 RID: 53153 RVA: 0x00294FBB File Offset: 0x002931BB
		public static string MediaReferenceRelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/media";
			}
		}

		// Token: 0x0600CFA2 RID: 53154 RVA: 0x00294FC2 File Offset: 0x002931C2
		internal MediaReferenceRelationship()
		{
		}

		// Token: 0x0600CFA3 RID: 53155 RVA: 0x00294FCA File Offset: 0x002931CA
		protected internal MediaReferenceRelationship(MediaDataPart mediaDataPart, string id)
			: base(mediaDataPart, "http://schemas.microsoft.com/office/2007/relationships/media", id)
		{
		}

		// Token: 0x170031E2 RID: 12770
		// (get) Token: 0x0600CFA4 RID: 53156 RVA: 0x00294FBB File Offset: 0x002931BB
		public override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/media";
			}
		}

		// Token: 0x040068B7 RID: 26807
		internal const string RelationshipTypeConst = "http://schemas.microsoft.com/office/2007/relationships/media";
	}
}
