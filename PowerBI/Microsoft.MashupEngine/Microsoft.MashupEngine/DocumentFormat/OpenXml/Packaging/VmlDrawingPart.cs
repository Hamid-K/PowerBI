using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A5 RID: 8613
	internal class VmlDrawingPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DA6C RID: 55916 RVA: 0x002AD9BC File Offset: 0x002ABBBC
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (VmlDrawingPart._partConstraint == null)
			{
				VmlDrawingPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/legacyDiagramText",
						new PartConstraintRule("LegacyDiagramTextPart", "application/vnd.ms-office.legacyDiagramText", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return VmlDrawingPart._partConstraint;
		}

		// Token: 0x0600DA6D RID: 55917 RVA: 0x002ADA18 File Offset: 0x002ABC18
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (VmlDrawingPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				VmlDrawingPart._dataPartReferenceConstraint = dictionary;
			}
			return VmlDrawingPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA6E RID: 55918 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal VmlDrawingPart()
		{
		}

		// Token: 0x0600DA6F RID: 55919 RVA: 0x002ADA40 File Offset: 0x002ABC40
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null)
			{
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
				{
					return new ImagePart();
				}
				if (relationshipType == "http://schemas.microsoft.com/office/2006/relationships/legacyDiagramText")
				{
					return new LegacyDiagramTextPart();
				}
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600DA70 RID: 55920 RVA: 0x002ADA98 File Offset: 0x002ABC98
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600DA71 RID: 55921 RVA: 0x002ADAB4 File Offset: 0x002ABCB4
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600DA72 RID: 55922 RVA: 0x002ADAE8 File Offset: 0x002ABCE8
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600DA73 RID: 55923 RVA: 0x002ADB08 File Offset: 0x002ABD08
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x170036B3 RID: 14003
		// (get) Token: 0x0600DA74 RID: 55924 RVA: 0x002ADB3D File Offset: 0x002ABD3D
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing";
			}
		}

		// Token: 0x170036B4 RID: 14004
		// (get) Token: 0x0600DA75 RID: 55925 RVA: 0x002ADB44 File Offset: 0x002ABD44
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.vmlDrawing";
			}
		}

		// Token: 0x170036B5 RID: 14005
		// (get) Token: 0x0600DA76 RID: 55926 RVA: 0x002A7FAF File Offset: 0x002A61AF
		internal sealed override string TargetPath
		{
			get
			{
				return "../drawings";
			}
		}

		// Token: 0x170036B6 RID: 14006
		// (get) Token: 0x0600DA77 RID: 55927 RVA: 0x002ADB4B File Offset: 0x002ABD4B
		internal sealed override string TargetName
		{
			get
			{
				return "vmldrawing";
			}
		}

		// Token: 0x170036B7 RID: 14007
		// (get) Token: 0x0600DA78 RID: 55928 RVA: 0x002ADB52 File Offset: 0x002ABD52
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".vml";
			}
		}

		// Token: 0x170036B8 RID: 14008
		// (get) Token: 0x0600DA79 RID: 55929 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170036B9 RID: 14009
		// (get) Token: 0x0600DA7A RID: 55930 RVA: 0x002ADB59 File Offset: 0x002ABD59
		public IEnumerable<LegacyDiagramTextPart> LegacyDiagramTextParts
		{
			get
			{
				return base.GetPartsOfType<LegacyDiagramTextPart>();
			}
		}

		// Token: 0x170036BA RID: 14010
		// (get) Token: 0x0600DA7B RID: 55931 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006BE5 RID: 27621
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing";

		// Token: 0x04006BE6 RID: 27622
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.vmlDrawing";

		// Token: 0x04006BE7 RID: 27623
		internal const string TargetPathConstant = "../drawings";

		// Token: 0x04006BE8 RID: 27624
		internal const string TargetNameConstant = "vmldrawing";

		// Token: 0x04006BE9 RID: 27625
		internal const string TargetFileExtensionConstant = ".vml";

		// Token: 0x04006BEA RID: 27626
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BEB RID: 27627
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
