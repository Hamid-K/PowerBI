using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020020F6 RID: 8438
	internal class DataPartReferenceRelationship : ReferenceRelationship
	{
		// Token: 0x0600CF9A RID: 53146 RVA: 0x00294EB2 File Offset: 0x002930B2
		internal DataPartReferenceRelationship()
		{
		}

		// Token: 0x0600CF9B RID: 53147 RVA: 0x00294EBA File Offset: 0x002930BA
		protected internal DataPartReferenceRelationship(DataPart dataPart, string relationshipType, string id)
			: base(dataPart.Uri, false, relationshipType, id)
		{
			this.DataPart = dataPart;
		}

		// Token: 0x170031E0 RID: 12768
		// (get) Token: 0x0600CF9C RID: 53148 RVA: 0x00294ED2 File Offset: 0x002930D2
		// (set) Token: 0x0600CF9D RID: 53149 RVA: 0x00294EDA File Offset: 0x002930DA
		public virtual DataPart DataPart { get; private set; }

		// Token: 0x0600CF9E RID: 53150 RVA: 0x00294EE3 File Offset: 0x002930E3
		internal void Initialize(OpenXmlPartContainer containter, DataPart dataPart, string relationshipType, string id)
		{
			base.Initialize(dataPart.Uri, false, relationshipType, id);
			base.Container = containter;
			this.DataPart = dataPart;
		}

		// Token: 0x0600CF9F RID: 53151 RVA: 0x00294F04 File Offset: 0x00293104
		internal static bool IsDataPartReferenceRelationship(string relationshipType)
		{
			return relationshipType != null && (relationshipType == "http://schemas.microsoft.com/office/2007/relationships/media" || relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio" || relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/video");
		}

		// Token: 0x0600CFA0 RID: 53152 RVA: 0x00294F40 File Offset: 0x00293140
		internal static DataPartReferenceRelationship CreateDataPartReferenceRelationship(OpenXmlPartContainer containter, DataPart dataPart, string relationshipType, string id)
		{
			if (relationshipType != null)
			{
				DataPartReferenceRelationship dataPartReferenceRelationship;
				if (!(relationshipType == "http://schemas.microsoft.com/office/2007/relationships/media"))
				{
					if (!(relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio"))
					{
						if (!(relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/video"))
						{
							goto IL_005B;
						}
						dataPartReferenceRelationship = new VideoReferenceRelationship((MediaDataPart)dataPart, id);
					}
					else
					{
						dataPartReferenceRelationship = new AudioReferenceRelationship((MediaDataPart)dataPart, id);
					}
				}
				else
				{
					dataPartReferenceRelationship = new MediaReferenceRelationship((MediaDataPart)dataPart, id);
				}
				dataPartReferenceRelationship.Container = containter;
				return dataPartReferenceRelationship;
			}
			IL_005B:
			throw new ArgumentOutOfRangeException("relationshipType");
		}
	}
}
