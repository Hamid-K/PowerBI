using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020020F9 RID: 8441
	internal class VideoReferenceRelationship : DataPartReferenceRelationship
	{
		// Token: 0x170031E5 RID: 12773
		// (get) Token: 0x0600CFA9 RID: 53161 RVA: 0x00294FEF File Offset: 0x002931EF
		public static string VideoReferenceRelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/video";
			}
		}

		// Token: 0x0600CFAA RID: 53162 RVA: 0x00294FC2 File Offset: 0x002931C2
		internal VideoReferenceRelationship()
		{
		}

		// Token: 0x0600CFAB RID: 53163 RVA: 0x00294FF6 File Offset: 0x002931F6
		protected internal VideoReferenceRelationship(MediaDataPart mediaDataPart, string id)
			: base(mediaDataPart, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/video", id)
		{
		}

		// Token: 0x170031E6 RID: 12774
		// (get) Token: 0x0600CFAC RID: 53164 RVA: 0x00294FEF File Offset: 0x002931EF
		public override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/video";
			}
		}

		// Token: 0x040068B9 RID: 26809
		internal const string RelationshipTypeConst = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/video";
	}
}
