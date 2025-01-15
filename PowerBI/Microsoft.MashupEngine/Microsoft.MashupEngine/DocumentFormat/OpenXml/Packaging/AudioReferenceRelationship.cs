using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020020F8 RID: 8440
	internal class AudioReferenceRelationship : DataPartReferenceRelationship
	{
		// Token: 0x170031E3 RID: 12771
		// (get) Token: 0x0600CFA5 RID: 53157 RVA: 0x00294FD9 File Offset: 0x002931D9
		public static string AudioReferenceRelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio";
			}
		}

		// Token: 0x0600CFA6 RID: 53158 RVA: 0x00294FC2 File Offset: 0x002931C2
		internal AudioReferenceRelationship()
		{
		}

		// Token: 0x0600CFA7 RID: 53159 RVA: 0x00294FE0 File Offset: 0x002931E0
		protected internal AudioReferenceRelationship(MediaDataPart mediaDataPart, string id)
			: base(mediaDataPart, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio", id)
		{
		}

		// Token: 0x170031E4 RID: 12772
		// (get) Token: 0x0600CFA8 RID: 53160 RVA: 0x00294FD9 File Offset: 0x002931D9
		public override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio";
			}
		}

		// Token: 0x040068B8 RID: 26808
		internal const string RelationshipTypeConst = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio";
	}
}
