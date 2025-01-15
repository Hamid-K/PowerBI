using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A9 RID: 8617
	internal class RibbonExtensibilityPart : CustomUIPart, IFixedContentTypePart
	{
		// Token: 0x0600DA92 RID: 55954 RVA: 0x002ADC60 File Offset: 0x002ABE60
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (RibbonExtensibilityPart._partConstraint == null)
			{
				RibbonExtensibilityPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
					new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return RibbonExtensibilityPart._partConstraint;
		}

		// Token: 0x0600DA93 RID: 55955 RVA: 0x002ADCA0 File Offset: 0x002ABEA0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (RibbonExtensibilityPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				RibbonExtensibilityPart._dataPartReferenceConstraint = dictionary;
			}
			return RibbonExtensibilityPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA94 RID: 55956 RVA: 0x002ADC41 File Offset: 0x002ABE41
		protected internal RibbonExtensibilityPart()
		{
		}

		// Token: 0x0600DA95 RID: 55957 RVA: 0x002ADCC8 File Offset: 0x002ABEC8
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				return new ImagePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600DA96 RID: 55958 RVA: 0x002ADD0C File Offset: 0x002ABF0C
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600DA97 RID: 55959 RVA: 0x002ADD28 File Offset: 0x002ABF28
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600DA98 RID: 55960 RVA: 0x002ADD5C File Offset: 0x002ABF5C
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600DA99 RID: 55961 RVA: 0x002ADD7C File Offset: 0x002ABF7C
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x170036C8 RID: 14024
		// (get) Token: 0x0600DA9A RID: 55962 RVA: 0x002ADDB1 File Offset: 0x002ABFB1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/ui/extensibility";
			}
		}

		// Token: 0x170036C9 RID: 14025
		// (get) Token: 0x0600DA9B RID: 55963 RVA: 0x002A7A24 File Offset: 0x002A5C24
		public sealed override string ContentType
		{
			get
			{
				return "application/xml";
			}
		}

		// Token: 0x170036CA RID: 14026
		// (get) Token: 0x0600DA9C RID: 55964 RVA: 0x002ADC57 File Offset: 0x002ABE57
		internal sealed override string TargetPath
		{
			get
			{
				return "customUI";
			}
		}

		// Token: 0x170036CB RID: 14027
		// (get) Token: 0x0600DA9D RID: 55965 RVA: 0x002ADC57 File Offset: 0x002ABE57
		internal sealed override string TargetName
		{
			get
			{
				return "customUI";
			}
		}

		// Token: 0x170036CC RID: 14028
		// (get) Token: 0x0600DA9E RID: 55966 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170036CD RID: 14029
		// (get) Token: 0x0600DA9F RID: 55967 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006BF9 RID: 27641
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/ui/extensibility";

		// Token: 0x04006BFA RID: 27642
		internal const string ContentTypeConstant = "application/xml";

		// Token: 0x04006BFB RID: 27643
		internal const string TargetPathConstant = "customUI";

		// Token: 0x04006BFC RID: 27644
		internal const string TargetNameConstant = "customUI";

		// Token: 0x04006BFD RID: 27645
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BFE RID: 27646
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
