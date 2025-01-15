using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office2010.CustomUI;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021B0 RID: 8624
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class RibbonAndBackstageCustomizationsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DB0E RID: 56078 RVA: 0x002AE908 File Offset: 0x002ACB08
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (RibbonAndBackstageCustomizationsPart._partConstraint == null)
			{
				RibbonAndBackstageCustomizationsPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
					new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return RibbonAndBackstageCustomizationsPart._partConstraint;
		}

		// Token: 0x0600DB0F RID: 56079 RVA: 0x002AE948 File Offset: 0x002ACB48
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (RibbonAndBackstageCustomizationsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				RibbonAndBackstageCustomizationsPart._dataPartReferenceConstraint = dictionary;
			}
			return RibbonAndBackstageCustomizationsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DB10 RID: 56080 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal RibbonAndBackstageCustomizationsPart()
		{
		}

		// Token: 0x0600DB11 RID: 56081 RVA: 0x002AE970 File Offset: 0x002ACB70
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

		// Token: 0x0600DB12 RID: 56082 RVA: 0x002AE9B4 File Offset: 0x002ACBB4
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600DB13 RID: 56083 RVA: 0x002AE9D0 File Offset: 0x002ACBD0
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600DB14 RID: 56084 RVA: 0x002AEA04 File Offset: 0x002ACC04
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600DB15 RID: 56085 RVA: 0x002AEA24 File Offset: 0x002ACC24
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x17003707 RID: 14087
		// (get) Token: 0x0600DB16 RID: 56086 RVA: 0x002AEA59 File Offset: 0x002ACC59
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/ui/extensibility";
			}
		}

		// Token: 0x17003708 RID: 14088
		// (get) Token: 0x0600DB17 RID: 56087 RVA: 0x002A7A24 File Offset: 0x002A5C24
		public sealed override string ContentType
		{
			get
			{
				return "application/xml";
			}
		}

		// Token: 0x17003709 RID: 14089
		// (get) Token: 0x0600DB18 RID: 56088 RVA: 0x002ADC57 File Offset: 0x002ABE57
		internal sealed override string TargetPath
		{
			get
			{
				return "customUI";
			}
		}

		// Token: 0x1700370A RID: 14090
		// (get) Token: 0x0600DB19 RID: 56089 RVA: 0x002ADC57 File Offset: 0x002ABE57
		internal sealed override string TargetName
		{
			get
			{
				return "customUI";
			}
		}

		// Token: 0x1700370B RID: 14091
		// (get) Token: 0x0600DB1A RID: 56090 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x0600DB1B RID: 56091 RVA: 0x002AE8FE File Offset: 0x002ACAFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return version == FileFormatVersions.Office2010;
		}

		// Token: 0x1700370C RID: 14092
		// (get) Token: 0x0600DB1C RID: 56092 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700370D RID: 14093
		// (get) Token: 0x0600DB1D RID: 56093 RVA: 0x002AEA60 File Offset: 0x002ACC60
		// (set) Token: 0x0600DB1E RID: 56094 RVA: 0x002AEA68 File Offset: 0x002ACC68
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as CustomUI;
			}
		}

		// Token: 0x1700370E RID: 14094
		// (get) Token: 0x0600DB1F RID: 56095 RVA: 0x002AEA76 File Offset: 0x002ACC76
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.CustomUI;
			}
		}

		// Token: 0x1700370F RID: 14095
		// (get) Token: 0x0600DB20 RID: 56096 RVA: 0x002AEA7E File Offset: 0x002ACC7E
		// (set) Token: 0x0600DB21 RID: 56097 RVA: 0x002A3296 File Offset: 0x002A1496
		public CustomUI CustomUI
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<CustomUI>();
				}
				return this._rootEle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x04006C26 RID: 27686
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2007/relationships/ui/extensibility";

		// Token: 0x04006C27 RID: 27687
		internal const string ContentTypeConstant = "application/xml";

		// Token: 0x04006C28 RID: 27688
		internal const string TargetPathConstant = "customUI";

		// Token: 0x04006C29 RID: 27689
		internal const string TargetNameConstant = "customUI";

		// Token: 0x04006C2A RID: 27690
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C2B RID: 27691
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006C2C RID: 27692
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private CustomUI _rootEle;
	}
}
